using System;
using PayrollBusiness.ServiceContracts;
using PayrollDAL;
using Framework.Data;

namespace PayrollBusiness.ServiceImplementations
{
    public partial class PayrollBusiness : ServiceContracts.IPayrollBusiness
    {
        #region "Calculate Salary"
        public bool Load_Calculate_Load(List<PAEmployeeCalculateDTO> lstEmp, int OrgId, int PeriodId, int IsDissolve, int IsLoad, UserLog log)
        {
            PayrollRepository rep = new PayrollRepository();
            return rep.Load_Calculate_Load(lstEmp, OrgId, PeriodId, IsDissolve, IsLoad, log);
        }
        public bool Calculate_data_sum(int OrgId, int PeriodId, int IsDissolve, int IsLoad, UserLog log)
        {
            PayrollRepository rep = new PayrollRepository();
            return rep.Calculate_data_sum(OrgId, PeriodId, IsDissolve, IsLoad, log);
        }
        public bool Load_data_sum(int OrgId, int PeriodId, int IsDissolve, int IsLoad, UserLog log)
        {
            PayrollRepository rep = new PayrollRepository();
            return rep.Load_data_sum(OrgId, PeriodId, IsDissolve, IsLoad, log);
        }
        public bool Calculate_data_temp(int OrgId, int PeriodId, int IsDissolve, int IsLoad, UserLog log)
        {
            PayrollRepository rep = new PayrollRepository();
            return rep.Calculate_data_temp(OrgId, PeriodId, IsDissolve, IsLoad, log);
        }

        public DataTable GetLitsCalculate(PA_ParamDTO param, int IsLoad, int PageIndex, int PageSize, int PageType, ref int TotalRow, UserLog log, string Sorts = "CREATED_DATE DESC")
        {
            PayrollRepository rep = new PayrollRepository();
            return rep.GetLitsCalculate(param, IsLoad, PageIndex, PageSize, PageType, TotalRow, log);
        }
        public List<PAListSalariesDTO> GetListSalaryVisibleCol()
        {
            PayrollRepository rep = new PayrollRepository();
            return rep.GetListSalaryVisibleCol();
        }

        public bool ActiveOrDeactive(PA_ParamDTO _param, UserLog log)
        {
            PayrollRepository rep = new PayrollRepository();
            return rep.ActiveOrDeactive(_param, log);
        }
        #endregion

