using System;
using PayrollBusiness.ServiceContracts;
using PayrollDAL;
using Framework.Data;

namespace PayrollBusiness.ServiceImplementations
{
    public partial class PayrollBusiness : ServiceContracts.IPayrollBusiness
    {
        #region "Taxation List"
        public List<PATaxationDTO> GetTaxation(PATaxationDTO _filter, int PageIndex, int PageSize, ref int Total, string Sorts = "CREATED_DATE desc")
        {
            try
            {
                PayrollRepository rep = new PayrollRepository();
                return rep.GetTaxation(_filter, PageIndex, PageSize, Total, Sorts);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool InsertTaxation(PATaxationDTO objTaxation, UserLog log, ref decimal gID)
        {
            try
            {
                PayrollRepository rep = new PayrollRepository();
                return rep.InsertTaxation(objTaxation, log, gID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ModifyTaxation(PATaxationDTO objTaxation, UserLog log, ref decimal gID)
        {
            try
            {
                PayrollRepository rep = new PayrollRepository();
                return rep.ModifyTaxation(objTaxation, log, gID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ActiveTaxation(List<decimal> lstID, UserLog log, string bActive)
        {
            try
            {
                PayrollRepository rep = new PayrollRepository();
                return rep.ActiveTaxation(lstID, log, bActive);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteTaxation(List<decimal> lstID)
        {
            try
            {
                PayrollRepository rep = new PayrollRepository();
                return rep.DeleteTaxation(lstID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region "Payment list"
        public List<PAPaymentListDTO> GetPaymentList(int PageIndex, int PageSize, ref int Total, string Sorts = "CREATED_DATE desc")
        {
            try
            {
                PayrollRepository rep = new PayrollRepository();
                return rep.GetPaymentList(PageIndex, PageSize, Total, Sorts);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<PAPaymentListDTO> GetPaymentListAll(string Sorts = "CREATED_DATE desc")
        {
            try
            {
                PayrollRepository rep = new PayrollRepository();
                return rep.GetPaymentListAll(Sorts);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool InsertPaymentList(PAPaymentListDTO objPaymentList, UserLog log, ref decimal gID)
        {
            try
            {
                PayrollRepository rep = new PayrollRepository();
                return rep.InsertPaymentList(objPaymentList, log, gID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ModifyPaymentList(PAPaymentListDTO objPaymentList, UserLog log, ref decimal gID)
        {
            try
            {
                PayrollRepository rep = new PayrollRepository();
                return rep.ModifyPaymentList(objPaymentList, log, gID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ActivePaymentList(List<decimal> lstID, UserLog log, string bActive)
        {
            try
            {
                PayrollRepository rep = new PayrollRepository();
                return rep.ActivePaymentList(lstID, log, bActive);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeletePaymentList(List<decimal> lstID)
        {
            try
            {
                PayrollRepository rep = new PayrollRepository();
                return rep.DeletePaymentList(lstID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region "ObjectSalary"
        public List<PAObjectSalaryDTO> GetObjectSalary(int PageIndex, int PageSize, ref int Total, string Sorts = "CREATED_DATE desc")
        {
            try
            {
                PayrollRepository rep = new PayrollRepository();
                return rep.GetObjectSalary(PageIndex, PageSize, Total, Sorts);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<PAObjectSalaryDTO> GetObjectSalaryAll(string Sorts = "CREATED_DATE desc")
        {
            try
            {
                PayrollRepository rep = new PayrollRepository();
                return rep.GetObjectSalaryAll(Sorts);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool InsertObjectSalary(PAObjectSalaryDTO objObjectSalary, UserLog log, ref decimal gID)
        {
            try
            {
                PayrollRepository rep = new PayrollRepository();
                return rep.InsertObjectSalary(objObjectSalary, log, gID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ValidateObjectSalary(PAObjectSalaryDTO objObjectSalary)
        {
            try
            {
                PayrollRepository rep = new PayrollRepository();
                return rep.ValidateObjectSalary(objObjectSalary);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ModifyObjectSalary(PAObjectSalaryDTO objObjectSalary, UserLog log, ref decimal gID)
        {
            try
            {
                PayrollRepository rep = new PayrollRepository();
                return rep.ModifyObjectSalary(objObjectSalary, log, gID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ActiveObjectSalary(List<decimal> lstID, UserLog log, string bActive)
        {
            try
            {
                PayrollRepository rep = new PayrollRepository();
                return rep.ActiveObjectSalary(lstID, log, bActive);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteObjectSalary(List<decimal> lstID)
        {
            try
            {
                PayrollRepository rep = new PayrollRepository();
                return rep.DeleteObjectSalary(lstID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region "Period List"
        public List<ATPeriodDTO> GetPeriodList(int PageIndex, int PageSize, ref int Total, string Sorts = "START_DATE desc")
        {
            try
            {
                var lst = PayrollRepositoryStatic.Instance.GetPeriodList(PageIndex, PageSize, Total, Sorts);
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ATPeriodDTO> GetPeriodbyYear(decimal year)
        {
            try
            {
                var lst = PayrollRepositoryStatic.Instance.GetPeriodbyYear(year);
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<decimal> GetOrgByPeriodID(decimal periodID)
        {
            try
            {
                var lst = PayrollRepositoryStatic.Instance.GetOrgByPeriodID(periodID);
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool InsertPeriod(ATPeriodDTO objPeriod, List<AT_ORG_PERIOD> objOrgPeriod, UserLog log, ref decimal gID)
        {
            try
            {
                return PayrollRepositoryStatic.Instance.InsertPeriod(objPeriod, objOrgPeriod, log, gID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ValidateATPeriod(ATPeriodDTO objPeriod)
        {
            try
            {
                return PayrollRepositoryStatic.Instance.ValidateATPeriod(objPeriod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ValidateATPeriodDay(ATPeriodDTO objPeriod)
        {
            try
            {
                return PayrollRepositoryStatic.Instance.ValidateATPeriodDay(objPeriod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ModifyPeriod(ATPeriodDTO objPeriod, List<AT_ORG_PERIOD> objOrgPeriod, UserLog log, ref decimal gID)
        {
            try
            {
                return PayrollRepositoryStatic.Instance.ModifyPeriod(objPeriod, objOrgPeriod, log, gID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeletePeriod(ATPeriodDTO lstPeriod)
        {
            try
            {
                return PayrollRepositoryStatic.Instance.DeletePeriod(lstPeriod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region "List Salary"
        public List<PAFomulerGroup> GetAllFomulerGroup()
        {
            try
            {
                return PayrollRepositoryStatic.Instance.GetAllFomulerGroup();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool InsertFomulerGroup(PAFomulerGroup objPeriod, UserLog log, ref decimal gID)
        {
            try
            {
                return PayrollRepositoryStatic.Instance.InsertFomulerGroup(objPeriod, log, gID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool ModifyFomulerGroup(PAFomulerGroup objPeriod, UserLog log, ref decimal gID)
        {
            try
            {
                return PayrollRepositoryStatic.Instance.ModifyFomulerGroup(objPeriod, log, gID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool DeleteFomulerGroup(PAFomulerGroup lstDelete)
        {
            try
            {
                return PayrollRepositoryStatic.Instance.DeleteFomulerGroup(lstDelete);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<PAFomuler> GetListAllSalary(decimal gID)
        {
            try
            {
                return PayrollRepositoryStatic.Instance.GetListAllSalary(gID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetListInputColumn(decimal gID)
        {
            try
            {
                return PayrollRepositoryStatic.Instance.GetListInputColumn(gID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<OT_OTHERLIST_DTO> GetListCalculation()
        {
            try
            {
                return PayrollRepositoryStatic.Instance.GetListCalculation();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool CheckFomuler(string sCol, string sFormuler, decimal objID)
        {
            try
            {
                return PayrollRepositoryStatic.Instance.CheckFomuler(sCol, sFormuler, objID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool SaveFomuler(PAFomuler objData, UserLog log, ref decimal gID)
        {
            try
            {
                return PayrollRepositoryStatic.Instance.SaveFomuler(objData, log, gID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool ActiveFolmulerGroup(List<decimal> lstID, UserLog log, decimal bActive)
        {
            try
            {
                return PayrollRepositoryStatic.Instance.ActiveFolmulerGroup(lstID, log, bActive);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region "Salary list"
        public List<PAListSalariesDTO> GetListSalaries(PAListSalariesDTO _filter, int TypePaymentId, int PageIndex = 0, int PageSize = int.MaxValue, ref int Total = 0, string Sorts = "CREATED_DATE desc")
        {
            try
            {
                PayrollRepository rep = new PayrollRepository();
                return rep.GetListSalaries(_filter, TypePaymentId, PageIndex, PageSize, Total, Sorts);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool CheckColName(string COl_NAME, decimal TypeID)
        {
            try
            {
                PayrollRepository rep = new PayrollRepository();
                return rep.CheckColName(COl_NAME, TypeID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool InsertListSalaries(PAListSalariesDTO objListSalaries, UserLog log, ref decimal gID)
        {
            try
            {
                PayrollRepository rep = new PayrollRepository();
                return rep.InsertListSalaries(objListSalaries, log, gID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ModifyListSalaries(PAListSalariesDTO objListSalaries, UserLog log, ref decimal gID)
        {
            try
            {
                PayrollRepository rep = new PayrollRepository();
                return rep.ModifyListSalaries(objListSalaries, log, gID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ActiveListSalaries(List<decimal> lstID, UserLog log, string bActive)
        {
            try
            {
                PayrollRepository rep = new PayrollRepository();
                return rep.ActiveListSalaries(lstID, log, bActive);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteListSalaries(List<decimal> lstID)
        {
            try
            {
                PayrollRepository rep = new PayrollRepository();
                return rep.DeleteListSalaries(lstID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region "lunch list : Đơn giá tiền ăn trưa"
        public List<ATPriceLunchDTO> GetPriceLunchList(int PageIndex, int PageSize, ref int Total, string Sorts = "EFFECT_DATE desc", UserLog log = null/* TODO Change to default(_) if this is not a reference type */)
        {
            try
            {
                var lst = PayrollRepositoryStatic.Instance.GetPriceLunchList(PageIndex, PageSize, Total, Sorts, log);
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ATPriceLunchDTO> GetPriceLunch(decimal year)
        {
            try
            {
                var lst = PayrollRepositoryStatic.Instance.GetPriceLunch(year);
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool InsertPriceLunch(ATPriceLunchDTO objPeriod, List<PA_ORG_LUNCH> objOrgPeriod, UserLog log, ref decimal gID)
        {
            try
            {
                return PayrollRepositoryStatic.Instance.InsertPriceLunch(objPeriod, objOrgPeriod, log, gID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ValidateATPriceLunch(ATPriceLunchDTO _validate)
        {
            try
            {
                return PayrollRepositoryStatic.Instance.ValidateATPriceLunch(_validate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ValidateATPriceLunchOrg(ATPriceLunchDTO _validate)
        {
            try
            {
                return PayrollRepositoryStatic.Instance.ValidateATPriceLunchOrg(_validate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool ModifyPriceLunch(ATPriceLunchDTO objPeriod, List<PA_ORG_LUNCH> objOrgPeriod, UserLog log, ref decimal gID)
        {
            try
            {
                return PayrollRepositoryStatic.Instance.ModifyPriceLunch(objPeriod, objOrgPeriod, log, gID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeletePriceLunch(ATPriceLunchDTO lstPeriod)
        {
            try
            {
                return PayrollRepositoryStatic.Instance.DeletePriceLunch(lstPeriod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<decimal> GetOrgByLunchID(decimal lunchID)
        {
            try
            {
                return PayrollRepositoryStatic.Instance.GetOrgByLunchID(lunchID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        #region "tien an trua theo nhan vien"
        public List<PA_EMP_LUNCHDTO> GetPA_EMP_LUNCH(PA_EMP_LUNCHDTO _filter, PA_ParamDTO _param, ref int Total = 0, int PageIndex = 0, int PageSize = int.MaxValue, string Sorts = "EFFECT_DATE desc", UserLog log = null/* TODO Change to default(_) if this is not a reference type */)
        {
            try
            {
                var lst = PayrollRepositoryStatic.Instance.GetPA_EMP_LUNCH(_filter, _param, Total, PageIndex, PageSize, Sorts, log);
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PA_EMP_LUNCHDTO GetPA_EMP_LUNCHbyID(PA_EMP_LUNCHDTO _filter)
        {
            try
            {
                var lst = PayrollRepositoryStatic.Instance.GetPA_EMP_LUNCHbyID(_filter);
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool InsertPA_EMP_LUNCH(List<PA_EMP_LUNCHDTO> lst, PA_EMP_LUNCHDTO objEmp, UserLog log, ref decimal gID)
        {
            try
            {
                var lstEmp = PayrollRepositoryStatic.Instance.InsertPA_EMP_LUNCH(lst, objEmp, log, gID);
                return lstEmp;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool ModifyPA_EMP_LUNCH(PA_EMP_LUNCHDTO obj, UserLog log, ref decimal gID)
        {
            try
            {
                var lst = PayrollRepositoryStatic.Instance.ModifyPA_EMP_LUNCH(obj, log, gID);
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool DeletePA_EMP_LUNCH(List<decimal> lstID)
        {
            try
            {
                var lst = PayrollRepositoryStatic.Instance.DeletePA_EMP_LUNCH(lstID);
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
