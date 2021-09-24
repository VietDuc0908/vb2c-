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
using System.Transactions;
using System.Web;
using Framework.Data;
using System.Data.Objects;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.EntityClient;
using Framework.Data.System.Linq.Dynamic;
using System.Data.Entity;
using System.Text.RegularExpressions;
using System.Data.Objects.DataClasses;
using Framework.Data.SystemConfig;
using Aspose.Words;

public class PayrollRepository : PayrollRepositoryBase
{
    public Dictionary<string, string> GetConfig(ModuleID eModule)
    {
        using (SystemConfig config = new SystemConfig())
        {
            try
            {
                return config.GetConfig(eModule);
            }
            catch (Exception ex)
            {
                WriteExceptionLog(ex, MethodBase.GetCurrentMethod().Name, "iPayroll");
                throw ex;
            }
        }
    }

    /// <summary>
    ///     ''' Gets the combobox data.
    ///     ''' </summary>
    ///     ''' <param name="cbxData">The CBX data.</param>
    ///     ''' <returns></returns>
    public bool GetComboboxData(ref ComboBoxDataDTO cbxData)
    {
        try
        {

            // Danh loại bảng lương
            if (cbxData.GET_TYPE_PAYMENT)
                cbxData.LIST_TYPE_PAYMENT = (from p in Context.OT_OTHER_LIST
                                             join t in Context.OT_OTHER_LIST_TYPE on p.TYPE_ID equals t.ID
                                             where p.ACTFLG == "A" & t.CODE == "TYPE_PAYMENT"
                                             orderby p.CREATED_DATE descending
                                             select new OT_OTHERLIST_DTO()
                                             {
                                                 ID = p.ID,
                                                 CODE = p.CODE,
                                                 NAME_EN = p.NAME_EN,
                                                 NAME_VN = p.NAME_VN,
                                                 TYPE_ID = p.TYPE_ID
                                             }).ToList;
            // Danh sách các đối tượng cư trú
            if (cbxData.GET_LIST_RESIDENT)
                cbxData.LIST_LIST_RESIDENT = (from p in Context.OT_OTHER_LIST
                                              join t in Context.OT_OTHER_LIST_TYPE on p.TYPE_ID equals t.ID
                                              where p.ACTFLG == "A" & t.CODE == "PA_RESIDENT"
                                              orderby p.CREATED_DATE descending
                                              select new OT_OTHERLIST_DTO()
                                              {
                                                  ID = p.ID,
                                                  CODE = p.CODE,
                                                  NAME_EN = p.NAME_EN,
                                                  NAME_VN = p.NAME_VN,
                                                  TYPE_ID = p.TYPE_ID
                                              }).ToList;
            // Danh sách các khoản tiền 
            if (cbxData.GET_LIST_PAYMENT)
                cbxData.LIST_LIST_PAYMENT = (from p in Context.OT_OTHER_LIST
                                             join t in Context.OT_OTHER_LIST_TYPE on p.TYPE_ID equals t.ID
                                             where p.ACTFLG == "A" & t.CODE == "PA_LISTPAYMENT"
                                             orderby p.CREATED_DATE descending
                                             select new OT_OTHERLIST_DTO()
                                             {
                                                 ID = p.ID,
                                                 CODE = p.CODE,
                                                 NAME_EN = p.NAME_EN,
                                                 NAME_VN = p.NAME_VN,
                                                 TYPE_ID = p.TYPE_ID
                                             }).ToList;
            // Danh sách các khoản tiền trong bảng lương
            // TYPE_PAYMENT = 4123 là danh mục bảng lương tổng hợp
            if (cbxData.GET_LIST_SALARY)
                cbxData.LIST_LIST_SALARY = (from p in Context.PA_LISTSALARIES
                                            where p.STATUS == "A" & p.TYPE_PAYMENT == 4123
                                            orderby p.CREATED_DATE descending
                                            select new PAListSalariesDTO()
                                            {
                                                ID = p.ID,
                                                NAME_EN = p.NAME_EN,
                                                NAME_VN = p.NAME_VN
                                            }).ToList;
            // Danh sách các đối tượng lương(bảng lương)
            if (cbxData.GET_OBJECT_PAYMENT)
                cbxData.LIST_OBJECT_PAYMENT = (from p in Context.PA_OBJECT_SALARY
                                               where p.ACTFLG == "A"
                                               orderby p.CREATED_DATE descending
                                               select new PAObjectSalaryDTO()
                                               {
                                                   ID = p.ID,
                                                   CODE = p.CODE,
                                                   NAME_VN = p.NAME_VN,
                                                   NAME_EN = p.NAME_EN
                                               }).ToList;
            // '''''''''''''''''''''
            if (cbxData.GET_SALARY_LEVEL)
                cbxData.LIST_SALARY_LEVEL = (from p in Context.PA_SALARY_LEVEL
                                             join o in Context.PA_SALARY_GROUP on p.SAL_GROUP_ID equals o.ID
                                             where p.ACTFLG == "A"
                                             orderby p.NAME.ToUpper
                                             select new SalaryLevelDTO()
                                             {
                                                 ID = p.ID,
                                                 NAME = p.NAME,
                                                 SAL_GROUP_ID = p.SAL_GROUP_ID
                                             }).ToList;
            // '''''''''''''''''''''

            if (cbxData.GET_SALARY_GROUP)
                cbxData.LIST_SALARY_GROUP = (from p in Context.PA_SALARY_GROUP
                                             orderby p.NAME.ToUpper
                                             select new SalaryGroupDTO()
                                             {
                                                 ID = p.ID,
                                                 NAME = p.NAME,
                                                 EFFECT_DATE = p.EFFECT_DATE
                                             }).ToList;

            return true;
        }
        catch (Exception ex)
        {
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod().Name, "iPayroll");
            // Utility.WriteExceptionLog(ex, Me.ToString() & ".GetComboboxData")
            return false;
        }
        finally
        {
        }
    }

    /// <summary>
    ///     ''' Kiểm tra dữ liệu đã được sử dụng hay chưa?
    ///     ''' </summary>
    ///     ''' <param name="table">Enum Table_Name</param>
    ///     ''' <returns>true:chưa có/false:có rồi</returns>
    ///     ''' <remarks></remarks>
    ///     '''
    public bool CheckExistInDatabase(List<decimal> lstID, TABLE_NAME table)
    {
        bool isExist = false;
        string strListID = lstID.Select(x => x.ToString()).Aggregate((x, y) => x + "," + y);
        decimal count = 0;
        try
        {
            switch (table)
            {
                case TABLE_NAME.PA_SALARY_GROUP:
                    {
                        isExist = Execute_ExistInDatabase("PA_SALARY_LEVEL", "SAL_GROUP_ID", strListID);
                        if (!isExist)
                            return isExist;
                        isExist = Execute_ExistInDatabase("HU_WORKING", "SAL_GROUP_ID", strListID);
                        if (!isExist)
                            return isExist;
                        break;
                    }

                case TABLE_NAME.PA_SALARY_LEVEL:
                    {
                        isExist = Execute_ExistInDatabase("PA_SALARY_RANK", "SAL_LEVEL_ID", strListID);
                        if (!isExist)
                            return isExist;
                        isExist = Execute_ExistInDatabase("HU_WORKING", "SAL_LEVEL_ID", strListID);
                        if (!isExist)
                            return isExist;
                        break;
                    }

                case TABLE_NAME.PA_SALARY_RANK:
                    {
                        isExist = Execute_ExistInDatabase("HU_WORKING", "SAL_RANK_ID", strListID);
                        if (!isExist)
                            return isExist;
                        break;
                    }

                case TABLE_NAME.AT_PERIOD:
                    {
                        isExist = Execute_ExistInDatabase("PA_PAYROLLSHEET_TEMP", "PERIOD_ID", strListID);
                        if (!isExist)
                            return isExist;
                        isExist = Execute_ExistInDatabase("PA_PAYROLLSHEET_SUM", "PERIOD_ID", strListID);
                        if (!isExist)
                            return isExist;
                        break;
                    }
            }
            return true;
        }
        catch (Exception ex)
        {
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod().Name, "iPayroll");
            return false;
        }
    }


    private void Execute_ExistInDatabase(string tableName, string colName, string strListID)
    {
        try
        {
            decimal count = 0;
            var Sql = "SELECT COUNT(" + colName + ") FROM " + tableName + " WHERE " + colName + " IN (" + strListID + ")";
            count = Context.ExecuteStoreQuery<decimal>(Sql).FirstOrDefault;
            if (count > 0)
                return false;
            return true;
        }
        catch (Exception ex)
        {
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod().Name, "iPayroll");
            throw ex;
        }
    }

    public List<Se_ReportDTO> GetReportById(Se_ReportDTO _filter, int PageIndex, int PageSize, ref int Total, UserLog log, string Sorts = "CODE ASC")
    {
        try
        {
            IQueryable<Se_ReportDTO> query;
            if (log.Username.ToUpper != "ADMIN" & log.Username.ToUpper != "SYS.ADMIN" & log.Username.ToUpper != "HR.ADMIN")
                  //query = From u In Context.SE_USER
                  //      From p In u.SE_REPORT
                  //      Where u.USERNAME.ToUpper = log.Username.ToUpper And p.MODULE_ID = _filter.MODULE_ID
                  //      Select New Se_ReportDTO With {.ID = p.ID,
                  //                                      .CODE = p.CODE,
                  //                                      .NAME = p.NAME,
                  //                                      .MODULE_ID = p.MODULE_ID}


            else
                query = from p in Context.SE_REPORT
                        where p.MODULE_ID == _filter.MODULE_ID
                        select new Se_ReportDTO() { ID = p.ID, CODE = p.CODE, NAME = p.NAME, MODULE_ID = p.MODULE_ID };
            var lst = query;
            if (_filter.CODE != "")
                lst = lst.Where(p => p.CODE.ToUpper.Contains(_filter.CODE.ToUpper));
            if (_filter.NAME != "")
                lst = lst.Where(p => p.NAME.ToUpper.Contains(_filter.NAME.ToUpper));


            lst = lst.OrderBy(Sorts);
            Total = lst.Count;
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize);

            return lst.ToList;
        }
        catch (Exception ex)
        {
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod().Name, "iPayroll");
            throw ex;
        }
    }

    public DataSet ExportReport(string sPkgName, decimal sMoth, decimal sYear, string sOrgId, decimal IsDissolve, UserLog log)
    {
        try
        {
            using (DataAccess.QueryData cls = new DataAccess.QueryData())
            {
                DataSet dtData = cls.ExecuteStore(sPkgName, new { P_ORG = sOrgId, P_ISDISSOLVE = IsDissolve, P_USERNAME = log.Username, P_MOTH = sMoth, P_YEAR = sYear, P_CUR = cls.OUT_CURSOR, P_CUR1 = cls.OUT_CURSOR }, false);

                return dtData;
            }
        }
        catch (Exception ex)
        {
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod().Name, "iPayroll");
            throw ex;
        }
    }

    public DataSet ExportReport_014(string sPkgName, decimal sMoth, decimal sYear, string sOrgId, decimal IsDissolve, UserLog log)
    {
        try
        {
            using (DataAccess.QueryData cls = new DataAccess.QueryData())
            {
                DataSet dsData = cls.ExecuteStore(sPkgName, new { P_ORG = sOrgId, P_ISDISSOLVE = IsDissolve, P_USERNAME = log.Username, P_MOTH = sMoth, P_YEAR = sYear, DATA = cls.OUT_CURSOR, PARAM = cls.OUT_CURSOR }, false);

                dsData.Tables(0).TableName = "DATA";
                dsData.Tables(1).TableName = "PARAM";

                return dsData;
            }
        }
        catch (Exception ex)
        {
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod().Name, "iPayroll");
            throw ex;
        }
    }

    public DataSet ExportReport_010(string sPkgName, decimal sMoth, decimal sYear, string sOrgId, decimal IsDissolve, UserLog log)
    {
        try
        {
            using (DataAccess.QueryData cls = new DataAccess.QueryData())
            {
                DataSet dtData = cls.ExecuteStore(sPkgName, new { P_ORG = sOrgId, P_ISDISSOLVE = IsDissolve, P_USERNAME = log.Username, P_MOTH = sMoth, P_YEAR = sYear, P_CUR = cls.OUT_CURSOR, P_CUR1 = cls.OUT_CURSOR, P_CUR2 = cls.OUT_CURSOR }, false);

                return dtData;
            }
        }
        catch (Exception ex)
        {
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod().Name, "iPayroll");
            throw ex;
        }
    }

    public DataSet ExportReport_008(string sPkgName, decimal sMoth, decimal sYear, string sOrgId, decimal IsDissolve, UserLog log)
    {
        try
        {
            using (DataAccess.QueryData cls = new DataAccess.QueryData())
            {
                DataSet dsData = cls.ExecuteStore(sPkgName, new { P_ORG = sOrgId, P_ISDISSOLVE = IsDissolve, P_USERNAME = log.Username, P_MOTH = sMoth, P_YEAR = sYear, DATA = cls.OUT_CURSOR, DATA1 = cls.OUT_CURSOR, DATA2 = cls.OUT_CURSOR, DATA3 = cls.OUT_CURSOR, DATA4 = cls.OUT_CURSOR, PARAM = cls.OUT_CURSOR }, false);

                dsData.Tables(0).TableName = "DATA";
                dsData.Tables(1).TableName = "DATA1";
                dsData.Tables(2).TableName = "DATA2";
                dsData.Tables(3).TableName = "DATA3";
                dsData.Tables(4).TableName = "DATA4";
                dsData.Tables(5).TableName = "PARAM";

                return dsData;
            }
        }
        catch (Exception ex)
        {
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod().Name, "iPayroll");
            throw ex;
        }
    }

    public DataSet ExportReport_005(string sPkgName, decimal sMoth, decimal sYear, string sOrgId, decimal IsDissolve, UserLog log)
    {
        try
        {
            using (DataAccess.QueryData cls = new DataAccess.QueryData())
            {
                DataSet dsData = cls.ExecuteStore(sPkgName, new { P_ORG = sOrgId, P_ISDISSOLVE = IsDissolve, P_USERNAME = log.Username, P_MOTH = sMoth, P_YEAR = sYear, DATA = cls.OUT_CURSOR, DATA1 = cls.OUT_CURSOR, PARAM = cls.OUT_CURSOR }, false);

                dsData.Tables(0).TableName = "DATA";
                dsData.Tables(1).TableName = "DATA1";
                dsData.Tables(2).TableName = "PARAM";

                return dsData;
            }
        }
        catch (Exception ex)
        {
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod().Name, "iPayroll");
            throw ex;
        }
    }

    public DataTable ExportPhieuLuong(List<decimal> lstEmployee, decimal? orgID, decimal? isDissolve, decimal periodID, UserLog log)
    {
        try
        {
            using (DataAccess.NonQueryData cls = new DataAccess.NonQueryData())
            {
                cls.ExecuteSQL("DELETE FROM SE_EMPLOYEE_CHOSEN S WHERE S.USING_USER ='" + log.Username.ToUpper + "'");
            }

            foreach (decimal emp in lstEmployee)
            {
                SE_EMPLOYEE_CHOSEN objNew = new SE_EMPLOYEE_CHOSEN();
                objNew.EMPLOYEE_ID = emp;
                objNew.USING_USER = log.Username.ToUpper;
                Context.SE_EMPLOYEE_CHOSEN.AddObject(objNew);
            }
            Context.SaveChanges();

            using (DataAccess.QueryData cls = new DataAccess.QueryData())
            {
                var dtData = cls.ExecuteStore("PKG_PA_REPORT.GET_PHIEULUONG", new { P_USERNAME = log.Username, P_PERIOD = periodID, P_ORGID = orgID, P_ISDISSOLVE = isDissolve, P_SENDMAIL = 0, P_CUR = cls.OUT_CURSOR });

                return dtData;
            }
        }
        catch (Exception ex)
        {
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod().Name, "iPayroll");
            throw ex;
        }
    }

    public bool CheckAndSendPayslip()
    {
        try
        {
            // 1. Kiểm tra có action nào đang chạy hay không?
            var isExist = (from p in Context.PA_SEND_PAYSLIP
                           where p.ACTION_STATUS == 1
                           select p).Any;

            if (!isExist)
            {
                // 2. Nếu chưa có action nào chạy thì tiến hành chạy
                // Đổi trạng thái đang thực hiện để không bị chạy chồng chéo
                var objAction = (from p in Context.PA_SEND_PAYSLIP
                                 where p.ACTION_STATUS == 0 | p.ACTION_STATUS == 3
                                 select p).FirstOrDefault;
                if (objAction != null)
                {
                    objAction.ACTION_STATUS = 1;
                    Context.SaveChanges();
                    // 3. Tiến hành thực hiện
                    try
                    {
                        List<decimal> lstEmp = new List<decimal>();
                        if (objAction.ACTION_TYPE == 1)
                        {
                            lstEmp = (from p in Context.PA_SEND_PAYSLIP_EMP
                                      where p.PA_SEND_PAYSLIP_ID == objAction.ID
                                      select p.EMPLOYEE_ID.Value).ToList;


                            using (DataAccess.NonQueryData cls = new DataAccess.NonQueryData())
                            {
                                cls.ExecuteSQL("DELETE FROM SE_EMPLOYEE_CHOSEN S WHERE S.USING_USER ='" + objAction.USERNAME.ToUpper + "'");
                                string sql = "INSERT INTO SE_EMPLOYEE_CHOSEN" + Constants.vbNewLine + "  (EMPLOYEE_ID, USING_USER)" + Constants.vbNewLine + "  (SELECT DISTINCT EMPLOYEE_ID, '" + objAction.USERNAME.ToUpper + "'" + Constants.vbNewLine + "     FROM PA_SEND_PAYSLIP_EMP" + Constants.vbNewLine + "    WHERE PA_SEND_PAYSLIP_ID = " + objAction.ID.ToString + ")";

                                cls.ExecuteSQL(sql);
                            }
                        }
                        decimal rowTotal = 0;
                        objAction.RUN_START = DateTime.Now;
                        if (SendPayslip(lstEmp, objAction.ORG_ID, objAction.IS_DISSOLVE, objAction.PERIOD_ID, objAction.USERNAME.ToUpper, ref rowTotal))
                        {
                            objAction.RUN_ROW = rowTotal;
                            objAction.ACTION_STATUS = 2;
                        }
                        else
                            objAction.ACTION_STATUS = 3;
                        objAction.RUN_END = DateTime.Now;
                    }
                    catch (Exception ex)
                    {
                        WriteExceptionLog(ex, MethodBase.GetCurrentMethod().Name, "iPayroll");
                        objAction.ACTION_STATUS = 3;
                    }
                    Context.SaveChanges();
                }
            }
        }
        catch (Exception ex)
        {
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod().Name, "iPayroll");
            throw ex;
        }
    }

    public bool ActionSendPayslip(List<decimal> lstEmployee, decimal? orgID, decimal? isDissolve, decimal periodID, UserLog log)
    {
        try
        {
            PA_SEND_PAYSLIP objPayslip = new PA_SEND_PAYSLIP();
            objPayslip.ID = Utilities.GetNextSequence(Context, Context.PA_SEND_PAYSLIP.EntitySet.Name);
            objPayslip.ACTION_DATE = DateTime.Now;
            objPayslip.ACTION_STATUS = 0;
            objPayslip.ACTION_TYPE = 0;
            objPayslip.IS_DISSOLVE = isDissolve;
            objPayslip.ORG_ID = orgID;
            objPayslip.PERIOD_ID = periodID;
            objPayslip.USERNAME = log.Username.ToUpper;
            Context.PA_SEND_PAYSLIP.AddObject(objPayslip);
            lstEmployee = lstEmployee.Distinct().ToList();

            foreach (decimal emp in lstEmployee)
            {
                PA_SEND_PAYSLIP_EMP objNew = new PA_SEND_PAYSLIP_EMP();
                objNew.ID = Utilities.GetNextSequence(Context, Context.PA_SEND_PAYSLIP_EMP.EntitySet.Name);
                objNew.EMPLOYEE_ID = emp;
                objNew.PA_SEND_PAYSLIP_ID = objPayslip.ID;
                Context.PA_SEND_PAYSLIP_EMP.AddObject(objNew);
                objPayslip.ACTION_TYPE = 1;
            }
            Context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod().Name, "iPayroll");
            throw ex;
        }
    }

    public bool SendPayslip(List<decimal> lstEmployee, decimal? orgID, decimal? isDissolve, decimal periodID, string userName, ref decimal rowTotal)
    {
        try
        {
            using (DataAccess.QueryData cls = new DataAccess.QueryData())
            {
                DataTable dtData = cls.ExecuteStore("PKG_PA_REPORT.GET_PHIEULUONG", new { P_USERNAME = userName.ToUpper(), P_PERIOD = periodID, P_ORGID = orgID, P_ISDISSOLVE = isDissolve, P_SENDMAIL = 1, P_CUR = cls.OUT_CURSOR });

                bool isSave = false;
                var config = GetConfig(ModuleID.All);
                var emailFrom = config.ContainsKey("MailFrom") ? config["MailFrom"] : "";
                rowTotal = 0;
                DataTable dtDataMerge;
                foreach (DataRow row in dtData.Rows)
                {
                    string filePath = AppDomain.CurrentDomain.BaseDirectory + @"\ReportTemplates\Payroll\Report\PhieuLuong.docx";
                    Document doc = new Document(filePath);
                    if (doc != null && row("WORK_EMAIL").ToString != "" & emailFrom != "")
                    {
                        filePath = System.AppDomain.CurrentDomain.BaseDirectory + @"Payroll\Attachment\" + "PhieuLuong_" + row("EMPLOYEE_CODE").ToString + "_" + Strings.Format(DateTime.Now, "yyyyMMddHHmmss");
                        dtDataMerge = dtData.Clone;
                        dtDataMerge.ImportRow(row);
                        SendPhieuLuong_File(doc, ref filePath, dtDataMerge, false);
                        string strContent = "Kính gửi Anh/Chị,<br/><br/>" + "Phòng HCNS gửi Anh/Chị phiếu lương chi tiết như file đính kèm.<br/>" + "Mọi thắc mắc về tiền lương xin phản hồi tới cán bộ/đơn vị phụ trách hoặc thông tin liên hệ ở phần cuối của Phiếu lương.<br/><br/>" + "<p style='color:red'>Lưu ý đây là email gửi tự động từ Hệ thống Phần mềm Quản trị Nhân sự, Anh/Chị không phản hồi email này.</p>" + "Trân trọng,<br/><br/>" + "<p style='color:gray'><b>HỆ THỐNG PHẦN MỀM QUẢN TRỊ NHÂN SỰ</b><br/><i>(Được quản lý bởi Ban Hành chính Nhân sự)</i><br/><p>";

                        InsertMail(emailFrom, row("WORK_EMAIL").ToString, row("TITLE_NAME").ToString, strContent, filePath, "", "", "iPayroll_SendMail");
                        isSave = true;
                        rowTotal += 1;
                    }
                }
                if (isSave)
                    Context.SaveChanges();
            }
            return true;
        }
        catch (Exception ex)
        {
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod().Name, "iPayroll");
            throw ex;
        }
    }

    public void InsertMail(string _from, string _to, string _subject, string _content, string _attachment = "", string _cc = "", string _bcc = "", string _viewName = "")
    {
        try
        {
            SE_MAIL _newMail = new SE_MAIL();
            _newMail.ID = Utilities.GetNextSequence(Context, Context.SE_MAIL.EntitySet.Name);
            _newMail.MAIL_FROM = _from;
            _newMail.MAIL_TO = _to;
            _newMail.MAIL_CC = _cc;
            _newMail.MAIL_BCC = _bcc;
            _newMail.SUBJECT = _subject;
            _newMail.CONTENT = _content;
            _newMail.VIEW_NAME = _viewName;
            _newMail.ATTACHMENT = _attachment;
            _newMail.ACTFLG = "I";
            Context.SE_MAIL.AddObject(_newMail);
            return true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void SendPhieuLuong_File(Document doc, ref string filename, DataTable dtData, bool is2003 = true)
    {
        try
        {
            // Fill the fields in the document with user data.
            doc.MailMerge.Execute(dtData);
            // Send the document in Word format to the client browser with an option to save to disk or open inside the current browser.

            // doc.Save(filename, SaveFormat.Doc, SaveType.OpenInApplication, response)
            if (is2003)
            {
                filename = filename + ".doc";
                doc.Save(filename, Aspose.Words.Saving.SaveOptions.CreateSaveOptions(SaveFormat.Doc));
            }
            else
            {
                filename = filename + ".docx";
                doc.Save(filename, Aspose.Words.Saving.SaveOptions.CreateSaveOptions(SaveFormat.Docx));
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    public bool IS_PERIODSTATUS(PA_ParamDTO _param, UserLog log)
    {
        try
        {
            using (DataAccess.QueryData cls = new DataAccess.QueryData())
            {
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG", new { P_USERNAME = log.Username.ToUpper, P_ORGID = _param.ORG_ID, P_ISDISSOLVE = _param.IS_DISSOLVE });
            };

            //Dim query = (From p In Context.AT_PERIOD
            //            From po In Context.AT_ORG_PERIOD.Where(Function(f) f.PERIOD_ID = p.ID)
            //            From k In Context.SE_CHOSEN_ORG.Where(Function(f) po.ORG_ID = f.ORG_ID And
            //                                                      f.USERNAME.ToUpper = log.Username.ToUpper)
            //            Where po.STATUSPAROX = _param.STATUS And p.ID = _param.PERIOD_ID And po.ORG_ID <> 46).Any

            return query;
        }
        catch (Exception ex)
        {
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod().Name, "iTime");
            throw ex;
        }
    }

    public bool IS_PERIOD_COLEXSTATUS(PA_ParamDTO _param, UserLog log)
    {
        try
        {
            using (DataAccess.QueryData cls = new DataAccess.QueryData())
            {
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG", new { P_USERNAME = log.Username.ToUpper, P_ORGID = _param.ORG_ID, P_ISDISSOLVE = _param.IS_DISSOLVE });
            };

            //Dim query = (From p In Context.AT_PERIOD
            //            From po In Context.AT_ORG_PERIOD.Where(Function(f) f.PERIOD_ID = p.ID)
            //            From k In Context.SE_CHOSEN_ORG.Where(Function(f) po.ORG_ID = f.ORG_ID And
            //                                                      f.USERNAME.ToUpper = log.Username.ToUpper)
            //            Where po.STATUSCOLEX = _param.STATUS And p.ID = _param.PERIOD_ID And po.ORG_ID <> 46).Any

            return query;
        }
        catch (Exception ex)
        {
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod().Name, "iTime");
            throw ex;
        }
    }
}

public enum TABLE_NAME
{
    PA_SALARY_GROUP = 1,
    PA_SALARY_LEVEL = 2,
    PA_SALARY_RANK = 3,
    AT_PERIOD = 4
}
