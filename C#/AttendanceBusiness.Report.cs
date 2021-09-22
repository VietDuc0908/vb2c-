using System;
using AttendanceBusiness.ServiceContracts;
using AttendanceDAL;
using Framework.Data;
using LinqKit;

// NOTE: You can use the "Rename" command on the context menu to change the class name "Service1" in both code and config file together.
namespace AttendanceBusiness.ServiceImplementations
{
    public partial class AttendanceBusiness : IAttendanceBusiness
    {
        public System.Data.DataTable GET_REPORT()
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    var lst = rep.GET_REPORT();
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public List<Se_ReportDTO> GetReportById(Se_ReportDTO _filter, int PageIndex, int PageSize, ref int Total, UserLog log, string Sorts = "CODE ASC")
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    var lst = rep.GetReportById(_filter, PageIndex, PageSize, Total, log, Sorts);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public System.Data.DataTable GETORGNAME(ParamDTO obj, UserLog log)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    var lst = rep.GETORGNAME(obj, log);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public System.Data.DataSet GET_AT001(ParamDTO obj, UserLog log)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    var lst = rep.GET_AT001(obj, log);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public System.Data.DataSet GET_AT002(ParamDTO obj, UserLog log)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    var lst = rep.GET_AT002(obj, log);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public System.Data.DataSet GET_AT003(ParamDTO obj, UserLog log)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    var lst = rep.GET_AT003(obj, log);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public System.Data.DataSet GET_AT004(ParamDTO obj, UserLog log)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    var lst = rep.GET_AT004(obj, log);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public System.Data.DataSet GET_AT005(ParamDTO obj, UserLog log)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    var lst = rep.GET_AT005(obj, log);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public System.Data.DataSet GET_AT006(ParamDTO obj, UserLog log)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    var lst = rep.GET_AT006(obj, log);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public System.Data.DataSet GET_AT007(ParamDTO obj, UserLog log)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    var lst = rep.GET_AT007(obj, log);
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
