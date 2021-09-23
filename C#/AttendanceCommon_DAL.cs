public class AttendanceCommon
{
    public class OT_ASSET_STATUS
    {
        public static string Name = "ASSET_STATUS";
        public static string STATUS_WAIT = "STATUS_WAIT";
        public static string STATUS_NEW = "STATUS_NEW";
        public static string STATUS_TRANSFER = "STATUS_TRANSFER";
    }
    public class OT_WORK_STATUS
    {
        public static string Name = "WORK_STATUS";
        public static decimal TYPE_ID = 59;
        public static string TERMINATE = "TERMINATE";
        public static string MISSION = "MISSION";
        public static decimal TERMINATE_ID = 257;
    }
    public class OT_TRANSFER_STATUS
    {
        public static string Name = "TRANSFER_STATUS";
        public static string APPROVE = "1";
        public static string WAIT_APPROVE = "0";
        public static decimal APPROVE_ID = 447;
        public static decimal WAIT_APPROVE_ID = 446;
    }
    public class OT_DECISION_TYPE
    {
        public static string Name = "DECISION_TYPE";
        public static string DISCIPLINE = "DISCIPLINE";
        public static string COMMEND = "COMMEND";
        public static string TERMINATE = "TERMINATE";
        public static string MISSION = "MISSION";
    }

    public class OT_TER_STATUS
    {
        public static string Name = "TER_STATUS";
        public static string APPROVE = "1";
        public static string WAIT_APPROVE = "0";
        public static decimal APPROVE_ID = 262;
        public static decimal WAIT_APPROVE_ID = 263;
    }

    public class OT_COMMEND_STATUS
    {
        public static string Name = "COMMEND_STATUS";
        public static string APPROVE = "1";
        public static string WAIT_APPROVE = "0";
        public static decimal APPROVE_ID = 714;
        public static decimal WAIT_APPROVE_ID = 715;
    }

    public class OT_DISCIPLINE_STATUS
    {
        public static string Name = "DISCIPLINE_STATUS";
        public static string APPROVE = "1";
        public static string WAIT_APPROVE = "0";
        public static decimal APPROVE_ID = 716;
        public static decimal WAIT_APPROVE_ID = 717;
    }

    public class OT_ORG_LEVEL
    {
        public static string Name = "ORG_LEVEL";
        public static string LOCATION_BASE = "1";
        public static string DEPARTMENT = "2";
        public static string SUB_DEPARTMENT = "3";
        public static string FUNCTION = "4";
        public static string SUB_FUNCTION = "5";
        public static string SECTION = "6";
        public static string GROUP = "7";
    }
    public class OT_GENDER
    {
        public static string Name = "GENDER";
        public static string MALE = "0";
        public static string FEMALE = "1";
        public static decimal MALE_ID = 565;
        public static decimal FEMALE_ID = 566;
    }
    public class OT_RECRUITMENT_REASON
    {
        public static string Name = "RECRUITMENT_REASON";
        public static string IN_AOP = "IN_AOP";               // Trong kế hoạch
        public static string OUT_AOP = "OUT_AOP";             // Ngoài kế hoạch
        public static string REPLACEMENT = "REPLACEMENT";     // Tuyển thay thế
        public static string NOT_PRESENT = "NOT_PRESENT";     // Không hiện diện
        public static string OTHER = "OTHER";                 // Lý do khác
    }
    public class OT_DASHBOARD_EMPLOYEE_STATISTIC
    {
        public static string Name = "DB_EMP";
        public static string GENDER = "GENDER";
        public static string CONTRACT_TYPE = "CONTRACT_TYPE";
    }
    public class OT_DASHBOARD_CHANGE_STATISTIC
    {
        public static string Name = "DB_CHANGE";
        public static string EMPLOYEE_COUNT_FOLLOW_YEAR = "EMP_COUNT_YEAR";
        public static string EMPLOYEE_COUNT_FOLLOW_MONTH = "EMP_COUNT_MONTH";
        public static string EMPLOYEE_CHANGE_FOLLOW_YEAR = "EMP_CHANGE_YEAR";
        public static string EMPLOYEE_CHANGE_FOLLOW_MONTH = "EMP_CHANGE_MON";
    }
    public class OT_TRANSFER_TYPE
    {
        public static string Name = "TRANSFER_TYPE";
    }
    public class OT_RELATION
    {
        public static string Name = "RELATION";
    }
    public class NATION_VN
    {
        public static string CODE = "VN";
    }

    public class OT_TER_REASON
    {
        public static string Name = "TER_REASON";
    }

    public class OT_CONTRACT_STATUS
    {
        public static string Name = "CONTRACT_STATUS";
        public static string APPROVE = "1";
        public static string WAIT_APPROVE = "0";
    }
    public class OT_COMMEND
    {
        public static string COMMEND_LEVEL = "COMMEND_LEVEL";
        public static string COMMEND_OBJECT = "COMMEND_OBJECT";
        public static string COMMEND_TYPE = "COMMEND_TYPE";
    }
    public class OT_DISCIPLINE
    {
        public static string DISCIPLINE_LEVEL = "DISCIPLINE_LEVEL";
        public static string DISCIPLINE_OBJECT = "DISCIPLINE_OBJECT";
        public static string DISCIPLINE_TYPE = "DISCIPLINE_TYPE";
    }


    public class OT_WORKING_TYPE
    {
        public static int TRANSFER_CHANGE_SALARY = 0;
        public static int TRANSFER = 1;
        public static int CHANGE_SALARY = 2;
        public static int NO_CHANGE = 3;
    }

    public class OT_MEAL_CHANGE_STATUS
    {
        public static string Name = "MEAL_CHANGE_STATUS";
        public static string NOT_APPROVE = "2";
        public static string APPROVE = "1";
        public static string WAIT_APPROVE = "0";
        public static string NOT_APPROVE_ID = 8002;
        public static string APPROVE_ID = 8001;
        public static string WAIT_APPROVE_ID = 8000;
    }

    public enum TABLE_NAME
    {
        AT_HOLIDAY = 3,
        AT_HOLIDAY_GENERAL = 4,
        AT_FML = 5,
        AT_GSIGN = 6,
        AT_DMVS = 7,
        AT_SHIFT = 8,
        AT_HOLIDAY_OBJECT = 9,
        AT_SETUP_SPECIAL = 10,
        AT_SETUP_TIME_EMP = 11,
        AT_TERMINALS = 12,
        AT_SIGNDEFAULT = 13,
        AT_TIME_MANUAL = 14,
        AT_LIST_PARAM_SYSTEM = 15
    }
}
