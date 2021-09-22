using System;
using Attendance.AttendanceBusiness;
using Framework.UI;

public class AttendanceRepository : AttendanceRepositoryBase
{
    private bool _isAvailable;
    public bool GetComboboxData(ref ComboBoxDataDTO cbxData)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.GetComboboxData(cbxData);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }
}
