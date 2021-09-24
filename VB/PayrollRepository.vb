Imports System.Transactions
Imports System.Web
Imports Framework.Data
Imports System.Data.Objects
Imports System.Data.Common
Imports System.Data.SqlClient
Imports System.Data.EntityClient
Imports Framework.Data.System.Linq.Dynamic
Imports System.Data.Entity
Imports System.Text.RegularExpressions
Imports System.Data.Objects.DataClasses
Imports Framework.Data.SystemConfig
Imports System.Reflection
Imports Aspose.Words

Public Class PayrollRepository
    Inherits PayrollRepositoryBase

    Public Function GetConfig(ByVal eModule As ModuleID) As Dictionary(Of String, String)
        Using config As New SystemConfig
            Try
                Return config.GetConfig(eModule)
            Catch ex As Exception
                WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
                Throw ex
            End Try
        End Using

    End Function

    ''' <summary>
    ''' Gets the combobox data.
    ''' </summary>
    ''' <param name="cbxData">The CBX data.</param>
    ''' <returns></returns>
    Public Function GetComboboxData(ByRef cbxData As ComboBoxDataDTO) As Boolean
        Try

            'Danh loại bảng lương
            If cbxData.GET_TYPE_PAYMENT Then
                cbxData.LIST_TYPE_PAYMENT = (From p In Context.OT_OTHER_LIST Join t In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals t.ID
                                             Where p.ACTFLG = "A" And t.CODE = "TYPE_PAYMENT" Order By p.CREATED_DATE Descending
                         Select New OT_OTHERLIST_DTO With {
                             .ID = p.ID,
                             .CODE = p.CODE,
                             .NAME_EN = p.NAME_EN,
                             .NAME_VN = p.NAME_VN,
                             .TYPE_ID = p.TYPE_ID
                         }).ToList
            End If
            'Danh sách các đối tượng cư trú
            If cbxData.GET_LIST_RESIDENT Then
                cbxData.LIST_LIST_RESIDENT = (From p In Context.OT_OTHER_LIST Join t In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals t.ID
                                             Where p.ACTFLG = "A" And t.CODE = "PA_RESIDENT" Order By p.CREATED_DATE Descending
                         Select New OT_OTHERLIST_DTO With {
                             .ID = p.ID,
                             .CODE = p.CODE,
                             .NAME_EN = p.NAME_EN,
                             .NAME_VN = p.NAME_VN,
                .TYPE_ID = p.TYPE_ID
                         }).ToList
            End If
            ' Danh sách các khoản tiền 
            If cbxData.GET_LIST_PAYMENT Then
                cbxData.LIST_LIST_PAYMENT = (From p In Context.OT_OTHER_LIST Join t In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals t.ID
                                             Where p.ACTFLG = "A" And t.CODE = "PA_LISTPAYMENT" Order By p.CREATED_DATE Descending
                         Select New OT_OTHERLIST_DTO With {
                             .ID = p.ID,
                             .CODE = p.CODE,
                             .NAME_EN = p.NAME_EN,
                             .NAME_VN = p.NAME_VN,
                .TYPE_ID = p.TYPE_ID
                         }).ToList
            End If
            ' Danh sách các khoản tiền trong bảng lương
            'TYPE_PAYMENT = 4123 là danh mục bảng lương tổng hợp
            If cbxData.GET_LIST_SALARY Then
                cbxData.LIST_LIST_SALARY = (From p In Context.PA_LISTSALARIES
                                             Where p.STATUS = "A" And p.TYPE_PAYMENT = 4123 Order By p.CREATED_DATE Descending
                         Select New PAListSalariesDTO With {
                             .ID = p.ID,
                             .NAME_EN = p.NAME_EN,
                             .NAME_VN = p.NAME_VN
                         }).ToList
            End If
            'Danh sách các đối tượng lương(bảng lương)
            If cbxData.GET_OBJECT_PAYMENT Then
                cbxData.LIST_OBJECT_PAYMENT = (From p In Context.PA_OBJECT_SALARY
                                             Where p.ACTFLG = "A" Order By p.CREATED_DATE Descending
                         Select New PAObjectSalaryDTO With {
                             .ID = p.ID,
                             .CODE = p.CODE,
                             .NAME_VN = p.NAME_VN,
                             .NAME_EN = p.NAME_EN
                         }).ToList
            End If
            ''''''''''''''''''''''
            If cbxData.GET_SALARY_LEVEL Then
                cbxData.LIST_SALARY_LEVEL = (From p In Context.PA_SALARY_LEVEL
                                             Join o In Context.PA_SALARY_GROUP On p.SAL_GROUP_ID Equals o.ID
                                             Where p.ACTFLG = "A"
                                             Order By p.NAME.ToUpper
                                             Select New SalaryLevelDTO With {
                                                 .ID = p.ID,
                                                 .NAME = p.NAME,
                                                 .SAL_GROUP_ID = p.SAL_GROUP_ID
                                             }).ToList
            End If
            ''''''''''''''''''''''

            If cbxData.GET_SALARY_GROUP Then
                cbxData.LIST_SALARY_GROUP = (From p In Context.PA_SALARY_GROUP
                                             Order By p.NAME.ToUpper
                                             Select New SalaryGroupDTO With {
                                                 .ID = p.ID,
                                                 .NAME = p.NAME,
                                                 .EFFECT_DATE = p.EFFECT_DATE
                                             }).ToList
            End If

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".GetComboboxData")
            Return False
        Finally

        End Try
    End Function

    ''' <summary>
    ''' Kiểm tra dữ liệu đã được sử dụng hay chưa?
    ''' </summary>
    ''' <param name="table">Enum Table_Name</param>
    ''' <returns>true:chưa có/false:có rồi</returns>
    ''' <remarks></remarks>
    ''' 
    Public Function CheckExistInDatabase(ByVal lstID As List(Of Decimal), ByVal table As TABLE_NAME) As Boolean
        Dim isExist As Boolean = False
        Dim strListID As String = lstID.Select(Function(x) x.ToString).Aggregate(Function(x, y) x & "," & y)
        Dim count As Decimal = 0
        Try

            Select Case table
                Case TABLE_NAME.PA_SALARY_GROUP
                    isExist = Execute_ExistInDatabase("PA_SALARY_LEVEL", "SAL_GROUP_ID", strListID)
                    If Not isExist Then
                        Return isExist
                    End If
                    isExist = Execute_ExistInDatabase("HU_WORKING", "SAL_GROUP_ID", strListID)
                    If Not isExist Then
                        Return isExist
                    End If
                Case TABLE_NAME.PA_SALARY_LEVEL
                    isExist = Execute_ExistInDatabase("PA_SALARY_RANK", "SAL_LEVEL_ID", strListID)
                    If Not isExist Then
                        Return isExist
                    End If
                    isExist = Execute_ExistInDatabase("HU_WORKING", "SAL_LEVEL_ID", strListID)
                    If Not isExist Then
                        Return isExist
                    End If
                Case TABLE_NAME.PA_SALARY_RANK
                    isExist = Execute_ExistInDatabase("HU_WORKING", "SAL_RANK_ID", strListID)
                    If Not isExist Then
                        Return isExist
                    End If
                Case TABLE_NAME.AT_PERIOD
                    isExist = Execute_ExistInDatabase("PA_PAYROLLSHEET_TEMP", "PERIOD_ID", strListID)
                    If Not isExist Then
                        Return isExist
                    End If
                    isExist = Execute_ExistInDatabase("PA_PAYROLLSHEET_SUM", "PERIOD_ID", strListID)
                    If Not isExist Then
                        Return isExist
                    End If
            End Select
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Return False
        End Try
    End Function


    Private Function Execute_ExistInDatabase(tableName As String, colName As String, strListID As String)
        Try
            Dim count As Decimal = 0
            Dim Sql = "SELECT COUNT(" & colName & ") FROM " & tableName & " WHERE " & colName & " IN (" & strListID & ")"
            count = Context.ExecuteStoreQuery(Of Decimal)(Sql).FirstOrDefault
            If count > 0 Then
                Return False
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function GetReportById(ByVal _filter As Se_ReportDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        log As UserLog,
                                        Optional ByVal Sorts As String = "CODE ASC") As List(Of Se_ReportDTO)

        Try
            Dim query As IQueryable(Of Se_ReportDTO)
            If log.Username.ToUpper <> "ADMIN" And log.Username.ToUpper <> "SYS.ADMIN" And log.Username.ToUpper <> "HR.ADMIN" Then
                query = From u In Context.SE_USER
                        From p In u.SE_REPORT
                        Where u.USERNAME.ToUpper = log.Username.ToUpper And p.MODULE_ID = _filter.MODULE_ID
                        Select New Se_ReportDTO With {.ID = p.ID,
                                                        .CODE = p.CODE,
                                                        .NAME = p.NAME,
                                                        .MODULE_ID = p.MODULE_ID}
            Else
                query = From p In Context.SE_REPORT
                        Where p.MODULE_ID = _filter.MODULE_ID
                        Select New Se_ReportDTO With {.ID = p.ID,
                                                    .CODE = p.CODE,
                                                    .NAME = p.NAME,
                                                    .MODULE_ID = p.MODULE_ID}

            End If
            Dim lst = query
            If _filter.CODE <> "" Then
                lst = lst.Where(Function(p) p.CODE.ToUpper.Contains(_filter.CODE.ToUpper))
            End If
            If _filter.NAME <> "" Then
                lst = lst.Where(Function(p) p.NAME.ToUpper.Contains(_filter.NAME.ToUpper))
            End If


            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function ExportReport(ByVal sPkgName As String,
                                 ByVal sMoth As Decimal,
                                 ByVal sYear As Decimal,
                                 ByVal sOrgId As String,
                                 ByVal IsDissolve As Decimal, ByVal log As UserLog) As DataSet
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataSet = cls.ExecuteStore(sPkgName,
                                           New With {.P_ORG = sOrgId,
                                                     .P_ISDISSOLVE = IsDissolve,
                                                     .P_USERNAME = log.Username,
                                                     .P_MOTH = sMoth,
                                                     .P_YEAR = sYear,
                                                     .P_CUR = cls.OUT_CURSOR,
                                                     .P_CUR1 = cls.OUT_CURSOR}, False)

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function ExportReport_014(ByVal sPkgName As String,
                                     ByVal sMoth As Decimal,
                                     ByVal sYear As Decimal,
                                     ByVal sOrgId As String,
                                     ByVal IsDissolve As Decimal,
                                     ByVal log As UserLog) As DataSet
        Try
            Using cls As New DataAccess.QueryData
                Dim dsData As DataSet = cls.ExecuteStore(sPkgName,
                                           New With {.P_ORG = sOrgId,
                                                     .P_ISDISSOLVE = IsDissolve,
                                                     .P_USERNAME = log.Username,
                                                     .P_MOTH = sMoth,
                                                     .P_YEAR = sYear,
                                                     .DATA = cls.OUT_CURSOR,
                                                     .PARAM = cls.OUT_CURSOR}, False)

                dsData.Tables(0).TableName = "DATA"
                dsData.Tables(1).TableName = "PARAM"

                Return dsData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function ExportReport_010(ByVal sPkgName As String,
                                     ByVal sMoth As Decimal,
                                     ByVal sYear As Decimal,
                                     ByVal sOrgId As String,
                                     ByVal IsDissolve As Decimal,
                                     ByVal log As UserLog) As DataSet
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataSet = cls.ExecuteStore(sPkgName,
                                           New With {.P_ORG = sOrgId,
                                                     .P_ISDISSOLVE = IsDissolve,
                                                     .P_USERNAME = log.Username,
                                                     .P_MOTH = sMoth,
                                                     .P_YEAR = sYear,
                                                     .P_CUR = cls.OUT_CURSOR,
                                                     .P_CUR1 = cls.OUT_CURSOR,
                                                     .P_CUR2 = cls.OUT_CURSOR}, False)

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function ExportReport_008(ByVal sPkgName As String,
                                     ByVal sMoth As Decimal,
                                     ByVal sYear As Decimal,
                                     ByVal sOrgId As String,
                                     ByVal IsDissolve As Decimal,
                                     ByVal log As UserLog) As DataSet
        Try
            Using cls As New DataAccess.QueryData
                Dim dsData As DataSet = cls.ExecuteStore(sPkgName,
                                           New With {.P_ORG = sOrgId,
                                                     .P_ISDISSOLVE = IsDissolve,
                                                     .P_USERNAME = log.Username,
                                                     .P_MOTH = sMoth,
                                                     .P_YEAR = sYear,
                                                     .DATA = cls.OUT_CURSOR,
                                                     .DATA1 = cls.OUT_CURSOR,
                                                     .DATA2 = cls.OUT_CURSOR,
                                                     .DATA3 = cls.OUT_CURSOR,
                                                     .DATA4 = cls.OUT_CURSOR,
                                                     .PARAM = cls.OUT_CURSOR}, False)

                dsData.Tables(0).TableName = "DATA"
                dsData.Tables(1).TableName = "DATA1"
                dsData.Tables(2).TableName = "DATA2"
                dsData.Tables(3).TableName = "DATA3"
                dsData.Tables(4).TableName = "DATA4"
                dsData.Tables(5).TableName = "PARAM"

                Return dsData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function ExportReport_005(ByVal sPkgName As String,
                                     ByVal sMoth As Decimal,
                                     ByVal sYear As Decimal,
                                     ByVal sOrgId As String,
                                     ByVal IsDissolve As Decimal,
                                     ByVal log As UserLog) As DataSet
        Try
            Using cls As New DataAccess.QueryData
                Dim dsData As DataSet = cls.ExecuteStore(sPkgName,
                                           New With {.P_ORG = sOrgId,
                                                     .P_ISDISSOLVE = IsDissolve,
                                                     .P_USERNAME = log.Username,
                                                     .P_MOTH = sMoth,
                                                     .P_YEAR = sYear,
                                                     .DATA = cls.OUT_CURSOR,
                                                     .DATA1 = cls.OUT_CURSOR,
                                                     .PARAM = cls.OUT_CURSOR}, False)

                dsData.Tables(0).TableName = "DATA"
                dsData.Tables(1).TableName = "DATA1"
                dsData.Tables(2).TableName = "PARAM"

                Return dsData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function ExportPhieuLuong(ByVal lstEmployee As List(Of Decimal),
                                     ByVal orgID As Decimal?,
                                     ByVal isDissolve As Decimal?,
                                     ByVal periodID As Decimal,
                                     ByVal log As UserLog) As DataTable
        Try
            Using cls As New DataAccess.NonQueryData
                cls.ExecuteSQL("DELETE FROM SE_EMPLOYEE_CHOSEN S WHERE S.USING_USER ='" + log.Username.ToUpper + "'")
            End Using

            For Each emp As Decimal In lstEmployee
                Dim objNew As New SE_EMPLOYEE_CHOSEN
                objNew.EMPLOYEE_ID = emp
                objNew.USING_USER = log.Username.ToUpper
                Context.SE_EMPLOYEE_CHOSEN.AddObject(objNew)
            Next
            Context.SaveChanges()

            Using cls As New DataAccess.QueryData
                Dim dtData = cls.ExecuteStore("PKG_PA_REPORT.GET_PHIEULUONG",
                                           New With {.P_USERNAME = log.Username,
                                                     .P_PERIOD = periodID,
                                                     .P_ORGID = orgID,
                                                     .P_ISDISSOLVE = isDissolve,
                                                     .P_SENDMAIL = 0,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function CheckAndSendPayslip() As Boolean
        Try
            '1. Kiểm tra có action nào đang chạy hay không?
            Dim isExist = (From p In Context.PA_SEND_PAYSLIP
                           Where p.ACTION_STATUS = 1).Any

            If Not isExist Then
                '2. Nếu chưa có action nào chạy thì tiến hành chạy
                'Đổi trạng thái đang thực hiện để không bị chạy chồng chéo
                Dim objAction = (From p In Context.PA_SEND_PAYSLIP
                                Where p.ACTION_STATUS = 0 Or p.ACTION_STATUS = 3).FirstOrDefault
                If objAction IsNot Nothing Then
                    objAction.ACTION_STATUS = 1
                    Context.SaveChanges()
                    '3. Tiến hành thực hiện
                    Try
                        Dim lstEmp As New List(Of Decimal)
                        If objAction.ACTION_TYPE = 1 Then
                            lstEmp = (From p In Context.PA_SEND_PAYSLIP_EMP
                                      Where p.PA_SEND_PAYSLIP_ID = objAction.ID
                                      Select p.EMPLOYEE_ID.Value).ToList


                            Using cls As New DataAccess.NonQueryData
                                cls.ExecuteSQL("DELETE FROM SE_EMPLOYEE_CHOSEN S WHERE S.USING_USER ='" + objAction.USERNAME.ToUpper + "'")
                                Dim sql As String = "INSERT INTO SE_EMPLOYEE_CHOSEN" & vbNewLine & _
                                    "  (EMPLOYEE_ID, USING_USER)" & vbNewLine & _
                                    "  (SELECT DISTINCT EMPLOYEE_ID, '" + objAction.USERNAME.ToUpper + "'" & vbNewLine & _
                                    "     FROM PA_SEND_PAYSLIP_EMP" & vbNewLine & _
                                    "    WHERE PA_SEND_PAYSLIP_ID = " + objAction.ID.ToString + ")"

                                cls.ExecuteSQL(sql)

                            End Using

                        End If
                        Dim rowTotal As Decimal = 0
                        objAction.RUN_START = Date.Now
                        If SendPayslip(lstEmp,
                                       objAction.ORG_ID,
                                       objAction.IS_DISSOLVE,
                                       objAction.PERIOD_ID,
                                       objAction.USERNAME.ToUpper,
                                       rowTotal) Then
                            objAction.RUN_ROW = rowTotal
                            objAction.ACTION_STATUS = 2
                        Else
                            objAction.ACTION_STATUS = 3
                        End If
                        objAction.RUN_END = Date.Now

                    Catch ex As Exception
                        WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
                        objAction.ACTION_STATUS = 3
                    End Try
                    Context.SaveChanges()
                End If
            End If
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function ActionSendPayslip(ByVal lstEmployee As List(Of Decimal),
                                ByVal orgID As Decimal?,
                                ByVal isDissolve As Decimal?,
                                ByVal periodID As Decimal,
                                ByVal log As UserLog) As Boolean
        Try
            Dim objPayslip As New PA_SEND_PAYSLIP
            objPayslip.ID = Utilities.GetNextSequence(Context, Context.PA_SEND_PAYSLIP.EntitySet.Name)
            objPayslip.ACTION_DATE = Date.Now
            objPayslip.ACTION_STATUS = 0
            objPayslip.ACTION_TYPE = 0
            objPayslip.IS_DISSOLVE = isDissolve
            objPayslip.ORG_ID = orgID
            objPayslip.PERIOD_ID = periodID
            objPayslip.USERNAME = log.Username.ToUpper
            Context.PA_SEND_PAYSLIP.AddObject(objPayslip)
            lstEmployee = lstEmployee.Distinct.ToList

            For Each emp As Decimal In lstEmployee
                Dim objNew As New PA_SEND_PAYSLIP_EMP
                objNew.ID = Utilities.GetNextSequence(Context, Context.PA_SEND_PAYSLIP_EMP.EntitySet.Name)
                objNew.EMPLOYEE_ID = emp
                objNew.PA_SEND_PAYSLIP_ID = objPayslip.ID
                Context.PA_SEND_PAYSLIP_EMP.AddObject(objNew)
                objPayslip.ACTION_TYPE = 1
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function SendPayslip(ByVal lstEmployee As List(Of Decimal),
                                ByVal orgID As Decimal?,
                                ByVal isDissolve As Decimal?,
                                ByVal periodID As Decimal,
                                ByVal userName As String,
                                ByRef rowTotal As Decimal) As Boolean
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PA_REPORT.GET_PHIEULUONG",
                                           New With {.P_USERNAME = userName.ToUpper,
                                                     .P_PERIOD = periodID,
                                                     .P_ORGID = orgID,
                                                     .P_ISDISSOLVE = isDissolve,
                                                     .P_SENDMAIL = 1,
                                                     .P_CUR = cls.OUT_CURSOR})

                Dim isSave As Boolean = False
                Dim config = GetConfig(ModuleID.All)
                Dim emailFrom = If(config.ContainsKey("MailFrom"), config("MailFrom"), "")
                rowTotal = 0
                Dim dtDataMerge As DataTable
                For Each row As DataRow In dtData.Rows
                    Dim filePath As String = AppDomain.CurrentDomain.BaseDirectory & "\ReportTemplates\Payroll\Report\PhieuLuong.docx"
                    Dim doc As Document = New Document(filePath)
                    If doc IsNot Nothing AndAlso row("WORK_EMAIL").ToString <> "" And emailFrom <> "" Then
                        filePath = System.AppDomain.CurrentDomain.BaseDirectory & "Payroll\Attachment\" & "PhieuLuong_" & row("EMPLOYEE_CODE").ToString & "_" & Format(Date.Now, "yyyyMMddHHmmss")
                        dtDataMerge = dtData.Clone
                        dtDataMerge.ImportRow(row)
                        SendPhieuLuong_File(doc, filePath, dtDataMerge, False)
                        Dim strContent As String = "Kính gửi Anh/Chị,<br/><br/>" & _
                            "Phòng HCNS gửi Anh/Chị phiếu lương chi tiết như file đính kèm.<br/>" & _
                            "Mọi thắc mắc về tiền lương xin phản hồi tới cán bộ/đơn vị phụ trách hoặc thông tin liên hệ ở phần cuối của Phiếu lương.<br/><br/>" & _
                            "<p style='color:red'>Lưu ý đây là email gửi tự động từ Hệ thống Phần mềm Quản trị Nhân sự, Anh/Chị không phản hồi email này.</p>" & _
                            "Trân trọng,<br/><br/>" & _
                            "<p style='color:gray'><b>HỆ THỐNG PHẦN MỀM QUẢN TRỊ NHÂN SỰ</b><br/><i>(Được quản lý bởi Ban Hành chính Nhân sự)</i><br/><p>"

                        InsertMail(emailFrom,
                                   row("WORK_EMAIL").ToString,
                                   row("TITLE_NAME").ToString,
                                   strContent,
                                   filePath,
                                   "",
                                   "",
                                   "iPayroll_SendMail")
                        isSave = True
                        rowTotal += 1
                    End If
                Next
                If isSave Then
                    Context.SaveChanges()
                End If
            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function InsertMail(ByVal _from As String,
                               ByVal _to As String,
                               ByVal _subject As String,
                               ByVal _content As String,
                               Optional ByVal _attachment As String = "",
                               Optional ByVal _cc As String = "",
                               Optional ByVal _bcc As String = "",
                               Optional ByVal _viewName As String = "")
        Try
            Dim _newMail As New SE_MAIL
            _newMail.ID = Utilities.GetNextSequence(Context, Context.SE_MAIL.EntitySet.Name)
            _newMail.MAIL_FROM = _from
            _newMail.MAIL_TO = _to
            _newMail.MAIL_CC = _cc
            _newMail.MAIL_BCC = _bcc
            _newMail.SUBJECT = _subject
            _newMail.CONTENT = _content
            _newMail.VIEW_NAME = _viewName
            _newMail.ATTACHMENT = _attachment
            _newMail.ACTFLG = "I"
            Context.SE_MAIL.AddObject(_newMail)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub SendPhieuLuong_File(ByVal doc As Document,
                               ByRef filename As String,
                               ByVal dtData As DataTable,
                               Optional is2003 As Boolean = True)
        Try
            ' Fill the fields in the document with user data.
            doc.MailMerge.Execute(dtData)
            ' Send the document in Word format to the client browser with an option to save to disk or open inside the current browser.

            'doc.Save(filename, SaveFormat.Doc, SaveType.OpenInApplication, response)
            If is2003 Then
                filename = filename & ".doc"
                doc.Save(filename, Aspose.Words.Saving.SaveOptions.CreateSaveOptions(SaveFormat.Doc))
            Else
                filename = filename & ".docx"
                doc.Save(filename, Aspose.Words.Saving.SaveOptions.CreateSaveOptions(SaveFormat.Docx))
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Public Function IS_PERIODSTATUS(ByVal _param As PA_ParamDTO, ByVal log As UserLog) As Boolean
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username.ToUpper,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using

            Dim query = (From p In Context.AT_PERIOD
                        From po In Context.AT_ORG_PERIOD.Where(Function(f) f.PERIOD_ID = p.ID)
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) po.ORG_ID = f.ORG_ID And
                                                                  f.USERNAME.ToUpper = log.Username.ToUpper)
                        Where po.STATUSPAROX = _param.STATUS And p.ID = _param.PERIOD_ID And po.ORG_ID <> 46).Any

            Return query

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function IS_PERIOD_COLEXSTATUS(ByVal _param As PA_ParamDTO, ByVal log As UserLog) As Boolean
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username.ToUpper,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using

            Dim query = (From p In Context.AT_PERIOD
                        From po In Context.AT_ORG_PERIOD.Where(Function(f) f.PERIOD_ID = p.ID)
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) po.ORG_ID = f.ORG_ID And
                                                                  f.USERNAME.ToUpper = log.Username.ToUpper)
                        Where po.STATUSCOLEX = _param.STATUS And p.ID = _param.PERIOD_ID And po.ORG_ID <> 46).Any

            Return query

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

End Class

Public Enum TABLE_NAME
    PA_SALARY_GROUP = 1
    PA_SALARY_LEVEL = 2
    PA_SALARY_RANK = 3
    AT_PERIOD = 4
End Enum