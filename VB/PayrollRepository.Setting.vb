Imports System.Linq.Expressions
Imports LinqKit.Extensions
Imports System.Data.Common
Imports Framework.Data
Imports Framework.Data.System.Linq.Dynamic
Imports System.Data.Entity
Imports System.Text.RegularExpressions
Imports System.Data.Objects
Imports System.Reflection

Partial Public Class PayrollRepository

#Region "Hold Salary"

    Public Function GetHoldSalaryList(ByVal PeriodId As Integer, ByVal OrgId As Integer, ByVal IsDissolve As Integer, ByVal log As UserLog,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PAHoldSalaryDTO)

        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = OrgId,
                                           .P_ISDISSOLVE = IsDissolve})
            End Using
            Dim query = From p In Context.PA_HOLD_SALARY
                        From pe In Context.AT_PERIOD.Where(Function(f) f.ID = p.PERIOD_ID)
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From l In Context.PA_PAYROLLSHEET_SUM.Where(Function(f) f.EMPLOYEE_ID = e.ID And pe.ID = f.PERIOD_ID).DefaultIfEmpty
                        From o In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = e.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper)

            query = query.Where(Function(f) f.p.PERIOD_ID = PeriodId)
            Dim lst = query.Select(Function(p) New PAHoldSalaryDTO With {
                                       .ID = p.p.ID,
                                       .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                       .FULLNAME_VN = p.e.FULLNAME_VN,
                                       .FULLNAME_EN = p.e.FULLNAME_EN,
                                       .ORG_NAME = p.org.NAME_VN,
                                       .TOTAL_SALARY = p.l.THUNHAP_THUCNHAN,
                                       .PERIOD_ID = p.p.PERIOD_ID,
                                       .PERIOD_NAME = p.pe.PERIOD_NAME,
                                       .CREATED_DATE = p.p.CREATED_DATE,
                                       .CREATED_BY = p.p.CREATED_BY})
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Return lst.ToList

            'Using cls As New DataAccess.QueryData
            '    Dim dtData As DataTable = cls.ExecuteStore("PKG_PA_SETTING.GET_HOLDSALARY_LIST",
            '                               New With {.P_ORG_ID = OrgId,
            '                                         .P_PERIOD_ID = PeriodId,
            '                                         .P_USERNAME = log.Username,
            '                                         .P_CUR = cls.OUT_CURSOR})
            '    Total = dtData.ToList(Of PAHoldSalaryDTO)().Count
            '    Return dtData.AsEnumerable().ToList().Skip(PageIndex * PageSize).Take(PageSize)
            'End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function InsertHoldSalary(ByVal objPeriod As List(Of PAHoldSalaryDTO), ByVal log As UserLog) As Boolean
        Dim iCount As Integer = 0
        Try
            For Each item As PAHoldSalaryDTO In objPeriod
                Dim exists = (From p In Context.PA_HOLD_SALARY Where p.PERIOD_ID = item.PERIOD_ID And p.EMPLOYEE_ID = item.EMPLOYEE_ID).FirstOrDefault
                If exists IsNot Nothing AndAlso exists.ID > 0 Then
                    exists.EMPLOYEE_ID = item.EMPLOYEE_ID
                    exists.PERIOD_ID = item.PERIOD_ID
                Else
                    Dim objHoldSalaryData As New PA_HOLD_SALARY
                    objHoldSalaryData.ID = Utilities.GetNextSequence(Context, Context.PA_HOLD_SALARY.EntitySet.Name)
                    objHoldSalaryData.EMPLOYEE_ID = item.EMPLOYEE_ID
                    objHoldSalaryData.PERIOD_ID = item.PERIOD_ID
                    Context.PA_HOLD_SALARY.AddObject(objHoldSalaryData)
                End If
                Context.SaveChanges(log)
            Next
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function DeleteHoldSalary(ByVal lstDelete As List(Of Decimal)) As Boolean
        Try

            For Each item As Decimal In lstDelete
                Dim DeleteData = (From d In Context.PA_HOLD_SALARY Where d.ID = item).SingleOrDefault
                If DeleteData.ID > 0 Then
                    Context.PA_HOLD_SALARY.DeleteObject(DeleteData)
                End If
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

#End Region



End Class

