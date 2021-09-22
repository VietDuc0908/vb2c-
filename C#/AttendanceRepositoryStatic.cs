using System;
using Microsoft.VisualBasic;
using System.Configuration;

public class AttendanceRepositoryStatic
{
    public static AttendanceRepository Instance
    {
        get
        {
            return new AttendanceRepository();
        }
    }
}
