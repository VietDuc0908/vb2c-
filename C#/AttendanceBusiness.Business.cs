using System;
using AttendanceBusiness.ServiceContracts;
using AttendanceDAL;
using Framework.Data;
using LinqKit;

// NOTE: You can use the "Rename" command on the context menu to change the class name "Service1" in both code and config file together.
namespace AttendanceBusiness.ServiceImplementations
{
    public partial class AttendanceBusiness :IAttendanceBusiness
    {
        public List<AT_DATAINOUTDTO> GetDataInout(AT_DATAINOUTDTO _filter, ParamDTO _param, ref int Total = 0, int PageIndex = 0, int PageSize = int.MaxValue, string Sorts = "EMPLOYEE_CODE, WORKINGDAY", UserLog log = null)
            :IAttendanceBusiness.GetDataInout
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    var lst = rep.GetDataInout(_filter, _param, Total, PageIndex, PageSize, Sorts, log);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool InsertDataInout(List<AT_DATAINOUTDTO> lstDataInout, DateTime fromDate, DateTime toDate, UserLog log)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.InsertDataInout(lstDataInout, fromDate, toDate, log);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool ModifyDataInout(AT_DATAINOUTDTO objDataInout, UserLog log, ref decimal gID)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.ModifyDataInout(objDataInout, log, gID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        public bool DeleteDataInout(AT_DATAINOUTDTO[] lstDataInout)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.DeleteDataInout(lstDataInout);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        public bool CALCULATE_ENTITLEMENT_NB(ParamDTO param, List<decimal?> listEmployeeId, Framework.Data.UserLog log)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    var lst = rep.CALCULATE_ENTITLEMENT_NB(param, listEmployeeId, log);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public List<AT_COMPENSATORYDTO> GetNB(AT_COMPENSATORYDTO _filter, ParamDTO _param, ref int Total = 0, int PageIndex = 0, int PageSize = int.MaxValue, string Sorts = "CREATED_DATE desc", UserLog log = null)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    var lst = rep.GetNB(_filter, _param, Total, PageIndex, PageSize, Sorts, log);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public List<AT_ENTITLEMENTDTO> GetEntitlement(AT_ENTITLEMENTDTO _filter, ParamDTO _param, ref int Total = 0, int PageIndex = 0, int PageSize = int.MaxValue, string Sorts = "CREATED_DATE desc", UserLog log = null)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    var lst = rep.GetEntitlement(_filter, _param, Total, PageIndex, PageSize, Sorts, log);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public bool CALCULATE_ENTITLEMENT(ParamDTO param, List<decimal?> listEmployeeId, Framework.Data.UserLog log)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    var lst = rep.CALCULATE_ENTITLEMENT(param, listEmployeeId, log);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }



