using System;
using PayrollBusiness.ServiceContracts;
using PayrollDAL;
using Framework.Data;

namespace PayrollBusiness.ServiceImplementations
{
    public partial class PayrollBusiness : ServiceContracts.IPayrollBusiness
    {
        #region "SalaryGroup"
        public List<SalaryGroupDTO> GetSalaryGroup(SalaryGroupDTO _filter, int PageIndex, int PageSize, ref int Total, string Sorts = "CREATED_DATE desc")
        {
            try
            {
                var lst = PayrollRepositoryStatic.Instance.GetSalaryGroup(_filter, PageIndex, PageSize, Total, Sorts);
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SalaryGroupDTO GetEffectSalaryGroup()
        {
            try
            {
                return PayrollRepositoryStatic.Instance.GetEffectSalaryGroup();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool InsertSalaryGroup(SalaryGroupDTO objSalaryGroup, UserLog log, ref decimal gID)
        {
            try
            {
                return PayrollRepositoryStatic.Instance.InsertSalaryGroup(objSalaryGroup, log, gID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ValidateSalaryGroup(SalaryGroupDTO objSalaryGroup)
        {
            try
            {
                return PayrollRepositoryStatic.Instance.ValidateSalaryGroup(objSalaryGroup);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ModifySalaryGroup(SalaryGroupDTO objSalaryGroup, UserLog log, ref decimal gID)
        {
            try
            {
                return PayrollRepositoryStatic.Instance.ModifySalaryGroup(objSalaryGroup, log, gID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteSalaryGroup(List<decimal> lstID)
        {
            try
            {
                return PayrollRepositoryStatic.Instance.DeleteSalaryGroup(lstID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
