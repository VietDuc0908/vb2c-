using System;
using PayrollBusiness.ServiceContracts;
using PayrollDAL;
using Framework.Data;

namespace PayrollBusiness.ServiceImplementations
{
    public partial class PayrollBusiness : ServiceContracts.IPayrollBusiness
    {
        #region "SalaryRank"
        public List<SalaryRankDTO> GetSalaryRank(SalaryRankDTO _filter, int PageIndex, int PageSize, ref int Total, string Sorts = "CREATED_DATE desc")
        {
            try
            {
                var lst = PayrollRepositoryStatic.Instance.GetSalaryRank(_filter, PageIndex, PageSize, Total, Sorts);
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool InsertSalaryRank(SalaryRankDTO objSalaryRank, UserLog log, ref decimal gID)
        {
            try
            {
                return PayrollRepositoryStatic.Instance.InsertSalaryRank(objSalaryRank, log, gID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ModifySalaryRank(SalaryRankDTO objSalaryRank, UserLog log, ref decimal gID)
        {
            try
            {
                return PayrollRepositoryStatic.Instance.ModifySalaryRank(objSalaryRank, log, gID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ValidateSalaryRank(SalaryRankDTO objSalaryRank)
        {
            try
            {
                return PayrollRepositoryStatic.Instance.ValidateSalaryRank(objSalaryRank);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ActiveSalaryRank(List<decimal> lstID, UserLog log, string bActive)
        {
            try
            {
                PayrollRepository rep = new PayrollRepository();
                return rep.ActiveSalaryRank(lstID, log, bActive);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteSalaryRank(SalaryRankDTO[] objSalaryRank)
        {
            try
            {
                return PayrollRepositoryStatic.Instance.DeleteSalaryRank(objSalaryRank);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
