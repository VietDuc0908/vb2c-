using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using System.Linq.Expressions;
using LinqKit.Extensions;
using System.Data.Common;
using Framework.Data;
using Framework.Data.System.Linq.Dynamic;
using System.Data.Entity;
using System.Text.RegularExpressions;
using System.Data.Objects;
using Oracle.DataAccess.Client;

public partial class PayrollRepository
{

    // Tải dứ liệu trong kỳ 
    // Tính toán dữ liệu trong kỳ. 
    // Tải sang bảog lương tổng hợp
    public bool Load_Calculate_Load(List<PAEmployeeCalculateDTO> lstEmp, int OrgId, int PeriodId, int IsDissolve, int IsLoad, UserLog log)
    {
        try
        {
            using (DataAccess.NonQueryData Sql = new DataAccess.NonQueryData())
            {
                decimal idEmpCal = 0;
                // If lstEmp.Count > 0 Then
                // Sql.ExecuteSQL(String.Format("DELETE PA_EMP_CALCULATE_SAL S WHERE S.CREATED_BY = '{0}'", log.Username.ToUpper).ToString())
                // idEmpCal = Utilities.GetNextSequence(Context, Context.PA_EMP_CALCULATE_SAL.EntitySet.Name)
                // For Each objEmp As PAEmployeeCalculateDTO In lstEmp
                // Dim objData As New PA_EMP_CALCULATE_SAL
                // objData.ID = idEmpCal
                // objData.EMPLOYEE_ID = objEmp.EMPLOYEE_ID
                // Context.PA_EMP_CALCULATE_SAL.AddObject(objData)
                // Next
                // Context.SaveChanges(log)
                // End If
                Sql.ExecuteStore("PKG_PA_BUSINESS.LOAD_DATA", new { P_PERIOD_ID = PeriodId, P_ORG_ID = OrgId, P_ISDISSOLVE = IsDissolve, P_USERNAME = log.Username, P_ISLOAD = IsLoad, P_EMPID = idEmpCal });
                // ghi log phần tải dữ liệu tính lương
                PA_ACTION_LOGDTO objLog = new PA_ACTION_LOGDTO();
                objLog.PERIOD_ID = PeriodId;
                LOG_PA(log, "TẢI DỮ LIỆU TÍNH LƯƠNG", objLog);
            }
            return true;
        }
        catch (Exception ex)
        {
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod().Name, "iPayroll");
            return false;
        }
    }
    // Tính lương tổng hợp
    public bool Calculate_data_sum(int OrgId, int PeriodId, int IsDissolve, int IsLoad, UserLog log)
    {
        try
        {
            using (DataAccess.NonQueryData Sql = new DataAccess.NonQueryData())
            {
                // ghi log phần tải dữ liệu tính lương
                PA_ACTION_LOGDTO objLog = new PA_ACTION_LOGDTO();
                objLog.PERIOD_ID = PeriodId;
                LOG_PA(log, "TÍNH LƯƠNG", objLog);

                Sql.ExecuteStore("PKG_PA_BUSINESS.CALCULATE_DATA_SUM", new { P_PERIOD_ID = PeriodId, P_ORG_ID = OrgId, P_ISDISSOLVE = IsDissolve, P_USERNAME = log.Username });
            }
            return true;
        }
        catch (Exception ex)
        {
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod().Name, "iPayroll");
            return false;
        }
    }
    // 
    public bool Load_data_sum(int OrgId, int PeriodId, int IsDissolve, int IsLoad, UserLog log)
    {
        try
        {
            using (DataAccess.NonQueryData Sql = new DataAccess.NonQueryData())
            {
                Sql.ExecuteStore("PKG_PA_BUSINESS.LOAD_DATA_SUM", new { P_PERIOD_ID = PeriodId, P_ORG_ID = OrgId, P_ISDISSOLVE = IsDissolve, P_USERNAME = log.Username, P_ISLOAD = IsLoad });
            }
            return true;
        }
        catch (Exception ex)
        {
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod().Name, "iPayroll");
            return false;
        }
    }
    public bool Calculate_data_temp(int OrgId, int PeriodId, int IsDissolve, int IsLoad, UserLog log)
    {
        try
        {
            using (DataAccess.NonQueryData Sql = new DataAccess.NonQueryData())
            {
                Sql.ExecuteStore("PKG_PA_BUSINESS.CALCULATE_DATA_TEMP", new { P_PERIOD_ID = PeriodId, P_ORG_ID = OrgId, P_ISDISSOLVE = IsDissolve, P_USERNAME = log.Username });
            }
            return true;
        }
        catch (Exception ex)
        {
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod().Name, "iPayroll");
            return false;
        }
    }
    // Tìm kiếm dữ liệu tính lương
    public DataTable GetLitsCalculate(PA_ParamDTO param, int IsLoad, int PageIndex, int PageSize, int PageType, ref int TotalRow, UserLog log, string Sorts = "CREATED_DATE DESC")
    {
        try
        {
            using (DataAccess.QueryData cls = new DataAccess.QueryData())
            {
                var obj = new { P_ORGID = param.ORG_ID, P_ISDISSOLVE = param.IS_DISSOLVE, P_USERNAME = log.Username, P_PERIOD_ID = param.PERIOD_ID, P_EMPLOYEE_CODE = param.EMPLOYEE_CODE, P_EMPLOYEE_NAME = param.FULLNAME_VN, P_ORG_NAME = param.ORG_NAME, P_TITLE_NAME = param.TITLE_NAME, P_STAFF_RANK_NAME = param.STAFF_RANK_NAME, P_SORT = Sorts, IS_LOAD = IsLoad, P_PAGESIZE = PageSize, P_PAGEINDEX = PageIndex, P_TYPE = PageType, P_ROWCOUNT = cls.OUT_NUMBER, P_CUR = cls.OUT_CURSOR };

                var dtData = cls.ExecuteStore("PKG_PA_BUSINESS.GET_LIST_CALCULATE", obj, true);
                TotalRow = obj.P_ROWCOUNT;
                return dtData;
            }
        }
        catch (Exception ex)
        {
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod().Name, "iPayroll");
            throw ex;
        }
    }

    public DataSet LoadCalculate(int PeriodId, int OrgId, int IsDissolve, List<string> listEmployee, UserLog log, string Sorts = "CREATED_DATE DESC")
    {
        try
        {
            using (DataAccess.NonQueryData cls = new DataAccess.NonQueryData())
            {
                cls.ExecuteSQL("DELETE SE_CHOSEN_CALCULATE");
                if (listEmployee.Count <= 0)
                    cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_CALCULATE", new { P_USERNAME = log.Username, P_ORGID = OrgId, P_ISDISSOLVE = IsDissolve });
                else
                {
                    foreach (string emp in listEmployee)
                    {
                        SE_CHOSEN_CALCULATE objNew = new SE_CHOSEN_CALCULATE();
                        objNew.EMPLOYEEID = Utilities.Obj2Decima(emp);
                        objNew.USERNAME = log.Username;
                        Context.SE_CHOSEN_CALCULATE.AddObject(objNew);
                    }
                    Context.SaveChanges();
                }
            }
            using (DataAccess.QueryData cls = new DataAccess.QueryData())
            {
                DataSet dtData = cls.ExecuteStore("PKG_PA_BUSINESS.GET_IMPORTSALARY", new { P_ORG_ID = OrgId, P_PERIOD_ID = PeriodId, P_USERNAME = log.Username, P_SORT = Sorts, P_CUR = cls.OUT_CURSOR });
                return dtData;
            }
        }
        catch (Exception ex)
        {
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod().Name, "iPayroll");
            throw ex;
        }
    }

    public List<PAListSalariesDTO> GetListSalaryVisibleCol()
    {
        try
        {
            var query = from s in Context.PA_LISTSALARIES
                        where s.IS_VISIBLE == true & s.STATUS == "A"
                        orderby s.COL_INDEX
                        select new PAListSalariesDTO() { ID = s.ID, TYPE_PAYMENT = s.TYPE_PAYMENT, COL_NAME = s.COL_NAME, NAME_EN = s.NAME_EN, NAME_VN = s.NAME_VN, COL_INDEX = s.COL_INDEX, CREATED_DATE = s.CREATED_DATE, IS_VISIBLE = s.IS_VISIBLE, STATUS = s.STATUS };

            return query.ToList;
        }
        catch (Exception ex)
        {
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod().Name, "iPayroll");
            throw ex;
        }
    }

    public bool ActiveOrDeactive(PA_ParamDTO _param, UserLog log)
    {
        try
        {
            using (DataAccess.NonQueryData sql = new DataAccess.NonQueryData())
            {
                sql.ExecuteStore("PKG_PA_BUSINESS.CLOSEDOPEN_PERIOD", new { P_USERNAME = log.Username.ToUpper, P_ORG_ID = _param.ORG_ID, P_ISDISSOLVE = _param.IS_DISSOLVE, P_STATUS = _param.STATUS, P_PERIOD_ID = _param.PERIOD_ID });
            }
            return true;
        }
        catch (Exception ex)
        {
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod().Name, "iPayroll");
            return false;
        }
    }


    public DataTable GetImportSalary(int PeriodId, int OrgId, int IsDissolve, string EmployeeId, int PageIndex, int PageSize, ref int TotalRow, UserLog log, string Sorts = "CREATED_DATE DESC")
    {
        try
        {
            using (DataAccess.QueryData cls = new DataAccess.QueryData())
            {
                var obj = new { P_ORGID = OrgId, P_ISDISSOLVE = IsDissolve, P_USERNAME = log.Username, P_PERIOD_ID = PeriodId, P_EMPLOYEE = EmployeeId, P_SORT = Sorts, P_PAGESIZE = PageSize, P_PAGEINDEX = PageIndex, P_ROWCOUNT = cls.OUT_NUMBER, P_CUR = cls.OUT_CURSOR };

                var dtData = cls.ExecuteStore("PKG_PA_BUSINESS.GET_IMPORTSALARY", obj, true);
                TotalRow = obj.P_ROWCOUNT;
                return dtData;
            }
        }
        catch (Exception ex)
        {
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod().Name, "iPayroll");
            throw ex;
        }
    }

    public List<PAListSalariesDTO> GetSalaryList()
    {
        try
        {
            var query = from p in Context.PA_LISTSALARIES
                        where p.IS_IMPORT == -1 & p.TYPE_PAYMENT == 4123
                        orderby p.COL_INDEX ascending
                        select p;
            var lst = query.Select(s => new PAListSalariesDTO()
            {
                ID = s.ID,
                TYPE_PAYMENT = s.TYPE_PAYMENT,
                COL_NAME = s.COL_NAME,
                NAME_EN = s.NAME_EN,
                NAME_VN = s.NAME_VN,
                COL_INDEX = s.COL_INDEX,
                CREATED_DATE = s.CREATED_DATE,
                IS_IMPORT = s.IS_IMPORT
            });
            return lst.ToList;
        }
        catch (Exception ex)
        {
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod().Name, "iPayroll");
            throw ex;
        }
    }

    public bool SaveImport(decimal Period, DataTable dtData, List<string> lstColVal, UserLog log, ref int RecordSussces)
    {
        DataTable dtCount;

        try
        {
            using (DataAccess.ConnectionManager conMng = new DataAccess.ConnectionManager())
            {
                using (OracleConnection conn = new OracleConnection(conMng.GetConnectionString()))
                {
                    using (OracleCommand cmd = new OracleCommand())
                    {
                        try
                        {
                            StringBuilder sql = new StringBuilder();
                            StringBuilder sqlUpdate = new StringBuilder();
                            foreach (string Str in lstColVal)
                            {
                                sqlUpdate.Append(Str);
                                sqlUpdate.Append("= :");
                                sqlUpdate.Append(Str);
                                if (lstColVal.Last() != Str)
                                    sqlUpdate.Append(",");
                            }

                            conn.Open();
                            cmd.Connection = conn;
                            cmd.Transaction = cmd.Connection.BeginTransaction();
                            RecordSussces = 0;
                            using (DataAccess.QueryData s = new DataAccess.QueryData())
                            {
                                dtCount = s.ExecuteSQL(string.Format("SELECT S.ID, S.EMPLOYEE_ID FROM PA_PAYROLLSHEET_SUM S WHERE S.PERIOD_ID ={0}", Period).ToString());
                            }
                            foreach (DataRow dr in dtData.Rows)
                            {
                                if (dr("ID").ToString == "")
                                    continue;

                                cmd.CommandText = string.Format("UPDATE PA_PAYROLLSHEET_SUM SET {0}  WHERE EMPLOYEE_ID = :EMPLOYEE_ID AND PERIOD_ID = :PERIOD_ID", sqlUpdate).ToString();
                                cmd.Parameters.Clear();

                                foreach (string parm in lstColVal)
                                {
                                    if (dr(parm).ToString == "")
                                        cmd.Parameters.Add(parm, 0);
                                    else
                                    {
                                        var parameter = dr(parm).ToString.Trim.Replace(",", "");
                                        cmd.Parameters.Add(parm, Utilities.Obj2Decima(parameter.Replace(" ", "")));
                                    }
                                }
                                cmd.Parameters.Add("EMPLOYEE_ID", dr("ID"));
                                cmd.Parameters.Add("PERIOD_ID", Period);

                                int r = 0;
                                r = cmd.ExecuteNonQuery();
                                if (r > 0)
                                    RecordSussces += 1;
                            }
                            cmd.Transaction.Commit();

                            // ghi log phần tải dữ liệu tính lương
                            PA_ACTION_LOGDTO objLog = new PA_ACTION_LOGDTO();
                            objLog.PERIOD_ID = Period;
                            LOG_PA(log, "IMPORT CÁC KHOẢN PHÁT SINH KHÁC", objLog);
                        }
                        catch (Exception ex)
                        {
                            WriteExceptionLog(ex, MethodBase.GetCurrentMethod().Name, "iPayroll");
                            cmd.Transaction.Rollback();
                        }

                        finally
                        {
                            cmd.Dispose();
                            conn.Close();
                            conn.Dispose();
                        }
                    }
                }
            }
            return true;
        }
        catch (Exception ex)
        {
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod().Name, "iPayroll");
            throw ex;
        }
    }



    public DataTable GetPayrollSheetSum(int PeriodId, string EmployeeId, UserLog log, string Sorts = "CREATED_DATE DESC")
    {
        try
        {
            using (DataAccess.QueryData cls = new DataAccess.QueryData())
            {
                DataTable dtData = cls.ExecuteStore("PKG_PA_BUSINESS.LOAD_PAYROLL_SHEET_SUM", new { P_PERIOD_ID = PeriodId, P_EMPLOYEE = EmployeeId, P_SORT = Sorts, P_CUR = cls.OUT_CURSOR });
                return dtData;
            }
        }
        catch (Exception ex)
        {
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod().Name, "iPayroll");
            throw ex;
        }
    }

    public bool CheckPeriod(int PeriodId, decimal EmployeeId)
    {
        try
        {
            HU_EMPLOYEE emp;
            emp = (from p in Context.HU_EMPLOYEE
                   where p.ID == EmployeeId
                   select p).FirstOrDefault;

            var query = (from p in Context.AT_ORG_PERIOD
                         where p.PERIOD_ID == PeriodId & p.ORG_ID == emp.ORG_ID
                         select p).FirstOrDefault;

            if (query != null)
                return query.STATUSCOLEX == 0;
            else
                return (query == null);
        }
        catch (Exception ex)
        {
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod().Name, "iPayroll");
            throw ex;
        }
    }



    public bool CalSeniorityProcess(PASeniorityProcessDTO _filter, UserLog log)
    {
        try
        {
            using (DataAccess.NonQueryData cls = new DataAccess.NonQueryData())
            {
                var obj = new { P_PERIOD_ID = _filter.PERIOD_ID, P_ORGID = _filter.ORG_ID, P_ISDISSOLVE = _filter.IS_DISSOLVE, P_USERNAME = log.Username.ToUpper };

                var dtData = cls.ExecuteStore("PKG_PA_BUSINESS.CAL_SENIORITY_DAILY", obj);
                return true;
            }
        }
        catch (Exception ex)
        {
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod().Name, "iPayroll");
            throw ex;
        }
    }

    public List<PASeniorityProcessDTO> GetSeniorityProcess(PASeniorityProcessDTO _filter, int PageIndex, int PageSize, ref int Total, UserLog log, string Sorts = "EMPLOYEE_CODE desc")
    {
        try
        {

            // lấy toàn bộ dữ liệu theo Org
            using (DataAccess.QueryData cls = new DataAccess.QueryData())
            {
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG", new { P_USERNAME = log.Username.ToUpper, P_ORGID = _filter.ORG_ID, P_ISDISSOLVE = _filter.IS_DISSOLVE });
            };

             //lst = From p In Context.PA_SENIORITY_PROCESS
             //         From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
             //         From t In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID)
             //         From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID)
             //         From s In Context.HU_STAFF_RANK.Where(Function(f) f.ID = p.STAFF_RANK_ID).DefaultIfEmpty
             //         From c In Context.HU_CONTRACT.Where(Function(f) f.ID = p.CONTRACT_ID).DefaultIfEmpty
             //         From c_type In Context.HU_CONTRACT_TYPE.Where(Function(f) f.ID = c.CONTRACT_TYPE_ID).DefaultIfEmpty
             //         From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = p.ORG_ID And f.USERNAME = log.Username.ToUpper)
             //         Where p.PERIOD_ID = _filter.PERIOD_ID;
             //         select new PASeniorityProcessDTO() {.ID = p.ID,
             //                                                .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
             //                                                .EMPLOYEE_NAME = e.FULLNAME_VN,
             //                                                 .CONTRACT_TYPE_NAME = c_type.NAME,
             //                                                 .DISCIPLINE_COUNT = p.DISCIPLINE_COUNT,
             //                                                 .DISCIPLINE_TYPE_NAME = p.DISCIPLINE_TYPE_NAME,
             //                                                 .ORG_NAME = o.NAME_VN,
             //                                                 .PERCENT_SALARY = p.PERCENT_SALARY,
             //                                                 .SALARY = p.SALARY,
             //                                                 .STAFF_RANK_NAME = s.NAME,
             //                                                 .TITLE_NAME = t.NAME_VN,
             //                                                 .D1 = p.D1,
             //                                                 .D2 = p.D2,
             //                                                 .D3 = p.D3,
             //                                                 .D4 = p.D4,
             //                                                 .D5 = p.D5,
             //                                                 .D6 = p.D6,
             //                                                 .D7 = p.D7,
             //                                                 .D8 = p.D8,
             //                                                 .D9 = p.D9,
             //                                                 .D10 = p.D10,
             //                                                 .D11 = p.D11,
             //                                                 .D12 = p.D12,
             //                                                 .D13 = p.D13,
             //                                                 .D14 = p.D14,
             //                                                 .D15 = p.D15,
             //                                                 .D16 = p.D16,
             //                                                 .D17 = p.D17,
             //                                                 .D18 = p.D18,
             //                                                 .D19 = p.D19,
             //                                                 .D20 = p.D20,
             //                                                 .D21 = p.D21,
             //                                                 .D22 = p.D22,
             //                                                 .D23 = p.D23,
             //                                                 .D24 = p.D24,
             //                                                 .D25 = p.D25,
             //                                                 .D26 = p.D26,
             //                                                 .D27 = p.D27,
             //                                                 .D28 = p.D28,
             //                                                 .D29 = p.D29,
             //                                                 .D30 = p.D30,
             //                                                 .D31 = p.D31}


            if (_filter.CONTRACT_TYPE_NAME != "")
                lst = lst.Where(f => f.CONTRACT_TYPE_NAME.ToUpper.Contains(_filter.CONTRACT_TYPE_NAME.ToUpper));
            if (_filter.ORG_NAME != "")
                lst = lst.Where(f => f.ORG_NAME.ToUpper.Contains(_filter.ORG_NAME.ToUpper));
            if (_filter.TITLE_NAME != "")
                lst = lst.Where(f => f.TITLE_NAME.ToUpper.Contains(_filter.TITLE_NAME.ToUpper));
            if (_filter.ORG_NAME != "")
                lst = lst.Where(f => f.ORG_NAME.ToUpper.Contains(_filter.ORG_NAME.ToUpper));
            if (_filter.EMPLOYEE_CODE != "")
                lst = lst.Where(f => f.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper));
            if (_filter.EMPLOYEE_NAME != "")
                lst = lst.Where(f => f.EMPLOYEE_NAME.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper));
            if (_filter.STAFF_RANK_NAME != "")
                lst = lst.Where(f => f.STAFF_RANK_NAME.ToUpper.Contains(_filter.STAFF_RANK_NAME.ToUpper));
            lst = lst.OrderBy(Sorts);
            Total = lst.Count;
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize);

            return lst.ToList;
        }
        catch (Exception ex)
        {
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod().Name, "iPayroll");
            throw ex;
        }
    }

    public DataTable GetSeniorityProcessImport(PASeniorityProcessDTO _filter, UserLog log)
    {
        try
        {
            using (DataAccess.QueryData cls = new DataAccess.QueryData())
            {
                var obj = new { P_PERIOD_ID = _filter.PERIOD_ID, P_ORGID = _filter.ORG_ID, P_ISDISSOLVE = _filter.IS_DISSOLVE, P_USERNAME = log.Username.ToUpper, P_CUR = cls.OUT_CURSOR };

                return cls.ExecuteStore("PKG_PA_BUSINESS.GET_SENIORITY_IMPORT", obj);
            }
        }
        catch (Exception ex)
        {
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod().Name, "iPayroll");
            throw ex;
        }
    }


    public bool SaveSeniorityProcessImport(DataTable dtData, decimal periodId, UserLog log)
    {
        try
        {
            using (DataAccess.ConnectionManager conMng = new DataAccess.ConnectionManager())
            {
                using (OracleConnection conn = new OracleConnection(conMng.GetConnectionString()))
                {
                    using (OracleCommand cmd = new OracleCommand())
                    {
                        try
                        {
                            conn.Open();
                            cmd.Connection = conn;
                            cmd.Transaction = cmd.Connection.BeginTransaction();
                            cmd.CommandType = CommandType.Text;
                            foreach (DataRow dr in dtData.Rows)
                            {
                                cmd.Parameters.Clear();
                                cmd.CommandText = "UPDATE PA_SENIORITY_PROCESS SET " + Constants.vbNewLine + "PERCENT_SALARY=:PERCENT_SALARY " + Constants.vbNewLine + "WHERE EMPLOYEE_ID = :EMPLOYEE_ID AND PERIOD_ID = :PERIOD_ID";

                                cmd.Parameters.Add("PERCENT_SALARY", dr("PERCENT_SALARY"));
                                cmd.Parameters.Add("EMPLOYEE_ID", dr("EMPLOYEE_ID"));
                                cmd.Parameters.Add("PERIOD_ID", periodId);
                                cmd.ExecuteNonQuery();
                            }

                            cmd.Transaction.Commit();
                            PA_ACTION_LOGDTO objLog = new PA_ACTION_LOGDTO();
                            objLog.PERIOD_ID = periodId;
                            LOG_PA(log, "IMPORT XỬ LÝ DỮ LIỆU THƯỞNG THÂM NIÊN", objLog);
                        }
                        catch (Exception ex)
                        {
                            WriteExceptionLog(ex, MethodBase.GetCurrentMethod().Name, "iPayroll");
                            cmd.Transaction.Rollback();
                        }

                        finally
                        {
                            cmd.Dispose();
                            conn.Close();
                            conn.Dispose();
                        }
                    }
                }
            }
            return true;
        }
        catch (Exception ex)
        {
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod().Name, "iPayroll");
            throw ex;
        }
    }

    public bool CalSeniorityMonthly(PASeniorityMonthlyDTO _filter, UserLog log)
    {
        try
        {
            using (DataAccess.NonQueryData cls = new DataAccess.NonQueryData())
            {
                var obj = new { P_PERIOD_ID = _filter.PERIOD_ID, P_ORGID = _filter.ORG_ID, P_ISDISSOLVE = _filter.IS_DISSOLVE, P_USERNAME = log.Username.ToUpper };

                var dtData = cls.ExecuteStore("PKG_PA_BUSINESS.CAL_SENIORITY_MONTHLY", obj);
                return true;
            }
        }
        catch (Exception ex)
        {
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod().Name, "iPayroll");
            throw ex;
        }
    }

    public List<PASeniorityMonthlyDTO> GetSeniorityMonthly(PASeniorityMonthlyDTO _filter, int PageIndex, int PageSize, ref int Total, UserLog log, string Sorts = "EMPLOYEE_CODE")
    {
        try
        {
            // lấy toàn bộ dữ liệu theo Org
            using (DataAccess.QueryData cls = new DataAccess.QueryData())
            {
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG", new { P_USERNAME = log.Username.ToUpper, P_ORGID = _filter.ORG_ID, P_ISDISSOLVE = _filter.IS_DISSOLVE });
            };


 //            list = From p In Context.PA_SENIORITY_MONTHLY
 //                     From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
 //                     From t In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID)
 //                     From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID)
 //                     From s In Context.HU_STAFF_RANK.Where(Function(f) f.ID = p.STAFF_RANK_ID).DefaultIfEmpty
 //                     From c In Context.HU_CONTRACT.Where(Function(f) f.ID = p.CONTRACT_ID).DefaultIfEmpty
 //                     From c_type In Context.HU_CONTRACT_TYPE.Where(Function(f) f.ID = c.CONTRACT_TYPE_ID).DefaultIfEmpty
 //                     From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = p.ORG_ID And f.USERNAME = log.Username.ToUpper)
 //                     Where p.PERIOD_ID = _filter.PERIOD_ID;
 //                     select new PASeniorityMonthlyDTO () {.ID = p.ID,
 //                                                            .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
 //                                                            .EMPLOYEE_NAME = e.FULLNAME_VN,
 //                                                            .CONTRACT_TYPE_NAME = c_type.NAME,
 //                                                            .ORG_NAME = o.NAME_VN,
 //                                                            .STAFF_RANK_NAME = s.NAME,
 //                                                            .TITLE_NAME = t.NAME_VN,
 //                                                            .PERCENT_SALARY = p.PERCENT_SALARY,
 //                                                            .DAY_COUNT1 = p.DAY_COUNT1,
 //                                                            .DAY_COUNT2 = p.DAY_COUNT2,
 //                                                            .DAY_COUNT3 = p.DAY_COUNT3,
 //                                                            .DAY_COUNT5 = p.DAY_COUNT5,
 //                                                            .SENIORITY = p.SENIORITY,
 //                                                            .SALARY_TOTAL = p.SALARY_TOTAL};


            if (_filter.CONTRACT_TYPE_NAME != "")
                lst = lst.Where(f => f.CONTRACT_TYPE_NAME.ToUpper.Contains(_filter.CONTRACT_TYPE_NAME.ToUpper));
            if (_filter.ORG_NAME != "")
                lst = lst.Where(f => f.ORG_NAME.ToUpper.Contains(_filter.ORG_NAME.ToUpper));
            if (_filter.TITLE_NAME != "")
                lst = lst.Where(f => f.TITLE_NAME.ToUpper.Contains(_filter.TITLE_NAME.ToUpper));
            if (_filter.ORG_NAME != "")
                lst = lst.Where(f => f.ORG_NAME.ToUpper.Contains(_filter.ORG_NAME.ToUpper));
            if (_filter.EMPLOYEE_CODE != "")
                lst = lst.Where(f => f.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper));
            if (_filter.EMPLOYEE_NAME != "")
                lst = lst.Where(f => f.EMPLOYEE_NAME.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper));
            if (_filter.STAFF_RANK_NAME != "")
                lst = lst.Where(f => f.STAFF_RANK_NAME.ToUpper.Contains(_filter.STAFF_RANK_NAME.ToUpper));
            if (_filter.PERCENT_SALARY != null)
                lst = lst.Where(f => f.PERCENT_SALARY == _filter.PERCENT_SALARY);
            lst = lst.OrderBy(Sorts);
            Total = lst.Count;
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize);

            return lst.ToList;
        }
        catch (Exception ex)
        {
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod().Name, "iPayroll");
            throw ex;
        }
    }


    public List<PA_ACTION_LOGDTO> GetActionLog(PA_ACTION_LOGDTO _filter, ref int Total, int PageIndex, int PageSize, string Sorts = "ACTION_DATE desc")
    {
        try
        {
            
            //Dim query = From p In Context.PA_ACTION_LOG
            //            From e In Context.SE_USER.Where(Function(f) f.USERNAME.ToUpper = p.USERNAME.ToUpper)
            //            From r In Context.AT_PERIOD.Where(Function(f) f.ID = p.PERIOD_ID).DefaultIfEmpty


            var lst = query.Select(p => new PA_ACTION_LOGDTO() { ID = p.p.ID, username = p.p.USERNAME, fullname = p.e.FULLNAME, email = p.e.EMAIL, mobile = p.e.TELEPHONE, action_name = p.p.ACTION_NAME, action_date = p.p.ACTION_DATE, object_name = p.p.OBJECT_NAME, PERIOD_ID = p.p.PERIOD_ID, PERIOD_NAME = p.r.PERIOD_NAME, ip = p.p.IP, computer_name = p.p.COMPUTER_NAME, action_type = p.p.ACTION_TYPE, EMPLOYEE_ID = p.p.EMPLOYEE_ID, NEW_VALUE = p.p.NEW_VALUE, OLD_VALUE = p.p.OLD_VALUE });


            if (!string.IsNullOrEmpty(_filter.username))
                lst = lst.Where(f => f.username.ToLower().Contains(_filter.username.ToLower()));
            if (!string.IsNullOrEmpty(_filter.fullname))
                lst = lst.Where(f => f.fullname.ToLower().Contains(_filter.fullname.ToLower()));
            if (!string.IsNullOrEmpty(_filter.email))
                lst = lst.Where(f => f.email.ToLower().Contains(_filter.email.ToLower()));
            if (!string.IsNullOrEmpty(_filter.mobile))
                lst = lst.Where(f => f.mobile.ToLower().Contains(_filter.mobile.ToLower()));
            if (!string.IsNullOrEmpty(_filter.action_name))
                lst = lst.Where(f => f.action_name.ToLower().Contains(_filter.action_name.ToLower()));
            if (_filter.action_date.HasValue)
                lst = lst.Where(f => f.action_date >= _filter.action_date);
            if (!string.IsNullOrEmpty(_filter.object_name))
                lst = lst.Where(f => f.object_name.ToLower().Contains(_filter.object_name.ToLower()));
            if (!string.IsNullOrEmpty(_filter.ip))
                lst = lst.Where(f => f.ip.ToLower().Contains(_filter.ip.ToLower()));
            if (!string.IsNullOrEmpty(_filter.computer_name))
                lst = lst.Where(f => f.computer_name.ToLower().Contains(_filter.computer_name.ToLower()));
            if (!string.IsNullOrEmpty(_filter.action_type))
                lst = lst.Where(f => f.action_type.ToLower().Contains(_filter.action_type.ToLower()));
            if (_filter.EMPLOYEE_ID.HasValue)
                lst = lst.Where(f => f.EMPLOYEE_ID == _filter.EMPLOYEE_ID);
            if (!string.IsNullOrEmpty(_filter.NEW_VALUE))
                lst = lst.Where(f => f.NEW_VALUE.ToLower().Contains(_filter.NEW_VALUE.ToLower()));
            if (!string.IsNullOrEmpty(_filter.PERIOD_NAME))
                lst = lst.Where(f => f.PERIOD_NAME.ToLower().Contains(_filter.PERIOD_NAME.ToLower()));
            if (!string.IsNullOrEmpty(_filter.OLD_VALUE))
                lst = lst.Where(f => f.OLD_VALUE.ToLower().Contains(_filter.OLD_VALUE.ToLower()));
            lst = lst.OrderBy(Sorts);
            Total = lst.Count;
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize);

            return lst.ToList;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public int DeleteActionLogsPA(List<decimal> lstDeleteIds)
    {
        List<PA_ACTION_LOG> lstData;
        try
        {
            lstData = (from p in Context.PA_ACTION_LOG
                       where lstDeleteIds.Contains(p.ID)
                       select p).ToList;
            for (var index = 0; index <= lstData.Count - 1; index++)
                Context.PA_ACTION_LOG.DeleteObject(lstData[index]);
            Context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod().Name, "iTime");
            throw ex;
        }
    }

    public bool LOG_PA(UserLog log, string Object_Name, PA_ACTION_LOGDTO action)
    {
        decimal? ActionId;
        PA_ACTION_LOG action_log = new PA_ACTION_LOG();
        action_log.ID = Utilities.GetNextSequence(Context, Context.PA_ACTION_LOG.EntitySet.Name);
        ActionId = action_log.ID;
        action_log.USERNAME = log.Username.ToUpper;
        action_log.IP = log.Ip;
        action_log.ACTION_NAME = log.ActionName;
        action_log.ACTION_DATE = DateTime.Now;
        action_log.OBJECT_NAME = Object_Name;
        action_log.COMPUTER_NAME = log.ComputerName;
        action_log.EMPLOYEE_ID = action.EMPLOYEE_ID;
        action_log.OLD_VALUE = action.OLD_VALUE;
        action_log.PERIOD_ID = action.PERIOD_ID;
        action_log.NEW_VALUE = action.NEW_VALUE;
        Context.PA_ACTION_LOG.AddObject(action_log);
        // If lstEmployee.Count > 0 Then
        // Dim action_logOrg As AT_ACTION_ORG_LOG
        // For Each emp As Decimal? In lstEmployee
        // action_logOrg = New AT_ACTION_ORG_LOG
        // action_logOrg.ID = Utilities.GetNextSequence(Context, Context.AT_ACTION_ORG_LOG.EntitySet.Name)
        // action_logOrg.EMPLOYEE_ID = emp
        // action_logOrg.ACTION_LOG_ID = ActionId
        // Context.AT_ACTION_ORG_LOG.AddObject(action_logOrg)
        // Next
        // Else
        // Using cls As New DataAccess.NonQueryData
        // cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.INSERT_CHOSEN_LOGORG",
        // New With {.P_USERNAME = log.Username.ToUpper,
        // .P_ORGID = _param.ORG_ID,
        // .P_ISDISSOLVE = _param.IS_DISSOLVE,
        // .P_ACTION_ID = ActionId})
        // End Using
        // End If
        Context.SaveChanges();
        return true;
    }


    public List<PAMailRemarkDTO> GetMailRemark(PAMailRemarkDTO _filter, int PageIndex, int PageSize, ref int Total, UserLog log, string Sorts = "CREATED_DATE desc")
    {
        try
        {
            using (DataAccess.QueryData cls = new DataAccess.QueryData())
            {
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG", new { P_USERNAME = log.Username.ToUpper, P_ORGID = 46, P_ISDISSOLVE = true });
            };

                //query = From p In Context.PA_MAIL_REMARK
                //        From pr In Context.AT_PERIOD.Where(Function(f) f.ID = p.PERIOD_ID)
                //        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID)
                //        From k In Context.SE_CHOSEN_ORG.Where(Function(f) p.ORG_ID = f.ORG_ID And
                //                                                  f.USERNAME.ToUpper = log.Username.ToUpper)
                //        Where pr.ID = _filter.PERIOD_ID
                //        Select New PAMailRemarkDTO With {
                //            .ID = p.ID,
                //            .ORG_ID = p.ORG_ID,
                //            .ORG_NAME = o.NAME_VN,
                //            .PERIOD_ID = p.PERIOD_ID,
                //            .PERIOD_NAME = pr.PERIOD_NAME,
                //            .REMARK = p.REMARK,
                //            .CREATED_DATE = p.CREATED_DATE};

 */
            if (_filter.PERIOD_NAME != "")
                query = query.Where(f => f.PERIOD_NAME.ToUpper.Contains(_filter.PERIOD_NAME.ToUpper));

            if (_filter.ORG_NAME != "")
                query = query.Where(f => f.ORG_NAME.ToUpper.Contains(_filter.ORG_NAME.ToUpper));

            if (_filter.REMARK != "")
                query = query.Where(f => f.REMARK.ToUpper.Contains(_filter.REMARK.ToUpper));

            query = query.OrderBy(Sorts);
            Total = query.Count;
            query = query.Skip(PageIndex * PageSize).Take(PageSize);

            return query.ToList;
        }
        catch (Exception ex)
        {
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod().Name, "iPayroll");
            throw ex;
        }
    }

    public bool InsertMailRemark(PAMailRemarkDTO objTitle, UserLog log, ref decimal gID)
    {
        PA_MAIL_REMARK objTitleData;
        int iCount = 0;
        try
        {
            objTitleData = (from p in Context.PA_MAIL_REMARK
                            where p.PERIOD_ID == objTitle.PERIOD_ID & p.ORG_ID == objTitle.ORG_ID
                            select p).FirstOrDefault;
            if (objTitleData == null)
            {
                objTitleData = new PA_MAIL_REMARK();
                objTitleData.ID = Utilities.GetNextSequence(Context, Context.PA_MAIL_REMARK.EntitySet.Name);
                objTitleData.ORG_ID = objTitle.ORG_ID;
                objTitleData.PERIOD_ID = objTitle.PERIOD_ID;
                objTitleData.REMARK = objTitle.REMARK;
                Context.PA_MAIL_REMARK.AddObject(objTitleData);
            }
            else
                objTitleData.REMARK = objTitle.REMARK;
            Context.SaveChanges(log);
            gID = objTitleData.ID;
            return true;
        }
        catch (Exception ex)
        {
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod().Name, "iPayroll");
            throw ex;
        }
    }

    public bool ModifyMailRemark(PAMailRemarkDTO objTitle, UserLog log, ref decimal gID)
    {
        PA_MAIL_REMARK objTitleData = new PA_MAIL_REMARK() { ID = objTitle.ID };
        try
        {
            objTitleData = (from p in Context.PA_MAIL_REMARK
                            where p.ID == objTitleData.ID
                            select p).FirstOrDefault;
            objTitleData.REMARK = objTitle.REMARK;
            Context.SaveChanges(log);
            gID = objTitleData.ID;
            return true;
        }
        catch (Exception ex)
        {
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod().Name, "iPayroll");
            throw ex;
        }
    }

    public bool DeleteMailRemark(List<decimal> lstID)
    {
        List<PA_MAIL_REMARK> lstMailRemarkData;
        try
        {
            lstMailRemarkData = (from p in Context.PA_MAIL_REMARK
                                 where lstID.Contains(p.ID)
                                 select p).ToList;
            for (var index = 0; index <= lstMailRemarkData.Count - 1; index++)
                Context.PA_MAIL_REMARK.DeleteObject(lstMailRemarkData[index]);
            Context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod().Name, "iPayroll");
            // Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteMailRemark")
            throw ex;
        }
    }
}