        public AT_WORKSIGNDTO GET_WORKSIGN_BYEMP(decimal Emp_ID, DateTime working_day)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    var lst = rep.GET_WORKSIGN_BYEMP(Emp_ID, working_day);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public System.Data.DataSet GET_WORKSIGN(AT_WORKSIGNDTO param, Framework.Data.UserLog log)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    var lst = rep.GET_WORKSIGN(param, log);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void InsertWorkSign(List<AT_WORKSIGNDTO> objWorkSigns, AT_WORKSIGNDTO objWork, DateTime p_fromdate, DateTime? p_endDate, UserLog log, ref decimal gID)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.InsertWorkSign(objWorkSigns, objWork, p_fromdate, p_endDate, log, gID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool InsertWORKSIGNByImport(DataTable dtData, decimal period_id, UserLog log, ref string lstEmp)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.InsertWorkSignByImport(dtData, period_id, log, lstEmp);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool CheckOffInMonth(ParamDTO _param, UserLog log = null)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.CheckOffInMonth(_param, log);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool CheckOffInMonthTable(DataTable dtData, decimal p_period_id, ref DataTable dtDataError)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.CheckOffInMonthTable(dtData, p_period_id, dtDataError);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool ValidateWORKSIGN(AT_WORKSIGNDTO objWORKSIGN)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.ValidateWorkSign(objWORKSIGN);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool ModifyWORKSIGN(AT_WORKSIGNDTO objWORKSIGN, UserLog log, ref decimal gID)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.ModifyWorkSign(objWORKSIGN, log, gID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool DeleteWORKSIGN(AT_WORKSIGNDTO[] lstWORKSIGN)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.DeleteWorkSign(lstWORKSIGN);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public System.Data.DataTable GETSIGNDEFAULT(ParamDTO param, Framework.Data.UserLog log)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    var lst = rep.GETSIGNDEFAULT(param, log);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool Del_WorkSign_ByEmp(decimal employee_id, DateTime p_From, DateTime p_to)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.Del_WorkSign_ByEmp(employee_id, p_From, p_to);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public List<AT_SWIPE_DATADTO> GetSwipeData(AT_SWIPE_DATADTO _filter, int PageIndex = 0, int PageSize = int.MaxValue, ref int Total = 0, string Sorts = "iTime_id, VALTIME desc")
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    var lst = rep.GetSwipeData(_filter, PageIndex, PageSize, Total, Sorts);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public List<AT_SWIPE_DATA_MEALDTO> GetSwipeDataMeal(AT_SWIPE_DATA_MEALDTO _filter, int PageIndex = 0, int PageSize = int.MaxValue, ref int Total = 0, string Sorts = "iTime_id, VALTIME desc")
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    var lst = rep.GetSwipeDataMeal(_filter, PageIndex, PageSize, Total, Sorts);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool ImportSwipeDataAuto(List<AT_SWIPE_DATADTO> lstSwipeData, UserLog log, bool isMeal = false)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    var lst = rep.ImportSwipeDataAuto(lstSwipeData, log, isMeal);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool InsertSwipeDataImport(List<AT_SWIPE_DATADTO> objDelareRice, UserLog log, bool isMeal)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    var lst = rep.InsertSwipeDataImport(objDelareRice, log, isMeal);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public List<AT_LATE_COMBACKOUTDTO> GetLate_combackout(AT_LATE_COMBACKOUTDTO _filter, ParamDTO _param, ref int Total = 0, int PageIndex = 0, int PageSize = int.MaxValue, string Sorts = "CREATED_DATE desc", UserLog log = null)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    var lst = rep.GetDSVM(_filter, _param, Total, PageIndex, PageSize, Sorts, log);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public AT_LATE_COMBACKOUTDTO GetLate_CombackoutById(decimal? _id)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.GetLate_CombackoutById(_id);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool ImportLate_combackout(AT_LATE_COMBACKOUTDTO objLate_combackout, UserLog log, ref decimal gID)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.ImportLate_combackout(objLate_combackout, log, gID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool InsertLate_combackout(List<AT_LATE_COMBACKOUTDTO> objRegisterDMVSList, AT_LATE_COMBACKOUTDTO objLate_combackout, UserLog log, ref decimal gID)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.InsertLate_combackout(objRegisterDMVSList, objLate_combackout, log, gID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool ValidateLate_combackout(AT_LATE_COMBACKOUTDTO objLate_combackout)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.ValidateLate_combackout(objLate_combackout);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool ModifyLate_combackout(AT_LATE_COMBACKOUTDTO objLate_combackout, UserLog log, ref decimal gID)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.ModifyLate_combackout(objLate_combackout, log, gID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool DeleteLate_combackout(List<decimal> lstID, ParamDTO _param, decimal period_id, List<decimal?> listEmployeeId, UserLog log)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.DeleteLate_combackout(lstID, _param, period_id, listEmployeeId, log);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public List<AT_REGISTER_OTDTO> GetRegisterOT(AT_REGISTER_OTDTO _filter, ParamDTO _param, ref int Total = 0, int PageIndex = 0, int PageSize = int.MaxValue, string Sorts = "CREATED_DATE desc", UserLog log = null)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    var lst = rep.GetRegisterOT(_filter, _param, Total, PageIndex, PageSize, Sorts, log);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public AT_REGISTER_OTDTO GetRegisterById(decimal? _id)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.GetRegisterById(_id);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public List<OT_OTHERLIST_DTO> GetListHsOT()
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    var lst = rep.GetListHsOT();
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        public bool ValidateRegisterOT(AT_REGISTER_OTDTO objLate_combackout)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.ValidateRegisterOT(objLate_combackout);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool InsertRegisterOT(AT_REGISTER_OTDTO objRegisterOT, UserLog log, ref decimal gID)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.InsertRegisterOT(objRegisterOT, log, gID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool InsertDataRegisterOT(List<AT_REGISTER_OTDTO> objRegisterOTList, AT_REGISTER_OTDTO objRegisterOT, UserLog log, ref decimal gID)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.InsertDataRegisterOT(objRegisterOTList, objRegisterOT, log, gID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool ModifyRegisterOT(AT_REGISTER_OTDTO objRegisterOT, UserLog log, ref decimal gID)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.ModifyRegisterOT(objRegisterOT, log, gID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool DeleteRegisterOT(List<decimal> lstID, ParamDTO _param, decimal period_id, List<decimal?> listEmployeeId, UserLog log)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.DeleteRegisterOT(lstID, _param, period_id, listEmployeeId, log);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool CheckImporAddNewtOT(AT_REGISTER_OTDTO objRegisterOT)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.CheckImporAddNewtOT(objRegisterOT);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool CheckDataListImportAddNew(List<AT_REGISTER_OTDTO> objRegisterOTList, AT_REGISTER_OTDTO objRegisterOT, ref string strEmployeeCode)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.CheckDataListImportAddNew(objRegisterOTList, objRegisterOT, strEmployeeCode);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public List<AT_TIME_TIMESHEET_MACHINETDTO> GetMachines(AT_TIME_TIMESHEET_MACHINETDTO _filter, ParamDTO _param, ref int Total = 0, int PageIndex = 0, int PageSize = int.MaxValue, string Sorts = "EMPLOYEE_ID, WORKINGDAY", UserLog log = null)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    var lst = rep.GetMachines(_filter, _param, Total, PageIndex, PageSize, Sorts, log);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public bool Init_TimeTImesheetMachines(ParamDTO _param, UserLog log, DateTime p_fromdate, DateTime p_enddate, decimal P_ORG_ID, List<decimal?> lstEmployee)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    var lst = rep.Init_TimeTImesheetMachines(_param, log, p_fromdate, p_enddate, P_ORG_ID, lstEmployee);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public System.Data.DataSet GetCCT(AT_TIME_TIMESHEET_DAILYDTO param, Framework.Data.UserLog log)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    var lst = rep.GetCCT(param, log);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable GetCCT_Origin(AT_TIME_TIMESHEET_DAILYDTO param, Framework.Data.UserLog log)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    var lst = rep.GetCCT_Origin(param, log);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool ModifyLeaveSheetDaily(AT_TIME_TIMESHEET_DAILYDTO objLeave, UserLog log, ref decimal gID)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    var lst = rep.ModifyLeaveSheetDaily(objLeave, log, gID);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public bool InsertLeaveSheetDaily(DataTable dtData, UserLog log, decimal PeriodID)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    var lst = rep.InsertLeaveSheetDaily(dtData, log, PeriodID);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public AT_TIME_TIMESHEET_DAILYDTO GetTimeSheetDailyById(AT_TIME_TIMESHEET_DAILYDTO obj)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.GetTimeSheetDailyById(obj);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        public bool Cal_TimeTImesheet_OT(ParamDTO _param, UserLog log, decimal? p_period_id, decimal P_ORG_ID, List<decimal?> lstEmployee)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    var lst = rep.Cal_TimeTImesheet_OT(_param, log, p_period_id, P_ORG_ID, lstEmployee);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public System.Data.DataSet GetSummaryOT(AT_TIME_TIMESHEET_OTDTO param, Framework.Data.UserLog log)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    var lst = rep.GetSummaryOT(param, log);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        public bool Cal_TimeTImesheet_NB(ParamDTO _param, UserLog log, decimal? p_period_id, decimal P_ORG_ID, List<decimal?> lstEmployee)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    var lst = rep.Cal_TimeTImesheet_NB(_param, log, p_period_id, P_ORG_ID, lstEmployee);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public System.Data.DataSet GetSummaryNB(AT_TIME_TIMESHEET_NBDTO param, Framework.Data.UserLog log)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    var lst = rep.GetSummaryNB(param, log);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        public bool ModifyLeaveSheetOt(AT_TIME_TIMESHEET_OTDTO objLeave, UserLog log, ref decimal gID)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    var lst = rep.ModifyLeaveSheetOt(objLeave, log, gID);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool InsertLeaveSheetOt(AT_TIME_TIMESHEET_OTDTO objLeave, UserLog log, ref decimal gID)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    var lst = rep.InsertLeaveSheetOt(objLeave, log, gID);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public AT_TIME_TIMESHEET_OTDTO GetTimeSheetOtById(AT_TIME_TIMESHEET_OTDTO obj)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.GetTimeSheetOtById(obj);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public List<AT_TIME_TIMESHEET_MONTHLYDTO> GetTimeSheet(AT_TIME_TIMESHEET_MONTHLYDTO _filter, ParamDTO _param, ref int Total = 0, int PageIndex = 0, int PageSize = int.MaxValue, string Sorts = "CREATED_DATE desc", UserLog log = null)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    var lst = rep.GetTimeSheet(_filter, _param, Total, PageIndex, PageSize, Sorts, log);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public bool CAL_TIME_TIMESHEET_MONTHLY(ParamDTO param, List<decimal?> lstEmployee, Framework.Data.UserLog log)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    var lst = rep.CAL_TIME_TIMESHEET_MONTHLY(param, lstEmployee, log);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void ValidateTimesheet(AT_TIME_TIMESHEET_MONTHLYDTO _validate, string sType, UserLog log)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    var lst = rep.ValidateTimesheet(_validate, sType, log);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public List<AT_LEAVESHEETDTO> GetLeaveSheet(AT_LEAVESHEETDTO _filter, ParamDTO _param, ref int Total = 0, int PageIndex = 0, int PageSize = int.MaxValue, string Sorts = "CREATED_DATE desc", UserLog log = null)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    var lst = rep.GetLeaveSheet(_filter, _param, Total, PageIndex, PageSize, Sorts, log);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public AT_LEAVESHEETDTO GetLeaveById(decimal? _id)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.GetLeaveById(_id);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable GetTotalPHEPNAM(int P_EMPLOYEE_ID, DateTime Date_cal, int P_TYPE_LEAVE_ID)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.GetTotalPHEPNAM(P_EMPLOYEE_ID, Date_cal, P_TYPE_LEAVE_ID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable GetTotalPHEPBU(int P_EMPLOYEE_ID, DateTime Date_cal, int P_TYPE_LEAVE_ID)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.GetTotalPHEPBU(P_EMPLOYEE_ID, Date_cal, P_TYPE_LEAVE_ID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable GetTotalDAY(int P_EMPLOYEE_ID, int P_TYPE_MANUAL, DateTime P_FROM_DATE, DateTime P_TO_DATE)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.GetTotalDAY(P_EMPLOYEE_ID, P_TYPE_MANUAL, P_FROM_DATE, P_TO_DATE);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable GetCAL_DAY_LEAVE_OLD(int P_EMPLOYEE_ID, DateTime P_FROM_DATE, DateTime P_TO_DATE)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.GetCAL_DAY_LEAVE_OLD(P_EMPLOYEE_ID, P_FROM_DATE, P_TO_DATE);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public AT_ENTITLEMENTDTO GetPhepNam(decimal? _id, decimal? _year)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.GetPhepNam(_id, _year);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public List<AT_LEAVESHEETDTO> GetPHEPBUCONLAI(List<AT_LEAVESHEETDTO> lstEmpID, decimal? _year)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.GetPHEPBUCONLAI(lstEmpID, _year);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public AT_COMPENSATORYDTO GetNghiBu(decimal? _id, decimal? _year)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.GetNghiBu(_id, _year);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool ValidateLeaveSheet(AT_LEAVESHEETDTO objtime)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.ValidateLeaveSheet(objtime);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool InsertLeaveSheet(AT_LEAVESHEETDTO objRegisterOT, UserLog log, ref decimal gID)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.InsertLeaveSheet(objRegisterOT, log, gID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool InsertLeaveSheetList(List<AT_LEAVESHEETDTO> objRegisterOTList, AT_LEAVESHEETDTO objRegisterOT, UserLog log, ref decimal gID)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.InsertLeaveSheetList(objRegisterOTList, objRegisterOT, log, gID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool ModifyLeaveSheet(AT_LEAVESHEETDTO objRegisterOT, UserLog log, ref decimal gID)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.ModifyLeaveSheet(objRegisterOT, log, gID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool DeleteLeaveSheet(List<AT_LEAVESHEETDTO> lstID, ParamDTO _param, decimal period_id, List<decimal?> listEmployeeId, UserLog log)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.DeleteLeaveSheet(lstID, _param, period_id, listEmployeeId, log);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable checkLeaveImport(DataTable dtData)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.checkLeaveImport(dtData);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public bool CheckDataCheckworksign(List<AT_REGISTER_OTDTO> objRegisterOTList, AT_REGISTER_OTDTO objRegisterOT, ref string strEmployeeCode)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.CheckDataCheckworksign(objRegisterOTList, objRegisterOT, strEmployeeCode);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public bool CheckDataCheckworksignImport(AT_REGISTER_OTDTO objRegisterOT)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.CheckDataCheckworksignImport(objRegisterOT);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public bool Check_DataRegister_OT(ref string _param, UserLog log, DateTime? Startdate, DateTime? Enddate, decimal? period_id)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.Check_DataRegister_OT(_param, log, Startdate, Enddate, period_id);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public bool Check_WorkSing_default(ParamDTO obj, UserLog log, ref string Employee_ID)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    var lst = rep.Check_WorkSing_default(obj, log, Employee_ID);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public List<AT_TIME_RICEDTO> GetDelareRice(AT_TIME_RICEDTO _filter, ParamDTO _param, ref int Total = 0, int PageIndex = 0, int PageSize = int.MaxValue, string Sorts = "CREATED_DATE desc", UserLog log = null)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    var lst = rep.GetDelareRice(_filter, _param, Total, PageIndex, PageSize, Sorts, log);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public AT_TIME_RICEDTO GetDelareRiceById(decimal? _id)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.GetDelareRiceById(_id);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public bool ActiveDelareRice(List<decimal> lstID, UserLog log, string bActive)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.ActiveDelareRice(lstID, log, bActive);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public bool ValidateDelareRice(AT_TIME_RICEDTO objDelareRice)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.ValidateDelareRice(objDelareRice);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public bool InsertDelareRice(AT_TIME_RICEDTO objDelareRice, UserLog log, ref decimal gID)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.InsertDelareRice(objDelareRice, log, gID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public bool InsertDelareRiceList(List<AT_TIME_RICEDTO> objDelareRiceList, AT_TIME_RICEDTO objDelareRice, UserLog log, ref decimal gID)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.InsertDelareRiceList(objDelareRiceList, objDelareRice, log, gID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool ModifyDelareRice(AT_TIME_RICEDTO objDelareRice, UserLog log, ref decimal gID)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.ModifyDelareRice(objDelareRice, log, gID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        public bool DeleteDelareRice(List<decimal> lstID)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.DeleteDelareRice(lstID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public List<AT_DECLARE_ENTITLEMENTDTO> GetDelareEntitlementNB(AT_DECLARE_ENTITLEMENTDTO _filter, ParamDTO _param, ref int Total = 0, int PageIndex = 0, int PageSize = int.MaxValue, string Sorts = "CREATED_DATE desc", UserLog log = null)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    var lst = rep.GetDelareEntitlementNB(_filter, _param, Total, PageIndex, PageSize, Sorts, log);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public AT_DECLARE_ENTITLEMENTDTO GetDelareEntitlementNBById(decimal? _id)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.GetDelareEntitlementNBById(_id);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool ActiveDelareEntitlementNB(List<decimal> lstID, UserLog log, string bActive)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.ActiveDelareEntitlementNB(lstID, log, bActive);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool InsertDelareEntitlementNB(AT_DECLARE_ENTITLEMENTDTO objDelareEntitlementNB, UserLog log, ref decimal gID, ref bool checkMonthNB, ref bool checkMonthNP)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.InsertDelareEntitlementNB(objDelareEntitlementNB, log, gID, checkMonthNB, checkMonthNP);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool InsertMultipleDelareEntitlementNB(List<AT_DECLARE_ENTITLEMENTDTO> objDelareEntitlementlist, AT_DECLARE_ENTITLEMENTDTO objDelareEntitlementNB, UserLog log, ref decimal gID, ref bool checkMonthNB, ref bool checkMonthNP)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.InsertMultipleDelareEntitlementNB(objDelareEntitlementlist, objDelareEntitlementNB, log, gID, checkMonthNB, checkMonthNP);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool ImportDelareEntitlementNB(DataTable dtData, UserLog log, ref decimal gID, ref bool checkMonthNB, ref bool checkMonthNP)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.ImportDelareEntitlementNB(dtData, log, gID, checkMonthNB, checkMonthNP);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool ModifyDelareEntitlementNB(AT_DECLARE_ENTITLEMENTDTO objDelareEntitlementNB, UserLog log, ref decimal gID)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.ModifyDelareEntitlementNB(objDelareEntitlementNB, log, gID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool DeleteDelareEntitlementNB(List<decimal> lstID)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.DeleteDelareEntitlementNB(lstID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool ValidateMonthThamNien(AT_DECLARE_ENTITLEMENTDTO objHOLIDAYGR)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.ValidateMonthThamNien(objHOLIDAYGR);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool ValidateMonthPhepNam(AT_DECLARE_ENTITLEMENTDTO objHOLIDAYGR)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.ValidateMonthPhepNam(objHOLIDAYGR);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool ValidateMonthNghiBu(AT_DECLARE_ENTITLEMENTDTO objHOLIDAYGR)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.ValidateMonthNghiBu(objHOLIDAYGR);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool Cal_TimeTImesheet_Rice(ParamDTO _param, UserLog log, decimal? p_period_id, decimal P_ORG_ID, List<decimal?> lstEmployee)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    var lst = rep.Cal_TimeTImesheet_Rice(_param, log, p_period_id, P_ORG_ID, lstEmployee);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public System.Data.DataSet GetSummaryRice(AT_TIME_TIMESHEET_RICEDTO param, Framework.Data.UserLog log)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    var lst = rep.GetSummaryRice(param, log);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool ModifyLeaveSheetRice(AT_TIME_TIMESHEET_RICEDTO objLeave, UserLog log, ref decimal gID)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    var lst = rep.ModifyLeaveSheetRice(objLeave, log, gID);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public bool ApprovedTimeSheetRice(AT_TIME_TIMESHEET_RICEDTO objLeave, UserLog log, ref decimal gID)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    var lst = rep.ApprovedTimeSheetRice(objLeave, log, gID);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public bool InsertLeaveSheetRice(AT_TIME_TIMESHEET_RICEDTO objLeave, UserLog log, ref decimal gID)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    var lst = rep.InsertLeaveSheetRice(objLeave, log, gID);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public AT_TIME_TIMESHEET_RICEDTO GetTimeSheetRiceById(AT_TIME_TIMESHEET_RICEDTO obj)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.GetTimeSheetRiceById(obj);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        public bool CheckPeriod(int PeriodId, decimal EmployeeId)
        {
            try
            {
                AttendanceRepository rep = new AttendanceRepository();
                return rep.CheckPeriod(PeriodId, EmployeeId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetTimeSheetPortal(AT_TIME_TIMESHEET_DAILYDTO _filter)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    var lst = rep.GetTimeSheetPortal(_filter);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        public List<AT_ACTION_LOGDTO> GetActionLog(AT_ACTION_LOGDTO _filter, ref int Total = 0, int PageIndex = 0, int PageSize = int.MaxValue, string Sorts = "ACTION_DATE desc")
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    var lst = rep.GetActionLog(_filter, Total, PageIndex, PageSize, Sorts);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public int DeleteActionLogsPA(List<decimal> lstDeleteIds)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    var lst = rep.DeleteActionLogsAT(lstDeleteIds);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public List<AT_TIME_TIMESHEET_DAILYDTO> GetListExplanation(AT_TIME_TIMESHEET_DAILYDTO _filter, ParamDTO _param, ref int Total = 0, int PageIndex = 0, int PageSize = int.MaxValue, string Sorts = "EMPLOYEE_CODE desc,WORKINGDAY asc", UserLog log = null)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    var lst = rep.GetListExplanation(_filter, _param, Total, PageIndex, PageSize, Sorts, log);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable GetExplanationManual()
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    var lst = rep.GetExplanationManual();
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable GetExplanationEmployee(ParamDTO _param, UserLog log)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    var lst = rep.GetExplanationEmployee(_param, log);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool ImportExplanation(DataTable dtData, UserLog log, ref decimal gID)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    var lst = rep.ImportExplanation(dtData, log, gID);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
