using System;
using Attendance.AttendanceBusiness;
using Framework.UI;

partial class AttendanceRepository : AttendanceRepositoryBase
{
    public DataTable GET_REPORT()
    {
        DataTable dt;
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                dt = rep.GET_REPORT();
                return dt;
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public List<Se_ReportDTO> GetReportById(Se_ReportDTO _filter, int PageIndex, int PageSize, ref int Total, string Sorts = "CODE ASC")
    {
        List<Se_ReportDTO> lstTitle;

        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                lstTitle = rep.GetReportById(_filter, PageIndex, PageSize, Total, this.Log, Sorts);
                return lstTitle;
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }

        return null;
    }

    public DataTable GETORGNAME(ParamDTO obj)
    {
        DataTable dt;
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                dt = rep.GETORGNAME(obj, this.Log);
                return dt;
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public DataSet GET_AT001(ParamDTO obj)
    {
        DataSet dt;
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                dt = rep.GET_AT001(obj, this.Log);
                return dt;
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public DataSet GET_AT002(ParamDTO obj)
    {
        DataSet dt;
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                dt = rep.GET_AT002(obj, this.Log);
                return dt;
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public DataSet GET_AT003(ParamDTO obj)
    {
        DataSet dt;
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                dt = rep.GET_AT003(obj, this.Log);
                return dt;
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public DataSet GET_AT004(ParamDTO obj)
    {
        DataSet dt;
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                dt = rep.GET_AT004(obj, this.Log);
                return dt;
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public DataSet GET_AT005(ParamDTO obj)
    {
        DataSet dt;
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                dt = rep.GET_AT005(obj, this.Log);
                return dt;
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public DataSet GET_AT006(ParamDTO obj)
    {
        DataSet dt;
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                dt = rep.GET_AT006(obj, this.Log);
                return dt;
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public DataSet GET_AT007(ParamDTO obj)
    {
        DataSet dt;
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                dt = rep.GET_AT007(obj, this.Log);
                return dt;
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public DataSet ExportReport(string _reportCode, string _pkgName, ParamDTO obj)
    {
        DataSet dt;
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                dt = rep.ExportReport(_reportCode, _pkgName, obj, this.Log);
                return dt;
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }
}
