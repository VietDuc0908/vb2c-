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

public static class ExtensionMethods
{
    public static DateTime FirstDateOfMonth(this DateTime currentDate)
    {
        return currentDate.ToString("01/MM/yyyy").ToDate().Value;
    }

    public static DateTime LastDateOfMonth(this DateTime currentDate)
    {
        return currentDate.FirstDateOfMonth().AddMonths(1).AddDays(-1);
    }

    public static DateTime FirstDateOfWeek(this DateTime currentDate)
    {
        return currentDate.AddDays(DayOfWeek.Monday - currentDate.DayOfWeek == DayOfWeek.Sunday ? DayOfWeek.Saturday + 1 : currentDate.DayOfWeek);
    }

    public static DateTime LastDateOfWeek(this DateTime currentDate)
    {
        return currentDate.FirstDateOfWeek().AddDays(6);
    }



    public static DateTime? ToDate(this string input)
    {
        DateTime retValue;

        if (DateTime.TryParse(input, CultureInfo.CreateSpecificCulture("vi-VN"), System.Globalization.DateTimeStyles.None, ref retValue))
            return retValue;
        else
            return default(Date?);
    }
}
