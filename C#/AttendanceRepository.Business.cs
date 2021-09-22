using System;
using Attendance.AttendanceBusiness;
using Framework.UI;

partial class AttendanceRepository
{
    #region Business

    #region quan ly vao ra
    public List<AT_DATAINOUTDTO> GetDataInout(AT_DATAINOUTDTO _filter, ParamDTO _param, ref int Total = 0, int PageIndex = 0, int PageSize = int.MaxValue, string Sorts = "EMPLOYEE_CODE, WORKINGDAY")
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                var lst = rep.GetDataInout(_filter, _param, Total, PageIndex, PageSize, Sorts, this.Log);
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public bool InsertDataInout(List<AT_DATAINOUTDTO> lstDataInout, DateTime fromDate, DateTime toDate)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.InsertDataInout(lstDataInout, fromDate, toDate, this.Log);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool ModifyDataInout(AT_DATAINOUTDTO objDataInout, ref decimal gID)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.ModifyDataInout(objDataInout, this.Log, gID);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool DeleteDataInout(List<AT_DATAINOUTDTO> lstDataInout)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.DeleteDataInout(lstDataInout);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }
    #endregion


    #region Đăng ký đi muộn về sớm
    public List<AT_LATE_COMBACKOUTDTO> GetLate_combackout(AT_LATE_COMBACKOUTDTO _filter, ParamDTO _param, ref int Total = 0, int PageIndex = 0, int PageSize = int.MaxValue, string Sorts = "CREATED_DATE desc")
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                var lst = rep.GetDSVM(_filter, _param, Total, PageIndex, PageSize, Sorts, this.Log);
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public bool ImportLate_combackout(AT_LATE_COMBACKOUTDTO objLate_combackout, ref decimal gID)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.ImportLate_combackout(objLate_combackout, this.Log, gID);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool InsertLate_combackout(List<AT_LATE_COMBACKOUTDTO> objRegisterDMVSList, AT_LATE_COMBACKOUTDTO objLate_combackout, ref decimal gID)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.InsertLate_combackout(objRegisterDMVSList, objLate_combackout, this.Log, gID);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public AT_LATE_COMBACKOUTDTO GetLate_CombackoutById(decimal? _id)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.GetLate_CombackoutById(_id);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool ValidateLate_combackout(AT_LATE_COMBACKOUTDTO objLate_combackout)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.ValidateLate_combackout(objLate_combackout);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool ModifyLate_combackout(AT_LATE_COMBACKOUTDTO objLate_combackout, ref decimal gID)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.ModifyLate_combackout(objLate_combackout, this.Log, gID);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool DeleteLate_combackout(List<decimal> lstID, ParamDTO _param, decimal period_id, List<decimal?> listEmployeeId)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.DeleteLate_combackout(lstID, _param, period_id, listEmployeeId, this.Log);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }
    #endregion


    #region lam them
    public List<AT_REGISTER_OTDTO> GetRegisterOT(AT_REGISTER_OTDTO _filter, ParamDTO _param, ref int Total = 0, int PageIndex = 0, int PageSize = int.MaxValue, string Sorts = "CREATED_DATE desc")
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                var lst = rep.GetRegisterOT(_filter, _param, Total, PageIndex, PageSize, Sorts, this.Log);
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public List<OT_OTHERLIST_DTO> GetListHsOT()
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
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

    public bool InsertRegisterOT(AT_REGISTER_OTDTO objRegisterOT, ref decimal gID)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.InsertRegisterOT(objRegisterOT, this.Log, gID);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool InsertDataRegisterOT(List<AT_REGISTER_OTDTO> objRegisterOTList, AT_REGISTER_OTDTO objRegisterOT, ref decimal gID)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.InsertDataRegisterOT(objRegisterOTList, objRegisterOT, this.Log, gID);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public AT_REGISTER_OTDTO GetRegisterById(decimal? _id)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.GetRegisterById(_id);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool ValidateRegisterOT(AT_REGISTER_OTDTO objRegisterOT)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.ValidateRegisterOT(objRegisterOT);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool ModifyRegisterOT(AT_REGISTER_OTDTO objRegisterOT, ref decimal gID)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.ModifyRegisterOT(objRegisterOT, this.Log, gID);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool DeleteRegisterOT(List<decimal> lstID, ParamDTO _param, decimal period_id, List<decimal?> listEmployeeId)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.DeleteRegisterOT(lstID, _param, period_id, listEmployeeId, this.Log);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool CheckImporAddNewtOT(AT_REGISTER_OTDTO objRegisterOT)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.CheckImporAddNewtOT(objRegisterOT);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool CheckDataListImportAddNew(List<AT_REGISTER_OTDTO> objRegisterOTList, AT_REGISTER_OTDTO objRegisterOT, ref string strEmployeeCode)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.CheckDataListImportAddNew(objRegisterOTList, objRegisterOT, strEmployeeCode);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }
    #endregion




    public bool Cal_TimeTImesheet_OT(ParamDTO _param, decimal? p_period_id, decimal P_ORG_ID, List<decimal?> lstEmployee)
    {
        bool dt;
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                dt = rep.Cal_TimeTImesheet_OT(_param, this.Log, p_period_id, P_ORG_ID, lstEmployee);
                return dt;
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public System.Data.DataSet GetSummaryOT(AT_TIME_TIMESHEET_OTDTO param)
    {
        DataSet dt;
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                dt = rep.GetSummaryOT(param, this.Log);
                return dt;
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool Cal_TimeTImesheet_NB(ParamDTO _param, decimal? p_period_id, decimal P_ORG_ID, List<decimal?> lstEmployee)
    {
        bool dt;
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                dt = rep.Cal_TimeTImesheet_NB(_param, this.Log, p_period_id, P_ORG_ID, lstEmployee);
                return dt;
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public System.Data.DataSet GetSummaryNB(AT_TIME_TIMESHEET_NBDTO param)
    {
        DataSet dt;
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                dt = rep.GetSummaryNB(param, this.Log);
                return dt;
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool ModifyLeaveSheetOt(AT_TIME_TIMESHEET_OTDTO objRegister, ref decimal gID)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.ModifyLeaveSheetOt(objRegister, this.Log, gID);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool InsertLeaveSheetOt(AT_TIME_TIMESHEET_OTDTO objRegister, ref decimal gID)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.InsertLeaveSheetOt(objRegister, this.Log, gID);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public AT_TIME_TIMESHEET_OTDTO GetTimeSheetOtById(AT_TIME_TIMESHEET_OTDTO obj)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.GetTimeSheetOtById(obj);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public List<AT_LEAVESHEETDTO> GetLeaveSheet(AT_LEAVESHEETDTO _filter, ParamDTO _param, ref int Total = 0, int PageIndex = 0, int PageSize = int.MaxValue, string Sorts = "CREATED_DATE desc")
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                var lst = rep.GetLeaveSheet(_filter, _param, Total, PageIndex, PageSize, Sorts, this.Log);
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public bool InsertLeaveSheet(AT_LEAVESHEETDTO objRegister, ref decimal gID)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.InsertLeaveSheet(objRegister, this.Log, gID);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool InsertLeaveSheetList(List<AT_LEAVESHEETDTO> objRegisterList, AT_LEAVESHEETDTO objRegister, ref decimal gID)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.InsertLeaveSheetList(objRegisterList, objRegister, this.Log, gID);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public AT_LEAVESHEETDTO GetLeaveById(decimal? _id)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.GetLeaveById(_id);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public DataTable GetTotalPHEPNAM(int P_EMPLOYEE_ID, DateTime Date_cal, int P_TYPE_LEAVE_ID)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.GetTotalPHEPNAM(P_EMPLOYEE_ID, Date_cal, P_TYPE_LEAVE_ID);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public DataTable GetTotalDAY(int P_EMPLOYEE_ID, int P_TYPE_MANUAL, DateTime P_FROM_DATE, DateTime P_TO_DATE)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.GetTotalDAY(P_EMPLOYEE_ID, P_TYPE_MANUAL, P_FROM_DATE, P_TO_DATE);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public DataTable GetCAL_DAY_LEAVE_OLD(int P_EMPLOYEE_ID, DateTime P_FROM_DATE, DateTime P_TO_DATE)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.GetCAL_DAY_LEAVE_OLD(P_EMPLOYEE_ID, P_FROM_DATE, P_TO_DATE);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public DataTable GetTotalPHEPBU(int P_EMPLOYEE_ID, DateTime Date_cal, int P_TYPE_LEAVE_ID)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.GetTotalPHEPBU(P_EMPLOYEE_ID, Date_cal, P_TYPE_LEAVE_ID);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public AT_ENTITLEMENTDTO GetPhepNam(decimal? _id, decimal? _year)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.GetPhepNam(_id, _year);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public List<AT_LEAVESHEETDTO> GetPHEPBUCONLAI(List<AT_LEAVESHEETDTO> lstEmpID, decimal? _year)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.GetPHEPBUCONLAI(lstEmpID, _year);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public AT_COMPENSATORYDTO GetNghiBu(decimal? _id, decimal? _year)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.GetNghiBu(_id, _year);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool ValidateLeaveSheet(AT_LEAVESHEETDTO objRegister)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.ValidateLeaveSheet(objRegister);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool ModifyLeaveSheet(AT_LEAVESHEETDTO objRegister, ref decimal gID)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.ModifyLeaveSheet(objRegister, this.Log, gID);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool DeleteLeaveSheet(List<AT_LEAVESHEETDTO> lstID, ParamDTO _param, decimal period_id, List<decimal?> listEmployeeId)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.DeleteLeaveSheet(lstID, _param, period_id, listEmployeeId, this.Log);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public DataTable checkLeaveImport(DataTable dtData)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.checkLeaveImport(dtData);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool CheckDataCheckworksign(List<AT_REGISTER_OTDTO> objRegisterOTList, AT_REGISTER_OTDTO objRegisterOT, ref string strEmployeeCode)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.CheckDataCheckworksign(objRegisterOTList, objRegisterOT, strEmployeeCode);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }
    public bool CheckDataCheckworksignImport(AT_REGISTER_OTDTO objRegisterOT)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.CheckDataCheckworksignImport(objRegisterOT);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }
    public bool Check_DataRegister_OT(ref string _param, DateTime? Startdate, DateTime? Enddate, decimal? period_id)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.Check_DataRegister_OT(_param, this.Log, Startdate, Enddate, period_id);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }
    public bool Check_WorkSing_default(ParamDTO obj, ref string Employee_ID)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.Check_WorkSing_default(obj, this.Log, Employee_ID);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public List<AT_TIME_TIMESHEET_MACHINETDTO> GetMachines(AT_TIME_TIMESHEET_MACHINETDTO _filter, ParamDTO _param, ref int Total = 0, int PageIndex = 0, int PageSize = int.MaxValue, string Sorts = "EMPLOYEE_ID, WORKINGDAY")
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                var lst = rep.GetMachines(_filter, _param, Total, PageIndex, PageSize, Sorts, this.Log);
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public bool Init_TimeTImesheetMachines(ParamDTO _param, DateTime p_fromdate, DateTime p_enddate, decimal P_ORG_ID, List<decimal?> lstEmployee)
    {
        bool dt;
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                dt = rep.Init_TimeTImesheetMachines(_param, this.Log, p_fromdate, p_enddate, P_ORG_ID, lstEmployee);
                return dt;
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }


    public System.Data.DataSet GetCCT(AT_TIME_TIMESHEET_DAILYDTO param)
    {
        DataSet dt;
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                dt = rep.GetCCT(param, this.Log);
                return dt;
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public DataTable GetCCT_Origin(AT_TIME_TIMESHEET_DAILYDTO param)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.GetCCT_Origin(param, this.Log);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool ModifyLeaveSheetDaily(AT_TIME_TIMESHEET_DAILYDTO objRegister, ref decimal gID)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.ModifyLeaveSheetDaily(objRegister, this.Log, gID);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool InsertLeaveSheetDaily(DataTable dtData, decimal PeriodID)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.InsertLeaveSheetDaily(dtData, this.Log, PeriodID);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public AT_TIME_TIMESHEET_DAILYDTO GetTimeSheetDailyById(AT_TIME_TIMESHEET_DAILYDTO obj)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.GetTimeSheetDailyById(obj);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public List<AT_TIME_TIMESHEET_MONTHLYDTO> GetTimeSheet(AT_TIME_TIMESHEET_MONTHLYDTO _filter, ParamDTO _param, ref int Total = 0, int PageIndex = 0, int PageSize = int.MaxValue, string Sorts = "EMPLOYEE_CODE,DECISION_START")
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                var lst = rep.GetTimeSheet(_filter, _param, Total, PageIndex, PageSize, Sorts, this.Log);
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public bool CAL_TIME_TIMESHEET_MONTHLY(ParamDTO param, List<decimal?> lstEmployee)
    {
        bool dt;
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                dt = rep.CAL_TIME_TIMESHEET_MONTHLY(param, lstEmployee, this.Log);
                return dt;
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public void ValidateTimesheet(AT_TIME_TIMESHEET_MONTHLYDTO _validate, string sType)
    {
        bool dt;
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                dt = rep.ValidateTimesheet(_validate, sType, this.Log);
                return dt;
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }


    public List<AT_ENTITLEMENTDTO> GetEntitlement(AT_ENTITLEMENTDTO _filter, ParamDTO _param, ref int Total = 0, int PageIndex = 0, int PageSize = int.MaxValue, string Sorts = "CREATED_DATE desc")
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                var lst = rep.GetEntitlement(_filter, _param, Total, PageIndex, PageSize, Sorts, this.Log);
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public bool CALCULATE_ENTITLEMENT(ParamDTO param, List<decimal?> listEmployeeId)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.CALCULATE_ENTITLEMENT(param, listEmployeeId, this.Log);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool CALCULATE_ENTITLEMENT_NB(ParamDTO param, List<decimal?> listEmployeeId)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.CALCULATE_ENTITLEMENT_NB(param, listEmployeeId, this.Log);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public List<AT_COMPENSATORYDTO> GetNB(AT_COMPENSATORYDTO _filter, ParamDTO _param, ref int Total = 0, int PageIndex = 0, int PageSize = int.MaxValue, string Sorts = "CREATED_DATE desc")
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                var lst = rep.GetNB(_filter, _param, Total, PageIndex, PageSize, Sorts, this.Log);
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public System.Data.DataSet GET_WORKSIGN(AT_WORKSIGNDTO param)
    {
        DataSet dt;
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                dt = rep.GET_WORKSIGN(param, this.Log);
                return dt;
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public AT_WORKSIGNDTO GET_WORKSIGN_BYEMP(decimal Emp_ID, DateTime working_day)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.GET_WORKSIGN_BYEMP(Emp_ID, working_day);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool InsertWorkSign(List<AT_WORKSIGNDTO> objWorkSigns, AT_WORKSIGNDTO objWork, DateTime p_fromdate, DateTime? p_endDate, ref decimal gID)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.InsertWorkSign(objWorkSigns, objWork, p_fromdate, p_endDate, this.Log, gID);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool InsertWORKSIGNByImport(DataTable dtData, decimal period_id, ref string lstEmp)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.InsertWORKSIGNByImport(dtData, period_id, this.Log, lstEmp);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool ValidateWORKSIGN(AT_WORKSIGNDTO objWORKSIGN)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.ValidateWORKSIGN(objWORKSIGN);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool ModifyWORKSIGN(AT_WORKSIGNDTO objWORKSIGN, ref decimal gID)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.ModifyWORKSIGN(objWORKSIGN, this.Log, gID);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool DeleteWORKSIGN(List<AT_WORKSIGNDTO> lstWORKSIGN)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.DeleteWORKSIGN(lstWORKSIGN);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public System.Data.DataTable GETSIGNDEFAULT(ParamDTO param)
    {
        DataTable dt;
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                dt = rep.GETSIGNDEFAULT(param, this.Log);
                return dt;
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool Del_WorkSign_ByEmp(decimal employee_id, DateTime p_From, DateTime p_to)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.Del_WorkSign_ByEmp(employee_id, p_From, p_to);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public List<AT_SWIPE_DATADTO> GetSwipeData(AT_SWIPE_DATADTO _filter, int PageIndex = 0, int PageSize = int.MaxValue, ref int Total = 0, string Sorts = "iTime_id, VALTIME desc")
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
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
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
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

    public bool CheckOffInMonth(ParamDTO _param)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                var lst = rep.CheckOffInMonth(_param, this.Log);
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public bool CheckOffInMonthTable(DataTable dtData, decimal p_period_id, ref DataTable dtDataError)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                var lst = rep.CheckOffInMonthTable(dtData, p_period_id, dtDataError);
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }


    public bool InsertSwipeDataImport(List<AT_SWIPE_DATADTO> objDelareRice, bool isMeal)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                var lst = rep.InsertSwipeDataImport(objDelareRice, this.Log, isMeal);
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public List<AT_TIME_RICEDTO> GetDelareRice(AT_TIME_RICEDTO _filter, ParamDTO _param, ref int Total = 0, int PageIndex = 0, int PageSize = int.MaxValue, string Sorts = "CREATED_DATE desc")
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                var lst = rep.GetDelareRice(_filter, _param, Total, PageIndex, PageSize, Sorts, this.Log);
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public bool InsertDelareRice(AT_TIME_RICEDTO objDelareRice, ref decimal gID)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.InsertDelareRice(objDelareRice, this.Log, gID);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool InsertDelareRiceList(List<AT_TIME_RICEDTO> objDelareRiceList, AT_TIME_RICEDTO objDelareRice, ref decimal gID)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.InsertDelareRiceList(objDelareRiceList, objDelareRice, this.Log, gID);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool ValidateDelareRice(AT_TIME_RICEDTO objDelareRice)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.ValidateDelareRice(objDelareRice);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public AT_TIME_RICEDTO GetDelareRiceById(decimal? _id)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.GetDelareRiceById(_id);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool ModifyDelareRice(AT_TIME_RICEDTO objDelareRice, ref decimal gID)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.ModifyDelareRice(objDelareRice, this.Log, gID);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool ActiveDelareRice(List<decimal> lstHoliday, string sActive)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.ActiveDelareRice(lstHoliday, this.Log, sActive);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool DeleteDelareRice(List<decimal> lstID)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.DeleteDelareRice(lstID);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }


    public List<AT_DECLARE_ENTITLEMENTDTO> GetDelareEntitlementNB(AT_DECLARE_ENTITLEMENTDTO _filter, ParamDTO _param, ref int Total = 0, int PageIndex = 0, int PageSize = int.MaxValue, string Sorts = "CREATED_DATE desc")
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                var lst = rep.GetDelareEntitlementNB(_filter, _param, Total, PageIndex, PageSize, Sorts, this.Log);
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public bool InsertDelareEntitlementNB(AT_DECLARE_ENTITLEMENTDTO objDelareEntitlementNB, ref decimal gID, ref bool checkMonthNB, ref bool checkMonthNP)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.InsertDelareEntitlementNB(objDelareEntitlementNB, this.Log, gID, checkMonthNB, checkMonthNP);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool InsertMultipleDelareEntitlementNB(List<AT_DECLARE_ENTITLEMENTDTO> objDelareEntitlementlist, AT_DECLARE_ENTITLEMENTDTO objDelareEntitlementNB, ref decimal gID, ref bool checkMonthNB, ref bool checkMonthNP)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.InsertMultipleDelareEntitlementNB(objDelareEntitlementlist, objDelareEntitlementNB, this.Log, gID, checkMonthNB, checkMonthNP);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool ImportDelareEntitlementNB(DataTable dtData, ref decimal gID, ref bool checkMonthNB, ref bool checkMonthNP)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.ImportDelareEntitlementNB(dtData, this.Log, gID, checkMonthNB, checkMonthNP);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public AT_DECLARE_ENTITLEMENTDTO GetDelareEntitlementNBById(decimal? _id)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.GetDelareEntitlementNBById(_id);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool ModifyDelareEntitlementNB(AT_DECLARE_ENTITLEMENTDTO objDelareEntitlementNB, ref decimal gID)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.ModifyDelareEntitlementNB(objDelareEntitlementNB, this.Log, gID);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool ActiveDelareEntitlementNB(List<decimal> lstHoliday, string sActive)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.ActiveDelareEntitlementNB(lstHoliday, this.Log, sActive);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool DeleteDelareEntitlementNB(List<decimal> lstID)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.DeleteDelareEntitlementNB(lstID);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool ValidateMonthThamNien(AT_DECLARE_ENTITLEMENTDTO objHoliday)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.ValidateMonthThamNien(objHoliday);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool ValidateMonthPhepNam(AT_DECLARE_ENTITLEMENTDTO objHoliday)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.ValidateMonthPhepNam(objHoliday);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool ValidateMonthNghiBu(AT_DECLARE_ENTITLEMENTDTO objHoliday)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.ValidateMonthNghiBu(objHoliday);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }


    public bool Cal_TimeTImesheet_Rice(ParamDTO _param, decimal? p_period_id, decimal? P_ORG_ID, List<decimal?> lstEmployee)
    {
        bool dt;
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                dt = rep.Cal_TimeTImesheet_Rice(_param, this.Log, p_period_id, P_ORG_ID, lstEmployee);
                return dt;
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public System.Data.DataSet GetSummaryRice(AT_TIME_TIMESHEET_RICEDTO param)
    {
        DataSet dt;
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                dt = rep.GetSummaryRice(param, this.Log);
                return dt;
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool ModifyLeaveSheetRice(AT_TIME_TIMESHEET_RICEDTO objRegister, ref decimal gID)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.ModifyLeaveSheetRice(objRegister, this.Log, gID);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool ApprovedTimeSheetRice(AT_TIME_TIMESHEET_RICEDTO objRegister, ref decimal gID)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.ApprovedTimeSheetRice(objRegister, this.Log, gID);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool InsertLeaveSheetRice(AT_TIME_TIMESHEET_RICEDTO objRegister, ref decimal gID)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.InsertLeaveSheetRice(objRegister, this.Log, gID);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public AT_TIME_TIMESHEET_RICEDTO GetTimeSheetRiceById(AT_TIME_TIMESHEET_RICEDTO obj)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.GetTimeSheetRiceById(obj);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public List<AT_ACTION_LOGDTO> GetActionLog(AT_ACTION_LOGDTO _filter, ref int Total = 0, int PageIndex = 0, int PageSize = int.MaxValue, string Sorts = "ACTION_DATE desc")
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
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

    public int DeleteActionLogsAT(List<decimal> lstDeleteIds)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
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

    public List<AT_TIME_TIMESHEET_DAILYDTO> GetListExplanation(AT_TIME_TIMESHEET_DAILYDTO _filter, ParamDTO _param, ref int Total = 0, int PageIndex = 0, int PageSize = int.MaxValue, string Sorts = "EMPLOYEE_CODE desc,WORKINGDAY asc")
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.GetListExplanation(_filter, _param, Total, PageIndex, PageSize, Sorts, this.Log);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public DataTable GetExplanationManual()
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
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

    public DataTable GetExplanationEmployee(ParamDTO _param)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                var lst = rep.GetExplanationEmployee(_param, this.Log);
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public bool ImportExplanation(DataTable dtData, ref decimal gID)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.ImportExplanation(dtData, this.Log, gID);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }



    public bool CheckPeriod(int PeriodId, decimal EmployeeId)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.CheckPeriod(PeriodId, EmployeeId);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public DataTable GetTimeSheetPortal(AT_TIME_TIMESHEET_DAILYDTO _filter)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
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
    #endregion
}
