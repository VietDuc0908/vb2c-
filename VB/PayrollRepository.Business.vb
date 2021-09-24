Imports System.Text
Imports System.Linq.Expressions
Imports LinqKit.Extensions
Imports System.Data.Common
Imports Framework.Data
Imports Framework.Data.System.Linq.Dynamic
Imports System.Data.Entity
Imports System.Text.RegularExpressions
Imports System.Data.Objects
Imports Oracle.DataAccess.Client
Imports System.Reflection

Partial Public Class PayrollRepository

#Region "Calculate Salary"
    'Tải dứ liệu trong kỳ 
    'Tính toán dữ liệu trong kỳ. 
    'Tải sang bảog lương tổng hợp
    Public Function Load_Calculate_Load(ByVal lstEmp As List(Of PAEmployeeCalculateDTO), ByVal OrgId As Integer, ByVal PeriodId As Integer, ByVal IsDissolve As Integer, ByVal IsLoad As Integer, ByVal log As UserLog) As Boolean
        Try

            Using Sql As New DataAccess.NonQueryData
                Dim idEmpCal As Decimal = 0
                'If lstEmp.Count > 0 Then
                '    Sql.ExecuteSQL(String.Format("DELETE PA_EMP_CALCULATE_SAL S WHERE S.CREATED_BY = '{0}'", log.Username.ToUpper).ToString())
                '    idEmpCal = Utilities.GetNextSequence(Context, Context.PA_EMP_CALCULATE_SAL.EntitySet.Name)
                '    For Each objEmp As PAEmployeeCalculateDTO In lstEmp
                '        Dim objData As New PA_EMP_CALCULATE_SAL
                '        objData.ID = idEmpCal
                '        objData.EMPLOYEE_ID = objEmp.EMPLOYEE_ID
                '        Context.PA_EMP_CALCULATE_SAL.AddObject(objData)
                '    Next
                '    Context.SaveChanges(log)
                'End If
                Sql.ExecuteStore("PKG_PA_BUSINESS.LOAD_DATA",
                               New With {.P_PERIOD_ID = PeriodId,
                                         .P_ORG_ID = OrgId,
                                         .P_ISDISSOLVE = IsDissolve,
                                         .P_USERNAME = log.Username,
                                         .P_ISLOAD = IsLoad,
                                         .P_EMPID = idEmpCal})
                ' ghi log phần tải dữ liệu tính lương
                Dim objLog As New PA_ACTION_LOGDTO
                objLog.PERIOD_ID = PeriodId
                LOG_PA(log, "TẢI DỮ LIỆU TÍNH LƯƠNG", objLog)

            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Return False
        End Try
    End Function
    'Tính lương tổng hợp
    Public Function Calculate_data_sum(ByVal OrgId As Integer, ByVal PeriodId As Integer, ByVal IsDissolve As Integer, ByVal IsLoad As Integer, ByVal log As UserLog) As Boolean
        Try
            Using Sql As New DataAccess.NonQueryData
                ' ghi log phần tải dữ liệu tính lương
                Dim objLog As New PA_ACTION_LOGDTO
                objLog.PERIOD_ID = PeriodId
                LOG_PA(log, "TÍNH LƯƠNG", objLog)

                Sql.ExecuteStore("PKG_PA_BUSINESS.CALCULATE_DATA_SUM",
                                 New With {.P_PERIOD_ID = PeriodId,
                                           .P_ORG_ID = OrgId,
                                           .P_ISDISSOLVE = IsDissolve,
                                           .P_USERNAME = log.Username})
            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Return False
        End Try
    End Function
    '
    Public Function Load_data_sum(ByVal OrgId As Integer, ByVal PeriodId As Integer, ByVal IsDissolve As Integer, ByVal IsLoad As Integer, ByVal log As UserLog) As Boolean
        Try
            Using Sql As New DataAccess.NonQueryData
                Sql.ExecuteStore("PKG_PA_BUSINESS.LOAD_DATA_SUM",
                                 New With {.P_PERIOD_ID = PeriodId,
                                           .P_ORG_ID = OrgId,
                                           .P_ISDISSOLVE = IsDissolve,
                                           .P_USERNAME = log.Username,
                                           .P_ISLOAD = IsLoad})
            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Return False
        End Try
    End Function
    Public Function Calculate_data_temp(ByVal OrgId As Integer, ByVal PeriodId As Integer, ByVal IsDissolve As Integer, ByVal IsLoad As Integer, ByVal log As UserLog) As Boolean
        Try
            Using Sql As New DataAccess.NonQueryData
                Sql.ExecuteStore("PKG_PA_BUSINESS.CALCULATE_DATA_TEMP",
                                 New With {.P_PERIOD_ID = PeriodId,
                                           .P_ORG_ID = OrgId,
                                           .P_ISDISSOLVE = IsDissolve,
                                           .P_USERNAME = log.Username})
            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Return False
        End Try
    End Function
    'Tìm kiếm dữ liệu tính lương
    Public Function GetLitsCalculate(ByVal param As PA_ParamDTO,
                                     ByVal IsLoad As Integer,
                                     ByVal PageIndex As Integer,
                                     ByVal PageSize As Integer,
                                     ByVal PageType As Integer,
                                     ByRef TotalRow As Integer,
                                     ByVal log As UserLog,
                                     Optional ByVal Sorts As String = "CREATED_DATE DESC") As DataTable

        Try
            Using cls As New DataAccess.QueryData

                Dim obj = New With {.P_ORGID = param.ORG_ID,
                                    .P_ISDISSOLVE = param.IS_DISSOLVE,
                                    .P_USERNAME = log.Username,
                                    .P_PERIOD_ID = param.PERIOD_ID,
                                    .P_EMPLOYEE_CODE = param.EMPLOYEE_CODE,
                                    .P_EMPLOYEE_NAME = param.FULLNAME_VN,
                                    .P_ORG_NAME = param.ORG_NAME,
                                    .P_TITLE_NAME = param.TITLE_NAME,
                                    .P_STAFF_RANK_NAME = param.STAFF_RANK_NAME,
                                    .P_SORT = Sorts,
                                    .IS_LOAD = IsLoad,
                                    .P_PAGESIZE = PageSize,
                                    .P_PAGEINDEX = PageIndex,
                                    .P_TYPE = PageType,
                                    .P_ROWCOUNT = cls.OUT_NUMBER,
                                    .P_CUR = cls.OUT_CURSOR}

                Dim dtData = cls.ExecuteStore("PKG_PA_BUSINESS.GET_LIST_CALCULATE", obj, True)
                TotalRow = obj.P_ROWCOUNT
                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function LoadCalculate(ByVal PeriodId As Integer, ByVal OrgId As Integer, ByVal IsDissolve As Integer, ByVal listEmployee As List(Of String),
                                     ByVal log As UserLog, Optional ByVal Sorts As String = "CREATED_DATE DESC") As DataSet

        Try

            Using cls As New DataAccess.NonQueryData
                cls.ExecuteSQL("DELETE SE_CHOSEN_CALCULATE")
                If listEmployee.Count <= 0 Then
                    cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_CALCULATE",
                                     New With {.P_USERNAME = log.Username,
                                               .P_ORGID = OrgId,
                                               .P_ISDISSOLVE = IsDissolve})
                Else
                    For Each emp As String In listEmployee
                        Dim objNew As New SE_CHOSEN_CALCULATE
                        objNew.EMPLOYEEID = Utilities.Obj2Decima(emp)
                        objNew.USERNAME = log.Username
                        Context.SE_CHOSEN_CALCULATE.AddObject(objNew)
                    Next
                    Context.SaveChanges()
                End If
            End Using
            Using cls As New DataAccess.QueryData
                Dim dtData As DataSet = cls.ExecuteStore("PKG_PA_BUSINESS.GET_IMPORTSALARY",
                                           New With {.P_ORG_ID = OrgId,
                                                     .P_PERIOD_ID = PeriodId,
                                                     .P_USERNAME = log.Username,
                                                     .P_SORT = Sorts,
                                                     .P_CUR = cls.OUT_CURSOR})
                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function GetListSalaryVisibleCol() As List(Of PAListSalariesDTO)
        Try
            Dim query = From s In Context.PA_LISTSALARIES
                        Where s.IS_VISIBLE = True And s.STATUS = "A"
                        Order By s.COL_INDEX
                       Select New PAListSalariesDTO With {
                                        .ID = s.ID,
                                        .TYPE_PAYMENT = s.TYPE_PAYMENT,
                                        .COL_NAME = s.COL_NAME,
                                        .NAME_EN = s.NAME_EN,
                                        .NAME_VN = s.NAME_VN,
                                        .COL_INDEX = s.COL_INDEX,
                                        .CREATED_DATE = s.CREATED_DATE,
                                        .IS_VISIBLE = s.IS_VISIBLE,
                                        .STATUS = s.STATUS}

            Return query.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function ActiveOrDeactive(ByVal _param As PA_ParamDTO,
                                     ByVal log As UserLog) As Boolean
        Try
            Using sql As New DataAccess.NonQueryData
                sql.ExecuteStore("PKG_PA_BUSINESS.CLOSEDOPEN_PERIOD",
                                New With {.P_USERNAME = log.Username.ToUpper,
                                          .P_ORG_ID = _param.ORG_ID,
                                          .P_ISDISSOLVE = _param.IS_DISSOLVE,
                                          .P_STATUS = _param.STATUS,
                                          .P_PERIOD_ID = _param.PERIOD_ID})
            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Return False
        End Try
    End Function
