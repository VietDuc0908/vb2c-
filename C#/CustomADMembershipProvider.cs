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
using Framework.UI;
using Common.CommonBusiness;

public class CustomADMembershipProvider : ActiveDirectoryMembershipProvider
{
    private var LoginFailCount
    {
        get
        {
            if (HttpContext.Current.Session(username + "_LoginFailCount") == null)
                return 0;
            return HttpContext.Current.Session(username + "_LoginFailCount");
        }
    }
    private void SetLoginFailCount(string username, int value)
    {
        HttpContext.Current.Session(username + "_LoginFailCount") = value;
    }

    public string domainName { get; set; } = "";

    public override bool ChangePassword(string username, string oldPassword, string newPassword)
    {
        try
        {
            CommonRepository rep = new CommonRepository();
            var user = rep.GetUserWithPermision(username);
            using (EncryptData encry = new EncryptData())
            {
                if (user == null)
                    return false;
                else if (user.IS_AD)
                    return false;
                else if (encry.DecryptString(user.PASSWORD) != oldPassword)
                    return false;
                else
                    return rep.ChangeUserPassword(username, oldPassword, newPassword);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public override bool UnlockUser(string username)
    {
        try
        {
            CommonRepository rep = new CommonRepository();
            return rep.UpdateUserStatus(username, "A");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private bool LockUser(string username)
    {
        try
        {
            CommonRepository rep = new CommonRepository();
            return rep.UpdateUserStatus(username, "I");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
    {
        try
        {
            if (config.AllKeys.Contains("domainName"))
            {
                domainName = config["domainName"];
                config.Remove("domainName");
            }
            base.Initialize(name, config);
        }
        catch (Exception ex)
        {
            throw new Exception(LoginError.LDAP_SERVER_NOT_FOUND);
        }
    }

    public override bool ValidateUser(string username, string password)
    {
        // if User dont have account return false
        bool bSuccess = false;
        try
        {
            CommonRepository rep = new CommonRepository();


            var user = rep.GetUserWithPermision(username);
            using (EncryptData encry = new EncryptData())
            {
                if (user == null)
                    throw new Exception(LoginError.USERNAME_NOT_EXISTS);
                if (user.IS_AD)
                {
                    bSuccess = base.ValidateUser(username + domainName, password);
                    if (!bSuccess)
                        throw new Exception(LoginError.WRONG_USERNAME_OR_PASSWORD);
                }
                else if (user.ACTFLG == "I")
                    throw new Exception(LoginError.USER_LOCKED);
                else if (encry.DecryptString(user.PASSWORD) == password)
                {
                    if (!user.IS_USER_PERMISSION)
                        throw new Exception(LoginError.NO_PERMISSION);
                    else if (user.EFFECT_DATE > DateTime.Now || (user.EXPIRE_DATE != null && user.EXPIRE_DATE <= DateTime.Now))
                        throw new Exception(LoginError.USERNAME_EXPIRED);
                    else
                        bSuccess = true;
                }
                else
                {
                    bool bIsAdmin = false;
                    if (user.MODULE_ADMIN != "")
                        bIsAdmin = true;
                    if (!bIsAdmin)
                    {
                        int maxFail;
                        maxFail = CommonConfig.MaxNumberLoginFail;
                        SetLoginFailCount(username, LoginFailCount(username) + 1);
                        if (LoginFailCount(username) >= maxFail)
                            LockUser(username);
                    }
                    throw new Exception(LoginError.WRONG_PASSWORD);
                }
            }


            if (bSuccess & user.IS_APP)
            {
                LogHelper.UpdateOnlineUser(user, "Main");
                LogHelper.CurrentUser = user;
            }
            else if (bSuccess)
                throw new Exception(LoginError.NO_PERMISSION);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return bSuccess;
    }
}
