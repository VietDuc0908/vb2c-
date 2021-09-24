
using System.Linq.Expressions;
using LinqKit.Extensions;
using System.Data.Common;
using Framework.Data;
using Framework.Data.System.Linq.Dynamic;
using System.Data.Entity;
using System.Text.RegularExpressions;
using System.Data.Objects;

public partial class PayrollRepository
{
    public List<SalaryGroupDTO> GetSalaryGroup(SalaryGroupDTO _filter, int PageIndex, int PageSize, ref int Total, string Sorts = "CREATED_DATE desc")
    {
        try
        {
            var query = from p in Context.PA_SALARY_GROUP
                        select p;

            if (_filter.CODE != "")
                query = query.Where(p => p.CODE.ToUpper.Contains(_filter.CODE.ToUpper));

            if (_filter.NAME != "")
                query = query.Where(p => p.NAME.ToUpper.Contains(_filter.NAME.ToUpper));

            if (_filter.EFFECT_DATE != null)
                query = query.Where(p => p.EFFECT_DATE == _filter.EFFECT_DATE);

            var lst = query.Select(p => new SalaryGroupDTO() { ID = p.ID, CODE = p.CODE, NAME = p.NAME, REMARK = p.REMARK, EFFECT_DATE = p.EFFECT_DATE, CREATED_DATE = p.CREATED_DATE });


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

    public SalaryGroupDTO GetEffectSalaryGroup()
    {
        try
        {
            var query = from p in Context.PA_SALARY_GROUP
                        where p.EFFECT_DATE <= DateTime.Now
                        orderby p.EFFECT_DATE descending, p.CREATED_DATE descending
                        select p;

            var EffectSalaryGroup = query.Select(p => new SalaryGroupDTO() { ID = p.ID, CODE = p.CODE, NAME = p.NAME, REMARK = p.REMARK, EFFECT_DATE = p.EFFECT_DATE, CREATED_DATE = p.CREATED_DATE }).FirstOrDefault;

            return EffectSalaryGroup;
        }
        catch (Exception ex)
        {
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod().Name, "iPayroll");
            throw ex;
        }
    }

    public bool InsertSalaryGroup(SalaryGroupDTO objSalaryGroup, UserLog log, ref decimal gID)
    {
        int iCount = 0;
        PA_SALARY_GROUP objSalaryGroupData = new PA_SALARY_GROUP();
        try
        {
            objSalaryGroupData.ID = Utilities.GetNextSequence(Context, Context.PA_SALARY_GROUP.EntitySet.Name);
            objSalaryGroupData.CODE = objSalaryGroup.CODE.Trim;
            objSalaryGroupData.NAME = objSalaryGroup.NAME.Trim;
            objSalaryGroupData.EFFECT_DATE = objSalaryGroup.EFFECT_DATE;
            objSalaryGroupData.REMARK = objSalaryGroup.REMARK;
            Context.PA_SALARY_GROUP.AddObject(objSalaryGroupData);
            Context.SaveChanges(log);


            Context.SaveChanges(log);
            gID = objSalaryGroupData.ID;
            return true;
        }
        catch (Exception ex)
        {
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod().Name, "iPayroll");
            throw ex;
        }
    }

    public void ValidateSalaryGroup(SalaryGroupDTO _validate)
    {
        var query;
        try
        {
            if (_validate.CODE != null/* TODO Change to default(_) if this is not a reference type */ )
            {
                if (_validate.ID != 0)
                    query = (from p in Context.PA_SALARY_GROUP
                             where p.CODE.ToUpper == _validate.CODE.ToUpper
                                                          & p.ID != _validate.ID
                             select p).SingleOrDefault;
                else
                    query = (from p in Context.PA_SALARY_GROUP
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

    public bool ModifySalaryGroup(SalaryGroupDTO objSalaryGroup, UserLog log, ref decimal gID)
    {
        PA_SALARY_GROUP objSalaryGroupData = new PA_SALARY_GROUP() { ID = objSalaryGroup.ID };
        try
        {
            Context.PA_SALARY_GROUP.Attach(objSalaryGroupData);
            objSalaryGroupData.CODE = objSalaryGroup.CODE.Trim;
            objSalaryGroupData.NAME = objSalaryGroup.NAME.Trim;
            objSalaryGroupData.EFFECT_DATE = objSalaryGroup.EFFECT_DATE;
            objSalaryGroupData.REMARK = objSalaryGroup.REMARK;

            Context.SaveChanges(log);
            gID = objSalaryGroupData.ID;
            return true;
        }
        catch (Exception ex)
        {
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod().Name, "iPayroll");
            throw ex;
        }
    }

    public bool DeleteSalaryGroup(List<decimal> lstID)
    {
        List<PA_SALARY_GROUP> lstSalaryGroupData;
        try
        {
            lstSalaryGroupData = (from p in Context.PA_SALARY_GROUP
                                  where lstID.Contains(p.ID)
                                  select p).ToList;

            for (var idx = 0; idx <= lstSalaryGroupData.Count - 1; idx++)
            {
                foreach (var item in lstSalaryGroupData[idx].PA_SALARY_LEVEL)
                {
                    foreach (var item1 in item.PA_SALARY_RANK)
                        Context.PA_SALARY_RANK.DeleteObject(item1);
                    Context.PA_SALARY_LEVEL.DeleteObject(item);
                }

                Context.PA_SALARY_GROUP.DeleteObject(lstSalaryGroupData[idx]);
            }
            Context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod().Name, "iPayroll");
            throw ex;
        }
    }
}
