using System;
using Attendance.AttendanceBusiness;

public partial class AttendanceRepository : AttendanceRepositoryBase
{
    #region List
    public DataTable Get_KITCHEN(decimal is_blank)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.Get_KITCHEN(is_blank);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }


    public DataTable Get_KITCHEN_BY_EMP(decimal is_blank, decimal employee_id, decimal Meal_ID)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.Get_KITCHEN_BY_EMP(is_blank, employee_id, Meal_ID);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public DataTable Get_KITCHEN_BY_STUDENT(decimal is_blank, decimal student_id, decimal Meal_ID)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.Get_KITCHEN_BY_STUDENT(is_blank, student_id, Meal_ID);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public DataTable Get_KITCHEN_BY_ORG(decimal is_blank, decimal Meal_ID, ParamDTO _param)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.Get_KITCHEN_BY_ORG(is_blank, Meal_ID, _param, this.Log);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public DataTable Get_MEAL_BY_EMP_EFFECT(decimal is_blank, decimal employee_id, DateTime effectDate)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.Get_MEAL_BY_EMP_EFFECT(is_blank, employee_id, effectDate);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public DataTable Get_MEAL_BY_EMP(decimal is_blank, decimal employee_id)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.Get_MEAL_BY_EMP(is_blank, employee_id);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public DataTable Get_MEAL_BY_ORG(decimal is_blank, decimal org_id)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.Get_MEAL_BY_ORG(is_blank, org_id);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }
    #endregion

    #region AT_KITCHEN
    public bool Insert_AT_KITCHEN(AT_KITCHEN_DTO objData, ref decimal gID)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.Insert_AT_KITCHEN(objData, this.Log, gID);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }
    public bool Modify_AT_KITCHEN(AT_KITCHEN_DTO objData, ref decimal gID)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.Modify_AT_KITCHEN(objData, this.Log, gID);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }
    public bool Delete_AT_KITCHEN(List<decimal> lstID)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.Delete_AT_KITCHEN(lstID);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public AT_KITCHEN_DTO Get_AT_KITCHENById(decimal? _id)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.Get_AT_KITCHENbyID(_id);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public List<AT_KITCHEN_DTO> Get_AT_KITCHEN(AT_KITCHEN_DTO _filter, int PageIndex = 0, int PageSize = int.MaxValue, ref int Total = 0, string Sorts = "CREATED_DATE desc")
    {
        List<AT_KITCHEN_DTO> lst;

        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                lst = rep.Get_AT_KITCHEN(_filter, PageIndex, PageSize, Total, Sorts);
                return lst;
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }

        return null;
    }

    public bool Validate_AT_KITCHEN(AT_KITCHEN_DTO _obj, string _action, ref string _error = "")
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.Validate_AT_KITCHEN(_obj, _action, _error);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }
    #endregion

    #region AT_TERMINALS_MEAL
    public List<AT_TERMINALS_MEALDTO> GetAT_TERMINAL_MEAL(AT_TERMINALS_MEALDTO _filter, int PageIndex = 0, int PageSize = int.MaxValue, ref int Total = 0, string Sorts = "CREATED_DATE desc")
    {
        List<AT_TERMINALS_MEALDTO> lstTerminal;

        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                lstTerminal = rep.GetAT_TERMINAL_MEAL(_filter, PageIndex, PageSize, Total, Sorts, Log);
                return lstTerminal;
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
        return null;
    }

    public List<AT_TERMINALS_MEALDTO> GetAT_TERMINAL_MEAL_STATUS(AT_TERMINALS_MEALDTO _filter, int PageIndex = 0, int PageSize = int.MaxValue, ref int Total = 0, string Sorts = "CREATED_DATE desc")
    {
        List<AT_TERMINALS_MEALDTO> lstTerminal;

        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                lstTerminal = rep.GetAT_TERMINAL_MEAL_STATUS(_filter, PageIndex, PageSize, Total, Sorts, Log);
                return lstTerminal;
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
        return null;
    }

    public bool InsertAT_TERMINAL_MEAL(AT_TERMINALS_MEALDTO objTerminal, ref decimal gID)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.InsertAT_TERMINAL_MEAL(objTerminal, this.Log, gID);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool ValidateAT_TERMINAL_MEAL(AT_TERMINALS_MEALDTO objTerminal, string sAction)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.ValidateAT_TERMINAL_MEAL(objTerminal, sAction);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool ModifyAT_TERMINAL_MEAL(AT_TERMINALS_MEALDTO objTerminal, ref decimal gID)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.ModifyAT_TERMINAL_MEAL(objTerminal, this.Log, gID);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool ActiveAT_TERMINAL_MEAL(List<decimal> lstTerminal, string sActive)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.ActiveAT_TERMINAL_MEAL(lstTerminal, this.Log, sActive);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool DeleteAT_TERMINAL_MEAL(List<decimal> lstTerminal)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.DeleteAT_TERMINAL_MEAL(lstTerminal);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }
    #endregion

    #region AT_MEAL_SETUP
    public bool Modify_AT_MEAL_SETUP(AT_MEAL_SETUP_DTO objData)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.Modify_AT_MEAL_SETUP(objData, this.Log);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }
    public bool Delete_AT_MEAL_SETUP(List<decimal> lstID)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.Delete_AT_MEAL_SETUP(lstID);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public AT_MEAL_SETUP_DTO Get_AT_MEAL_SETUPById(decimal? _id)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.Get_AT_MEAL_SETUPbyID(_id);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }
    public List<AT_MEAL_SETUP_DTO> Get_AT_MEAL_SETUP(AT_MEAL_SETUP_DTO _filter, ParamDTO _param = null/* TODO Change to default(_) if this is not a reference type */, int PageIndex = 0, int PageSize = int.MaxValue, ref int Total = 0, string Sorts = "ORG_PATH")
    {
        List<AT_MEAL_SETUP_DTO> lst;

        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                lst = rep.Get_AT_MEAL_SETUP(_filter, _param, PageIndex, PageSize, Total, Sorts, this.Log);
                return lst;
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }

        return null;
    }
    #endregion

    #region AT_KITCHEN_ORG
    public List<AT_KITCHEN_ORG_DTO> GetAT_KITCHEN_ORG(AT_KITCHEN_ORG_DTO filter, int PageIndex, int PageSize, ref int Total, string Sorts = "CREATED_DATE desc")
    {
        List<AT_KITCHEN_ORG_DTO> lstAT_KITCHEN_ORG;

        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                lstAT_KITCHEN_ORG = rep.GetAT_KITCHEN_ORG(filter, PageIndex, PageSize, Total, Sorts);
                return lstAT_KITCHEN_ORG;
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }

        return null;
    }

    public bool InsertAT_KITCHEN_ORG(List<AT_KITCHEN_ORG_DTO> lstAT_KITCHEN_ORG, ref decimal gID = default(Decimal))
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.InsertAT_KITCHEN_ORG(lstAT_KITCHEN_ORG, this.Log, gID);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }



    public bool CheckKitchenInUsing(List<decimal> lstID, decimal orgID)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.CheckKitchenInUsing(lstID, orgID);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool DeleteAT_KITCHEN_ORG(List<decimal> lstAT_KITCHEN_ORG)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.DeleteAT_KITCHEN_ORG(lstAT_KITCHEN_ORG, this.Log);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool ActiveAT_KITCHEN_ORG(List<decimal> lstAT_KITCHEN_ORG, string sActive)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.ActiveAT_KITCHEN_ORG(lstAT_KITCHEN_ORG, sActive, this.Log);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }
    #endregion

    #region AT_MEAL_MANAGER
    public bool Insert_AT_MEAL_MANAGER(List<AT_MEAL_MANAGER_DTO> lstData, ref decimal gID)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.Insert_AT_MEAL_MANAGER(lstData, this.Log, gID);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool Insert_AT_MEAL_MANAGER_BY_ORG(List<AT_MEAL_MANAGER_DTO> lstData, ParamDTO _param)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.Insert_AT_MEAL_MANAGER_BY_ORG(lstData, _param, this.Log);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool CHECK_TIME_BY_EMP(decimal Employee_ID, DateTime Effect_date)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.CHECK_TIME_BY_EMP(Employee_ID, Effect_date);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool CHECK_TIME_BY_STUDENT(decimal STUDENT_ID, DateTime Effect_date)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.CHECK_TIME_BY_STUDENT(STUDENT_ID, Effect_date);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool CHECK_TIME_BY_ORG(decimal ORG_ID, DateTime Effect_date)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.CHECK_TIME_BY_ORG(ORG_ID, Effect_date);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool Insert_AT_MEAL_MANAGER_BY_EMP(List<AT_MEAL_MANAGER_DTO> lstData, List<Attendance.AttendanceBusiness.EmployeeDTO> lstEmp, ParamDTO _param)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.Insert_AT_MEAL_MANAGER_BY_EMP(lstData, lstEmp, _param, this.Log);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }



    public bool Delete_AT_MEAL_MANAGER(List<decimal> lstID)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.Delete_AT_MEAL_MANAGER(lstID);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public List<AT_MEAL_MANAGER_DTO> Get_AT_MEAL_MANAGERById(AT_MEAL_MANAGER_DTO obj)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.Get_AT_MEAL_MANAGERbyID(obj);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public List<AT_MEAL_MANAGER_DTO> Get_AT_MEAL_MANAGER(AT_MEAL_MANAGER_DTO _filter, ParamDTO _param, int PageIndex = 0, int PageSize = int.MaxValue, ref int Total = 0, string Sorts = "EMPLOYEE_CODE asc, EFFECT_DATE asc, MEAL_ID asc")
    {
        List<AT_MEAL_MANAGER_DTO> lst;

        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                lst = rep.Get_AT_MEAL_MANAGER(_filter, _param, PageIndex, PageSize, Total, Sorts, this.Log);
                return lst;
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }

        return null;
    }


    public bool Validate_AT_MEAL_SWAP(AT_MEAL_SWAP_DTO objData, string _action, ref string _error = "")
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.Validate_AT_MEAL_SWAP(objData, _action, _error);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool Swap_AT_MEAL_MANAGER(AT_MEAL_SWAP_DTO objData)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.Swap_AT_MEAL_MANAGER(objData, this.Log);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }


    public DataSet GETDATA_MANAGER_IMPORT(ParamDTO obj)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.GETDATA_MANAGER_IMPORT(obj, this.Log);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool ImportMealManager(DataTable dtData, DateTime StartDate, DateTime EndDate, ref DataTable dtError)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.ImportMealManager(dtData, StartDate, EndDate, dtError, this.Log);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }


    public List<EmployeeDTO> GetListStudent_ByOrg(ParamDTO _param)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.GetListStudent_ByOrg(_param, this.Log);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }
    #endregion

    #region AT_MEAL_STUDENT
    public bool Insert_AT_MEAL_STUDENT(List<AT_MEAL_STUDENT_DTO> lstData, ref decimal gID)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.Insert_AT_MEAL_STUDENT(lstData, this.Log, gID);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool Insert_AT_MEAL_STUDENT_BY_ORG(List<AT_MEAL_STUDENT_DTO> lstData, ParamDTO _param)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.Insert_AT_MEAL_STUDENT_BY_ORG(lstData, _param, this.Log);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool Insert_AT_MEAL_STUDENT_BY_EMP(List<AT_MEAL_STUDENT_DTO> lstData, List<Attendance.AttendanceBusiness.EmployeeDTO> lstEmp, ParamDTO _param)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.Insert_AT_MEAL_STUDENT_BY_EMP(lstData, lstEmp, _param, this.Log);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }



    public bool Delete_AT_MEAL_STUDENT(List<decimal> lstID)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.Delete_AT_MEAL_STUDENT(lstID);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public List<AT_MEAL_STUDENT_DTO> Get_AT_MEAL_STUDENTById(AT_MEAL_STUDENT_DTO obj)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.Get_AT_MEAL_STUDENTbyID(obj);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public List<AT_MEAL_STUDENT_DTO> Get_AT_MEAL_STUDENT(AT_MEAL_STUDENT_DTO _filter, ParamDTO _param, int PageIndex = 0, int PageSize = int.MaxValue, ref int Total = 0, string Sorts = "STUDENT_CODE asc, EFFECT_DATE asc, MEAL_ID asc")
    {
        List<AT_MEAL_STUDENT_DTO> lst;

        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                lst = rep.Get_AT_MEAL_STUDENT(_filter, _param, PageIndex, PageSize, Total, Sorts, this.Log);
                return lst;
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }

        return null;
    }

    public DataSet GETDATA_STUDENT_IMPORT(ParamDTO obj)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.GETDATA_STUDENT_IMPORT(obj, this.Log);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool ImportMealSTUDENT(DataTable dtData, DateTime StartDate, DateTime EndDate, ref DataTable dtError)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.ImportMealSTUDENT(dtData, StartDate, EndDate, dtError, this.Log);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }


    public List<EmployeeDTO> GetListEmployee_ByOrg(ParamDTO _param)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.GetListEmployee_ByOrg(_param, this.Log);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }
    #endregion

    #region AT_FORECAST_SUM
    public List<AT_MEAL_FORECAST_SUM_DTO> Get_AT_MEAL_FORECAST_SUM(AT_MEAL_FORECAST_SUM_DTO _filter, ParamDTO _param, ref int Total, int PageIndex, int PageSize, string Sorts = "EFFECT_DATE,ORG_PATH,ORG_NAME,KITCHEN_NAME,MEAL_NAME,RATION_NAME")
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                var lst = rep.Get_AT_MEAL_FORECAST_SUM(_filter, _param, Total, PageIndex, PageSize, Sorts, this.Log);
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public List<AT_MEAL_FORECAST_SUM_DTO> Get_AT_MEAL_FORECAST_SUM(AT_MEAL_FORECAST_SUM_DTO _filter, ParamDTO _param, string Sorts = "EFFECT_DATE,ORG_PATH,ORG_NAME,KITCHEN_NAME,MEAL_NAME,RATION_NAME")
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                var lst = rep.Get_AT_MEAL_FORECAST_SUM(_filter, _param, 0, 0, int.MaxValue, Sorts, this.Log);
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public bool CAL_AT_MEAL_FORECAST_SUM(ParamDTO _param)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.CAL_AT_MEAL_FORECAST_SUM(_param, this.Log);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }


    public DataTable Get_AT_MEAL_FORECAST_SUM_IMPORT(AT_MEAL_FORECAST_SUM_DTO _param)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                var lst = rep.Get_AT_MEAL_FORECAST_SUM_IMPORT(_param, this.Log);
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public bool Import_AT_MEAL_FORECAST_SUM(List<AT_MEAL_FORECAST_SUM_DTO> lstData)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.Import_AT_MEAL_FORECAST_SUM(lstData, this.Log);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }
    #endregion

    #region AT_MEAL_PARTNER
    public bool Insert_AT_MEAL_PARTNER(AT_MEAL_PARTNER_DTO objData, ref decimal gID)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.Insert_AT_MEAL_PARTNER(objData, this.Log, gID);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }
    public bool Modify_AT_MEAL_PARTNER(AT_MEAL_PARTNER_DTO objData, ref decimal gID)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.Modify_AT_MEAL_PARTNER(objData, this.Log, gID);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }
    public bool Delete_AT_MEAL_PARTNER(List<decimal> lstID)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.Delete_AT_MEAL_PARTNER(lstID);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public AT_MEAL_PARTNER_DTO Get_AT_MEAL_PARTNERById(decimal? _id)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.Get_AT_MEAL_PARTNERbyID(_id);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }
    public List<AT_MEAL_PARTNER_DTO> Get_AT_MEAL_PARTNER(AT_MEAL_PARTNER_DTO _filter, ParamDTO _param, int PageIndex = 0, int PageSize = int.MaxValue, ref int Total = 0, string Sorts = "CREATED_DATE desc")
    {
        List<AT_MEAL_PARTNER_DTO> lst;

        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                lst = rep.Get_AT_MEAL_PARTNER(_filter, _param, PageIndex, PageSize, Total, Sorts, this.Log);
                return lst;
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }

        return null;
    }
    #endregion

    #region AT_MEAL_CHANGE
    public bool Insert_AT_MEAL_CHANGE(AT_MEAL_CHANGE_DTO objData, ref decimal gID)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.Insert_AT_MEAL_CHANGE(objData, this.Log, gID);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }
    public bool Modify_AT_MEAL_CHANGE(AT_MEAL_CHANGE_DTO objData, ref decimal gID)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.Modify_AT_MEAL_CHANGE(objData, this.Log, gID);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool Delete_AT_MEAL_CHANGE(List<decimal> lstID)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.Delete_AT_MEAL_CHANGE(lstID, Log);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public AT_MEAL_CHANGE_DTO Get_AT_MEAL_CHANGEById(decimal? _id)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.Get_AT_MEAL_CHANGEbyID(_id);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public List<AT_MEAL_CHANGE_DTO> Get_AT_MEAL_CHANGE(AT_MEAL_CHANGE_DTO _filter, ParamDTO _param, int PageIndex = 0, int PageSize = int.MaxValue, ref int Total = 0, string Sorts = "CREATED_DATE desc")
    {
        List<AT_MEAL_CHANGE_DTO> lst;

        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                lst = rep.Get_AT_MEAL_CHANGE(_filter, _param, PageIndex, PageSize, Total, Sorts, this.Log);
                return lst;
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }

        return null;
    }

    public List<AT_MEAL_CHANGE_DTO> Get_AT_MEAL_CHANGEApprove(AT_MEAL_CHANGE_DTO _filter, ParamDTO _param, int PageIndex = 0, int PageSize = int.MaxValue, ref int Total = 0, string Sorts = "CREATED_DATE desc")
    {
        List<AT_MEAL_CHANGE_DTO> lst;

        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                lst = rep.Get_AT_MEAL_CHANGEApprove(_filter, _param, PageIndex, PageSize, Total, Sorts, this.Log);
                return lst;
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }

        return null;
    }


    public bool Approve_AT_MEAL_CHANGE(List<decimal> lstID, decimal _status, string _reason = "")
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.Approve_AT_MEAL_CHANGE(lstID, _status, _reason, Log);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }


    public DataSet GETDATA_CHANGE_IMPORT(ParamDTO obj)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.GETDATA_CHANGE_IMPORT(obj, this.Log);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool ImportMealChange(List<string> lstID, DataTable dtData, ref DataTable dtError)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.ImportMealChange(lstID, dtData, dtError, this.Log);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }
    #endregion

    #region AT_MEAL_COST_CHANGE

    public bool Insert_AT_MEAL_COST_SETUP(List<AT_MEAL_COST_SETUP_DTO> lst, ref decimal gID)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.Insert_AT_MEAL_COST_SETUP(lst, this.Log, gID);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool Modify_AT_MEAL_COST_SETUP(AT_MEAL_COST_SETUP_DTO objData, ref decimal gID)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.Modify_AT_MEAL_COST_SETUP(objData, this.Log, gID);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool Delete_AT_MEAL_COST_SETUP(List<decimal> lstID)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.Delete_AT_MEAL_COST_SETUP(lstID);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }


    public AT_MEAL_COST_SETUP_DTO Get_AT_MEAL_COST_SETUPById(decimal? _id)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.Get_AT_MEAL_COST_SETUPbyID(_id);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }
    #endregion

    #region AT_MEAL_REAL
    public List<AT_MEAL_COST_SETUP_DTO> Get_AT_MEAL_COST_SETUP(AT_MEAL_COST_SETUP_DTO _filter, int PageIndex = 0, int PageSize = int.MaxValue, ref int Total = 0, string Sorts = "CREATED_DATE desc")
    {
        List<AT_MEAL_COST_SETUP_DTO> lst;

        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                lst = rep.Get_AT_MEAL_COST_SETUP(_filter, PageIndex, PageSize, Total, Sorts);
                return lst;
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }

        return null;
    }


    public List<AT_MEAL_REAL_DTO> Get_AT_MEAL_REAL(AT_MEAL_REAL_DTO _filter, ParamDTO _param, ref int Total, int PageIndex, int PageSize, string Sorts = "EFFECT_DATE,EMPLOYEE_CODE,ORG_PATH,ORG_NAME,KITCHEN_NAME,MEAL_NAME,RATION_NAME")
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                var lst = rep.Get_AT_MEAL_REAL(_filter, _param, Total, PageIndex, PageSize, Sorts, this.Log);
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public List<AT_MEAL_REAL_DTO> Get_AT_MEAL_REAL(AT_MEAL_REAL_DTO _filter, ParamDTO _param, string Sorts = "EFFECT_DATE,EMPLOYEE_CODE,ORG_PATH,ORG_NAME,KITCHEN_NAME,MEAL_NAME,RATION_NAME")
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                var lst = rep.Get_AT_MEAL_REAL(_filter, _param, 0, 0, int.MaxValue, Sorts, this.Log);
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public bool CAL_AT_MEAL_REAL(ParamDTO _param)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.CAL_AT_MEAL_REAL(_param, this.Log);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }
    #endregion

    #region AT_MEAL_EXPLAN
    public List<AT_MEAL_EXPLAN_DTO> Get_AT_MEAL_EXPLAN(AT_MEAL_EXPLAN_DTO _filter, ParamDTO _param, ref int Total, int PageIndex, int PageSize, string Sorts = "WORKINGDAY,EMPLOYEE_CODE,ORG_PATH,ORG_NAME,VALTIME,KITCHEN_NAME,MEAL_NAME,RATION_NAME")
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                var lst = rep.Get_AT_MEAL_EXPLAN(_filter, _param, Total, PageIndex, PageSize, Sorts, this.Log);
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public List<AT_MEAL_EXPLAN_DTO> Get_AT_MEAL_EXPLAN(AT_MEAL_EXPLAN_DTO _filter, ParamDTO _param, string Sorts = "WORKINGDAY,EMPLOYEE_CODE,ORG_PATH,ORG_NAME,VALTIME,KITCHEN_NAME,MEAL_NAME,RATION_NAME")
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                var lst = rep.Get_AT_MEAL_EXPLAN(_filter, _param, 0, 0, int.MaxValue, Sorts, this.Log);
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public DataSet Get_AT_MEAL_EXPLAN_IMPORT(AT_MEAL_EXPLAN_DTO _param)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                var lst = rep.Get_AT_MEAL_EXPLAN_IMPORT(_param, this.Log);
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public bool Import_AT_MEAL_EXPLAN(List<AT_MEAL_EXPLAN_DTO> lstData, ref DataTable dtError)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.Import_AT_MEAL_EXPLAN(lstData, this.Log, dtError);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }
    #endregion
}
