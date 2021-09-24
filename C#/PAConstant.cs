public class PAConstant
{
    public const string ACTFLG_ACTIVE = "A";
    public const string ACTFLG_DEACTIVE = "I";
    public const string SALARYMODECODE_SUMMARY = "SUM";
    public const string SALARYMODECODE_IMPORT = "IMPORT";

    public const string FORMULAR_STATUS_REGIST = "R";
    public const string FORMULAR_STATUS_PROCESS = "P";
    public const string FORMULAR_STATUS_COMPLETE = "C";
    public const string FORMULAR_STATUS_EXCEPTION = "E";

    public const string CODE_SALARYMODE = "PA_SALARY_MODE";
    public const string CODE_SALARYTYPE = "PA_SALARY_TYPE";
    public const string CODE_PERIODSTATUS = "AT_PERIODSTATUS";

    public const string PERIODSTATUS_OPEN = "O";
    public const string PERIODSTATUS_CLOSE = "C";
    public const string PERIODSTATUS_FINALCLOSE = "FC";
    public const string PERIODSTATUS_REOPEN = "RO";

    public const decimal HU_TERMINATE = 257;
    public const string GSALARY_INPUT = "INPUT";

    // PhongDV
    public const string CODE_FAMILY_CONDITION = "PA_FAMILY_CONDITION";
}

public class OT_TRANSFER_STATUS
{
    public static decimal APPROVE = 447;
    public static decimal WAIT = 446;
}

public class PA_GSALARY
{
    public static decimal OUTPUT = 1;
    public static decimal TAX = 2;
    public static decimal INPUT = 3;
    public static decimal IMPORT = 4;
    public static decimal ADVANCE = 6;
}

public class PA_SALARY_TYPE
{
    public static string DATETIME = "DATETIME";
    public static string NUMBER = "NUMBER";
    public static string NVARCHAR = "STRING";
    public static decimal DATETIME_ID = 443;
    public static decimal NUMBER_ID = 444;
    public static decimal NVARCHAR_ID = 445;
}
