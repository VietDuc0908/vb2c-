using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using AttendanceBusiness.ServiceContracts;
using AttendanceDAL;
using Framework.Data;
using LinqKit;

// NOTE: You can use the "Rename" command on the context menu to change the class name "Service1" in both code and config file together.
namespace AttendanceBusiness.ServiceImplementations
{
    public partial class AttendanceBusiness : IAttendanceBusiness
    {
        public bool GetComboboxData(ref ComboBoxDataDTO cbxData)
        {
            using (AttendanceRepository rep = new AttendanceRepository())
            {
                try
                {
                    return rep.GetComboboxData(cbxData);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
