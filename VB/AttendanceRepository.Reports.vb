Imports Framework.Data
Imports LinqKit
Imports System.Data.Objects.DataClasses
Imports System.Data.Common
Imports System.Data.Entity
Imports System.Threading
Imports Framework.Data.System.Linq.Dynamic

Imports System.Configuration
Imports System.Reflection

Partial Public Class AttendanceRepository

#Region "Báo cáo"
    Public Function GET_REPORT() As DataTable
        Using cls As New DataAccess.QueryData
            Dim dtData As DataTable = cls.ExecuteStore("PKG_COMMON_LIST.GET_REPORT",
                                           New With {.P_LIKE = "AT",
                                                     .CUR = cls.OUT_CURSOR})
            Return dtData
        End Using
        Return Nothing
    End Function

    Public Function GetReportById(ByVal _filter As Se_ReportDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        ByVal log As UserLog,
                                        Optional ByVal Sorts As String = "CODE ASC") As List(Of Se_ReportDTO)

        Try
            Dim query As IQueryable(Of Se_ReportDTO)
            If log.Username.ToUpper <> "ADMIN" And log.Username.ToUpper <> "SYS.ADMIN" And log.Username.ToUpper <> "HR.ADMIN" Then
                query = From u In Context.SE_USER
                        From p In u.SE_REPORT
                        Where u.USERNAME.ToUpper = log.Username.ToUpper And p.MODULE_ID = _filter.MODULE_ID
                        Select New Se_ReportDTO With {
                            .ID = p.ID,
                            .CODE = p.CODE,
                            .NAME = p.NAME,
                            .MODULE_ID = p.MODULE_ID}
            Else
                query = From p In Context.SE_REPORT
                        Where p.MODULE_ID = _filter.MODULE_ID
                        Select New Se_ReportDTO With {
                            .ID = p.ID,
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
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GETORGNAME(ByVal obj As ParamDTO, ByVal log As UserLog) As DataTable
        Using cls As New DataAccess.QueryData
            Dim dtData As DataTable = cls.ExecuteStore("PKG_ATTENDANCE_REPORT.GETORGNAME",
                                           New With {.P_USERNAME = log.Username,
                                                     .P_ORG_ID = obj.S_ORG_ID,
                                                     .P_CUR = cls.OUT_CURSOR})
            Return dtData
        End Using
        Return Nothing
    End Function

    Public Function GET_AT001(ByVal obj As ParamDTO, ByVal log As UserLog) As DataSet
        Using cls As New DataAccess.QueryData
            Dim dtData As DataSet = cls.ExecuteStore("PKG_ATTENDANCE_REPORT.AT001",
                                           New With {.P_USERNAME = log.Username.ToUpper,
                                                     .P_ORG_ID = obj.S_ORG_ID,
                                                     .P_PERIOD_ID = obj.PERIOD_ID,
                                                     .P_COL_PERIOD = cls.OUT_CURSOR,
                                                     .P_COL_MANUAL = cls.OUT_CURSOR,
                                                     .P_CUR_PERIOD = cls.OUT_CURSOR,
                                                     .P_CUR_MANUAL = cls.OUT_CURSOR}, False)
            Return dtData
        End Using
        Return Nothing
    End Function

    Public Function GET_AT002(ByVal obj As ParamDTO, ByVal log As UserLog) As DataSet
        Using cls As New DataAccess.QueryData
            Dim dtData As DataSet = cls.ExecuteStore("PKG_ATTENDANCE_REPORT.AT002",
                                           New With {.P_USERNAME = log.Username.ToUpper,
                                                     .P_ORG_ID = obj.S_ORG_ID,
                                                     .P_PERIOD_ID = obj.PERIOD_ID,
                                                     .P_COL_PERIOD = cls.OUT_CURSOR,
                                                     .P_CUR_PERIOD = cls.OUT_CURSOR}, False)
            Return dtData
        End Using
        Return Nothing
    End Function

    Public Function GET_AT003(ByVal obj As ParamDTO, ByVal log As UserLog) As DataSet
        Using cls As New DataAccess.QueryData
            Dim dtData As DataSet = cls.ExecuteStore("PKG_ATTENDANCE_REPORT.AT003",
                                           New With {.P_USERNAME = log.Username.ToUpper,
                                                     .P_YEAR = obj.YEAR,
                                                     .P_ORG_ID = obj.S_ORG_ID,
                                                     .P_LEAVE = obj.IS_FULL,
                                                     .P_CUR_DETAILS = cls.OUT_CURSOR,
                                                     .P_CUR1 = cls.OUT_CURSOR}, False)
            Return dtData
        End Using
        Return Nothing
    End Function

    Public Function GET_AT004(ByVal obj As ParamDTO, ByVal log As UserLog) As DataSet
        Using cls As New DataAccess.QueryData
            Dim dtData As DataSet = cls.ExecuteStore("PKG_ATTENDANCE_REPORT.AT004",
                                           New With {.P_ORG = obj.S_ORG_ID,
                                                     .P_ISDISSOLVE = obj.IS_DISSOLVE,
                                                     .P_USERNAME = log.Username,
                                                     .P_PERIOD = obj.PERIOD_ID,
                                                     .P_CUR = cls.OUT_CURSOR,
                                                     .P_CUR1 = cls.OUT_CURSOR,
                                                     .P_CUR2 = cls.OUT_CURSOR}, False)
            Return dtData
        End Using
        Return Nothing
    End Function

    Public Function GET_AT005(ByVal obj As ParamDTO, ByVal log As UserLog) As DataSet
        Using cls As New DataAccess.QueryData
            Dim dtData As DataSet = cls.ExecuteStore("PKG_ATTENDANCE_REPORT.AT005",
                                           New With {.P_USERNAME = log.Username.ToUpper,
                                                     .P_YEAR = obj.YEAR,
                                                     .P_ORG_ID = obj.S_ORG_ID,
                                                     .P_LEAVE = obj.IS_FULL,
                                                     .P_CUR_DETAILS = cls.OUT_CURSOR}, False)
            Return dtData
        End Using
        Return Nothing
    End Function

    Public Function GET_AT006(ByVal obj As ParamDTO, ByVal log As UserLog) As DataSet
        Using cls As New DataAccess.QueryData
            Dim dtData As DataSet = cls.ExecuteStore("PKG_ATTENDANCE_REPORT.AT006",
                                           New With {.P_ORG = obj.S_ORG_ID,
                                                     .P_ISDISSOLVE = obj.IS_DISSOLVE,
                                                     .P_USERNAME = log.Username,
                                                     .P_PERIOD = obj.PERIOD_ID,
                                                     .P_CUR = cls.OUT_CURSOR,
                                                     .P_CUR1 = cls.OUT_CURSOR,
                                                     .P_CUR2 = cls.OUT_CURSOR}, False)
            Return dtData
        End Using
        Return Nothing
    End Function

    Public Function GET_AT007(ByVal obj As ParamDTO, ByVal log As UserLog) As DataSet
        Using cls As New DataAccess.QueryData
            Dim dtData As DataSet = cls.ExecuteStore("PKG_ATTENDANCE_REPORT.AT007",
                                           New With {.P_ORG = obj.S_ORG_ID,
                                                     .P_ISDISSOLVE = obj.IS_DISSOLVE,
                                                     .P_USERNAME = log.Username,
                                                     .P_PERIOD = obj.PERIOD_ID,
                                                     .P_CUR = cls.OUT_CURSOR,
                                                     .P_CUR1 = cls.OUT_CURSOR}, False)
            Return dtData
        End Using
        Return Nothing
    End Function

#End Region

#Region "Meal"

    Public Function ExportReport(ByVal _reportCode As String,
                                 ByVal _pkgName As String,
                                 ByVal _param As ParamDTO,
                                 ByVal log As UserLog) As DataSet
        Dim obj As Object
        Using cls As New DataAccess.QueryData
            Select Case _reportCode
                Case "ME_001"
                    obj = New With {.P_ORG = _param.S_ORG_ID,
                                    .P_USERNAME = log.Username,
                                    .P_ISDISSOLVE = _param.IS_DISSOLVE,
                                    .P_STARTDATE = _param.FROMDATE,
                                    .P_ENDDATE = _param.ENDDATE,
                                    .P_CUR = cls.OUT_CURSOR,
                                    .P_CUR1 = cls.OUT_CURSOR}
                Case "ME_002"
                    obj = New With {.P_ORG = _param.S_ORG_ID,
                                    .P_USERNAME = log.Username,
                                    .P_ISDISSOLVE = _param.IS_DISSOLVE,
                                    .P_STARTDATE = _param.FROMDATE.Value,
                                    .P_ENDDATE = _param.ENDDATE.Value,
                                    .P_CUR = cls.OUT_CURSOR,
                                    .P_CUR1 = cls.OUT_CURSOR}
                Case "ME_003"
                    obj = New With {.P_ORG = _param.S_ORG_ID,
                                    .P_USERNAME = log.Username,
                                    .P_ISDISSOLVE = _param.IS_DISSOLVE,
                                    .P_STARTDATE = _param.FROMDATE.Value,
                                    .P_ENDDATE = _param.ENDDATE.Value,
                                    .P_CUR = cls.OUT_CURSOR,
                                    .P_CUR1 = cls.OUT_CURSOR}
                Case "ME_004"
                    obj = New With {.P_ORG = _param.S_ORG_ID,
                                    .P_USERNAME = log.Username,
                                    .P_ISDISSOLVE = _param.IS_DISSOLVE,
                                    .P_STARTDATE = _param.FROMDATE.Value.FirstDateOfMonth,
                                    .P_ENDDATE = _param.ENDDATE.Value.LastDateOfMonth,
                                    .P_CUR = cls.OUT_CURSOR,
                                    .P_CUR1 = cls.OUT_CURSOR}
                Case "ME_005"
                    obj = New With {.P_ORG = _param.S_ORG_ID,
                                    .P_USERNAME = log.Username,
                                    .P_ISDISSOLVE = _param.IS_DISSOLVE,
                                    .P_STARTDATE = _param.FROMDATE.Value,
                                    .P_ENDDATE = _param.ENDDATE.Value,
                                    .P_CUR = cls.OUT_CURSOR,
                                    .P_CUR1 = cls.OUT_CURSOR}
            End Select
            If obj Is Nothing Then
                Return New DataSet
            End If
            Dim dtData As DataSet = cls.ExecuteStore(_pkgName, obj, False)
            Return dtData
        End Using
        Return Nothing
    End Function

#End Region

End Class