        #region "Import Salary"
        public DataTable GetImportSalary(int PeriodId, int OrgId, int IsDissolve, string EmployeeId, int PageIndex, int PageSize, ref int TotalRow, UserLog log, string Sorts = "CREATED_DATE DESC")
        {
            try
            {
                PayrollRepository rep = new PayrollRepository();
                return rep.GetImportSalary(PeriodId, OrgId, IsDissolve, EmployeeId, PageIndex, PageSize, TotalRow, log, Sorts);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<PAListSalariesDTO> GetSalaryList()
        {
            try
            {
                PayrollRepository rep = new PayrollRepository();
                return rep.GetSalaryList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool SaveImport(decimal Period, DataTable dtData, List<string> lstColVal, UserLog log, ref int RecordSussces)
        {
            try
            {
                PayrollRepository rep = new PayrollRepository();
                return rep.SaveImport(Period, dtData, lstColVal, log, RecordSussces);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region "IPORTAL - View phiếu lương"
        public DataTable GetPayrollSheetSum(int PeriodId, string EmployeeId, UserLog log, string Sorts = "CREATED_DATE DESC")
        {
            try
            {
                PayrollRepository rep = new PayrollRepository();
                return rep.GetPayrollSheetSum(PeriodId, EmployeeId, log, Sorts);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool CheckPeriod(int PeriodId, decimal EmployeeId)
        {
            try
            {
                PayrollRepository rep = new PayrollRepository();
                return rep.CheckPeriod(PeriodId, EmployeeId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region "Báo cáo lương "
        public List<Se_ReportDTO> GetReportById(Se_ReportDTO _filter, int PageIndex, int PageSize, ref int Total, UserLog log, string Sorts = "CODE ASC")
        {
            using (PayrollRepository rep = new PayrollRepository())
            {
                try
                {
                    var lst = rep.GetReportById(_filter, PageIndex, PageSize, Total, log, Sorts);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataSet ExportReport(string sPkgName, decimal sMoth, decimal sYear, string sOrgId, decimal IsDissolve, UserLog log)
        {
            using (PayrollRepository rep = new PayrollRepository())
            {
                try
                {
                    var lst = rep.ExportReport(sPkgName, sMoth, sYear, sOrgId, IsDissolve, log);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataSet ExportReport_005(string sPkgName, decimal sMoth, decimal sYear, string sOrgId, decimal IsDissolve, UserLog log)
        {
            using (PayrollRepository rep = new PayrollRepository())
            {
                try
                {
                    var lst = rep.ExportReport_005(sPkgName, sMoth, sYear, sOrgId, IsDissolve, log);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataSet ExportReport_008(string sPkgName, decimal sMoth, decimal sYear, string sOrgId, decimal IsDissolve, UserLog log)
        {
            using (PayrollRepository rep = new PayrollRepository())
            {
                try
                {
                    var lst = rep.ExportReport_008(sPkgName, sMoth, sYear, sOrgId, IsDissolve, log);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataSet ExportReport_010(string sPkgName, decimal sMoth, decimal sYear, string sOrgId, decimal IsDissolve, UserLog log)
        {
            using (PayrollRepository rep = new PayrollRepository())
            {
                try
                {
                    var lst = rep.ExportReport_010(sPkgName, sMoth, sYear, sOrgId, IsDissolve, log);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataSet ExportReport_014(string sPkgName, decimal sMoth, decimal sYear, string sOrgId, decimal IsDissolve, UserLog log)
        {
            using (PayrollRepository rep = new PayrollRepository())
            {
                try
                {
                    var lst = rep.ExportReport_014(sPkgName, sMoth, sYear, sOrgId, IsDissolve, log);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable ExportPhieuLuong(List<decimal> lstEmployee, decimal? orgID, decimal? isDissolve, decimal periodID, UserLog log)
        {
            using (PayrollRepository rep = new PayrollRepository())
            {
                try
                {
                    var lst = rep.ExportPhieuLuong(lstEmployee, orgID, isDissolve, periodID, log);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool ActionSendPayslip(List<decimal> lstEmployee, decimal? orgID, decimal? isDissolve, decimal periodID, UserLog log)
        {
            using (PayrollRepository rep = new PayrollRepository())
            {
                try
                {
                    return rep.ActionSendPayslip(lstEmployee, orgID, isDissolve, periodID, log);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        #endregion

        #region "LOG"
        public List<PA_ACTION_LOGDTO> GetActionLog(PA_ACTION_LOGDTO _filter, ref int Total = 0, int PageIndex = 0, int PageSize = int.MaxValue, string Sorts = "ACTION_DATE desc")
        {
            using (PayrollRepository rep = new PayrollRepository())
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
            using (PayrollRepository rep = new PayrollRepository())
            {
                try
                {
                    var lst = rep.DeleteActionLogsPA(lstDeleteIds);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        #endregion

        #region "Seniority"
        public bool CalSeniorityProcess(PASeniorityProcessDTO _filter, UserLog log)
        {
            using (PayrollRepository rep = new PayrollRepository())
            {
                try
                {
                    var lst = rep.CalSeniorityProcess(_filter, log);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public List<PASeniorityProcessDTO> GetSeniorityProcess(PASeniorityProcessDTO _filter, int PageIndex, int PageSize, ref int Total, UserLog log, string Sorts = "EMPLOYEE_CODE")
        {
            using (PayrollRepository rep = new PayrollRepository())
            {
                try
                {
                    var lst = rep.GetSeniorityProcess(_filter, PageIndex, PageSize, Total, log, Sorts);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable GetSeniorityProcessImport(PASeniorityProcessDTO _filter, UserLog log)
        {
            using (PayrollRepository rep = new PayrollRepository())
            {
                try
                {
                    var lst = rep.GetSeniorityProcessImport(_filter, log);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void SaveSeniorityProcessImport(DataTable dtData, decimal periodId, UserLog log)
        {
            using (PayrollRepository rep = new PayrollRepository())
            {
                try
                {
                    var lst = rep.SaveSeniorityProcessImport(dtData, periodId, log);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool CalSeniorityMonthly(PASeniorityMonthlyDTO _filter, UserLog log)
        {
            using (PayrollRepository rep = new PayrollRepository())
            {
                try
                {
                    var lst = rep.CalSeniorityMonthly(_filter, log);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        public List<PASeniorityMonthlyDTO> GetSeniorityMonthly(PASeniorityMonthlyDTO _filter, int PageIndex, int PageSize, ref int Total, UserLog log, string Sorts = "EMPLOYEE_CODE")
        {
            using (PayrollRepository rep = new PayrollRepository())
            {
                try
                {
                    var lst = rep.GetSeniorityMonthly(_filter, PageIndex, PageSize, Total, log, Sorts);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        #endregion



        #region "MailRemark"
        public List<PAMailRemarkDTO> GetMailRemark(PAMailRemarkDTO _filter, int PageIndex, int PageSize, ref int Total, UserLog log, string Sorts = "CREATED_DATE desc")
        {
            try
            {
                PayrollRepository rep = new PayrollRepository();
                return rep.GetMailRemark(_filter, PageIndex, PageSize, Total, log, Sorts);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool InsertMailRemark(PAMailRemarkDTO objMailRemark, UserLog log, ref decimal gID)
        {
            try
            {
                PayrollRepository rep = new PayrollRepository();
                return rep.InsertMailRemark(objMailRemark, log, gID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ModifyMailRemark(PAMailRemarkDTO objMailRemark, UserLog log, ref decimal gID)
        {
            try
            {
                PayrollRepository rep = new PayrollRepository();
                return rep.ModifyMailRemark(objMailRemark, log, gID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteMailRemark(List<decimal> lstID)
        {
            try
            {
                PayrollRepository rep = new PayrollRepository();
                return rep.DeleteMailRemark(lstID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
