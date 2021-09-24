using System.Linq.Expressions
using LinqKit.Extensions
using System.Data.Common
using Framework.Data
using Framework.Data.System.Linq.Dynamic
using System.Data.Entity
using System.Text.RegularExpressions
using System.Data.Objects
using System.Reflection


partial public class PayrollRepository
{
    #region "Taxation "
    public List<PATaxationDTO> GetTaxation(PATaxationDTO _filter, int PageIndex, int PageSize, ref int Total, string Sorts = "CREATED_DATE desc")
        {
            try
            {
                var query = from p in Context.PA_TAXATION
                            select p;
                var lst = query.Select(p => new PATaxationDTO() { ID = p.ID, VALUE_FROM = p.VALUE_FROM, VALUE_TO = p.VALUE_TO, RATE = p.RATE, EXCEPT_FAST = p.EXCEPT_FAST, FROM_DATE = p.FROM_DATE, TO_DATE = p.TO_DATE, SDESC = p.SDESC, ACTFLG = p.ACTFLG == "A" ? "Áp dụng" : "Ngừng Áp dụng", CREATED_DATE = p.CREATED_DATE });

                if (_filter.VALUE_FROM.HasValue)
                    lst = lst.Where(f => f.VALUE_FROM == _filter.VALUE_FROM);
                if (_filter.VALUE_TO.HasValue)
                    lst = lst.Where(f => f.VALUE_TO == _filter.VALUE_TO);
                if (_filter.EXCEPT_FAST.HasValue)
                    lst = lst.Where(f => f.EXCEPT_FAST == _filter.EXCEPT_FAST);
                if (_filter.RATE.HasValue)
                    lst = lst.Where(f => f.RATE == _filter.RATE);
                if (_filter.FROM_DATE.HasValue)
                    lst = lst.Where(f => f.FROM_DATE == _filter.FROM_DATE);
                if (_filter.TO_DATE.HasValue)
                    lst = lst.Where(f => f.TO_DATE == _filter.TO_DATE);
                if (!string.IsNullOrEmpty(_filter.SDESC))
                    lst = lst.Where(f => f.SDESC.ToLower().Contains(_filter.SDESC.ToLower()));
                if (!string.IsNullOrEmpty(_filter.ACTFLG))
                    lst = lst.Where(f => f.ACTFLG.ToLower().Contains(_filter.ACTFLG.ToLower()));

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

        public bool InsertTaxation(PATaxationDTO objTitle, UserLog log, ref decimal gID)
        {
            PA_TAXATION objTitleData = new PA_TAXATION();
            int iCount = 0;
            try
            {
                objTitleData.ID = Utilities.GetNextSequence(Context, Context.PA_TAXATION.EntitySet.Name);
                objTitleData.VALUE_FROM = objTitle.VALUE_FROM;
                objTitleData.VALUE_TO = objTitle.VALUE_TO;
                objTitleData.RATE = objTitle.RATE;
                objTitleData.EXCEPT_FAST = objTitle.EXCEPT_FAST;
                objTitleData.FROM_DATE = objTitle.FROM_DATE;
                objTitleData.TO_DATE = objTitle.TO_DATE;
                objTitleData.ACTFLG = "A";
                objTitleData.SDESC = objTitle.SDESC;
                objTitleData.CREATED_BY = objTitle.CREATED_BY;
                objTitleData.CREATED_DATE = objTitle.CREATED_DATE;
                objTitleData.CREATED_LOG = objTitle.CREATED_LOG;
                objTitleData.MODIFIED_BY = objTitle.MODIFIED_BY;
                objTitleData.MODIFIED_DATE = objTitle.MODIFIED_DATE;
                objTitleData.MODIFIED_LOG = objTitle.MODIFIED_LOG;
                Context.PA_TAXATION.AddObject(objTitleData);
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

        public bool ModifyTaxation(PATaxationDTO objTitle, UserLog log, ref decimal gID)
        {
            PA_TAXATION objTitleData = new PA_TAXATION() { ID = objTitle.ID };
            try
            {
                objTitleData = (from p in Context.PA_TAXATION
                                where p.ID == objTitleData.ID
                                select p).SingleOrDefault;
                objTitleData.ID = objTitle.ID;
                objTitleData.VALUE_FROM = objTitle.VALUE_FROM;
                objTitleData.VALUE_TO = objTitle.VALUE_TO;
                objTitleData.RATE = objTitle.RATE;
                objTitleData.EXCEPT_FAST = objTitle.EXCEPT_FAST;
                objTitleData.FROM_DATE = objTitle.FROM_DATE;
                objTitleData.TO_DATE = objTitle.TO_DATE;
                objTitleData.SDESC = objTitle.SDESC;
                objTitleData.CREATED_BY = objTitle.CREATED_BY;
                objTitleData.CREATED_DATE = objTitle.CREATED_DATE;
                objTitleData.CREATED_LOG = objTitle.CREATED_LOG;
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

        public bool ActiveTaxation(List<decimal> lstID, UserLog log, string bActive)
        {
            List<PA_TAXATION> lstData;
            try
            {
                lstData = (from p in Context.PA_TAXATION
                           where lstID.Contains(p.ID)
                           select p).ToList;
                for (var index = 0; index <= lstData.Count - 1; index++)
                    lstData[index].ACTFLG = bActive;
                Context.SaveChanges(log);
                return true;
            }
            catch (Exception ex)
            {
                WriteExceptionLog(ex, MethodBase.GetCurrentMethod().Name, "iPayroll");
                throw ex;
            }
        }

        public bool DeleteTaxation(List<decimal> lstID)
        {
            List<PA_TAXATION> lstTaxationData;
            try
            {
                lstTaxationData = (from p in Context.PA_TAXATION
                                   where lstID.Contains(p.ID)
                                   select p).ToList;
                for (var index = 0; index <= lstTaxationData.Count - 1; index++)
                    Context.PA_TAXATION.DeleteObject(lstTaxationData[index]);
                Context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                WriteExceptionLog(ex, MethodBase.GetCurrentMethod().Name, "iPayroll");
                // Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteTaxation")
                throw ex;
            }
        }
    #endregion

    #region Payment_list
    public List<PAPaymentListDTO> GetPaymentList(int PageIndex, int PageSize, ref int Total, string Sorts = "CREATED_DATE desc")
    {
        try
        {
            var query = from p in Context.PA_PAYMENT_LIST
                        join o in Context.PA_OBJECT_SALARY on o.ID equals p.OBJ_PAYMENT_ID
                        select p;
            var lst = query.Select(s => new PAPaymentListDTO()
            {
                ID = s.p.ID,
                CODE = s.p.CODE,
                NAME = s.p.NAME,
                OBJ_PAYMENT_ID = s.p.OBJ_PAYMENT_ID,
                OBJ_PAYMENT_NAME_VN = s.o.NAME_EN,
                OBJ_PAYMENT_NAME_EN = s.o.NAME_VN,
                EFFECTIVE_DATE = s.p.EFFECTIVE_DATE,
                VALUE = s.p.VALUE,
                SDESC = s.p.SDESC,
                ACTFLG = s.p.ACTFLG == "A" ? "Áp dụng" : "Ngưng áp dụng",
                CREATED_DATE = s.p.CREATED_DATE
            });
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

    public List<PAPaymentListDTO> GetPaymentListAll(string Sorts = "CREATED_DATE desc")
    {
        try
        {
            var query = from p in Context.PA_PAYMENT_LIST
                        select p;
            var lst = query.Select(p => new PAPaymentListDTO()
            {
                ID = p.ID,
                OBJ_PAYMENT_ID = p.OBJ_PAYMENT_ID,
                CODE = p.CODE,
                NAME = p.NAME,
                EFFECTIVE_DATE = p.EFFECTIVE_DATE,
                VALUE = p.VALUE,
                SDESC = p.SDESC,
                ACTFLG = p.ACTFLG
            });
            lst = lst.OrderBy(Sorts);
            return lst.ToList;
        }
        catch (Exception ex)
        {
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod().Name, "iPayroll");
            throw ex;
        }
    }

    public bool InsertPaymentList(PAPaymentListDTO objTitle, UserLog log, ref decimal gID)
    {
        PA_PAYMENT_LIST objTitleData = new PA_PAYMENT_LIST();
        int iCount = 0;
        try
        {
            objTitleData.ID = Utilities.GetNextSequence(Context, Context.PA_PAYMENT_LIST.EntitySet.Name);
            objTitleData.OBJ_PAYMENT_ID = objTitle.OBJ_PAYMENT_ID;
            objTitleData.CODE = objTitle.CODE;
            objTitleData.NAME = objTitle.NAME;
            objTitleData.EFFECTIVE_DATE = objTitle.EFFECTIVE_DATE;
            objTitleData.VALUE = objTitle.VALUE;
            objTitleData.SDESC = objTitle.SDESC;
            objTitleData.ACTFLG = objTitle.ACTFLG;
            objTitleData.CREATED_BY = objTitle.CREATED_BY;
            objTitleData.CREATED_DATE = objTitle.CREATED_DATE;
            objTitleData.CREATED_LOG = objTitle.CREATED_LOG;
            objTitleData.MODIFIED_BY = objTitle.MODIFIED_BY;
            objTitleData.MODIFIED_DATE = objTitle.MODIFIED_DATE;
            objTitleData.MODIFIED_LOG = objTitle.MODIFIED_LOG;
            Context.PA_PAYMENT_LIST.AddObject(objTitleData);
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

    public bool ModifyPaymentList(PAPaymentListDTO objTitle, UserLog log, ref decimal gID)
    {
        PA_PAYMENT_LIST objTitleData = new PA_PAYMENT_LIST() { ID = objTitle.ID };
        try
        {
            objTitleData = (from p in Context.PA_PAYMENT_LIST
                            where p.ID == objTitleData.ID
                            select p).SingleOrDefault;
            objTitleData.ID = objTitle.ID;
            objTitleData.OBJ_PAYMENT_ID = objTitle.OBJ_PAYMENT_ID;
            objTitleData.EFFECTIVE_DATE = objTitle.EFFECTIVE_DATE;
            objTitleData.VALUE = objTitle.VALUE;
            objTitleData.SDESC = objTitle.SDESC;
            objTitleData.CREATED_BY = objTitle.CREATED_BY;
            objTitleData.CREATED_DATE = objTitle.CREATED_DATE;
            objTitleData.CREATED_LOG = objTitle.CREATED_LOG;
            objTitleData.MODIFIED_BY = objTitle.MODIFIED_BY;
            objTitleData.MODIFIED_DATE = objTitle.MODIFIED_DATE;
            objTitleData.MODIFIED_LOG = objTitle.MODIFIED_LOG;
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

    public bool ActivePaymentList(List<decimal> lstID, UserLog log, string bActive)
    {
        List<PA_PAYMENT_LIST> lstData;
        try
        {
            lstData = (from p in Context.PA_PAYMENT_LIST
                       where lstID.Contains(p.ID)
                       select p).ToList;
            for (var index = 0; index <= lstData.Count - 1; index++)
                lstData[index].ACTFLG = bActive;
            Context.SaveChanges(log);
            return true;
        }
        catch (Exception ex)
        {
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod().Name, "iPayroll");
            throw ex;
        }
    }

    public bool DeletePaymentList(List<decimal> lstID)
    {
        List<PA_PAYMENT_LIST> lstPaymentListData;
        try
        {
            lstPaymentListData = (from p in Context.PA_PAYMENT_LIST
                                  where lstID.Contains(p.ID)
                                  select p).ToList;
            for (var index = 0; index <= lstPaymentListData.Count - 1; index++)
                Context.PA_PAYMENT_LIST.DeleteObject(lstPaymentListData[index]);
            Context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod().Name, "iPayroll");
            // Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteCostCenter")
            throw ex;
        }
    }
    #endregion

    #region Object Salary
    public List<PAObjectSalaryDTO> GetObjectSalary(int PageIndex, int PageSize, ref int Total, string Sorts = "CREATED_DATE desc")
    {
        try
        {
            var query = from p in Context.PA_OBJECT_SALARY
                        select p;
            var lst = query.Select(p => new PAObjectSalaryDTO()
            {
                ID = p.ID,
                CODE = p.CODE,
                NAME_VN = p.NAME_VN,
                NAME_EN = p.NAME_EN,
                EFFECTIVE_DATE = p.EFFECTIVE_DATE,
                ACTFLG = p.ACTFLG,
                SDESC = p.SDESC,
                CREATED_BY = p.CREATED_BY,
                CREATED_DATE = p.CREATED_DATE,
                CREATED_LOG = p.CREATED_LOG,
                MODIFIED_BY = p.MODIFIED_BY,
                MODIFIED_DATE = p.MODIFIED_DATE,
                MODIFIED_LOG = p.MODIFIED_LOG
            });
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

    public List<PAObjectSalaryDTO> GetObjectSalaryAll(string Sorts = "CREATED_DATE desc")
    {
        try
        {
            var query = from p in Context.PA_OBJECT_SALARY
                        select p;
            var lst = query.Select(p => new PAObjectSalaryDTO()
            {
                ID = p.ID,
                CODE = p.CODE,
                NAME_VN = p.NAME_VN,
                NAME_EN = p.NAME_EN,
                EFFECTIVE_DATE = p.EFFECTIVE_DATE,
                SDESC = p.SDESC,
                CREATED_BY = p.CREATED_BY,
                CREATED_DATE = p.CREATED_DATE,
                CREATED_LOG = p.CREATED_LOG,
                MODIFIED_BY = p.MODIFIED_BY,
                MODIFIED_DATE = p.MODIFIED_DATE,
                MODIFIED_LOG = p.MODIFIED_LOG
            });
            lst = lst.OrderBy(Sorts);
            return lst.ToList;
        }
        catch (Exception ex)
        {
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod().Name, "iPayroll");
            throw ex;
        }
    }

    public bool InsertObjectSalary(PAObjectSalaryDTO objTitle, UserLog log, ref decimal gID)
    {
        PA_OBJECT_SALARY objTitleData = new PA_OBJECT_SALARY();
        int iCount = 0;
        try
        {
            objTitleData.ID = Utilities.GetNextSequence(Context, Context.PA_OBJECT_SALARY.EntitySet.Name);
            objTitleData.CODE = objTitle.CODE;
            objTitleData.NAME_VN = objTitle.NAME_VN;
            objTitleData.NAME_EN = objTitle.NAME_EN;
            objTitleData.EFFECTIVE_DATE = objTitle.EFFECTIVE_DATE;
            objTitleData.SDESC = objTitle.SDESC;
            objTitleData.CREATED_BY = objTitle.CREATED_BY;
            objTitleData.CREATED_DATE = objTitle.CREATED_DATE;
            objTitleData.CREATED_LOG = objTitle.CREATED_LOG;
            objTitleData.MODIFIED_BY = objTitle.MODIFIED_BY;
            objTitleData.MODIFIED_DATE = objTitle.MODIFIED_DATE;
            objTitleData.MODIFIED_LOG = objTitle.MODIFIED_LOG;
            Context.PA_OBJECT_SALARY.AddObject(objTitleData);
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

    public void ValidateObjectSalary(PAObjectSalaryDTO _validate)
    {
        var query;
        try
        {
            if (_validate.CODE != null/* TODO Change to default(_) if this is not a reference type */ )
            {
                if (_validate.ID != 0)
                    query = (from p in Context.PA_OBJECT_SALARY
                             where p.CODE.ToUpper == _validate.CODE.ToUpper
                                                          & p.ID != _validate.ID
                             select p).FirstOrDefault;
                else
                    query = (from p in Context.PA_OBJECT_SALARY
                             where p.CODE.ToUpper == _validate.CODE.ToUpper
                             select p).FirstOrDefault;
                return (query == null);
            }
            return true;
        }
        catch (Exception ex)
        {
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod().Name, "iPayroll");
            throw ex;
        }
    }

    public bool ModifyObjectSalary(PAObjectSalaryDTO objTitle, UserLog log, ref decimal gID)
    {
        PA_OBJECT_SALARY objTitleData = new PA_OBJECT_SALARY() { ID = objTitle.ID };
        try
        {
            objTitleData = (from p in Context.PA_OBJECT_SALARY
                            where p.ID == objTitleData.ID
                            select p).SingleOrDefault;
            objTitleData.ID = objTitle.ID;
            objTitleData.CODE = objTitle.CODE;
            objTitleData.NAME_VN = objTitle.NAME_VN;
            objTitleData.NAME_EN = objTitle.NAME_EN;
            objTitleData.EFFECTIVE_DATE = objTitle.EFFECTIVE_DATE;
            objTitleData.SDESC = objTitle.SDESC;
            objTitleData.CREATED_BY = objTitle.CREATED_BY;
            objTitleData.CREATED_DATE = objTitle.CREATED_DATE;
            objTitleData.CREATED_LOG = objTitle.CREATED_LOG;
            objTitleData.MODIFIED_BY = objTitle.MODIFIED_BY;
            objTitleData.MODIFIED_DATE = objTitle.MODIFIED_DATE;
            objTitleData.MODIFIED_LOG = objTitle.MODIFIED_LOG;
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

    public bool ActiveObjectSalary(List<decimal> lstID, UserLog log, string bActive)
    {
        List<PA_OBJECT_SALARY> lstData;
        try
        {
            lstData = (from p in Context.PA_OBJECT_SALARY
                       where lstID.Contains(p.ID)
                       select p).ToList;
            for (var index = 0; index <= lstData.Count - 1; index++)
                lstData[index].ACTFLG = bActive;
            Context.SaveChanges(log);
            return true;
        }
        catch (Exception ex)
        {
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod().Name, "iPayroll");
            throw ex;
        }
    }

    public bool DeleteObjectSalary(List<decimal> lstID)
    {
        List<PA_OBJECT_SALARY> lstObjectSalaryData;
        try
        {
            lstObjectSalaryData = (from p in Context.PA_OBJECT_SALARY
                                   where lstID.Contains(p.ID)
                                   select p).ToList;
            for (var index = 0; index <= lstObjectSalaryData.Count - 1; index++)
                Context.PA_OBJECT_SALARY.DeleteObject(lstObjectSalaryData[index]);
            Context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod().Name, "iPayroll");
            // Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteCostCenter")
            throw ex;
        }
    }
    #endregion

    #region "Period list"
    public List<ATPeriodDTO> GetPeriodList(int PageIndex, int PageSize, ref int Total, string Sorts = "START_DATE desc")
        {
            try
            {
                var query = from p in Context.AT_PERIOD
                            select p;

                var lst = query.Select(p => new ATPeriodDTO() { ID = p.ID, YEAR = p.YEAR, MONTH = p.MONTH, PERIOD_NAME = p.PERIOD_NAME, START_DATE = p.START_DATE, END_DATE = p.END_DATE, CREATED_DATE = p.CREATED_DATE, CREATED_BY = p.CREATED_BY, PERIOD_STANDARD = p.PERIOD_STANDARD });


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

        public List<ATPeriodDTO> GetPeriodbyYear(decimal year)
        {
            try
            {
                var query = from p in Context.AT_PERIOD
                            where p.YEAR == year
                            orderby p.MONTH ascending, p.START_DATE ascending
                            select p;
                var Period = query.Select(p => new ATPeriodDTO() { ID = p.ID, YEAR = p.YEAR, MONTH = p.MONTH, PERIOD_NAME = p.PERIOD_NAME, START_DATE = p.START_DATE, END_DATE = p.END_DATE, CREATED_DATE = p.CREATED_DATE, CREATED_BY = p.CREATED_BY, PERIOD_STANDARD = p.PERIOD_STANDARD });


                return Period.ToList;
            }
            catch (Exception ex)
            {
                WriteExceptionLog(ex, MethodBase.GetCurrentMethod().Name, "iPayroll");
                throw ex;
            }
        }

        public List<decimal> GetOrgByPeriodID(decimal periodID)
        {
            try
            {
                var query = from p in Context.AT_ORG_PERIOD
                            where p.PERIOD_ID == periodID
                            select p.ORG_ID.Value;

                return query.ToList;
            }
            catch (Exception ex)
            {
                WriteExceptionLog(ex, MethodBase.GetCurrentMethod().Name, "iPayroll");
                throw ex;
            }
        }

        public bool InsertPeriod(ATPeriodDTO objPeriod, List<AT_ORG_PERIOD> objOrgPeriod, UserLog log, ref decimal gID)
        {
            int iCount = 0;
            AT_PERIOD objPeriodData = new AT_PERIOD();
            AT_ORG_PERIOD objOrgPeriodData;
            try
            {
                objPeriodData.ID = Utilities.GetNextSequence(Context, Context.AT_PERIOD.EntitySet.Name);
                objPeriodData.YEAR = objPeriod.YEAR;
                objPeriodData.MONTH = objPeriod.MONTH;
                objPeriodData.PERIOD_NAME = objPeriod.PERIOD_NAME;
                objPeriodData.START_DATE = objPeriod.START_DATE;
                objPeriodData.END_DATE = objPeriod.END_DATE;
                objPeriodData.PERIOD_STANDARD = objPeriod.PERIOD_STANDARD;
                Context.AT_PERIOD.AddObject(objPeriodData);
                // Context.SaveChanges(log)
                if (objPeriodData.ID > 0)
                {
                    foreach (AT_ORG_PERIOD obj in objOrgPeriod)
                    {
                        objOrgPeriodData = new AT_ORG_PERIOD();
                        objOrgPeriodData.ID = Utilities.GetNextSequence(Context, Context.AT_ORG_PERIOD.EntitySet.Name);
                        objOrgPeriodData.ORG_ID = obj.ORG_ID;
                        objOrgPeriodData.PERIOD_ID = objPeriodData.ID;
                        objOrgPeriodData.STATUSCOLEX = 1;
                        objOrgPeriodData.STATUSPAROX = 1;
                        Context.AT_ORG_PERIOD.AddObject(objOrgPeriodData);
                    }
                }
                Context.SaveChanges(log);
                gID = objPeriodData.ID;
                return true;
            }
            catch (Exception ex)
            {
                WriteExceptionLog(ex, MethodBase.GetCurrentMethod().Name, "iPayroll");
                throw ex;
            }
        }

        public bool ValidateATPeriod(ATPeriodDTO _validate)
        {
            try
            {
                if (_validate.ID != 0)
                {
                    if (_validate.ID != 0)
                    {
                        var query = (from p in Context.AT_ORG_PERIOD
                                     where p.PERIOD_ID == _validate.ID
                                     select p).ToList;
                        if (query.Count > 0)
                            return false;
                        else
                            return true;
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

        public void ValidateATPeriodDay(ATPeriodDTO _validate)
        {
            var query;
            try
            {
                if (_validate.START_DATE != null & _validate.END_DATE != null)
                {
                    if (_validate.ID != 0)
                        query = (from p in Context.AT_PERIOD
                                 where (_validate.START_DATE <= p.END_DATE & _validate.END_DATE >= p.START_DATE)
                                                              & p.YEAR == _validate.YEAR
                                                              & p.ID != _validate.ID
                                 select p).FirstOrDefault;
                    else
                        query = (from p in Context.AT_PERIOD
                                 where (_validate.START_DATE <= p.END_DATE & _validate.END_DATE >= p.START_DATE)
                                                              & p.YEAR == _validate.YEAR
                                 select p).FirstOrDefault;
                    return (query == null);
                }
                return true;
            }
            catch (Exception ex)
            {
                WriteExceptionLog(ex, MethodBase.GetCurrentMethod().Name, "iPayroll");
                throw ex;
            }
        }

        public bool ModifyPeriod(ATPeriodDTO objPeriod, List<AT_ORG_PERIOD> objOrgPeriod, UserLog log, ref decimal gID)
        {
            AT_PERIOD objPeriodData = new AT_PERIOD() { ID = objPeriod.ID };
            AT_ORG_PERIOD objOrgPeriodData;
            try
            {
                Context.AT_PERIOD.Attach(objPeriodData);
                objPeriodData.YEAR = objPeriod.YEAR;
                objPeriodData.MONTH = objPeriod.MONTH;
                objPeriodData.PERIOD_NAME = objPeriod.PERIOD_NAME;
                objPeriodData.START_DATE = objPeriod.START_DATE;
                objPeriodData.END_DATE = objPeriod.END_DATE;
                objPeriodData.PERIOD_STANDARD = objPeriod.PERIOD_STANDARD;
                if (objPeriodData.ID > 0)
                {
                    foreach (AT_ORG_PERIOD ObjIns in objOrgPeriod)
                    {
                        var org_id = ObjIns.ORG_ID;
                        List<AT_ORG_PERIOD> objDelete = (from p in Context.AT_ORG_PERIOD
                                                         where p.PERIOD_ID == objPeriodData.ID & p.ORG_ID == org_id
                                                         select p).ToList;
                        if (objDelete.Count == 0)
                        {
                            objOrgPeriodData = new AT_ORG_PERIOD();
                            objOrgPeriodData.ID = Utilities.GetNextSequence(Context, Context.AT_ORG_PERIOD.EntitySet.Name);
                            objOrgPeriodData.ORG_ID = ObjIns.ORG_ID;
                            objOrgPeriodData.STATUSCOLEX = 1;
                            objOrgPeriodData.STATUSPAROX = 1;
                            objOrgPeriodData.PERIOD_ID = objPeriodData.ID;
                            Context.AT_ORG_PERIOD.AddObject(objOrgPeriodData);
                        }
                    }
                }
                Context.SaveChanges(log);
                gID = objPeriodData.ID;
                return true;
            }
            catch (Exception ex)
            {
                WriteExceptionLog(ex, MethodBase.GetCurrentMethod().Name, "iPayroll");
                throw ex;
            }
        }

        public bool DeletePeriod(ATPeriodDTO lstPeriod)
        {
            List<AT_ORG_PERIOD> objOrgPeriod = (from p in Context.AT_ORG_PERIOD
                                                where p.PERIOD_ID == lstPeriod.ID
                                                select p).ToList;
            List<AT_PERIOD> objPeriod = (from p in Context.AT_PERIOD
                                         where p.ID == lstPeriod.ID
                                         select p).ToList;
            try
            {
                foreach (var item in objOrgPeriod)
                    Context.AT_ORG_PERIOD.DeleteObject(item);
                foreach (var item in objPeriod)
                    Context.AT_PERIOD.DeleteObject(item);
                Context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                WriteExceptionLog(ex, MethodBase.GetCurrentMethod().Name, "iPayroll");
                throw ex;
            }
        }
    #endregion

    #region "List Salary Fomuler"
   //Còn tiếp :<