public class ATConstant
{
    public const string HISTAFF_SHAREDFOLDER_USERNAME = "SFUSERNAME";
    public const string HISTAFF_SHAREDFOLDER_PASSWORD = "SFPASSWORD";
    public const string HISTAFF_SHAREDFOLDER_DOMAIN = "SFDOMAIN";

    public const string HISTAFF_INITDATA_TIME = "ATINITTIME";

    public const string CONFIG_FEXESLEEP = "FEXESLEEP";

    public const string ACTFLG_ACTIVE = "A";
    public const string ACTFLG_DEACTIVE = "I";


    public const string CODE_SIGNMODE = "AT_SIGN_MODE";
    public const string CODE_SIGNTYPE = "AT_SIGN_TYPE";
    public const string CODE_PERIODSTATUS = "AT_PERIODSTATUS";
    public const string CODE_TIMELEAVE = "AT_TIMELEAVE";

    public const string PERIODSTATUS_OPEN = "O";
    public const string PERIODSTATUS_CLOSE = "C";
    public const string PERIODSTATUS_FINALCLOSE = "FC";
    public const string PERIODSTATUS_REOPEN = "RO";

    public const string GSIGNCODE_SHIFT = "SHIFT";
    public const string GSIGNCODE_LEAVE = "LEAVE";
    public const string GSIGNCODE_OVERTIME = "OVERTIME";
    public const string GSIGNCODE_LB = "LB";
    public const string GSIGNCODE_INOUT = "INOUT";
    public const string GSIGNCODE_WLEO = "WLEO";
    public const string GSIGNCODE_WORK = "WORK";
    public const string GSIGNCODE_AL = "AL";
    public const string GSIGNCODE_OTHERS = "OTHERS";

    public const string GSIGNCODE_TIME = "TIME"; // BangDV: Thêm để phục vụ cho phần phê duyệt

    public const string SIGNTYPECODE_STRING = "STRING";
    public const string SIGNTYPECODE_DATETIME = "DATETIME";
    public const string SIGNTYPECODE_NUMBER = "NUMBER";

    public const string SIGNMODECODE_REGISTER = "RGT";
    public const string SIGNMODECODE_SUMMARY = "SUM";

    public const string EXTENVALUE_FORMAT = "[NEXT1-NEXT2-NEXT3-SEXT1-SEXT2-SEXT3-DEXT1-DEXT2-DEXT3]";

    public const string FORMULAR_VIEWNAME_FORMAT = "ATV_{0}_{1}";

    public const string FORMULAR_STATUS_REGIST = "R";
    public const string FORMULAR_STATUS_PROCESS = "P";
    public const string FORMULAR_STATUS_COMPLETE = "C";
    public const string FORMULAR_STATUS_EXCEPTION = "E";

    public const string SYSTEM_USERNAME = "SYSTEM";
    public const decimal HU_TERMINATE = 257;
}

public class OT_AT_TIMELEAVE
{
    public static decimal AM = 342;
    public static decimal PM = 340;
    public static decimal CANGAY = 341;
}

public class OT_TRANSFER_STATUS
{
    public static decimal APPROVE = 447;
    public static decimal WAIT = 446;
}

public enum RegisterStatus
{
    Regist = 0,
    WaitForApprove = 1,
    Approved = 2,
    Denied = 3
}
