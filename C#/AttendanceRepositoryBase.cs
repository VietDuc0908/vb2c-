using System;
using Attendance;
using Framework.UI;
using Common.CommonBusiness;
using Common;

// Imports Attendance.CommonBusiness

public class AttendanceRepositoryBase : IDisposable
{
    // To detect redundant calls
    private bool disposed = false;
    private const string SESSNAME_ATTENDANCEBUSINESSCLIENT = "AttendanceBusinessClient";



    public UserLog Log
    {
        get
        {
            return LogHelper.GetUserLog;
        }
    }

    // IDisposable
    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
            }
        }
        this.disposed = true;
    }

    // This code added by Visual Basic to 
    // correctly implement the disposable pattern.
    public void Dispose() : IDisposable.Dispose
    {
        // Do not change this code. 
        // Put cleanup code in
        // Dispose(ByVal disposing As Boolean) above.
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~AttendanceRepositoryBase()
    {
        // Do not change this code. 
        // Put cleanup code in
        // Dispose(ByVal disposing As Boolean) above.
        Dispose(false);
        base.Finalize();
    }
}
