using System;
using PayrollBusiness.ServiceContracts;
using PayrollDAL;
using Framework.Data;

namespace PayrollBusiness.ServiceImplementations
{
    public partial class PayrollBusiness : ServiceContracts.IPayrollBusiness
    {
        #region "Hold Salary"
        public List<PAHoldSalaryDTO> GetHoldSalaryList(int PeriodId, int OrgId, int IsDissolve, UserLog log, int PageIndex, int PageSize, ref int Total, string Sorts = "CREATED_DATE desc")
        {
            try
            {
                PayrollRepository rep = new PayrollRepository();
                return rep.GetHoldSalaryList(PeriodId, OrgId, IsDissolve, log, PageIndex, PageSize, Total, Sorts);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool InsertHoldSalary(List<PAHoldSalaryDTO> objPeriod, UserLog log)
        {
            try
            {
                PayrollRepository rep = new PayrollRepository();
                return rep.InsertHoldSalary(objPeriod, log);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteHoldSalary(List<decimal> lstDelete)
        {
            try
            {
                PayrollRepository rep = new PayrollRepository();
                return rep.DeleteHoldSalary(lstDelete);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
