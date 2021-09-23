using System;
using Framework.Data.System.Linq.Dynamic;
using Framework.Data.SystemConfig;
using System.Configuration;

public partial class AttendanceRepository : IDisposable
{
    private AttendanceContext _ctx;
    private bool _isAvailable;

    public AttendanceContext Context
    {
        get
        {
            if (_ctx == null)
            {
                _ctx = new AttendanceContext();
                _ctx.ContextOptions.LazyLoadingEnabled = true;
            }
            return _ctx;
        }
    }

    public bool GetComboboxData(ref ComboBoxDataDTO cbxData)
    {
        try
        {
            // Danh sách các đối tượng cư trú
            if (cbxData.GET_LIST_TYPEPUNISH)
            {
                cbxData.LIST_LIST_TYPEPUNISH = (from p in Context.OT_OTHER_LIST
                                                join t in Context.OT_OTHER_LIST_TYPE on p.TYPE_ID equals t.ID
                                                where p.ACTFLG == "A" & t.CODE == "TYPE_PUNISH"
                                                orderby p.CREATED_DATE descending
                                                select new OT_OTHERLIST_DTO() { ID = p.ID, CODE = p.CODE, NAME_EN = p.NAME_EN, NAME_VN = p.NAME_VN, TYPE_ID = p.TYPE_ID }).ToList()();
            }
            if (cbxData.GET_LIST_TYPESHIFT)
            {
                cbxData.LIST_LIST_TYPESHIFT = (from p in Context.OT_OTHER_LIST
                                               join t in Context.OT_OTHER_LIST_TYPE on p.TYPE_ID equals t.ID
                                               where p.ACTFLG == "A" & t.CODE == "TYPE_SHIFT"
                                               orderby p.CREATED_DATE descending
                                               select new OT_OTHERLIST_DTO() { ID = p.ID, CODE = p.CODE, NAME_EN = p.NAME_EN, NAME_VN = p.NAME_VN, TYPE_ID = p.TYPE_ID }).ToList();
            }
            if (cbxData.GET_LIST_APPLY_LAW)
            {
                cbxData.LIST_LIST_APPLY_LAW = (from p in Context.AT_GSIGN
                                               where p.ACTFLG == "A"
                                               orderby p.NAME_VN descending
                                               select new AT_GSIGNDTO() { ID = p.ID, CODE = p.CODE, NAME_VN = "[" + p.CODE + "] " + p.NAME_VN }).ToList();
            }
            if (cbxData.GET_LIST_PENALIZEA)
            {
                cbxData.LIST_LIST_PENALIZEA = (from p in Context.AT_DMVS
                                               where p.ACTFLG == "A"
                                               orderby p.NAME_VN descending
                                               select new AT_DMVSDTO() { ID = p.ID, CODE = p.CODE, NAME_VN = p.NAME_VN }).ToList(); 
            }

            if (cbxData.GET_LIST_SHIFT)
            {
                cbxData.LIST_LIST_SHIFT = (from p in Context.AT_SHIFT
                                           where p.ACTFLG == "A"
                                           orderby p.NAME_VN descending
                                           select new AT_SHIFTDTO() { ID = p.ID, CODE = p.CODE, NAME_VN = "[" + p.CODE + "] " + p.NAME_VN }).ToList();
            }
            if (cbxData.GET_LIST_SIGN)
            {
                cbxData.LIST_LIST_SIGN = (from p in Context.AT_FML
                                          where p.ACTFLG == "A"
                                          orderby p.NAME_VN descending
                                          select new AT_FMLDTO() { ID = p.ID, CODE = p.CODE, NAME_VN = "[" + p.CODE + "] " + p.NAME_VN }).ToList();
            }
            if (cbxData.GET_LIST_TYPEEMPLOYEE)
            {
                cbxData.LIST_LIST_TYPEEMPLOYEE = (from p in Context.OT_OTHER_LIST
                                                  join t in Context.OT_OTHER_LIST_TYPE on p.TYPE_ID equals t.ID
                                                  where p.ACTFLG == "A" & t.CODE == "TYPE_EMPLOYEE"
                                                  orderby p.CREATED_DATE descending
                                                  select new OT_OTHERLIST_DTO() { ID = p.ID, CODE = p.CODE, NAME_EN = p.NAME_EN, NAME_VN = p.NAME_VN, TYPE_ID = p.TYPE_ID }).ToList();
            }
            if (cbxData.GET_LIST_TYPEE_FML)
            {
                cbxData.LIST_LIST_TYPE_FML = (from p in Context.AT_FML
                                              where p.ACTFLG == "A"
                                              orderby p.NAME_VN descending
                                              select new AT_FMLDTO() { ID = p.ID, CODE = p.CODE, NAME_VN = "[" + p.CODE + "] " + p.NAME_VN }).ToList();
            }
            if (cbxData.GET_LIST_REST_DAY)
            {
                cbxData.LIST_LIST_REST_DAY = (from p in Context.OT_OTHER_LIST
                                              join t in Context.OT_OTHER_LIST_TYPE on p.TYPE_ID equals t.ID
                                              where p.ACTFLG == "A" & t.CODE == "AT_TIMELEAVE"
                                              orderby p.CREATED_DATE descending
                                              select new OT_OTHERLIST_DTO() { ID = p.ID, CODE = p.CODE, NAME_EN = p.NAME_EN, NAME_VN = p.NAME_VN, TYPE_ID = p.TYPE_ID }).ToList();
            }
            if (cbxData.GET_LIST_TYPE_DMVS)
            {
                cbxData.LIST_LIST_TYPE_DMVS = (from p in Context.AT_TIME_MANUAL
                                               where p.ACTFLG == "A" & p.CODE == "RDT" | p.CODE == "RVS"
                                               orderby p.NAME descending
                                               select new OT_OTHERLIST_DTO() { ID = p.ID, CODE = p.CODE, NAME_VN = p.NAME }).ToList();
            }

            if (cbxData.GET_LIST_TYPE_MANUAL_LEAVE)
            {
                cbxData.LIST_LIST_TYPE_MANUAL_LEAVE = (From p In Context.AT_TIME_MANUAL
                                                       From F In Context.AT_FML.Where(Function(f) f.ID = p.MORNING_ID).DefaultIfEmpty
                                                       From F2 In Context.AT_FML.Where(Function(f2) f2.ID = p.AFTERNOON_ID).DefaultIfEmpty
                                                       Where p.ACTFLG = "A" And(F.IS_LEAVE = -1 Or F2.IS_LEAVE = -1) Order By p.NAME Descending
                                                       Select New AT_TIME_MANUALDTO With {
                                                           .ID = p.ID,
                                                           .CODE = p.CODE,
                                                           .MORNING_ID = p.MORNING_ID,
                                                           .AFTERNOON_ID = p.AFTERNOON_ID,
                                                           .NAME_VN = "[" & p.CODE & "] " & p.NAME}).ToList()
            }



            if (cbxData.GET_LIST_TYPE_MANUAL)
                cbxData.LIST_LIST_TYPE_MANUAL = (from p in Context.AT_TIME_MANUAL
                                                 where p.ACTFLG == "A" & p.CODE != "RVS" & p.CODE != "RDT"
                                                 orderby p.NAME descending
                                                 select new AT_TIME_MANUALDTO() { ID = p.ID, CODE = p.CODE, NAME_VN = "[" + p.CODE + "] " + p.NAME }).ToList();

            if (cbxData.GET_LIST_SHIFT_SUNDAY)
                cbxData.LIST_LIST_SHIFT_SUNDAY = (from p in Context.AT_TIME_MANUAL
                                                  where p.ACTFLG == "A" & p.CODE == "OFF"
                                                  orderby p.NAME descending
                                                  select new AT_TIME_MANUALDTO() { ID = p.ID, CODE = p.CODE, NAME_VN = "[" + p.CODE + "] " + p.NAME }).ToList();
            // danh mục cấp nhân sự
            if (cbxData.GET_LIST_STAFF_RANK)
            {
                cbxData.LIST_LIST_STAFF_RANK = (from p in Context.HU_STAFF_RANK
                                                where p.ACTFLG == "A"
                                                orderby p.NAME descending
                                                select new HU_STAFF_RANKDTO() { ID = p.ID, CODE = p.CODE, NAME = p.NAME }).ToList();
            }
            if (cbxData.GET_LIST_HS_OT)
            {
                cbxData.LIST_LIST_HS_OT = (from p in Context.OT_OTHER_LIST
                                           join t in Context.OT_OTHER_LIST_TYPE on p.TYPE_ID equals t.ID
                                           where p.ACTFLG == "A" & t.CODE == "HS_OT"
                                           orderby p.ID descending
                                           select new OT_OTHERLIST_DTO() { ID = p.ID, CODE = p.CODE, NAME_EN = p.NAME_EN, NAME_VN = p.NAME_VN, TYPE_ID = p.TYPE_ID }).ToList();
            }
            if (cbxData.GET_LIST_TYPE_OT)
                cbxData.LIST_LIST_TYPE_OT = (from p in Context.OT_OTHER_LIST
                                             join t in Context.OT_OTHER_LIST_TYPE on p.TYPE_ID equals t.ID
                                             where p.ACTFLG == "A" & t.CODE == "TYPE_OT"
                                             orderby p.ID descending
                                             select new OT_OTHERLIST_DTO() { ID = p.ID, CODE = p.CODE, NAME_EN = p.NAME_EN, NAME_VN = p.NAME_VN, TYPE_ID = p.TYPE_ID }).ToList();

            if (cbxData.GET_LIST_TERMINALS_MEAL)
                cbxData.LIST_LIST_TERMINALS_MEAL = (from p in Context.AT_TERMINALS_MEAL
                                                    where p.ACTFLG == "A"
                                                    orderby p.TERMINAL_NAME
                                                    select new AT_TERMINALS_MEALDTO() { ID = p.ID, TERMINAL_NAME = p.TERMINAL_NAME }).ToList();

            if (cbxData.GET_LIST_TIME_TYPE)
                cbxData.LIST_LIST_TIME_TYPE = (from p in Context.OT_OTHER_LIST
                                               join t in Context.OT_OTHER_LIST_TYPE on p.TYPE_ID equals t.ID
                                               where p.ACTFLG == "A" & t.CODE == "TIME_TYPE"
                                               orderby p.CREATED_DATE descending
                                               select new OT_OTHERLIST_DTO() { ID = p.ID, CODE = p.CODE, NAME_EN = p.NAME_EN, NAME_VN = p.NAME_VN, TYPE_ID = p.TYPE_ID }).ToList();

            // loại suất ăn
            if (cbxData.GET_LIST_RATION)
                cbxData.LIST_LIST_RATION = (from p in Context.OT_OTHER_LIST
                                            join t in Context.OT_OTHER_LIST_TYPE on p.TYPE_ID equals t.ID
                                            where p.ACTFLG == "A" & t.CODE == "RATION"
                                            orderby p.CREATED_DATE descending
                                            select new OT_OTHERLIST_DTO() { ID = p.ID, CODE = p.CODE, NAME_EN = p.NAME_EN, NAME_VN = p.NAME_VN, TYPE_ID = p.TYPE_ID }).ToList();

            // Danh sách bếp
            if (cbxData.GET_LIST_KITCHEN)
                cbxData.LIST_LIST_KITCHEN = (from p in Context.AT_KITCHEN
                                             orderby p.CREATED_DATE descending
                                             select new OT_OTHERLIST_DTO()
                                             {
                                                 ID = p.ID,
                                                 CODE = p.KITCHEN_CODE,
                                                 NAME_VN = p.KITCHEN_NAME
                                             }).ToList();

            // Danh sách bếp bữa ăn
            if (cbxData.GET_LIST_MEAL)
                cbxData.LIST_LIST_MEAL = (from p in Context.AT_MEAL
                                          orderby p.ID
                                          select new OT_OTHERLIST_DTO()
                                          {
                                              ID = p.ID,
                                              CODE = p.CODE,
                                              NAME_VN = p.NAME
                                          }).ToList();

            // danh sách loại đối tác
            if (cbxData.GET_LIST_PARTNER)
                cbxData.LIST_LIST_PARTNER = (from p in Context.OT_OTHER_LIST
                                             join t in Context.OT_OTHER_LIST_TYPE on p.TYPE_ID equals t.ID
                                             where p.ACTFLG == "A" & t.CODE == "PARTNER_TYPE"
                                             orderby p.CREATED_DATE descending
                                             select new OT_OTHERLIST_DTO()
                                             {
                                                 ID = p.ID,
                                                 CODE = p.CODE,
                                                 NAME_VN = p.NAME_VN
                                             }).ToList();

            return true;
        }
        catch (Exception ex)
        {
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod().Name, "iTime");
            return false;
        }
        finally
        {
        }
    }

    public List<ApproveUserDTO> GetApproveUsers(decimal employeeId, string processCode, List<decimal> lstOrg = null, bool isTimesheet = false)
    {
        try
        {
            List<ApproveUserDTO> listResult = new List<ApproveUserDTO>();

            var process = Context.SE_APP_PROCESS.SingleOrDefault(p => p.PROCESS_CODE == processCode);

            if (process == null)
                throw new Exception("Chưa thiết lập quy trình phê duyệt HOẶC Mã quy trình phê duyệt sai.");

            // Lấy template phê duyệt đang áp dụng cho nhân viên
            List<SE_APP_SETUP> usingSetups = GetCurrentEmployeeSetup(employeeId, process, lstOrg, isTimesheet);

           
