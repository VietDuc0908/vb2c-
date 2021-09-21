using System;
using Attendance.AttendanceBusiness;
using Framework.UI;

partial class AttendanceRepository : AttendanceRepositoryBase
{
    public List<AT_HOLIDAYDTO> GetHoliday(AT_HOLIDAYDTO _filter, int PageIndex = 0, int PageSize = int.MaxValue, ref int Total = 0, string Sorts = "CREATED_DATE desc")
    {
        List<AT_HOLIDAYDTO> lstHoliday;

        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                lstHoliday = rep.GetHoliday(_filter, PageIndex, PageSize, Total, Sorts);
                return lstHoliday;
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }

        return null;
    }

    public bool InsertHoliday(AT_HOLIDAYDTO objHoliday, ref decimal gID)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.InsertHOLIDAY(objHoliday, this.Log, gID);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool ValidateHoliday(AT_HOLIDAYDTO objHoliday)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.ValidateHOLIDAY(objHoliday);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool ModifyHoliday(AT_HOLIDAYDTO objHoliday, ref decimal gID)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.ModifyHOLIDAY(objHoliday, this.Log, gID);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool ActiveHoliday(List<decimal> lstHoliday, string sActive)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.ActiveHoliday(lstHoliday, this.Log, sActive);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool DeleteHoliday(List<decimal> lstHoliday)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.DeleteHOLIDAY(lstHoliday);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }



    public List<AT_HOLIDAY_GENERALDTO> GetHolidayGerenal(AT_HOLIDAY_GENERALDTO _filter, int PageIndex = 0, int PageSize = int.MaxValue, ref int Total = 0, string Sorts = "CREATED_DATE desc")
    {
        List<AT_HOLIDAY_GENERALDTO> lstHoliday;

        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                lstHoliday = rep.GetHolidayGerenal(_filter, PageIndex, PageSize, Total, Sorts);
                return lstHoliday;
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }

        return null;
    }

    public bool InsertHolidayGerenal(AT_HOLIDAY_GENERALDTO objHoliday, ref decimal gID)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.InsertHolidayGerenal(objHoliday, this.Log, gID);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool ValidateHolidayGerenal(AT_HOLIDAY_GENERALDTO objHoliday)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.ValidateHolidayGerenal(objHoliday);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool ModifyHolidayGerenal(AT_HOLIDAY_GENERALDTO objHoliday, ref decimal gID)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.ModifyHolidayGerenal(objHoliday, this.Log, gID);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool ActiveHolidayGerenal(List<decimal> lstHoliday, string sActive)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.ActiveHolidayGerenal(lstHoliday, this.Log, sActive);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool DeleteHolidayGerenal(List<decimal> lstHoliday)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.DeleteHolidayGerenal(lstHoliday);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }


    public List<AT_TIME_MANUALDTO> GetSignByPage(string pagecode)
    {
        List<AT_TIME_MANUALDTO> lstManual;

        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                lstManual = rep.GetSignByPage(pagecode);
                return lstManual;
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
        return null;
    }

    public List<AT_FMLDTO> GetAT_FML(AT_FMLDTO _filter, int PageIndex = 0, int PageSize = int.MaxValue, ref int Total = 0, string Sorts = "CREATED_DATE desc")
    {
        List<AT_FMLDTO> lstHoliday;

        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                lstHoliday = rep.GetAT_FML(_filter, PageIndex, PageSize, Total, Sorts);
                return lstHoliday;
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }

        return null;
    }

    public bool InsertAT_FML(AT_FMLDTO objHoliday, ref decimal gID)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.InsertAT_FML(objHoliday, this.Log, gID);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool ValidateAT_FML(AT_FMLDTO objHoliday)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.ValidateAT_FML(objHoliday);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool ModifyAT_FML(AT_FMLDTO objHoliday, ref decimal gID)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.ModifyAT_FML(objHoliday, this.Log, gID);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool ActiveAT_FML(List<decimal> lstHoliday, string sActive)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.ActiveAT_FML(lstHoliday, this.Log, sActive);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool DeleteAT_FML(List<decimal> lstHoliday)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.DeleteAT_FML(lstHoliday);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }


    public List<AT_GSIGNDTO> GetAT_GSIGN(AT_GSIGNDTO _filter, int PageIndex = 0, int PageSize = int.MaxValue, ref int Total = 0, string Sorts = "CREATED_DATE desc")
    {
        List<AT_GSIGNDTO> lstAt_GSIGN;

        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                lstAt_GSIGN = rep.GetAT_GSIGN(_filter, PageIndex, PageSize, Total, Sorts);
                return lstAt_GSIGN;
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }

        return null;
    }

    public bool InsertAT_GSIGN(AT_GSIGNDTO objHoliday, ref decimal gID)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.InsertAT_GSIGN(objHoliday, this.Log, gID);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool ValidateAT_GSIGN(AT_GSIGNDTO objHoliday)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.ValidateAT_GSIGN(objHoliday);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool ModifyAT_GSIGN(AT_GSIGNDTO objHoliday, ref decimal gID)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.ModifyAT_GSIGN(objHoliday, this.Log, gID);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool ActiveAT_GSIGN(List<decimal> lstAT_GSIGN, string sActive)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.ActiveAT_GSIGN(lstAT_GSIGN, this.Log, sActive);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool DeleteAT_GSIGN(List<decimal> lstAT_GSIGN)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.DeleteAT_GSIGN(lstAT_GSIGN);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }


    public List<AT_DMVSDTO> GetAT_DMVS(AT_DMVSDTO _filter, int PageIndex = 0, int PageSize = int.MaxValue, ref int Total = 0, string Sorts = "CREATED_DATE desc")
    {
        List<AT_DMVSDTO> lstDMVS;

        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                lstDMVS = rep.GetAT_DMVS(_filter, PageIndex, PageSize, Total, Sorts);
                return lstDMVS;
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }

        return null;
    }

    public bool InsertAT_DMVS(AT_DMVSDTO objDMVS, ref decimal gID)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.InsertAT_DMVS(objDMVS, this.Log, gID);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool ValidateAT_DMVS(AT_DMVSDTO objDMVS)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.ValidateAT_DMVS(objDMVS);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool ModifyAT_DMVS(AT_DMVSDTO objDMVS, ref decimal gID)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.ModifyAT_DMVS(objDMVS, this.Log, gID);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool ActiveAT_DMVS(List<decimal> lstDMVS, string sActive)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.ActiveAT_DMVS(lstDMVS, this.Log, sActive);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool DeleteAT_DMVS(List<decimal> lstDMVS)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.DeleteAT_DMVS(lstDMVS);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }


    public List<AT_SHIFTDTO> GetAT_SHIFT(AT_SHIFTDTO _filter, int PageIndex = 0, int PageSize = int.MaxValue, ref int Total = 0, string Sorts = "CREATED_DATE desc")
    {
        List<AT_SHIFTDTO> lstDMVS;

        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                lstDMVS = rep.GetAT_SHIFT(_filter, PageIndex, PageSize, Total, Sorts);
                return lstDMVS;
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
        return null;
    }

    public bool InsertAT_SHIFT(AT_SHIFTDTO objDMVS, ref decimal gID)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.InsertAT_SHIFT(objDMVS, this.Log, gID);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool ValidateAT_SHIFT(AT_SHIFTDTO objDMVS)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.ValidateAT_SHIFT(objDMVS);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool ModifyAT_SHIFT(AT_SHIFTDTO objDMVS, ref decimal gID)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.ModifyAT_SHIFT(objDMVS, this.Log, gID);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool ActiveAT_SHIFT(List<decimal> lstDMVS, string sActive)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.ActiveAT_SHIFT(lstDMVS, this.Log, sActive);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool DeleteAT_SHIFT(List<decimal> lstDMVS)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.DeleteAT_SHIFT(lstDMVS);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public DataTable GetAT_TIME_MANUALBINCOMBO()
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.GetAT_TIME_MANUALBINCOMBO();
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public List<AT_HOLIDAY_OBJECTDTO> GetAT_Holiday_Object(AT_HOLIDAY_OBJECTDTO _filter, int PageIndex = 0, int PageSize = int.MaxValue, ref int Total = 0, string Sorts = "CREATED_DATE desc")
    {
        List<AT_HOLIDAY_OBJECTDTO> lstHolidayObj;

        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                lstHolidayObj = rep.GetAT_Holiday_Object(_filter, PageIndex, PageSize, Total, Sorts);
                return lstHolidayObj;
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
        return null;
    }

    public bool InsertAT_Holiday_Object(AT_HOLIDAY_OBJECTDTO objDMVS, ref decimal gID)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.InsertAT_Holiday_Object(objDMVS, this.Log, gID);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool ValidateAT_Holiday_Object(AT_HOLIDAY_OBJECTDTO objDMVS)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.ValidateAT_Holiday_Object(objDMVS);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool ModifyAT_Holiday_Object(AT_HOLIDAY_OBJECTDTO objDMVS, ref decimal gID)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.ModifyAT_Holiday_Object(objDMVS, this.Log, gID);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool ActiveAT_Holiday_Object(List<decimal> lstHolidayObj, string sActive)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.ActiveAT_Holiday_Object(lstHolidayObj, this.Log, sActive);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool DeleteAT_Holiday_Object(List<decimal> lstHolidayObj)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.DeleteAT_Holiday_Object(lstHolidayObj);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public List<AT_SETUP_SPECIALDTO> GetAT_SETUP_SPECIAL(AT_SETUP_SPECIALDTO _filter, int PageIndex = 0, int PageSize = int.MaxValue, ref int Total = 0, string Sorts = "CREATED_DATE desc")
    {
        List<AT_SETUP_SPECIALDTO> lstSetUp_SP;

        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                lstSetUp_SP = rep.GetAT_SETUP_SPECIAL(_filter, PageIndex, PageSize, Total, Sorts);
                return lstSetUp_SP;
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
        return null;
    }

    public bool InsertAT_SETUP_SPECIAL(AT_SETUP_SPECIALDTO objDMVS, ref decimal gID)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.InsertAT_SETUP_SPECIAL(objDMVS, this.Log, gID);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool ValidateAT_SETUP_SPECIAL(AT_SETUP_SPECIALDTO objDMVS)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.ValidateAT_SETUP_SPECIAL(objDMVS);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool ModifyAT_SETUP_SPECIAL(AT_SETUP_SPECIALDTO objDMVS, ref decimal gID)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.ModifyAT_SETUP_SPECIAL(objDMVS, this.Log, gID);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool ActiveAT_SETUP_SPECIAL(List<decimal> lstSetUp, string sActive)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.ActiveAT_SETUP_SPECIAL(lstSetUp, this.Log, sActive);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool DeleteAT_SETUP_SPECIAL(List<decimal> lstSetUp)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.DeleteAT_SETUP_SPECIAL(lstSetUp);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public List<AT_SETUP_TIME_EMPDTO> GetAT_SETUP_TIME_EMP(AT_SETUP_TIME_EMPDTO _filter, int PageIndex = 0, int PageSize = int.MaxValue, ref int Total = 0, string Sorts = "CREATED_DATE desc")
    {
        List<AT_SETUP_TIME_EMPDTO> lstSetUp_SP;

        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                lstSetUp_SP = rep.GetAT_SETUP_TIME_EMP(_filter, PageIndex, PageSize, Total, Sorts);
                return lstSetUp_SP;
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
        return null;
    }

    public bool InsertAT_SETUP_TIME_EMP(AT_SETUP_TIME_EMPDTO objDMVS, ref decimal gID)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.InsertAT_SETUP_TIME_EMP(objDMVS, this.Log, gID);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool ValidateAT_SETUP_TIME_EMP(AT_SETUP_TIME_EMPDTO objDMVS)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.ValidateAT_SETUP_TIME_EMP(objDMVS);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool ModifyAT_SETUP_TIME_EMP(AT_SETUP_TIME_EMPDTO objDMVS, ref decimal gID)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.ModifyAT_SETUP_TIME_EMP(objDMVS, this.Log, gID);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool ActiveAT_SETUP_TIME_EMP(List<decimal> lstSetUp, string sActive)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.ActiveAT_SETUP_TIME_EMP(lstSetUp, this.Log, sActive);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool DeleteAT_SETUP_TIME_EMP(List<decimal> lstSetUp)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.DeleteAT_SETUP_TIME_EMP(lstSetUp);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public List<AT_TERMINALSDTO> GetAT_TERMINAL(AT_TERMINALSDTO _filter, int PageIndex = 0, int PageSize = int.MaxValue, ref int Total = 0, string Sorts = "CREATED_DATE desc")
    {
        List<AT_TERMINALSDTO> lstTerminal;

        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                lstTerminal = rep.GetAT_TERMINAL(_filter, PageIndex, PageSize, Total, Sorts, Log);
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

    public List<AT_TERMINALSDTO> GetAT_TERMINAL_STATUS(AT_TERMINALSDTO _filter, int PageIndex = 0, int PageSize = int.MaxValue, ref int Total = 0, string Sorts = "CREATED_DATE desc")
    {
        List<AT_TERMINALSDTO> lstTerminal;

        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                lstTerminal = rep.GetAT_TERMINAL_STATUS(_filter, PageIndex, PageSize, Total, Sorts, Log);
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

    public bool InsertAT_TERMINAL(AT_TERMINALSDTO objTerminal, ref decimal gID)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.InsertAT_TERMINAL(objTerminal, this.Log, gID);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool ValidateAT_TERMINAL(AT_TERMINALSDTO objTerminal)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.ValidateAT_TERMINAL(objTerminal);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool ModifyAT_TERMINAL(AT_TERMINALSDTO objTerminal, ref decimal gID)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.ModifyAT_TERMINAL(objTerminal, this.Log, gID);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool ActiveAT_TERMINAL(List<decimal> lstTerminal, string sActive)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.ActiveAT_TERMINAL(lstTerminal, this.Log, sActive);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool DeleteAT_TERMINAL(List<decimal> lstTerminal)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.DeleteAT_TERMINAL(lstTerminal);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public List<AT_SIGNDEFAULTDTO> GetAT_SIGNDEFAULT(AT_SIGNDEFAULTDTO _filter, int PageIndex = 0, int PageSize = int.MaxValue, ref int Total = 0, string Sorts = "CREATED_DATE desc")
    {
        List<AT_SIGNDEFAULTDTO> lstSign;

        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                lstSign = rep.GetAT_SIGNDEFAULT(_filter, PageIndex, PageSize, Total, Sorts, this.Log);
                return lstSign;
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
        return null;
    }

    public DataTable GetAT_ListShift()
    {
        DataTable lstSign;

        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                lstSign = rep.GetAT_ListShift();
                return lstSign;
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
        return null/* TODO Change to default(_) if this is not a reference type */;
    }

    public DataTable GetAT_PERIOD()
    {
        DataTable lstSign;

        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                lstSign = rep.GetAT_PERIOD();
                return lstSign;
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
        return null/* TODO Change to default(_) if this is not a reference type */;
    }

    public DataTable GetEmployeeID(string employee_code, decimal period_id)
    {
        DataTable lstSign;

        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                lstSign = rep.GetEmployeeID(employee_code, period_id);
                return lstSign;
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
        return null/* TODO Change to default(_) if this is not a reference type */;
    }

    public DataTable GetEmployeeIDInSign(string employee_code)
    {
        DataTable lstSign;

        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                lstSign = rep.GetEmployeeIDInSign(employee_code);
                return lstSign;
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
        return null/* TODO Change to default(_) if this is not a reference type */;
    }

    public DataTable GetEmployeeByTimeID(decimal time_id)
    {
        DataTable lstSign;

        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                lstSign = rep.GetEmployeeByTimeID(time_id);
                return lstSign;
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
        return null/* TODO Change to default(_) if this is not a reference type */;
    }

    public bool InsertAT_SIGNDEFAULT(AT_SIGNDEFAULTDTO objSign, ref decimal gID)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.InsertAT_SIGNDEFAULT(objSign, this.Log, gID);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool ModifyAT_SIGNDEFAULT(AT_SIGNDEFAULTDTO objSign, ref decimal gID)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.ModifyAT_SIGNDEFAULT(objSign, this.Log, gID);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool ValidateAT_SIGNDEFAULT(AT_SIGNDEFAULTDTO objSign)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.ValidateAT_SIGNDEFAULT(objSign);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool ActiveAT_SIGNDEFAULT(List<decimal> lstSign, string sActive)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.ActiveAT_SIGNDEFAULT(lstSign, this.Log, sActive);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool DeleteAT_SIGNDEFAULT(List<decimal> lstSign)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.DeleteAT_SIGNDEFAULT(lstSign);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public List<AT_TIMESHEET_REGISTERDTO> GetRegisterAppointmentInPortalByEmployee(decimal empid, DateTime startdate, DateTime enddate, List<AT_TIME_MANUALDTO> listSign, List<short> status)
    {
        try
        {
            _isAvailable = false;
            using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
            {
                try
                {
                    return rep.GetRegisterAppointmentInPortalByEmployee(empid, startdate, enddate, listSign, status);
                }
                catch (Exception ex)
                {
                    rep.Abort();
                    throw ex;
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            _isAvailable = true;
        }
    }
    public decimal GetTotalLeaveInYear(decimal empid, decimal p_year)
    {
        try
        {
            _isAvailable = false;
            using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
            {
                try
                {
                    return rep.GetTotalLeaveInYear(empid, p_year);
                }
                catch (Exception ex)
                {
                    rep.Abort();
                    throw ex;
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            _isAvailable = true;
        }
    }

    public bool InsertPortalRegister(AttendanceBusiness.AT_PORTAL_REG_DTO itemRegister)
    {
        try
        {
            _isAvailable = false;

            using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
            {
                try
                {
                    return rep.InsertPortalRegister(itemRegister, this.Log);
                }
                catch (Exception ex)
                {
                    rep.Abort();
                    throw ex;
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            _isAvailable = true;
        }
    }

    public List<DateTime> GetHolidayByCalender(DateTime startdate, DateTime enddate)
    {
        try
        {
            _isAvailable = false;
            using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
            {
                try
                {
                    return rep.GetHolidayByCalender(startdate, enddate);
                }
                catch (Exception ex)
                {
                    rep.Abort();
                    throw ex;
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            _isAvailable = true;
        }
    }

    public bool DeletePortalRegisterByDate(List<AT_TIMESHEET_REGISTERDTO> listappointment, List<AT_TIME_MANUALDTO> listSign)
    {
        try
        {
            _isAvailable = false;
            using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
            {
                try
                {
                    return rep.DeletePortalRegisterByDate(listappointment, listSign);
                }
                catch (Exception ex)
                {
                    rep.Abort();
                    throw ex;
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            _isAvailable = true;
        }
    }

    public bool DeletePortalRegister(decimal id)
    {
        try
        {
            _isAvailable = false;
            using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
            {
                try
                {
                    return rep.DeletePortalRegister(id);
                }
                catch (Exception ex)
                {
                    rep.Abort();
                    throw ex;
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            _isAvailable = true;
        }
    }

    public string SendRegisterToApprove(List<decimal> objLstRegisterId, string process, string currentUrl)
    {
        try
        {
            _isAvailable = false;
            using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
            {
                try
                {
                    return rep.SendRegisterToApprove(objLstRegisterId, process, currentUrl);
                }
                catch (Exception ex)
                {
                    rep.Abort();
                    throw ex;
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            _isAvailable = true;
        }
    }


    public List<AT_TIME_MANUALDTO> GetListSignCode(string gSignCode)
    {
        try
        {
            _isAvailable = false;
            using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
            {
                try
                {
                    return rep.GetListSignCode(gSignCode);
                }
                catch (Exception ex)
                {
                    rep.Abort();
                    throw ex;
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            _isAvailable = true;
        }
    }
    public List<AttendanceBusiness.AT_PORTAL_REG_DTO> GetListWaitingForApprove(decimal approveId, string process, AttendanceBusiness.ATRegSearchDTO filter)
    {
        try
        {
            _isAvailable = false;

            using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
            {
                try
                {
                    return rep.GetListWaitingForApprove(approveId, process, filter);
                }
                catch (Exception ex)
                {
                    rep.Abort();
                    throw ex;
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            _isAvailable = true;
        }
    }
    public bool ApprovePortalRegister(Guid regID, decimal approveId, int status, string note, string currentUrl, string process)
    {
        try
        {
            _isAvailable = false;
            using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
            {
                try
                {
                    return rep.ApprovePortalRegister(regID, approveId, status, note, currentUrl, process, Log);
                }
                catch (Exception ex)
                {
                    rep.Abort();
                    throw ex;
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            _isAvailable = true;
        }
    }
    public DataTable GetEmployeeList()
    {
        try
        {
            _isAvailable = false;

            using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
            {
                try
                {
                    return rep.GetEmployeeList();
                }
                catch (Exception ex)
                {
                    rep.Abort();
                    throw ex;
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            _isAvailable = true;
        }
    }
    public DataTable GetLeaveDay(DateTime dDate)
    {
        try
        {
            _isAvailable = false;

            using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
            {
                try
                {
                    return rep.GetLeaveDay(dDate);
                }
                catch (Exception ex)
                {
                    rep.Abort();
                    throw ex;
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            _isAvailable = true;
        }
    }


    public List<AT_TIME_MANUALDTO> GetAT_TIME_MANUAL(AT_TIME_MANUALDTO _filter, int PageIndex = 0, int PageSize = int.MaxValue, ref int Total = 0, string Sorts = "CREATED_DATE desc")
    {
        List<AT_TIME_MANUALDTO> lstHoliday;

        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                lstHoliday = rep.GetAT_TIME_MANUAL(_filter, PageIndex, PageSize, Total, Sorts);
                return lstHoliday;
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }

        return null;
    }

    public AT_TIME_MANUALDTO GetAT_TIME_MANUALById(decimal? _id)
    {
        AT_TIME_MANUALDTO lstHoliday;
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                lstHoliday = rep.GetAT_TIME_MANUALById(_id);
                return lstHoliday;
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }

        return null/* TODO Change to default(_) if this is not a reference type */;
    }

    public bool InsertAT_TIME_MANUAL(AT_TIME_MANUALDTO objHoliday, ref decimal gID)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.InsertAT_TIME_MANUAL(objHoliday, this.Log, gID);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool ValidateAT_TIME_MANUAL(AT_TIME_MANUALDTO objHoliday)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.ValidateAT_TIME_MANUAL(objHoliday);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool ModifyAT_TIME_MANUAL(AT_TIME_MANUALDTO objHoliday, ref decimal gID)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.ModifyAT_TIME_MANUAL(objHoliday, this.Log, gID);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool ActiveAT_TIME_MANUAL(List<decimal> lstHoliday, string sActive)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.ActiveAT_TIME_MANUAL(lstHoliday, this.Log, sActive);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool DeleteAT_TIME_MANUAL(List<decimal> lstHoliday)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.DeleteAT_TIME_MANUAL(lstHoliday);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }
    public DataTable GetDataImportCO()
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.GetDataImportCO();
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public List<AT_LISTPARAM_SYSTEAMDTO> GetListParamItime(AT_LISTPARAM_SYSTEAMDTO _filter, int PageIndex = 0, int PageSize = int.MaxValue, ref int Total = 0, string Sorts = "CREATED_DATE desc")
    {
        List<AT_LISTPARAM_SYSTEAMDTO> lstHoliday;

        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                lstHoliday = rep.GetListParamItime(_filter, PageIndex, PageSize, Total, Sorts);
                return lstHoliday;
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }

        return null;
    }

    public bool InsertListParamItime(AT_LISTPARAM_SYSTEAMDTO lstData, ref decimal gID)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.InsertListParamItime(lstData, this.Log, gID);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool ValidateListParamItime(AT_LISTPARAM_SYSTEAMDTO lstData)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.ValidateListParamItime(lstData);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool ModifyListParamItime(AT_LISTPARAM_SYSTEAMDTO lstData, ref decimal gID)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.ModifyListParamItime(lstData, this.Log, gID);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool ActiveListParamItime(List<decimal> lstData, string sActive)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.ActiveListParamItime(lstData, this.Log, sActive);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public bool DeleteListParamItime(List<decimal> lstData)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.DeleteListParamItime(lstData);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public string AutoGenCode(string firstChar, string tableName, string colName)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.AutoGenCode(firstChar, tableName, colName);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }

        return null;
    }
    public bool CheckExistInDatabase(List<decimal> lstID, AttendanceCommonTABLE_NAME table)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.CheckExistInDatabase(lstID, table);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }

        return default(Boolean);
    }

    public bool CheckExistInDatabaseAT_SIGNDEFAULT(List<decimal> lstID, List<DateTime> lstWorking, List<decimal> lstShift, AttendanceCommonTABLE_NAME table)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.CheckExistInDatabaseAT_SIGNDEFAULT(lstID, lstWorking, lstShift, table);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }

        return default(Boolean);
    }
}