#End Region

#Region "Import Salary"

    Public Function GetImportSalary(ByVal PeriodId As Integer, ByVal OrgId As Integer, ByVal IsDissolve As Integer, ByVal EmployeeId As String,
                                     ByVal PageIndex As Integer,
                                     ByVal PageSize As Integer,
                                     ByRef TotalRow As Integer,
                                     ByVal log As UserLog,
                                        Optional ByVal Sorts As String = "CREATED_DATE DESC") As DataTable

        Try
            Using cls As New DataAccess.QueryData

                Dim obj = New With {.P_ORGID = OrgId,
                                   .P_ISDISSOLVE = IsDissolve,
                                   .P_USERNAME = log.Username,
                                   .P_PERIOD_ID = PeriodId,
                                   .P_EMPLOYEE = EmployeeId,
                                   .P_SORT = Sorts,
                                   .P_PAGESIZE = PageSize,
                                   .P_PAGEINDEX = PageIndex,
                                   .P_ROWCOUNT = cls.OUT_NUMBER,
                                   .P_CUR = cls.OUT_CURSOR}

                Dim dtData = cls.ExecuteStore("PKG_PA_BUSINESS.GET_IMPORTSALARY", obj, True)
                TotalRow = obj.P_ROWCOUNT
                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function GetSalaryList() As List(Of PAListSalariesDTO)
        Try
            Dim query = From p In Context.PA_LISTSALARIES Where p.IS_IMPORT = -1 And p.TYPE_PAYMENT = 4123 Order By p.COL_INDEX Ascending
            Dim lst = query.Select(Function(s) New PAListSalariesDTO With {
                                        .ID = s.ID,
                                        .TYPE_PAYMENT = s.TYPE_PAYMENT,
                                        .COL_NAME = s.COL_NAME,
                                        .NAME_EN = s.NAME_EN,
                                        .NAME_VN = s.NAME_VN,
                                        .COL_INDEX = s.COL_INDEX,
                                        .CREATED_DATE = s.CREATED_DATE,
                                        .IS_IMPORT = s.IS_IMPORT
                                    })
            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function SaveImport(ByVal Period As Decimal, ByVal dtData As DataTable, ByVal lstColVal As List(Of String), ByVal log As UserLog, ByRef RecordSussces As Integer) As Boolean
        Dim dtCount As DataTable

        Try
            Using conMng As New DataAccess.ConnectionManager
                Using conn As New OracleConnection(conMng.GetConnectionString())
                    Using cmd As New OracleCommand
                        Try
                            Dim sql As New StringBuilder
                            Dim sqlUpdate As New StringBuilder
                            For Each Str As String In lstColVal
                                sqlUpdate.Append(Str)
                                sqlUpdate.Append("= :")
                                sqlUpdate.Append(Str)
                                If lstColVal.Last <> Str Then
                                    sqlUpdate.Append(",")
                                End If
                            Next

                            conn.Open()
                            cmd.Connection = conn
                            cmd.Transaction = cmd.Connection.BeginTransaction()
                            RecordSussces = 0
                            Using s As New DataAccess.QueryData
                                dtCount = s.ExecuteSQL(String.Format("SELECT S.ID, S.EMPLOYEE_ID FROM PA_PAYROLLSHEET_SUM S WHERE S.PERIOD_ID ={0}", Period).ToString())

                            End Using
                            For Each dr As DataRow In dtData.Rows

                                If dr("ID").ToString = "" Then
                                    Continue For
                                End If

                                cmd.CommandText = String.Format("UPDATE PA_PAYROLLSHEET_SUM SET {0}  WHERE EMPLOYEE_ID = :EMPLOYEE_ID AND PERIOD_ID = :PERIOD_ID", sqlUpdate).ToString()
                                cmd.Parameters.Clear()

                                For Each parm As String In lstColVal
                                    If dr(parm).ToString = "" Then
                                        cmd.Parameters.Add(parm, 0)
                                    Else
                                        Dim parameter = dr(parm).ToString.Trim.Replace(",", "")
                                        cmd.Parameters.Add(parm, Utilities.Obj2Decima(parameter.Replace(" ", "")))
                                    End If
                                Next
                                cmd.Parameters.Add("EMPLOYEE_ID", dr("ID"))
                                cmd.Parameters.Add("PERIOD_ID", Period)

                                Dim r As Integer = 0
                                r = cmd.ExecuteNonQuery()
                                If r > 0 Then
                                    RecordSussces += 1
                                End If
                            Next
                            cmd.Transaction.Commit()

                            ' ghi log phần tải dữ liệu tính lương
                            Dim objLog As New PA_ACTION_LOGDTO
                            objLog.PERIOD_ID = Period
                            LOG_PA(log, "IMPORT CÁC KHOẢN PHÁT SINH KHÁC", objLog)

                        Catch ex As Exception
                            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
                            cmd.Transaction.Rollback()

                        Finally
                            cmd.Dispose()
                            conn.Close()
                            conn.Dispose()

                        End Try
                    End Using
                End Using
            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

