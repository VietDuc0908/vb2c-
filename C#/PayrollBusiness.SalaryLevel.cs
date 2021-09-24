using System;
using PayrollBusiness.ServiceContracts;
using PayrollDAL;
using Framework.Data;

namespace PayrollBusiness.ServiceImplementations
{
    public partial class PayrollBusiness : ServiceContracts.IPayrollBusiness
    {
        #region "SalaryLevel"
        public List<SalaryLevelDTO> GetSalaryLevel(SalaryLevelDTO _filter, int PageIndex, int PageSize, ref int Total, string Sorts = "CREATED_DATE desc")
        {
            try
            {
                var lst = PayrollRepositoryStatic.Instance.GetSalaryLevel(_filter, PageIndex, PageSize, Total, Sorts);
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool InsertSalaryLevel(SalaryLevelDTO objSalaryLevel, UserLog log, ref decimal gID)
        {
            try
            {
                return PayrollRepositoryStatic.Instance.InsertSalaryLevel(objSalaryLevel, log, gID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ValidateSalaryLevel(SalaryLevelDTO objSalaryLevel)
        {
            try
            {
                return PayrollRepositoryStatic.Instance.ValidateSalaryLevel(objSalaryLevel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ModifySalaryLevel(SalaryLevelDTO objSalaryLevel, UserLog log, ref decimal gID)
        {
            try
            {
                return PayrollRepositoryStatic.Instance.ModifySalaryLevel(objSalaryLevel, log, gID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ActiveSalaryLevel(List<decimal> lstID, UserLog log, string bActive)
        {
            try
            {
                PayrollRepository rep = new PayrollRepository();
                return rep.ActiveSalaryLevel(lstID, log, bActive);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool DeleteSalaryLevel(SalaryLevelDTO[] objSalaryLevel)
        {
            try
            {
                return PayrollRepositoryStatic.Instance.DeleteSalaryLevel(objSalaryLevel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
