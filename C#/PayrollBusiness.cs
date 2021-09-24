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
using PayrollBusiness.ServiceContracts;
using PayrollDAL;
using Framework.Data;

namespace PayrollBusiness.ServiceImplementations
{
    public partial class PayrollBusiness : ServiceContracts.IPayrollBusiness
    {
        #region "Test Service"
        public string TestService(string str)
        {
            return "Hello world " + str;
        }
        #endregion

        public bool GetComboboxData(ref ComboBoxDataDTO cbxData)
        {
            using (PayrollRepository rep = new PayrollRepository())
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

        public bool CheckExistInDatabase(List<decimal> lstID, TABLE_NAME table)
        {
            using (PayrollRepository rep = new PayrollRepository())
            {
                try
                {
                    return rep.CheckExistInDatabase(lstID, table);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        public bool IS_PERIODSTATUS(PA_ParamDTO _param, UserLog log)
        {
            try
            {
                var lst = PayrollRepositoryStatic.Instance.IS_PERIODSTATUS(_param, log);
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool IS_PERIOD_COLEXSTATUS(PA_ParamDTO _param, UserLog log)
        {
            try
            {
                var lst = PayrollRepositoryStatic.Instance.IS_PERIOD_COLEXSTATUS(_param, log);
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