#End Region

#Region "IPORTAL - View phiếu lương"

    Public Function GetPayrollSheetSum(ByVal PeriodId As Integer, ByVal EmployeeId As String, ByVal log As UserLog,
                                        Optional ByVal Sorts As String = "CREATED_DATE DESC") As DataTable

        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PA_BUSINESS.LOAD_PAYROLL_SHEET_SUM",
                                           New With {.P_PERIOD_ID = PeriodId,
                                                     .P_EMPLOYEE = EmployeeId,
                                                     .P_SORT = Sorts,
                                                     .P_CUR = cls.OUT_CURSOR})
                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function CheckPeriod(ByVal PeriodId As Integer, ByVal EmployeeId As Decimal) As Boolean
        Try
            Dim emp As HU_EMPLOYEE
            emp = (From p In Context.HU_EMPLOYEE Where p.ID = EmployeeId).FirstOrDefault

            Dim query = (From p In Context.AT_ORG_PERIOD
                         Where p.PERIOD_ID = PeriodId And p.ORG_ID = emp.ORG_ID).FirstOrDefault

            If query IsNot Nothing Then
                Return query.STATUSCOLEX = 0
            Else
                Return (query Is Nothing)
            End If

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

#End Region

#Region "Seniority"

    Public Function CalSeniorityProcess(ByVal _filter As PASeniorityProcessDTO, ByVal log As UserLog) As Boolean
        Try
            Using cls As New DataAccess.NonQueryData

                Dim obj = New With {.P_PERIOD_ID = _filter.PERIOD_ID,
                                    .P_ORGID = _filter.ORG_ID,
                                    .P_ISDISSOLVE = _filter.IS_DISSOLVE,
                                    .P_USERNAME = log.Username.ToUpper}

                Dim dtData = cls.ExecuteStore("PKG_PA_BUSINESS.CAL_SENIORITY_DAILY", obj)
                Return True
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function GetSeniorityProcess(ByVal _filter As PASeniorityProcessDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer, ByVal log As UserLog,
                                        Optional ByVal Sorts As String = "EMPLOYEE_CODE desc") As List(Of PASeniorityProcessDTO)
        Try

            ' lấy toàn bộ dữ liệu theo Org
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username.ToUpper,
                                           .P_ORGID = _filter.ORG_ID,
                                           .P_ISDISSOLVE = _filter.IS_DISSOLVE})
            End Using

            Dim lst = From p In Context.PA_SENIORITY_PROCESS
                      From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                      From t In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID)
                      From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID)
                      From s In Context.HU_STAFF_RANK.Where(Function(f) f.ID = p.STAFF_RANK_ID).DefaultIfEmpty
                      From c In Context.HU_CONTRACT.Where(Function(f) f.ID = p.CONTRACT_ID).DefaultIfEmpty
                      From c_type In Context.HU_CONTRACT_TYPE.Where(Function(f) f.ID = c.CONTRACT_TYPE_ID).DefaultIfEmpty
                      From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = p.ORG_ID And f.USERNAME = log.Username.ToUpper)
                      Where p.PERIOD_ID = _filter.PERIOD_ID
                      Select New PASeniorityProcessDTO With {.ID = p.ID,
                                                             .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                                                             .EMPLOYEE_NAME = e.FULLNAME_VN,
                                                              .CONTRACT_TYPE_NAME = c_type.NAME,
                                                              .DISCIPLINE_COUNT = p.DISCIPLINE_COUNT,
                                                              .DISCIPLINE_TYPE_NAME = p.DISCIPLINE_TYPE_NAME,
                                                              .ORG_NAME = o.NAME_VN,
                                                              .PERCENT_SALARY = p.PERCENT_SALARY,
                                                              .SALARY = p.SALARY,
                                                              .STAFF_RANK_NAME = s.NAME,
                                                              .TITLE_NAME = t.NAME_VN,
                                                              .D1 = p.D1,
                                                              .D2 = p.D2,
                                                              .D3 = p.D3,
                                                              .D4 = p.D4,
                                                              .D5 = p.D5,
                                                              .D6 = p.D6,
                                                              .D7 = p.D7,
                                                              .D8 = p.D8,
                                                              .D9 = p.D9,
                                                              .D10 = p.D10,
                                                              .D11 = p.D11,
                                                              .D12 = p.D12,
                                                              .D13 = p.D13,
                                                              .D14 = p.D14,
                                                              .D15 = p.D15,
                                                              .D16 = p.D16,
                                                              .D17 = p.D17,
                                                              .D18 = p.D18,
                                                              .D19 = p.D19,
                                                              .D20 = p.D20,
                                                              .D21 = p.D21,
                                                              .D22 = p.D22,
                                                              .D23 = p.D23,
                                                              .D24 = p.D24,
                                                              .D25 = p.D25,
                                                              .D26 = p.D26,
                                                              .D27 = p.D27,
                                                              .D28 = p.D28,
                                                              .D29 = p.D29,
                                                              .D30 = p.D30,
                                                              .D31 = p.D31}


            If _filter.CONTRACT_TYPE_NAME <> "" Then
                lst = lst.Where(Function(f) f.CONTRACT_TYPE_NAME.ToUpper.Contains(_filter.CONTRACT_TYPE_NAME.ToUpper))
            End If
            If _filter.ORG_NAME <> "" Then
                lst = lst.Where(Function(f) f.ORG_NAME.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
            End If
            If _filter.TITLE_NAME <> "" Then
                lst = lst.Where(Function(f) f.TITLE_NAME.ToUpper.Contains(_filter.TITLE_NAME.ToUpper))
            End If
            If _filter.ORG_NAME <> "" Then
                lst = lst.Where(Function(f) f.ORG_NAME.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
            End If
            If _filter.EMPLOYEE_CODE <> "" Then
                lst = lst.Where(Function(f) f.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If
            If _filter.EMPLOYEE_NAME <> "" Then
                lst = lst.Where(Function(f) f.EMPLOYEE_NAME.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper))
            End If
            If _filter.STAFF_RANK_NAME <> "" Then
                lst = lst.Where(Function(f) f.STAFF_RANK_NAME.ToUpper.Contains(_filter.STAFF_RANK_NAME.ToUpper))
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

    Public Function GetSeniorityProcessImport(ByVal _filter As PASeniorityProcessDTO,
                                        ByVal log As UserLog) As DataTable

        Try
            Using cls As New DataAccess.QueryData
                Dim obj = New With {.P_PERIOD_ID = _filter.PERIOD_ID,
                                    .P_ORGID = _filter.ORG_ID,
                                    .P_ISDISSOLVE = _filter.IS_DISSOLVE,
                                    .P_USERNAME = log.Username.ToUpper,
                                    .P_CUR = cls.OUT_CURSOR}

                Return cls.ExecuteStore("PKG_PA_BUSINESS.GET_SENIORITY_IMPORT", obj)
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function


    Public Function SaveSeniorityProcessImport(ByVal dtData As DataTable,
                                               ByVal periodId As Decimal,
                                               ByVal log As UserLog) As Boolean

        Try
            Using conMng As New DataAccess.ConnectionManager
                Using conn As New OracleConnection(conMng.GetConnectionString())
                    Using cmd As New OracleCommand
                        Try

                            conn.Open()
                            cmd.Connection = conn
                            cmd.Transaction = cmd.Connection.BeginTransaction()
                            cmd.CommandType = CommandType.Text
                            For Each dr As DataRow In dtData.Rows
                                cmd.Parameters.Clear()
                                cmd.CommandText = "UPDATE PA_SENIORITY_PROCESS SET " & vbNewLine &
                                    "PERCENT_SALARY=:PERCENT_SALARY " & vbNewLine &
                                    "WHERE EMPLOYEE_ID = :EMPLOYEE_ID AND PERIOD_ID = :PERIOD_ID"

                                cmd.Parameters.Add("PERCENT_SALARY", dr("PERCENT_SALARY"))
                                cmd.Parameters.Add("EMPLOYEE_ID", dr("EMPLOYEE_ID"))
                                cmd.Parameters.Add("PERIOD_ID", periodId)
                                cmd.ExecuteNonQuery()
                            Next

                            cmd.Transaction.Commit()
                            Dim objLog As New PA_ACTION_LOGDTO
                            objLog.PERIOD_ID = periodId
                            LOG_PA(log, "IMPORT XỬ LÝ DỮ LIỆU THƯỞNG THÂM NIÊN", objLog)

                        Catch ex As Exception
                            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
                            cmd.Transaction.Rollback()

                        Finally
                            cmd.Dispose()
                            conn.Close()
                            conn.Dispose()

                        End Try
                    End Using
                End Using
            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function CalSeniorityMonthly(ByVal _filter As PASeniorityMonthlyDTO, ByVal log As UserLog) As Boolean
        Try
            Using cls As New DataAccess.NonQueryData

                Dim obj = New With {.P_PERIOD_ID = _filter.PERIOD_ID,
                                    .P_ORGID = _filter.ORG_ID,
                                    .P_ISDISSOLVE = _filter.IS_DISSOLVE,
                                    .P_USERNAME = log.Username.ToUpper}

                Dim dtData = cls.ExecuteStore("PKG_PA_BUSINESS.CAL_SENIORITY_MONTHLY", obj)
                Return True
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function GetSeniorityMonthly(ByVal _filter As PASeniorityMonthlyDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer, ByVal log As UserLog,
                                        Optional ByVal Sorts As String = "EMPLOYEE_CODE") As List(Of PASeniorityMonthlyDTO)
        Try
            ' lấy toàn bộ dữ liệu theo Org
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username.ToUpper,
                                           .P_ORGID = _filter.ORG_ID,
                                           .P_ISDISSOLVE = _filter.IS_DISSOLVE})
            End Using


            Dim lst = From p In Context.PA_SENIORITY_MONTHLY
                      From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                      From t In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID)
                      From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID)
                      From s In Context.HU_STAFF_RANK.Where(Function(f) f.ID = p.STAFF_RANK_ID).DefaultIfEmpty
                      From c In Context.HU_CONTRACT.Where(Function(f) f.ID = p.CONTRACT_ID).DefaultIfEmpty
                      From c_type In Context.HU_CONTRACT_TYPE.Where(Function(f) f.ID = c.CONTRACT_TYPE_ID).DefaultIfEmpty
                      From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = p.ORG_ID And f.USERNAME = log.Username.ToUpper)
                      Where p.PERIOD_ID = _filter.PERIOD_ID
                      Select New PASeniorityMonthlyDTO With {.ID = p.ID,
                                                             .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                                                             .EMPLOYEE_NAME = e.FULLNAME_VN,
                                                             .CONTRACT_TYPE_NAME = c_type.NAME,
                                                             .ORG_NAME = o.NAME_VN,
                                                             .STAFF_RANK_NAME = s.NAME,
                                                             .TITLE_NAME = t.NAME_VN,
                                                             .PERCENT_SALARY = p.PERCENT_SALARY,
                                                             .DAY_COUNT1 = p.DAY_COUNT1,
                                                             .DAY_COUNT2 = p.DAY_COUNT2,
                                                             .DAY_COUNT3 = p.DAY_COUNT3,
                                                             .DAY_COUNT5 = p.DAY_COUNT5,
                                                             .SENIORITY = p.SENIORITY,
                                                             .SALARY_TOTAL = p.SALARY_TOTAL}


            If _filter.CONTRACT_TYPE_NAME <> "" Then
                lst = lst.Where(Function(f) f.CONTRACT_TYPE_NAME.ToUpper.Contains(_filter.CONTRACT_TYPE_NAME.ToUpper))
            End If
            If _filter.ORG_NAME <> "" Then
                lst = lst.Where(Function(f) f.ORG_NAME.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
            End If
            If _filter.TITLE_NAME <> "" Then
                lst = lst.Where(Function(f) f.TITLE_NAME.ToUpper.Contains(_filter.TITLE_NAME.ToUpper))
            End If
            If _filter.ORG_NAME <> "" Then
                lst = lst.Where(Function(f) f.ORG_NAME.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
            End If
            If _filter.EMPLOYEE_CODE <> "" Then
                lst = lst.Where(Function(f) f.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If
            If _filter.EMPLOYEE_NAME <> "" Then
                lst = lst.Where(Function(f) f.EMPLOYEE_NAME.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper))
            End If
            If _filter.STAFF_RANK_NAME <> "" Then
                lst = lst.Where(Function(f) f.STAFF_RANK_NAME.ToUpper.Contains(_filter.STAFF_RANK_NAME.ToUpper))
            End If
            If _filter.PERCENT_SALARY IsNot Nothing Then
                lst = lst.Where(Function(f) f.PERCENT_SALARY = _filter.PERCENT_SALARY)
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

#End Region

#Region "LOG"
    Public Function GetActionLog(ByVal _filter As PA_ACTION_LOGDTO,
                                        ByRef Total As Integer,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        Optional ByVal Sorts As String = "ACTION_DATE desc") As List(Of PA_ACTION_LOGDTO)

        Try
            Dim query = From p In Context.PA_ACTION_LOG
                        From e In Context.SE_USER.Where(Function(f) f.USERNAME.ToUpper = p.USERNAME.ToUpper)
                        From r In Context.AT_PERIOD.Where(Function(f) f.ID = p.PERIOD_ID).DefaultIfEmpty

            Dim lst = query.Select(Function(p) New PA_ACTION_LOGDTO With {
                                       .ID = p.p.ID,
                                       .username = p.p.USERNAME,
                                       .fullname = p.e.FULLNAME,
                                       .email = p.e.EMAIL,
                                       .mobile = p.e.TELEPHONE,
                                       .action_name = p.p.ACTION_NAME,
                                       .action_date = p.p.ACTION_DATE,
                                       .object_name = p.p.OBJECT_NAME,
                                       .PERIOD_ID = p.p.PERIOD_ID,
                                       .PERIOD_NAME = p.r.PERIOD_NAME,
                                       .ip = p.p.IP,
                                       .computer_name = p.p.COMPUTER_NAME,
                                       .action_type = p.p.ACTION_TYPE,
                                       .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                       .NEW_VALUE = p.p.NEW_VALUE,
                                       .OLD_VALUE = p.p.OLD_VALUE})


            If Not String.IsNullOrEmpty(_filter.username) Then
                lst = lst.Where(Function(f) f.username.ToLower().Contains(_filter.username.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.fullname) Then
                lst = lst.Where(Function(f) f.fullname.ToLower().Contains(_filter.fullname.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.email) Then
                lst = lst.Where(Function(f) f.email.ToLower().Contains(_filter.email.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.mobile) Then
                lst = lst.Where(Function(f) f.mobile.ToLower().Contains(_filter.mobile.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.action_name) Then
                lst = lst.Where(Function(f) f.action_name.ToLower().Contains(_filter.action_name.ToLower()))
            End If
            If _filter.action_date.HasValue Then
                lst = lst.Where(Function(f) f.action_date >= _filter.action_date)
            End If
            If Not String.IsNullOrEmpty(_filter.object_name) Then
                lst = lst.Where(Function(f) f.object_name.ToLower().Contains(_filter.object_name.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ip) Then
                lst = lst.Where(Function(f) f.ip.ToLower().Contains(_filter.ip.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.computer_name) Then
                lst = lst.Where(Function(f) f.computer_name.ToLower().Contains(_filter.computer_name.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.action_type) Then
                lst = lst.Where(Function(f) f.action_type.ToLower().Contains(_filter.action_type.ToLower()))
            End If
            If _filter.EMPLOYEE_ID.HasValue Then
                lst = lst.Where(Function(f) f.EMPLOYEE_ID = _filter.EMPLOYEE_ID)
            End If
            If Not String.IsNullOrEmpty(_filter.NEW_VALUE) Then
                lst = lst.Where(Function(f) f.NEW_VALUE.ToLower().Contains(_filter.NEW_VALUE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.PERIOD_NAME) Then
                lst = lst.Where(Function(f) f.PERIOD_NAME.ToLower().Contains(_filter.PERIOD_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.OLD_VALUE) Then
                lst = lst.Where(Function(f) f.OLD_VALUE.ToLower().Contains(_filter.OLD_VALUE.ToLower()))
            End If
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function DeleteActionLogsPA(ByVal lstDeleteIds As List(Of Decimal)) As Integer
        Dim lstData As List(Of PA_ACTION_LOG)
        Try
            lstData = (From p In Context.PA_ACTION_LOG Where lstDeleteIds.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                Context.PA_ACTION_LOG.DeleteObject(lstData(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function LOG_PA(ByVal log As UserLog,
                           ByVal Object_Name As String,
                           ByVal action As PA_ACTION_LOGDTO) As Boolean
        Dim ActionId As Decimal?
        Dim action_log As New PA_ACTION_LOG
        action_log.ID = Utilities.GetNextSequence(Context, Context.PA_ACTION_LOG.EntitySet.Name)
        ActionId = action_log.ID
        action_log.USERNAME = log.Username.ToUpper
        action_log.IP = log.Ip
        action_log.ACTION_NAME = log.ActionName
        action_log.ACTION_DATE = DateTime.Now
        action_log.OBJECT_NAME = Object_Name
        action_log.COMPUTER_NAME = log.ComputerName
        action_log.EMPLOYEE_ID = action.EMPLOYEE_ID
        action_log.OLD_VALUE = action.OLD_VALUE
        action_log.PERIOD_ID = action.PERIOD_ID
        action_log.NEW_VALUE = action.NEW_VALUE
        Context.PA_ACTION_LOG.AddObject(action_log)
        'If lstEmployee.Count > 0 Then
        '    Dim action_logOrg As AT_ACTION_ORG_LOG
        '    For Each emp As Decimal? In lstEmployee
        '        action_logOrg = New AT_ACTION_ORG_LOG
        '        action_logOrg.ID = Utilities.GetNextSequence(Context, Context.AT_ACTION_ORG_LOG.EntitySet.Name)
        '        action_logOrg.EMPLOYEE_ID = emp
        '        action_logOrg.ACTION_LOG_ID = ActionId
        '        Context.AT_ACTION_ORG_LOG.AddObject(action_logOrg)
        '    Next
        'Else
        '    Using cls As New DataAccess.NonQueryData
        '        cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.INSERT_CHOSEN_LOGORG",
        '                     New With {.P_USERNAME = log.Username.ToUpper,
        '                               .P_ORGID = _param.ORG_ID,
        '                               .P_ISDISSOLVE = _param.IS_DISSOLVE,
        '                               .P_ACTION_ID = ActionId})
        '    End Using
        'End If
        Context.SaveChanges()
        Return True
    End Function
#End Region

#Region "MailRemark "

    Public Function GetMailRemark(ByVal _filter As PAMailRemarkDTO,
                                  ByVal PageIndex As Integer,
                                  ByVal PageSize As Integer,
                                  ByRef Total As Integer, ByVal log As UserLog,
                                  Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PAMailRemarkDTO)
        Try

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username.ToUpper,
                                           .P_ORGID = 46,
                                           .P_ISDISSOLVE = True})
            End Using

            Dim query = From p In Context.PA_MAIL_REMARK
                        From pr In Context.AT_PERIOD.Where(Function(f) f.ID = p.PERIOD_ID)
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID)
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) p.ORG_ID = f.ORG_ID And
                                                                  f.USERNAME.ToUpper = log.Username.ToUpper)
                        Where pr.ID = _filter.PERIOD_ID
                        Select New PAMailRemarkDTO With {
                            .ID = p.ID,
                            .ORG_ID = p.ORG_ID,
                            .ORG_NAME = o.NAME_VN,
                            .PERIOD_ID = p.PERIOD_ID,
                            .PERIOD_NAME = pr.PERIOD_NAME,
                            .REMARK = p.REMARK,
                            .CREATED_DATE = p.CREATED_DATE}

            If _filter.PERIOD_NAME <> "" Then
                query = query.Where(Function(f) f.PERIOD_NAME.ToUpper.Contains(_filter.PERIOD_NAME.ToUpper))
            End If

            If _filter.ORG_NAME <> "" Then
                query = query.Where(Function(f) f.ORG_NAME.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
            End If

            If _filter.REMARK <> "" Then
                query = query.Where(Function(f) f.REMARK.ToUpper.Contains(_filter.REMARK.ToUpper))
            End If

            query = query.OrderBy(Sorts)
            Total = query.Count
            query = query.Skip(PageIndex * PageSize).Take(PageSize)

            Return query.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function InsertMailRemark(ByVal objTitle As PAMailRemarkDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As PA_MAIL_REMARK
        Dim iCount As Integer = 0
        Try
            objTitleData = (From p In Context.PA_MAIL_REMARK
                            Where p.PERIOD_ID = objTitle.PERIOD_ID And
                            p.ORG_ID = objTitle.ORG_ID).FirstOrDefault
            If objTitleData Is Nothing Then
                objTitleData = New PA_MAIL_REMARK
                objTitleData.ID = Utilities.GetNextSequence(Context, Context.PA_MAIL_REMARK.EntitySet.Name)
                objTitleData.ORG_ID = objTitle.ORG_ID
                objTitleData.PERIOD_ID = objTitle.PERIOD_ID
                objTitleData.REMARK = objTitle.REMARK
                Context.PA_MAIL_REMARK.AddObject(objTitleData)
            Else
                objTitleData.REMARK = objTitle.REMARK
            End If
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function ModifyMailRemark(ByVal objTitle As PAMailRemarkDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New PA_MAIL_REMARK With {.ID = objTitle.ID}
        Try
            objTitleData = (From p In Context.PA_MAIL_REMARK Where p.ID = objTitleData.ID).FirstOrDefault
            objTitleData.REMARK = objTitle.REMARK
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function DeleteMailRemark(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstMailRemarkData As List(Of PA_MAIL_REMARK)
        Try
            lstMailRemarkData = (From p In Context.PA_MAIL_REMARK Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstMailRemarkData.Count - 1
                Context.PA_MAIL_REMARK.DeleteObject(lstMailRemarkData(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteMailRemark")
            Throw ex
        End Try
    End Function

#End Region


End Class

