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
using Common.CommonBusiness;
using Framework.UI;

public class LogHelper
{
    public static Dictionary<string, OnlineUser> OnlineUsers { get; set; }

    public static UserDTO CurrentUser
    {
        get
        {
            return HttpContext.Current.Session("CurrentUser");
        }
        set
        {
            HttpContext.Current.Session("CurrentUser") = value;
        }
    }

    public static byte[] ImageUser
    {
        get
        {
            return HttpContext.Current.Session("ImageUser");
        }
        set
        {
            HttpContext.Current.Session("ImageUser") = value;
        }
    }

    public static void UpdateOnlineUser(UserDTO user, string currentApp)
    {
        if (OnlineUsers == null)
            OnlineUsers = new Dictionary<string, OnlineUser>();

        if (user != null)
        {
            if (!OnlineUsers.ContainsKey(HttpContext.Current.Session.SessionID))
                OnlineUsers.Add(HttpContext.Current.Session.SessionID, new OnlineUser() { User = user, LoginDate = DateTime.Now, LastAccessDate = DateTime.Now, ComputerName = System.Security.Principal.WindowsIdentity.GetCurrent().Name, CurrentApp = currentApp, IP = HttpContext.Current.Request.UserHostAddress });
        }
    }

    public static string ViewName
    {
        get
        {
            return HttpContext.Current.Session("ViewName");
        }
        set
        {
            HttpContext.Current.Session("ViewName") = value;
        }
    }

    public static string ViewDescription
    {
        get
        {
            return HttpContext.Current.Session("ViewDescription");
        }

        set
        {
            HttpContext.Current.Session("ViewDescription") = value;
        }
    }

    public static string ViewGroup
    {
        get
        {
            return HttpContext.Current.Session("ViewGroup");
        }

        set
        {
            HttpContext.Current.Session("ViewGroup") = value;
        }
    }


    public static string ActionName
    {
        get
        {
            return HttpContext.Current.Session("ActionName");
        }

        set
        {
            HttpContext.Current.Session("ActionName") = value;
        }
    }

    public static void UpdateAccessLog(ViewBase view)
    {
        try
        {
            if (OnlineUsers != null && OnlineUsers.ContainsKey(HttpContext.Current.Session.SessionID) && view.Allow)
            {
                ViewName = view.ViewName;
                var user = OnlineUsers[HttpContext.Current.Session.SessionID];
                user.LastAccessDate = DateTime.Now;
                if (user.AccessFunctions == null)
                    user.AccessFunctions = new Dictionary<string, string>();
                if (!user.AccessFunctions.ContainsKey(view.ViewName))
                    user.AccessFunctions.Add(view.ViewName, view.ViewDescription);
                if (view.ViewName.ToUpper != "CTRLONLINEUSER")
                {
                    user.CurrentViewName = view.ViewName;
                    user.CurrentViewDesc = view.ViewDescription;
                }
                OnlineUsers[HttpContext.Current.Session.SessionID] = user;
                UpdateAllOnlineUserStatus();
            }
        }
        catch (Exception ex)
        {
        }
    }

    public static void UpdateAllOnlineUserStatus()
    {
        foreach (var item in OnlineUsers)
        {
            if (item.Value != null)
            {
                if (item.Value.Status != "KILLED")
                {
                    if ((DateTime.Now - item.Value.LastAccessDate).TotalMinutes > CommonConfig.ActiveTimeout)
                        item.Value.Status = "INACTIVE";
                    else
                        item.Value.Status = "ACTIVE";
                }
            }
        }
    }


    public static void Kill(string SessionID)
    {
        try
        {
            if (OnlineUsers != null && OnlineUsers.ContainsKey(HttpContext.Current.Session.SessionID))
                OnlineUsers[SessionID].Status = "KILLED";
        }
        catch (Exception ex)
        {
        }
    }

    public static string GetSessionStatus(string SessionID)
    {
        try
        {
            if (OnlineUsers != null && OnlineUsers.ContainsKey(HttpContext.Current.Session.SessionID))
                return OnlineUsers[SessionID].Status;
        }
        catch (Exception ex)
        {
        }
        return "";
    }

    public static string GetSessionCurrentApp(string SessionID)
    {
        try
        {
            if (OnlineUsers != null && OnlineUsers.ContainsKey(HttpContext.Current.Session.SessionID))
                return OnlineUsers[SessionID].CurrentApp;
        }
        catch (Exception ex)
        {
        }
        return "";
    }

    public static void SaveAccessLog(string SessionID, string LoginStatus)
    {
        try
        {
            if (LogHelper.OnlineUsers != null && LogHelper.OnlineUsers.ContainsKey(SessionID))
            {
                OnlineUser user = LogHelper.OnlineUsers[SessionID];
                CommonRepository rep = new CommonRepository();
                AccessLog accessLog = new AccessLog() { Username = user.User.USERNAME, Fullname = user.User.FULLNAME, Email = user.User.EMAIL, Mobile = user.User.TELEPHONE, AccessFunctions = user.AccessFunctionsStr, LoginDate = user.LoginDate, LogoutDate = DateTime.Now, LogoutStatus = LoginStatus, IP = user.IP, ComputerName = user.ComputerName };
                // Update access log
                rep.InsertAccessLog(accessLog);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static CommonBusiness.UserLog GetUserLog()
    {
        return new UserLog() { Username = Common.GetUsername, Fullname = LogHelper.CurrentUser.FULLNAME, Email = LogHelper.CurrentUser.EMAIL, Mobile = LogHelper.CurrentUser.TELEPHONE, ComputerName = System.Security.Principal.WindowsIdentity.GetCurrent().Name, Ip = HttpContext.Current.Request.UserHostAddress, ViewName = LogHelper.ViewName, ViewDescription = LogHelper.ViewDescription, ViewGroup = LogHelper.ViewGroup, ActionName = LogHelper.ActionName };
    }
}


public class OnlineUser
{
    public UserDTO User { get; set; }
    public DateTime LoginDate { get; set; }
    public int LoginTime
    {
        get
        {
            return (DateTime.Now - LoginDate).TotalMinutes;
        }
    }

    public DateTime LastAccessDate { get; set; }

    public int NoAccessTime
    {
        get
        {
            return (DateTime.Now - LastAccessDate).TotalMinutes;
        }
    }

    // ACTIVE, INACTIVE, KILLED
    public string Status { get; set; } = "ACTIVE";

    public Dictionary<string, string> AccessFunctions { get; set; }
    public string CurrentViewDesc { get; set; }
    public string CurrentViewName { get; set; }
    public string CurrentApp { get; set; }
    public string AccessFunctionsStr
    {
        get
        {
            if (AccessFunctions != null)
            {
                List<string> lst = (from p in AccessFunctions
                                    select p.Value).ToList();
                return string.Join(";", lst);
            }
            return "";
        }
    }


    public string IP { get; set; }
    public string ComputerName { get; set; }
}
