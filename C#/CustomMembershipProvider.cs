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
using System.Threading;

public class CustomMembershipProvider : MembershipProvider
{
    private LoginError _error;
    public LoginError GetError
    {
        get
        {
            return _error;
        }
    }
    public var LoginFailCount
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
                else if (user.PASSWORD == encry.EncryptString(newPassword))
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
            using (CommonRepository rep = new CommonRepository())
            {
                var user = rep.GetUserWithPermision(username);
                using (EncryptData encry = new EncryptData())
                {
                    if (user == null)
                        throw new Exception(LoginError.USERNAME_NOT_EXISTS);
                    if (user.IS_AD)
                    {
                        LdapDTO LdapCorrect;
                        var email = username.Substring(username.IndexOf("@") + 1).ToLower();
                        foreach (var item in CommonConfig.ListLDAP)
                        {
                            if (item.DOMAIN_NAME.ToLower == email)
                            {
                                LdapCorrect = item;
                                break;
                            }
                        }
                        if (LdapCorrect != null)
                            bSuccess = TVC.LDAP.ADManager.IsUserExists(LdapCorrect.LDAP_NAME, username.Substring(0, username.IndexOf("@")).ToLower(), password, LdapCorrect.BASE_DN);
                        else
                            throw new Exception(LoginError.LDAP_SERVER_NOT_FOUND);
                        if (!bSuccess)
                            throw new Exception(LoginError.WRONG_USERNAME_OR_PASSWORD);
                    }
                    else if (!user.IS_USER_PERMISSION)
                        throw new Exception(LoginError.NO_PERMISSION);
                    else if (user.ACTFLG == "I")
                        throw new Exception(LoginError.USER_LOCKED);
                    else if (user.PASSWORD == encry.EncryptString(password))
                    {
                        if (user.EFFECT_DATE > DateTime.Now || (user.EXPIRE_DATE != null && user.EXPIRE_DATE <= DateTime.Now))
                            throw new Exception(LoginError.USERNAME_EXPIRED);

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
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return bSuccess;
    }

    public override string ApplicationName
    {
        get
        {
            throw new NotImplementedException();
        }
        set
        {
        }
    }

    public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
    {
        throw new NotImplementedException();
    }

    public override System.Web.Security.MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, ref System.Web.Security.MembershipCreateStatus status)
    {
        throw new NotImplementedException();
    }

    public override bool DeleteUser(string username, bool deleteAllRelatedData)
    {
        throw new NotImplementedException();
    }

    public override bool EnablePasswordReset
    {
        get
        {
            throw new NotImplementedException();
        }
    }

    public override bool EnablePasswordRetrieval
    {
        get
        {
            throw new NotImplementedException();
        }
    }

    public override System.Web.Security.MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, ref int totalRecords)
    {
        throw new NotImplementedException();
    }

    public override System.Web.Security.MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, ref int totalRecords)
    {
        throw new NotImplementedException();
    }

    public override System.Web.Security.MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, ref int totalRecords)
    {
        throw new NotImplementedException();
    }

    public override int GetNumberOfUsersOnline()
    {
        throw new NotImplementedException();
    }

    public override string GetPassword(string username, string answer)
    {
        CommonRepository rep = new CommonRepository();
        return rep.GetPassword(username);
    }

    public new override System.Web.Security.MembershipUser GetUser(object providerUserKey, bool userIsOnline)
    {
        throw new NotImplementedException();
    }

    public new override System.Web.Security.MembershipUser GetUser(string username, bool userIsOnline)
    {
        throw new NotImplementedException();
    }

    public override string GetUserNameByEmail(string email)
    {
        throw new NotImplementedException();
    }

    public override int MaxInvalidPasswordAttempts
    {
        get
        {
            throw new NotImplementedException();
        }
    }

    public override int MinRequiredNonAlphanumericCharacters
    {
        get
        {
            throw new NotImplementedException();
        }
    }

    public override int MinRequiredPasswordLength
    {
        get
        {
            throw new NotImplementedException();
        }
    }

    public override int PasswordAttemptWindow
    {
        get
        {
            throw new NotImplementedException();
        }
    }

    public override System.Web.Security.MembershipPasswordFormat PasswordFormat
    {
        get
        {
            throw new NotImplementedException();
        }
    }

    public override string PasswordStrengthRegularExpression
    {
        get
        {
            throw new NotImplementedException();
        }
    }

    public override bool RequiresQuestionAndAnswer
    {
        get
        {
            throw new NotImplementedException();
        }
    }

    public override bool RequiresUniqueEmail
    {
        get
        {
            throw new NotImplementedException();
        }
    }

    public override string ResetPassword(string username, string answer)
    {
        throw new NotImplementedException();
    }

    public override void UpdateUser(System.Web.Security.MembershipUser user)
    {
        throw new NotImplementedException();
    }
}
