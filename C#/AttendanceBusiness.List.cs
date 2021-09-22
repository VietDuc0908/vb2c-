using System;
using AttendanceBusiness.ServiceContracts;
using AttendanceDAL;
using Framework.Data;
using LinqKit;

// NOTE: You can use the "Rename" command on the context menu to change the class name "Service1" in both code and config file together.
namespace AttendanceBusiness.ServiceImplementations
{
    public partial class AttendanceBusiness
    {
        public List<AT_HOLIDAYDTO> GetHoliday(AT_HOLIDAYDTO _filter, int PageIndex = 0, int PageSize = int.MaxValue, ref int Total = 0, string Sorts = "CREATED_DATE desc")
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.GetHoliday(_filter, PageIndex, PageSize, Total, Sorts);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool InsertHOLIDAY(AT_HOLIDAYDTO objHOLIDAY, UserLog log, ref decimal gID)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.InsertHoliday(objHOLIDAY, log, gID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool ValidateHOLIDAY(AT_HOLIDAYDTO objHOLIDAY)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.ValidateHoliday(objHOLIDAY);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool ModifyHOLIDAY(AT_HOLIDAYDTO objHOLIDAY, UserLog log, ref decimal gID)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.ModifyHoliday(objHOLIDAY, log, gID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool ActiveHoliday(List<decimal> lstID, UserLog log, string bActive)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.ActiveHoliday(lstID, log, bActive);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool DeleteHOLIDAY(List<decimal> lstID)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.DeleteHoliday(lstID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        public List<AT_HOLIDAY_GENERALDTO> GetHolidayGerenal(AT_HOLIDAY_GENERALDTO _filter, int PageIndex = 0, int PageSize = int.MaxValue, ref int Total = 0, string Sorts = "CREATED_DATE desc")
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.GetHolidayGerenal(_filter, PageIndex, PageSize, Total, Sorts);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool InsertHolidayGerenal(AT_HOLIDAY_GENERALDTO objHOLIDAYGR, UserLog log, ref decimal gID)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.InsertHolidayGerenal(objHOLIDAYGR, log, gID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool ValidateHolidayGerenal(AT_HOLIDAY_GENERALDTO objHOLIDAYGR)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.ValidateHolidayGerenal(objHOLIDAYGR);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool ModifyHoliday(AT_HOLIDAY_GENERALDTO objHOLIDAYGR, UserLog log, ref decimal gID)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.ModifyHolidayGerenal(objHOLIDAYGR, log, gID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool ActiveHolidayGerenal(List<decimal> lstID, UserLog log, string bActive)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.ActiveHolidayGerenal(lstID, log, bActive);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool DeleteHolidayGerenal(List<decimal> lstID)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.DeleteHolidayGerenal(lstID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public List<AT_TIME_MANUALDTO> GetSignByPage(string pagecode)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.GetSignByPage(pagecode);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public List<AT_FMLDTO> GetAT_FML(AT_FMLDTO _filter, int PageIndex = 0, int PageSize = int.MaxValue, ref int Total = 0, string Sorts = "CREATED_DATE desc")
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.GetAT_FML(_filter, PageIndex, PageSize, Total, Sorts);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool InsertAT_FML(AT_FMLDTO objAT_FML, UserLog log, ref decimal gID)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.InsertAT_FML(objAT_FML, log, gID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool ValidateAT_FML(AT_FMLDTO objAT_FML)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.ValidateAT_FML(objAT_FML);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool ModifyAT_FML(AT_FMLDTO objAT_FML, UserLog log, ref decimal gID)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.ModifyAT_FML(objAT_FML, log, gID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool ActiveAT_FML(List<decimal> lstID, UserLog log, string bActive)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.ActiveAT_FML(lstID, log, bActive);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool DeleteAT_FML(List<decimal> lstID)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.DeleteAT_FML(lstID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public List<AT_GSIGNDTO> GetAT_GSIGN(AT_GSIGNDTO _filter, int PageIndex = 0, int PageSize = int.MaxValue, ref int Total = 0, string Sorts = "CREATED_DATE desc")
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.GetAT_GSIGN(_filter, PageIndex, PageSize, Total, Sorts);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool InsertAT_GSIGN(AT_GSIGNDTO objGSFND, UserLog log, ref decimal gID)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.InsertAT_GSIGN(objGSFND, log, gID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool ValidateAT_GSIGN(AT_GSIGNDTO objGSFND)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.ValidateAT_GSIGN(objGSFND);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool ModifyAT_GSIGN(AT_GSIGNDTO objGSFND, UserLog log, ref decimal gID)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.ModifyAT_GSIGN(objGSFND, log, gID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool ActiveAT_GSIGN(List<decimal> lstID, UserLog log, string bActive)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.ActiveAT_GSIGN(lstID, log, bActive);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool DeleteAT_GSIGN(List<decimal> lstID)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.DeleteAT_GSIGN(lstID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public List<AT_DMVSDTO> GetAT_DMVS(AT_DMVSDTO _filter, int PageIndex = 0, int PageSize = int.MaxValue, ref int Total = 0, string Sorts = "CREATED_DATE desc")
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.GetAT_DMVS(_filter, PageIndex, PageSize, Total, Sorts);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool InsertAT_DMVS(AT_DMVSDTO objData, UserLog log, ref decimal gID)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.InsertAT_DMVS(objData, log, gID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool ValidateAT_DMVS(AT_DMVSDTO objData)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.ValidateAT_DMVS(objData);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool ModifyAT_DMVS(AT_DMVSDTO objData, UserLog log, ref decimal gID)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.ModifyAT_DMVS(objData, log, gID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool ActiveAT_DMVS(List<decimal> lstID, UserLog log, string bActive)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.ActiveAT_DMVS(lstID, log, bActive);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool DeleteAT_DMVS(List<decimal> lstID)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.DeleteAT_DMVS(lstID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public List<AT_SHIFTDTO> GetAT_SHIFT(AT_SHIFTDTO _filter, int PageIndex = 0, int PageSize = int.MaxValue, ref int Total = 0, string Sorts = "CREATED_DATE desc")
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.GetAT_SHIFT(_filter, PageIndex, PageSize, Total, Sorts);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool InsertAT_SHIFT(AT_SHIFTDTO objData, UserLog log, ref decimal gID)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.InsertAT_SHIFT(objData, log, gID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool ValidateAT_SHIFT(AT_SHIFTDTO objData)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.ValidateAT_SHIFT(objData);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool ModifyAT_SHIFT(AT_SHIFTDTO objData, UserLog log, ref decimal gID)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.ModifyAT_SHIFT(objData, log, gID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool ActiveAT_SHIFT(List<decimal> lstID, UserLog log, string bActive)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.ActiveAT_SHIFT(lstID, log, bActive);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool DeleteAT_SHIFT(List<decimal> lstID)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.DeleteAT_SHIFT(lstID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable GetAT_TIME_MANUALBINCOMBO()
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.GetAT_TIME_MANUALBINCOMBO();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        public List<AT_HOLIDAY_OBJECTDTO> GetAT_Holiday_Object(AT_HOLIDAY_OBJECTDTO _filter, int PageIndex = 0, int PageSize = int.MaxValue, ref int Total = 0, string Sorts = "CREATED_DATE desc")
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.GetAT_Holiday_Object(_filter, PageIndex, PageSize, Total, Sorts);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool InsertAT_Holiday_Object(AT_HOLIDAY_OBJECTDTO objData, UserLog log, ref decimal gID)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.InsertAT_Holiday_Object(objData, log, gID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool ValidateAT_Holiday_Object(AT_HOLIDAY_OBJECTDTO objData)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.ValidateAT_Holiday_Object(objData);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool ModifyAT_Holiday_Object(AT_HOLIDAY_OBJECTDTO objData, UserLog log, ref decimal gID)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.ModifyAT_Holiday_Object(objData, log, gID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool ActiveAT_Holiday_Object(List<decimal> lstID, UserLog log, string bActive)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.ActiveAT_Holiday_Object(lstID, log, bActive);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool DeleteAT_Holiday_Object(List<decimal> lstID)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.DeleteAT_Holiday_Object(lstID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public List<AT_SETUP_SPECIALDTO> GetAT_SETUP_SPECIAL(AT_SETUP_SPECIALDTO _filter, int PageIndex = 0, int PageSize = int.MaxValue, ref int Total = 0, string Sorts = "CREATED_DATE desc")
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.GetAT_SETUP_SPECIAL(_filter, PageIndex, PageSize, Total, Sorts);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool InsertAT_SETUP_SPECIAL(AT_SETUP_SPECIALDTO objData, UserLog log, ref decimal gID)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.InsertAT_SETUP_SPECIAL(objData, log, gID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool ValidateAT_SETUP_SPECIAL(AT_SETUP_SPECIALDTO objData)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.ValidateAT_SETUP_SPECIAL(objData);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool ModifyAT_SETUP_SPECIAL(AT_SETUP_SPECIALDTO objData, UserLog log, ref decimal gID)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.ModifyAT_SETUP_SPECIAL(objData, log, gID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool ActiveAT_SETUP_SPECIAL(List<decimal> lstID, UserLog log, string bActive)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.ActiveAT_SETUP_SPECIAL(lstID, log, bActive);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool DeleteAT_SETUP_SPECIAL(List<decimal> lstID)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.DeleteAT_SETUP_SPECIAL(lstID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public List<AT_SETUP_TIME_EMPDTO> GetAT_SETUP_TIME_EMP(AT_SETUP_TIME_EMPDTO _filter, int PageIndex = 0, int PageSize = int.MaxValue, ref int Total = 0, string Sorts = "CREATED_DATE desc")
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.GetAT_SETUP_TIME_EMP(_filter, PageIndex, PageSize, Total, Sorts);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool InsertAT_SETUP_TIME_EMP(AT_SETUP_TIME_EMPDTO objData, UserLog log, ref decimal gID)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.InsertAT_SETUP_TIME_EMP(objData, log, gID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool ValidateAT_SETUP_SPECIAL(AT_SETUP_TIME_EMPDTO objData)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.ValidateAT_SETUP_TIME_EMP(objData);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool ModifyAT_SETUP_TIME_EMP(AT_SETUP_TIME_EMPDTO objData, UserLog log, ref decimal gID)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.ModifyAT_SETUP_TIME_EMP(objData, log, gID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool ActiveAT_SETUP_TIME_EMP(List<decimal> lstID, UserLog log, string bActive)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.ActiveAT_SETUP_TIME_EMP(lstID, log, bActive);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool DeleteAT_SETUP_TIME_EMP(List<decimal> lstID)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.DeleteAT_SETUP_TIME_EMP(lstID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public List<AT_TERMINALSDTO> GetAT_TERMINAL(AT_TERMINALSDTO _filter, int PageIndex = 0, int PageSize = int.MaxValue, ref int Total = 0, string Sorts = "CREATED_DATE desc", UserLog log = null/* TODO Change to default(_) if this is not a reference type */)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.GetAT_TERMINAL(_filter, PageIndex, PageSize, Total, Sorts, log);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public List<AT_TERMINALSDTO> GetAT_TERMINAL_STATUS(AT_TERMINALSDTO _filter, int PageIndex = 0, int PageSize = int.MaxValue, ref int Total = 0, string Sorts = "CREATED_DATE desc", UserLog log = null/* TODO Change to default(_) if this is not a reference type */)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.GetAT_TERMINAL_STATUS(_filter, PageIndex, PageSize, Total, Sorts, log);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public bool InsertAT_TERMINAL(AT_TERMINALSDTO objData, UserLog log, ref decimal gID)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.InsertAT_TERMINAL(objData, log, gID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool ValidateAT_TERMINAL(AT_TERMINALSDTO objData)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.ValidateAT_TERMINAL(objData);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool ModifyAT_TERMINAL(AT_TERMINALSDTO objData, UserLog log, ref decimal gID)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.ModifyAT_TERMINAL(objData, log, gID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool ActiveAT_TERMINAL(List<decimal> lstID, UserLog log, string bActive)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.ActiveAT_TERMINAL(lstID, log, bActive);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool DeleteAT_TERMINAL(List<decimal> lstID)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.DeleteAT_TERMINAL(lstID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public List<AT_SIGNDEFAULTDTO> GetAT_SIGNDEFAULT(AT_SIGNDEFAULTDTO _filter, int PageIndex = 0, int PageSize = int.MaxValue, ref int Total = 0, string Sorts = "CREATED_DATE desc", UserLog log = null/* TODO Change to default(_) if this is not a reference type */)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.GetAT_SIGNDEFAULT(_filter, PageIndex, PageSize, Total, Sorts, log);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable GetAT_ListShift()
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.GetAT_ListShift();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable GetAT_PERIOD()
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.GetAT_PERIOD();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable GetEmployeeID(string employee_code, decimal period_id)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.GetEmployeeID(employee_code, period_id);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public DataTable GetEmployeeIDInSign(string employee_code)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.GetEmployeeIDInSign(employee_code);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public DataTable GetEmployeeByTimeID(decimal time_id)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.GetEmployeeByTimeID(time_id);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool InsertAT_SIGNDEFAULT(AT_SIGNDEFAULTDTO objSIGN, UserLog log, ref decimal gID)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.InsertAT_SIGNDEFAULT(objSIGN, log, gID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool ModifyAT_SINGDEFAULT(AT_SIGNDEFAULTDTO objSIGN, UserLog log, ref decimal gID)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.ModifyAT_SIGNDEFAULT(objSIGN, log, gID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool ValidateAT_SINGDEFAULT(AT_SIGNDEFAULTDTO objSIGN)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.ValidateAT_SIGNDEFAULT(objSIGN);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool ActiveAT_SIGNDEFAULT(List<decimal> lstID, UserLog log, string bActive)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.ActiveAT_SIGNDEFAULT(lstID, log, bActive);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool DeleteAT_SIGNDEFAULT(List<decimal> lstID)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.DeleteAT_SIGNDEFAULT(lstID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public List<AT_TIMESHEET_REGISTERDTO> GetPlanningAppointmentByEmployee(decimal empid, DateTime startdate, DateTime enddate, List<AT_TIME_MANUALDTO> listSign)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.GetPlanningAppointmentByEmployee(empid, startdate, enddate, listSign);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public bool InsertPortalRegister(AT_PORTAL_REG_DTO itemRegister, UserLog log)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.InsertPortalRegister(itemRegister, log);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public List<DateTime> GetHolidayByCalender(DateTime startdate, DateTime enddate)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.GetHolidayByCalender(startdate, enddate);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public List<AT_TIMESHEET_REGISTERDTO> GetRegisterAppointmentInPortalByEmployee(decimal empid, DateTime startdate, DateTime enddate, List<AT_TIME_MANUALDTO> listSign, List<short> status)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.GetRegisterAppointmentInPortalByEmployee(empid, startdate, enddate, listSign, status);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public decimal GetTotalLeaveInYear(decimal empid, decimal p_year)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.GetTotalLeaveInYear(empid, p_year);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public bool DeletePortalRegisterByDate(List<AT_TIMESHEET_REGISTERDTO> listappointment, List<AT_TIME_MANUALDTO> listSign)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.DeletePortalRegisterByDate(listappointment, listSign);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public bool DeletePortalRegister(decimal id)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.DeletePortalRegister(id);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public string SendRegisterToApprove(List<decimal> objLstRegisterId, string process, string currentUrl)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.SendRegisterToApprove(objLstRegisterId, process, currentUrl);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public List<AT_TIME_MANUALDTO> GetListSignCode(string gSignCode)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.GetListSignCode(gSignCode);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public List<AT_PORTAL_REG_DTO> GetListWaitingForApprove(decimal approveId, string process, ATRegSearchDTO filter)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.GetListWaitingForApprove(approveId, process, filter);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public bool ApprovePortalRegister(Guid regID, decimal approveId, int status, string note, string currentUrl, string process, UserLog log)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.ApprovePortalRegister(regID, approveId, status, note, currentUrl, process, log);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public DataTable GetEmployeeList()
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.GetEmployeeList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public DataTable GetLeaveDay(DateTime dDate)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.GetLeaveDay(dDate);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        public List<AT_TIME_MANUALDTO> GetAT_TIME_MANUAL(AT_TIME_MANUALDTO _filter, int PageIndex = 0, int PageSize = int.MaxValue, ref int Total = 0, string Sorts = "CREATED_DATE desc")
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.GetAT_TIME_MANUAL(_filter, PageIndex, PageSize, Total, Sorts);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public AT_TIME_MANUALDTO GetAT_TIME_MANUALById(decimal? _id)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.GetAT_TIME_MANUALById(_id);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool InsertAT_TIME_MANUAL(AT_TIME_MANUALDTO objHOLIDAY, UserLog log, ref decimal gID)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.InsertAT_TIME_MANUAL(objHOLIDAY, log, gID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool ValidateAT_TIME_MANUAL(AT_TIME_MANUALDTO objHOLIDAY)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.ValidateAT_TIME_MANUAL(objHOLIDAY);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool ModifyAT_TIME_MANUAL(AT_TIME_MANUALDTO objHOLIDAY, UserLog log, ref decimal gID)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.ModifyAT_TIME_MANUAL(objHOLIDAY, log, gID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool ActiveAT_TIME_MANUAL(List<decimal> lstID, UserLog log, string bActive)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.ActiveAT_TIME_MANUAL(lstID, log, bActive);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool DeleteAT_TIME_MANUAL(List<decimal> lstID)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.DeleteAT_TIME_MANUAL(lstID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public DataTable GetDataImportCO()
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.GetDataImportCO();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        public List<AT_LISTPARAM_SYSTEAMDTO> GetListParamItime(AT_LISTPARAM_SYSTEAMDTO _filter, int PageIndex = 0, int PageSize = int.MaxValue, ref int Total = 0, string Sorts = "CREATED_DATE desc")
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.GetListParamItime(_filter, PageIndex, PageSize, Total, Sorts);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool InsertListParamItime(AT_LISTPARAM_SYSTEAMDTO objData, UserLog log, ref decimal gID)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.InsertListParamItime(objData, log, gID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool ValidateListParamItime(AT_LISTPARAM_SYSTEAMDTO objData)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.ValidateListParamItime(objData);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool ModifyListParamItime(AT_LISTPARAM_SYSTEAMDTO objData, UserLog log, ref decimal gID)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.ModifyListParamItime(objData, log, gID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool ActiveListParamItime(List<decimal> lstID, UserLog log, string bActive)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.ActiveListParamItime(lstID, log, bActive);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool DeleteListParamItime(List<decimal> lstID)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.DeleteListParamItime(lstID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public string AutoGenCode(string firstChar, string tableName, string colName)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    var lst = rep.AutoGenCode(firstChar, tableName, colName);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool CheckExistInDatabase(List<decimal> lstID, AttendanceCommon.TABLE_NAME table)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    var lst = rep.CheckExistInDatabase(lstID, table);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool CheckExistInDatabaseAT_SIGNDEFAULT(List<decimal> lstID, List<DateTime> lstWorking, List<decimal> lstShift, AttendanceCommon.TABLE_NAME table)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    var lst = rep.CheckExistInDatabaseAT_SIGNDEFAULT(lstID, lstWorking, lstShift, table);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
