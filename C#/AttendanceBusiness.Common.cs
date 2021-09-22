using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using AttendanceBusiness.ServiceContracts;
using AttendanceDAL;
using Framework.Data;
using LinqKit;

// NOTE: You can use the "Rename" command on the context menu to change the class name "Service1" in both code and config file together.
namespace AttendanceBusiness.ServiceImplementations
{
    public partial class AttendanceBusiness
    {
        public System.Data.DataTable LOAD_PERIOD(AT_PERIODDTO obj, UserLog log)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    var lst = rep.LOAD_PERIOD(obj, log);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public List<AT_PERIODDTO> LOAD_PERIODBylinq(AT_PERIODDTO obj, UserLog log)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    var lst = rep.LOAD_PERIODBylinq(obj, log);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public AT_PERIODDTO LOAD_PERIODByID(AT_PERIODDTO obj, UserLog log)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    var lst = rep.LOAD_PERIODByID(obj, log);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public System.Data.DataTable GetTerminal(AT_TERMINALSDTO obj, UserLog log)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    var lst = rep.GetTerminal(obj, log);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public System.Data.DataTable GetTerminalMeal(AT_TERMINALSDTO obj, UserLog log)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    var lst = rep.GetTerminalMeal(obj, log);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public System.Data.DataTable GetTerminalAuto()
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    var lst = rep.GetTerminalAuto();
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool UpdateTerminalLastTime(AT_TERMINALSDTO obj, bool isMeal = false)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.UpdateTerminalLastTime(obj, isMeal);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool UpdateTerminalStatus(AT_TERMINALSDTO obj, bool isMeal = false)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.UpdateTerminalStatus(obj, isMeal);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public System.Data.DataSet GetDataFromOrg(ParamDTO obj, UserLog log)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    var lst = rep.GetDataFromOrg(obj, log);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool CLOSEDOPEN_PERIOD(ParamDTO param, Framework.Data.UserLog log)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    var lst = rep.CLOSEDOPEN_PERIOD(param, log);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool IS_PERIOD_PAYSTATUS(ParamDTO param, bool isAfter, Framework.Data.UserLog log)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    var lst = rep.IS_PERIOD_PAYSTATUS(param, isAfter, log);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool IS_PERIODSTATUS(ParamDTO param, Framework.Data.UserLog log)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    var lst = rep.IS_PERIODSTATUS(param, log);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool IS_PERIODSTATUS_BY_DATE(ParamDTO param, Framework.Data.UserLog log)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    var lst = rep.IS_PERIODSTATUS_BY_DATE(param, log);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool CheckPeriodClose(List<decimal> lstEmp, DateTime startdate, DateTime enddate, ref string sAction)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    var lst = rep.CheckPeriodClose(lstEmp, startdate, enddate, sAction);
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
