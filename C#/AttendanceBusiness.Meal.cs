using System;
using AttendanceBusiness.ServiceContracts;
using AttendanceDAL;
using Framework.Data;
using System.ServiceModel.Activation;
using System.Configuration;

namespace AttendanceBusiness.ServiceImplementations
{
    partial class AttendanceBusiness : IAttendanceBusiness
    {
        #region List
        public DataTable Get_KITCHEN(decimal is_blank)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.Get_KITCHEN(is_blank);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable Get_KITCHEN_BY_ORG(decimal is_blank, decimal Meal_ID, ParamDTO _param, UserLog log)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.Get_KITCHEN_BY_ORG(is_blank, Meal_ID, _param, log);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable Get_KITCHEN_BY_EMP(decimal is_blank, decimal employee_id, decimal Meal_ID)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.Get_KITCHEN_BY_EMP(is_blank, employee_id, Meal_ID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable Get_KITCHEN_BY_STUDENT(decimal is_blank, decimal student_id, decimal Meal_ID)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.Get_KITCHEN_BY_STUDENT(is_blank, student_id, Meal_ID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable Get_MEAL_BY_EMP(decimal is_blank, decimal employee_id)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.Get_MEAL_BY_EMP(is_blank, employee_id);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable Get_MEAL_BY_EMP_EFFECT(decimal is_blank, decimal employee_id, DateTime effectDate)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.Get_MEAL_BY_EMP_EFFECT(is_blank, employee_id, effectDate);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable Get_MEAL_BY_ORG(decimal is_blank, decimal org_id)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.Get_MEAL_BY_ORG(is_blank, org_id);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        #endregion

        #region AT_KITCHEN
        public bool Insert_AT_KITCHEN(AT_KITCHEN_DTO lstData, UserLog log, ref decimal gID)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.Insert_AT_KITCHEN(lstData, log, gID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool Modify_AT_KITCHEN(AT_KITCHEN_DTO lstData, UserLog log, ref decimal gID)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.Modify_AT_KITCHEN(lstData, log, gID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool Delete_AT_KITCHEN(List<decimal> lstID)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.Delete_AT_KITCHEN(lstID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public AT_KITCHEN_DTO Get_AT_KITCHENbyID(decimal? _id)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.Get_AT_KITCHENById(_id);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public List<AT_KITCHEN_DTO> Get_AT_KITCHEN(AT_KITCHEN_DTO _filter, int PageIndex = 0, int PageSize = int.MaxValue, ref int Total = 0, string Sorts = "CREATED_DATE desc")
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.Get_AT_KITCHEN(_filter, PageIndex, PageSize, Total, Sorts);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool Validate_AT_KITCHEN(AT_KITCHEN_DTO _obj, string _action, ref string _error = "")
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.Validate_AT_KITCHEN(_obj, _action, _error);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool CHECK_TIME_BY_EMP(decimal Employee_ID, DateTime Effect_date)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.CHECK_TIME_BY_EMP(Employee_ID, Effect_date);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool CHECK_TIME_BY_STUDENT(decimal STUDENT_ID, DateTime Effect_date)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.CHECK_TIME_BY_STUDENT(STUDENT_ID, Effect_date);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool CHECK_TIME_BY_ORG(decimal ORG_ID, DateTime Effect_date)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.CHECK_TIME_BY_ORG(ORG_ID, Effect_date);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        #endregion

        #region AT_TERMINALS_MEAL
        public List<AT_TERMINALS_MEALDTO> GetAT_TERMINAL_MEAL(AT_TERMINALS_MEALDTO _filter, int PageIndex = 0, int PageSize = int.MaxValue, ref int Total = 0, string Sorts = "CREATED_DATE desc", UserLog log = null/* TODO Change to default(_) if this is not a reference type */)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.GetAT_TERMINAL_MEAL(_filter, PageIndex, PageSize, Total, Sorts, log);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public List<AT_TERMINALS_MEALDTO> GetAT_TERMINAL_MEAL_STATUS(AT_TERMINALS_MEALDTO _filter, int PageIndex = 0, int PageSize = int.MaxValue, ref int Total = 0, string Sorts = "CREATED_DATE desc", UserLog log = null/* TODO Change to default(_) if this is not a reference type */)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.GetAT_TERMINAL_MEAL_STATUS(_filter, PageIndex, PageSize, Total, Sorts, log);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public bool InsertAT_TERMINAL_MEAL(AT_TERMINALS_MEALDTO lstData, UserLog log, ref decimal gID)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.InsertAT_TERMINAL_MEAL(lstData, log, gID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool ValidateAT_TERMINAL_MEAL(AT_TERMINALS_MEALDTO lstData, string sAction)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.ValidateAT_TERMINAL_MEAL(lstData, sAction);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool ModifyAT_TERMINAL_MEAL(AT_TERMINALS_MEALDTO lstData, UserLog log, ref decimal gID)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.ModifyAT_TERMINAL_MEAL(lstData, log, gID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool ActiveAT_TERMINAL_MEAL(List<decimal> lstID, UserLog log, string bActive)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.ActiveAT_TERMINAL_MEAL(lstID, log, bActive);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool DeleteAT_TERMINAL_MEAL(List<decimal> lstID)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.DeleteAT_TERMINAL_MEAL(lstID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        #endregion

        #region AT_MEAL_SETUP
        public bool Modify_AT_MEAL_SETUP(AT_MEAL_SETUP_DTO objData, UserLog log)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.Modify_AT_MEAL_SETUP(objData, log);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool Delete_AT_MEAL_SETUP(List<decimal> lstID)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.Delete_AT_MEAL_SETUP(lstID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        public AT_MEAL_SETUP_DTO Get_AT_MEAL_SETUPbyID(decimal? _id)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.Get_AT_MEAL_SETUPById(_id);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public List<AT_MEAL_SETUP_DTO> Get_AT_MEAL_SETUP(AT_MEAL_SETUP_DTO _filter, ParamDTO _param = null/* TODO Change to default(_) if this is not a reference type */, int PageIndex = 0, int PageSize = int.MaxValue, ref int Total = 0, string Sorts = "ORG_PATH", UserLog log = null/* TODO Change to default(_) if this is not a reference type */)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.Get_AT_MEAL_SETUP(_filter, _param, PageIndex, PageSize, Total, Sorts, log);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        #endregion

        #region AT_KITCHEN_ORG
        public List<AT_KITCHEN_ORG_DTO> GetAT_KITCHEN_ORG(AT_KITCHEN_ORG_DTO filter, int PageIndex, int PageSize, ref int Total, string Sorts = "CREATED_DATE desc")
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    var lst = rep.GetAT_KITCHEN_ORG(filter, PageIndex, PageSize, Total, Sorts);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool InsertAT_KITCHEN_ORG(List<AT_KITCHEN_ORG_DTO> objAT_KITCHEN_ORG, UserLog log, ref decimal gID)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.InsertAT_KITCHEN_ORG(objAT_KITCHEN_ORG, log, gID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool CheckKitchenInUsing(List<decimal> lstID, decimal orgID)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.CheckKitchenInUsing(lstID, orgID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool DeleteAT_KITCHEN_ORG(List<decimal> objAT_KITCHEN_ORG, UserLog log)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.DeleteAT_KITCHEN_ORG(objAT_KITCHEN_ORG, log);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool ActiveAT_KITCHEN_ORG(List<decimal> objAT_KITCHEN_ORG, string sActive, UserLog log)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.ActiveAT_KITCHEN_ORG(objAT_KITCHEN_ORG, sActive, log);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        #endregion

        #region AT_MEAL_MANAGER
        public bool Insert_AT_MEAL_MANAGER(List<AT_MEAL_MANAGER_DTO> lstData, UserLog log, ref decimal gID)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.Insert_AT_MEAL_MANAGER(lstData, log, gID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool Insert_AT_MEAL_MANAGER_BY_ORG(List<AT_MEAL_MANAGER_DTO> lstData, ParamDTO _param, UserLog log)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.Insert_AT_MEAL_MANAGER_BY_ORG(lstData, _param, log);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool Insert_AT_MEAL_MANAGER_BY_EMP(List<AT_MEAL_MANAGER_DTO> lstData, List<EmployeeDTO> lstEmp, ParamDTO _param, UserLog log)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.Insert_AT_MEAL_MANAGER_BY_EMP(lstData, lstEmp, _param, log);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool Delete_AT_MEAL_MANAGER(List<decimal> lstID)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.Delete_AT_MEAL_MANAGER(lstID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        #endregion

        #region 
        public List<AT_MEAL_MANAGER_DTO> Get_AT_MEAL_MANAGERbyID(AT_MEAL_MANAGER_DTO obj)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.Get_AT_MEAL_MANAGERById(obj);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public List<AT_MEAL_MANAGER_DTO> Get_AT_MEAL_MANAGER(AT_MEAL_MANAGER_DTO _filter, ParamDTO _param, int PageIndex = 0, int PageSize = int.MaxValue, ref int Total = 0, string Sorts = "EMPLOYEE_CODE asc, EFFECT_DATE asc, MEAL_ID asc", UserLog log = null/* TODO Change to default(_) if this is not a reference type */)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.Get_AT_MEAL_MANAGER(_filter, _param, PageIndex, PageSize, Total, Sorts, log);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool Validate_AT_MEAL_SWAP(AT_MEAL_SWAP_DTO objData, string _action, ref string _error = "")
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.Validate_AT_MEAL_SWAP(objData, _action, _error);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        public bool Swap_AT_MEAL_MANAGER(AT_MEAL_SWAP_DTO objData, UserLog log)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.Swap_AT_MEAL_MANAGER(objData, log);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataSet GETDATA_MANAGER_IMPORT(ParamDTO obj, UserLog log)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.GETDATA_MANAGER_IMPORT(obj, log);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool ImportMealManager(DataTable dtData, DateTime StartDate, DateTime EndDate, ref DataTable dtError, UserLog log)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.ImportMealManager(dtData, StartDate, EndDate, dtError, log);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataSet GETDATA_CHANGE_IMPORT(ParamDTO obj, UserLog log)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.GETDATA_CHANGE_IMPORT(obj, log);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool ImportMealChange(List<string> lstID, DataTable dtData, ref DataTable dtError, UserLog log)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.ImportMealChange(lstID, dtData, dtError, log);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public List<EmployeeDTO> GetListEmployee_ByOrg(ParamDTO _param, UserLog log)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.GetListEmployee_ByOrg(_param, log);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        #endregion

        #region AT_MEAL_STUDENT
        public bool Insert_AT_MEAL_STUDENT(List<AT_MEAL_STUDENT_DTO> lstData, UserLog log, ref decimal gID)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.Insert_AT_MEAL_STUDENT(lstData, log, gID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool Insert_AT_MEAL_STUDENT_BY_ORG(List<AT_MEAL_STUDENT_DTO> lstData, ParamDTO _param, UserLog log)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.Insert_AT_MEAL_STUDENT_BY_ORG(lstData, _param, log);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool Insert_AT_MEAL_STUDENT_BY_EMP(List<AT_MEAL_STUDENT_DTO> lstData, List<EmployeeDTO> lstEmp, ParamDTO _param, UserLog log)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.Insert_AT_MEAL_STUDENT_BY_EMP(lstData, lstEmp, _param, log);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool Delete_AT_MEAL_STUDENT(List<decimal> lstID)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.Delete_AT_MEAL_STUDENT(lstID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        public List<AT_MEAL_STUDENT_DTO> Get_AT_MEAL_STUDENTbyID(AT_MEAL_STUDENT_DTO obj)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.Get_AT_MEAL_STUDENTById(obj);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public List<AT_MEAL_STUDENT_DTO> Get_AT_MEAL_STUDENT(AT_MEAL_STUDENT_DTO _filter, ParamDTO _param, int PageIndex = 0, int PageSize = int.MaxValue, ref int Total = 0, string Sorts = "STUDENT_CODE asc, EFFECT_DATE asc, MEAL_ID asc", UserLog log = null/* TODO Change to default(_) if this is not a reference type */)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.Get_AT_MEAL_STUDENT(_filter, _param, PageIndex, PageSize, Total, Sorts, log);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataSet GETDATA_STUDENT_IMPORT(ParamDTO obj, UserLog log)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.GETDATA_STUDENT_IMPORT(obj, log);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool ImportMealSTUDENT(DataTable dtData, DateTime StartDate, DateTime EndDate, ref DataTable dtError, UserLog log)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.ImportMealStudent(dtData, StartDate, EndDate, dtError, log);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public List<EmployeeDTO> GetListStudent_ByOrg(ParamDTO _param, UserLog log)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.GetListStudent_ByOrg(_param, log);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        #endregion

        #region AT_MEAL_FORECAST_SUM
        public List<AT_MEAL_FORECAST_SUM_DTO> Get_AT_MEAL_FORECAST_SUM(AT_MEAL_FORECAST_SUM_DTO _filter, ParamDTO _param, ref int Total, int PageIndex, int PageSize, string Sorts = "CREATED_DATE desc", UserLog log = null/* TODO Change to default(_) if this is not a reference type */)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.Get_AT_MEAL_FORECAST_SUM(_filter, _param, Total, PageIndex, PageSize, Sorts, log);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool CAL_AT_MEAL_FORECAST_SUM(ParamDTO _param, UserLog log)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.CAL_AT_MEAL_FORECAST_SUM(_param, log);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        public DataTable Get_AT_MEAL_FORECAST_SUM_IMPORT(AT_MEAL_FORECAST_SUM_DTO _param, UserLog log)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    var lst = rep.Get_AT_MEAL_FORECAST_SUM_IMPORT(_param, log);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool Import_AT_MEAL_FORECAST_SUM(List<AT_MEAL_FORECAST_SUM_DTO> lstData, UserLog log)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    var lst = rep.Import_AT_MEAL_FORECAST_SUM(lstData, log);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        #endregion

        #region AT_MEAL_PARTNER
        public bool Insert_AT_MEAL_PARTNER(AT_MEAL_PARTNER_DTO objData, UserLog log, ref decimal gID)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.Insert_AT_MEAL_PARTNER(objData, log, gID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool Modify_AT_MEAL_PARTNER(AT_MEAL_PARTNER_DTO objData, UserLog log, ref decimal gID)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.Modify_AT_MEAL_PARTNER(objData, log, gID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool Delete_AT_MEAL_PARTNER(List<decimal> lstID)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.Delete_AT_MEAL_PARTNER(lstID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }



        public AT_MEAL_PARTNER_DTO Get_AT_MEAL_PARTNERbyID(decimal? _id)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.Get_AT_MEAL_PARTNERById(_id);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public List<AT_MEAL_PARTNER_DTO> Get_AT_MEAL_PARTNER(AT_MEAL_PARTNER_DTO _filter, ParamDTO _param, int PageIndex = 0, int PageSize = int.MaxValue, ref int Total = 0, string Sorts = "CREATED_DATE desc", UserLog Log = null/* TODO Change to default(_) if this is not a reference type */)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.Get_AT_MEAL_PARTNER(_filter, _param, PageIndex, PageSize, Total, Sorts, Log);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        #endregion


        #region AT_MEAL_CHANGE
        public bool Insert_AT_MEAL_CHANGE(AT_MEAL_CHANGE_DTO objData, UserLog log, ref decimal gID)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.Insert_AT_MEAL_CHANGE(objData, log, gID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool Modify_AT_MEAL_CHANGE(AT_MEAL_CHANGE_DTO objData, UserLog log, ref decimal gID)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.Modify_AT_MEAL_CHANGE(objData, log, gID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool Delete_AT_MEAL_CHANGE(List<decimal> lstID, UserLog log)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.Delete_AT_MEAL_CHANGE(lstID, log);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public AT_MEAL_CHANGE_DTO Get_AT_MEAL_CHANGEbyID(decimal? _id)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.Get_AT_MEAL_CHANGEById(_id);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public List<AT_MEAL_CHANGE_DTO> Get_AT_MEAL_CHANGE(AT_MEAL_CHANGE_DTO _filter, ParamDTO _param, int PageIndex = 0, int PageSize = int.MaxValue, ref int Total = 0, string Sorts = "CREATED_DATE desc", UserLog log = null/* TODO Change to default(_) if this is not a reference type */)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.Get_AT_MEAL_CHANGE(_filter, _param, PageIndex, PageSize, Total, Sorts, log);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public List<AT_MEAL_CHANGE_DTO> Get_AT_MEAL_CHANGEApprove(AT_MEAL_CHANGE_DTO _filter, ParamDTO _param, int PageIndex = 0, int PageSize = int.MaxValue, ref int Total = 0, string Sorts = "CREATED_DATE desc", UserLog log = null/* TODO Change to default(_) if this is not a reference type */)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.Get_AT_MEAL_CHANGEApprove(_filter, _param, PageIndex, PageSize, Total, Sorts, log);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool Approve_AT_MEAL_CHANGE(List<decimal> lstID, decimal _status, string _reason, UserLog log)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.Approve_AT_MEAL_CHANGE(lstID, _status, _reason, log);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        #endregion

        #region AT_MEAL_COST_SETUP
        public bool Insert_AT_MEAL_COST_SETUP(List<AT_MEAL_COST_SETUP_DTO> lst, UserLog log, ref decimal gID)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.Insert_AT_MEAL_COST_SETUP(lst, log, gID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool Modify_AT_MEAL_COST_SETUP(AT_MEAL_COST_SETUP_DTO objData, UserLog log, ref decimal gID)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.Modify_AT_MEAL_COST_SETUP(objData, log, gID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool Delete_AT_MEAL_COST_SETUP(List<decimal> lstID)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.Delete_AT_MEAL_COST_SETUP(lstID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public AT_MEAL_COST_SETUP_DTO Get_AT_MEAL_COST_SETUPbyID(decimal? _id)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.Get_AT_MEAL_COST_SETUPById(_id);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public List<AT_MEAL_COST_SETUP_DTO> Get_AT_MEAL_COST_SETUP(AT_MEAL_COST_SETUP_DTO _filter, int PageIndex = 0, int PageSize = int.MaxValue, ref int Total = 0, string Sorts = "CREATED_DATE desc")
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.Get_AT_MEAL_COST_SETUP(_filter, PageIndex, PageSize, Total, Sorts);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        #endregion

        #region AT_MEAL_REAL
        public List<AT_MEAL_REAL_DTO> Get_AT_MEAL_REAL(AT_MEAL_REAL_DTO _filter, ParamDTO _param, ref int Total, int PageIndex, int PageSize, string Sorts = "CREATED_DATE desc", UserLog log = null/* TODO Change to default(_) if this is not a reference type */)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.Get_AT_MEAL_REAL(_filter, _param, Total, PageIndex, PageSize, Sorts, log);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool CAL_AT_MEAL_REAL(ParamDTO _param, UserLog log)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.CAL_AT_MEAL_REAL(_param, log);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        #endregion

        #region AT_MEAL_EXPLAN
        public List<AT_MEAL_EXPLAN_DTO> Get_AT_MEAL_EXPLAN(AT_MEAL_EXPLAN_DTO _filter, ParamDTO _param, ref int Total, int PageIndex, int PageSize, string Sorts = "CREATED_DATE desc", UserLog log = null/* TODO Change to default(_) if this is not a reference type */)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.Get_AT_MEAL_EXPLAN(_filter, _param, Total, PageIndex, PageSize, Sorts, log);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataSet Get_AT_MEAL_EXPLAN_IMPORT(AT_MEAL_EXPLAN_DTO _param, UserLog log)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    var lst = rep.Get_AT_MEAL_EXPLAN_IMPORT(_param, log);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool Import_AT_MEAL_EXPLAN(List<AT_MEAL_EXPLAN_DTO> lstData, UserLog log, ref DataTable dtError)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    var lst = rep.Import_AT_MEAL_EXPLAN(lstData, log, dtError);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        #endregion

        #region 
        public DataSet ExportReport(string _reportCode, string _pkgName, ParamDTO obj, UserLog log)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    var lst = rep.ExportReport(_reportCode, _pkgName, obj, log);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        #endregion
    }
}
