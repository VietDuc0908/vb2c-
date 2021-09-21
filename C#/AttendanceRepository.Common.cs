using System;
using Attendance.AttendanceBusiness;
using Framework.UI;

partial class AttendanceRepository : AttendanceRepositoryBase
{
    public DataTable LOAD_PERIOD(AT_PERIODDTO obj)
    {
        DataTable dt;
        using (New AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                dt = rep.LOAD_PERIOD(Me.Log);
                return dt;
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }
    public List<AT_PERIODDTO> LOAD_PERIODBylinq(AT_PERIODDTO obj)
    {
        List<AT_PERIODDTO> dt;
        using (New AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                dt = rep.LOAD_PERIODBylinq(Me.Log);
                return dt;
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }
    public AT_PERIODDTO LOAD_PERIODByID(AT_PERIODDTO obj)
    {
        AT_PERIODDTO dt;
        using (New AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                dt = rep.LOAD_PERIODByID(Me.Log);
                return dt;
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public DataTable GetTerminal(AT_TERMINALSDTO obj)
    {
        DataTable dt;
        using (New AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                dt = rep.GetTerminal(Me.Log);
                return dt;
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public DataTable GetTerminalMeal(AT_TERMINALSDTO obj) As DataTable
    {
        DataTable dt;
        using (New AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                dt = rep.GetTerminal(Me.Log);
                return dt;
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public DataSet GetDataFromOrg(PARAMDTO obj ) 
    { 
        DataSet dt;
        using (New AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                dt = rep.GetDataFromOrg(Me.Log);
                return dt;
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }
    public Boolean CLOSEDOPEN_PERIOD(PARAMDTO param)
    {
        Boolean dt;
        using (New AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                dt = rep.GetTerminal(Me.Log);
                return dt;
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    } 
    public Boolean IS_PERIODSTATUS(PARAMDTO param)
    {
        Boolean dt;
        using (New AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                dt = rep.IS_PERIODSTATUS(Me.Log);
                return dt;
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    } 
    public bool IS_PERIOD_PAYSTATUS(ParamDTO param, bool isAfter = false)
    {
        bool dt;
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                dt = rep.IS_PERIOD_PAYSTATUS(param, isAfter, this.Log);
                return dt;
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }
    public bool IS_PERIODSTATUS_BY_DATE(ParamDTO param)
    {
        bool dt;
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                dt = rep.IS_PERIODSTATUS_BY_DATE(param, isAfter, this.Log);
                return dt;
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }
    public bool CheckPeriodClose(List<decimal> lstEmp, DateTime startdate, DateTime enddate, ref string sAction)
    {
        try
        {
            _isAvailable = false;
            using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
            {
                try
                {
                    _isAvailable = rep.CheckPeriodClose(lstEmp, startdate, enddate, sAction);
                    return _isAvailable;
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
}

}


