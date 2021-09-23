Imports System.Data.Objects
Imports System.Configuration
Imports System.Linq.Expressions
Imports LinqKit.Extensions
Imports System.Data.Entity
Imports System.Text
Imports System.Runtime.CompilerServices
Imports System.Data.Common
Imports System.Threading
Imports Framework.Data
Imports Framework.Data.System.Linq.Dynamic
Imports System.Reflection
Imports Framework.Data.DataAccess
Imports Oracle.DataAccess.Client

Partial Public Class AttendanceRepository
    Dim ls_AT_SWIPE_DATADTO As New List(Of AT_SWIPE_DATADTO)

#Region "Di som ve muon"
    Public Function GetDSVM(ByVal _filter As AT_LATE_COMBACKOUTDTO,
                                     ByVal _param As ParamDTO,
                                     Optional ByRef Total As Integer = 0,
                                     Optional ByVal PageIndex As Integer = 0,
                                     Optional ByVal PageSize As Integer = Integer.MaxValue,
                                     Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_LATE_COMBACKOUTDTO)
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username.ToUpper,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using
            Dim query = From p In Context.AT_LATE_COMBACKOUT
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From c In Context.HU_STAFF_RANK.Where(Function(f) f.ID = e.STAFF_RANK_ID).DefaultIfEmpty
                        From type In Context.AT_TIME_MANUAL.Where(Function(f) f.ID = p.TYPE_DSVM).DefaultIfEmpty
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) e.ORG_ID = f.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper)

            Dim lst = query.Select(Function(p) New AT_LATE_COMBACKOUTDTO With {
                                       .ID = p.p.ID,
                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                       .VN_FULLNAME = p.e.FULLNAME_VN,
                                       .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                       .TITLE_NAME = p.t.NAME_VN,
                                       .TYPE_DMVS_NAME = p.type.NAME,
                                       .TO_HOUR = p.p.TO_HOUR,
                                       .FROM_HOUR = p.p.FROM_HOUR,
                                       .MINUTE = p.p.MINUTE,
                                       .STAFF_RANK_NAME = p.c.NAME,
                                       .WORKINGDAY = p.p.WORKINGDAY,
                                       .REMARK = p.p.REMARK,
                                       .ORG_ID = p.p.ORG_ID,
                                       .ORG_NAME = p.o.NAME_VN,
                                       .ORG_DESC = p.o.DESCRIPTION_PATH,
                                       .WORK_STATUS = p.e.WORK_STATUS,
                                       .TER_LAST_DATE = p.e.TER_LAST_DATE,
                                       .IS_BETWEEN_SHIFT = p.p.IS_BETWEEN_SHIFT,
                                       .CREATED_BY = p.p.CREATED_BY,
                                       .CREATED_DATE = p.p.CREATED_DATE,
                                       .CREATED_LOG = p.p.CREATED_LOG,
                                       .MODIFIED_BY = p.p.MODIFIED_BY,
                                       .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                       .MODIFIED_LOG = p.p.MODIFIED_LOG})
            If _filter.IS_TERMINATE Then
                lst = lst.Where(Function(f) f.WORK_STATUS = 257)
                If _filter.WORKINGDAY.HasValue Then
                    lst = lst.Where(Function(f) f.TER_LAST_DATE <= _filter.WORKINGDAY)
                End If
            End If
            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                lst = lst.Where(Function(f) f.EMPLOYEE_CODE.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower()) Or f.VN_FULLNAME.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower()))
            End If

            If Not String.IsNullOrEmpty(_filter.VN_FULLNAME) Then
                lst = lst.Where(Function(f) f.VN_FULLNAME.ToLower().Contains(_filter.VN_FULLNAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TITLE_NAME) Then
                lst = lst.Where(Function(f) f.TITLE_NAME.ToLower().Contains(_filter.TITLE_NAME.ToLower()))
            End If
            If _filter.FROM_DATE.HasValue Then
                lst = lst.Where(Function(f) f.WORKINGDAY >= _filter.FROM_DATE)
            End If
            If _filter.END_DATE.HasValue Then
                lst = lst.Where(Function(f) f.WORKINGDAY <= _filter.END_DATE)
            End If
            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                lst = lst.Where(Function(f) f.ORG_NAME.ToLower().Contains(_filter.ORG_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.STAFF_RANK_NAME) Then
                lst = lst.Where(Function(f) f.STAFF_RANK_NAME.ToLower().Contains(_filter.STAFF_RANK_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TYPE_DMVS_NAME) Then
                lst = lst.Where(Function(f) f.TYPE_DMVS_NAME.ToLower().Contains(_filter.TYPE_DMVS_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.REMARK) Then
                lst = lst.Where(Function(f) f.REMARK.ToLower().Contains(_filter.REMARK.ToLower()))
            End If
            If _filter.MINUTE.HasValue Then
                lst = lst.Where(Function(f) f.MINUTE = _filter.MINUTE)
            End If


            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function GetLate_CombackoutById(ByVal _id As Decimal?) As AT_LATE_COMBACKOUTDTO
        Try

            Dim query = From p In Context.AT_LATE_COMBACKOUT
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        From c In Context.HU_STAFF_RANK.Where(Function(f) f.ID = e.STAFF_RANK_ID).DefaultIfEmpty
                        From type In Context.AT_TIME_MANUAL.Where(Function(f) f.ID = p.TYPE_DSVM).DefaultIfEmpty
                        Where p.ID = _id

            Dim lst = query.Select(Function(p) New AT_LATE_COMBACKOUTDTO With {
                                       .ID = p.p.ID,
                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                       .VN_FULLNAME = p.e.FULLNAME_VN,
                                       .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                       .TITLE_NAME = p.t.NAME_VN,
                                       .TYPE_DMVS_NAME = p.type.NAME,
                                       .TYPE_DMVS_ID = p.p.TYPE_DSVM,
                                       .TO_HOUR = p.p.TO_HOUR,
                                       .FROM_HOUR = p.p.FROM_HOUR,
                                       .MINUTE = p.p.MINUTE,
                                       .STAFF_RANK_NAME = p.c.NAME,
                                       .REMARK = p.p.REMARK,
                                       .ORG_ID = p.p.ORG_ID,
                                       .WORKINGDAY = p.p.WORKINGDAY,
                                       .ORG_NAME = p.o.NAME_VN,
                                       .WORK_STATUS = p.e.WORK_STATUS,
                                       .TER_LAST_DATE = p.e.TER_LAST_DATE,
                                       .IS_BETWEEN_SHIFT = p.p.IS_BETWEEN_SHIFT,
                                       .ORG_DESC = p.o.DESCRIPTION_PATH,
                                       .CREATED_BY = p.p.CREATED_BY,
                                       .CREATED_DATE = p.p.CREATED_DATE,
                                       .CREATED_LOG = p.p.CREATED_LOG,
                                       .MODIFIED_BY = p.p.MODIFIED_BY,
                                       .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                       .MODIFIED_LOG = p.p.MODIFIED_LOG}).FirstOrDefault
            Return lst
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ImportLate_combackout(ByVal objLate_combackout As AT_LATE_COMBACKOUTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objLate_combackoutData As AT_LATE_COMBACKOUT
        Dim exits As Boolean?
        Dim employee_id As Decimal?
        Dim org_id As Decimal?

        Dim sql = (From e In Context.HU_EMPLOYEE
                          From w In Context.HU_WORKING.Where(Function(f) f.EMPLOYEE_ID = e.ID)
                          Where e.EMPLOYEE_CODE = objLate_combackout.EMPLOYEE_CODE And e.JOIN_DATE <= objLate_combackout.WORKINGDAY And _
                          (e.TER_EFFECT_DATE Is Nothing Or _
                           (e.TER_EFFECT_DATE IsNot Nothing And _
                            e.TER_EFFECT_DATE > objLate_combackout.WORKINGDAY)) And w.EFFECT_DATE <= objLate_combackout.WORKINGDAY
                    Order By w.EFFECT_DATE Descending
                    Select w).FirstOrDefault
        If sql IsNot Nothing Then
            employee_id = sql.EMPLOYEE_ID
            org_id = sql.ORG_ID
        Else
            Exit Function
        End If

        If Not employee_id Is Nothing Then
            Try
                exits = (From p In Context.AT_LATE_COMBACKOUT _
                Where p.EMPLOYEE_ID = employee_id And p.WORKINGDAY = objLate_combackout.WORKINGDAY And p.TYPE_DSVM = objLate_combackout.TYPE_DMVS_ID And p.IS_BETWEEN_SHIFT = objLate_combackout.IS_BETWEEN_SHIFT).Any
                If exits Then
                    Dim objlate = (From p In Context.AT_LATE_COMBACKOUT _
                                   Where p.EMPLOYEE_ID = employee_id And p.WORKINGDAY = objLate_combackout.WORKINGDAY And p.TYPE_DSVM = objLate_combackout.TYPE_DMVS_ID And p.IS_BETWEEN_SHIFT = objLate_combackout.IS_BETWEEN_SHIFT).FirstOrDefault
                    objlate.EMPLOYEE_ID = employee_id
                    objlate.ORG_ID = org_id
                    objlate.TITLE_ID = sql.TITLE_ID
                    objlate.WORKINGDAY = objLate_combackout.WORKINGDAY
                    objlate.REMARK = objLate_combackout.REMARK
                    objlate.TYPE_DSVM = objLate_combackout.TYPE_DMVS_ID
                    objlate.MINUTE = objLate_combackout.MINUTE
                    objlate.TO_HOUR = objLate_combackout.TO_HOUR
                    objlate.FROM_HOUR = objLate_combackout.FROM_HOUR
                    objlate.IS_BETWEEN_SHIFT = objLate_combackout.IS_BETWEEN_SHIFT
                Else
                    objLate_combackoutData = New AT_LATE_COMBACKOUT
                    objLate_combackoutData.ID = Utilities.GetNextSequence(Context, Context.AT_LATE_COMBACKOUT.EntitySet.Name)
                    objLate_combackoutData.EMPLOYEE_ID = employee_id
                    objLate_combackoutData.ORG_ID = org_id
                    objLate_combackoutData.TITLE_ID = sql.TITLE_ID
                    objLate_combackoutData.MINUTE = objLate_combackout.MINUTE
                    objLate_combackoutData.TO_HOUR = objLate_combackout.TO_HOUR
                    objLate_combackoutData.FROM_HOUR = objLate_combackout.FROM_HOUR
                    objLate_combackoutData.WORKINGDAY = objLate_combackout.WORKINGDAY
                    objLate_combackoutData.TYPE_DSVM = objLate_combackout.TYPE_DMVS_ID
                    objLate_combackoutData.REMARK = objLate_combackout.REMARK
                    objLate_combackoutData.IS_BETWEEN_SHIFT = objLate_combackout.IS_BETWEEN_SHIFT
                    Context.AT_LATE_COMBACKOUT.AddObject(objLate_combackoutData)
                End If
                Context.SaveChanges(log)
                Return True
            Catch ex As Exception
                WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
                Throw ex
            End Try
        Else
            Return False
        End If
    End Function

    Public Function InsertLate_combackout(ByVal objRegisterDMVSList As List(Of AT_LATE_COMBACKOUTDTO), ByVal objLate_combackout As AT_LATE_COMBACKOUTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objLate_combackoutData As AT_LATE_COMBACKOUT
        Dim objData As New AT_LATE_COMBACKOUTDTO
        Dim exits As Boolean?
        Dim employee_id As Decimal?
        Dim org_id As Decimal?
        Try
            For index = 0 To objRegisterDMVSList.Count - 1
                objData = objRegisterDMVSList(index)
                Dim sql = (From e In Context.HU_EMPLOYEE
                                  From w In Context.HU_WORKING.Where(Function(f) f.EMPLOYEE_ID = e.ID)
                                  Where e.EMPLOYEE_CODE = objData.EMPLOYEE_CODE And e.JOIN_DATE <= objLate_combackout.WORKINGDAY And _
                                  (e.TER_EFFECT_DATE Is Nothing Or _
                                   (e.TER_EFFECT_DATE IsNot Nothing And _
                                    e.TER_EFFECT_DATE > objLate_combackout.WORKINGDAY)) And w.EFFECT_DATE <= objLate_combackout.WORKINGDAY
                            Order By w.EFFECT_DATE Descending
                            Select w).FirstOrDefault
                If sql IsNot Nothing Then
                    employee_id = sql.EMPLOYEE_ID
                    org_id = sql.ORG_ID
                Else
                    Exit Function
                End If
                If Not employee_id Is Nothing Then
                    exits = (From p In Context.AT_LATE_COMBACKOUT _
                    Where p.EMPLOYEE_ID = employee_id And p.WORKINGDAY = objLate_combackout.WORKINGDAY And p.TYPE_DSVM = objLate_combackout.TYPE_DMVS_ID And p.IS_BETWEEN_SHIFT = objLate_combackout.IS_BETWEEN_SHIFT).Any
                    If exits Then
                        Dim objlate = (From p In Context.AT_LATE_COMBACKOUT _
                                       Where p.EMPLOYEE_ID = employee_id And p.WORKINGDAY = objLate_combackout.WORKINGDAY And p.TYPE_DSVM = objLate_combackout.TYPE_DMVS_ID And p.IS_BETWEEN_SHIFT = objLate_combackout.IS_BETWEEN_SHIFT).FirstOrDefault
                        objlate.EMPLOYEE_ID = employee_id
                        objlate.ORG_ID = org_id
                        objlate.TITLE_ID = sql.TITLE_ID
                        objlate.WORKINGDAY = objLate_combackout.WORKINGDAY
                        objlate.REMARK = objLate_combackout.REMARK
                        objlate.TYPE_DSVM = objLate_combackout.TYPE_DMVS_ID
                        objlate.MINUTE = objLate_combackout.MINUTE
                        objlate.TO_HOUR = objLate_combackout.TO_HOUR
                        objlate.FROM_HOUR = objLate_combackout.FROM_HOUR
                        objlate.IS_BETWEEN_SHIFT = objLate_combackout.IS_BETWEEN_SHIFT
                    Else
                        objLate_combackoutData = New AT_LATE_COMBACKOUT
                        objLate_combackoutData.ID = Utilities.GetNextSequence(Context, Context.AT_LATE_COMBACKOUT.EntitySet.Name)
                        objLate_combackoutData.EMPLOYEE_ID = employee_id
                        objLate_combackoutData.ORG_ID = org_id
                        objLate_combackoutData.TITLE_ID = sql.TITLE_ID
                        objLate_combackoutData.MINUTE = objLate_combackout.MINUTE
                        objLate_combackoutData.TO_HOUR = objLate_combackout.TO_HOUR
                        objLate_combackoutData.FROM_HOUR = objLate_combackout.FROM_HOUR
                        objLate_combackoutData.WORKINGDAY = objLate_combackout.WORKINGDAY
                        objLate_combackoutData.TYPE_DSVM = objLate_combackout.TYPE_DMVS_ID
                        objLate_combackoutData.REMARK = objLate_combackout.REMARK
                        objLate_combackoutData.IS_BETWEEN_SHIFT = objLate_combackout.IS_BETWEEN_SHIFT
                        Context.AT_LATE_COMBACKOUT.AddObject(objLate_combackoutData)
                    End If
                    Context.SaveChanges(log)
                End If
            Next
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ValidateLate_combackout(ByVal _validate As AT_LATE_COMBACKOUTDTO)
        Dim query
        Try
            If _validate.WORKINGDAY.HasValue Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.AT_LATE_COMBACKOUT
                             Where p.WORKINGDAY = _validate.WORKINGDAY And p.EMPLOYEE_ID = _validate.EMPLOYEE_ID And p.ID <> _validate.ID And p.TYPE_DSVM = _validate.TYPE_DMVS_ID).Any
                Else
                    query = (From p In Context.AT_LATE_COMBACKOUT
                             Where p.WORKINGDAY = _validate.WORKINGDAY And p.EMPLOYEE_ID = _validate.EMPLOYEE_ID And p.TYPE_DSVM = _validate.TYPE_DMVS_ID).Any
                End If
                If query Then
                    Return True
                Else
                    Return False
                End If
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ModifyLate_combackout(ByVal objLate_combackout As AT_LATE_COMBACKOUTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objLate_combackoutData As New AT_LATE_COMBACKOUT With {.ID = objLate_combackout.ID}
        Try
            Dim objlate = (From p In Context.AT_LATE_COMBACKOUT Where p.ID = objLate_combackout.ID).FirstOrDefault
            'objlate.EMPLOYEE_ID = objLate_combackout.EMPLOYEE_ID không cho phép sửa nhân viên
            objlate.WORKINGDAY = objLate_combackout.WORKINGDAY
            objlate.ORG_ID = objLate_combackout.ORG_ID
            objlate.REMARK = objLate_combackout.REMARK
            objlate.TYPE_DSVM = objLate_combackout.TYPE_DMVS_ID
            objlate.MINUTE = objLate_combackout.MINUTE
            objlate.TO_HOUR = objLate_combackout.TO_HOUR
            objlate.FROM_HOUR = objLate_combackout.FROM_HOUR
            objlate.IS_BETWEEN_SHIFT = objLate_combackout.IS_BETWEEN_SHIFT
            Context.SaveChanges(log)
            gID = objLate_combackoutData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

    End Function

    Public Function DeleteLate_combackout(ByVal lstID As List(Of Decimal),
                                          ByVal _param As ParamDTO,
                                          ByVal period_id As Decimal,
                                          ByVal listEmployeeId As List(Of Decimal?),
                                          ByVal log As UserLog) As Boolean
        Dim lstl As List(Of AT_LATE_COMBACKOUT)
        Try
            If listEmployeeId Is Nothing Then
                listEmployeeId = New List(Of Decimal?)
            End If
            Dim obj As New AT_ACTION_LOGDTO
            obj.PERIOD_ID = period_id
            lstl = (From p In Context.AT_LATE_COMBACKOUT Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstl.Count - 1
                listEmployeeId.Add(lstl(index).EMPLOYEE_ID)
                Context.AT_LATE_COMBACKOUT.DeleteObject(lstl(index))
            Next
            Context.SaveChanges(log)
            LOG_AT(_param, log, listEmployeeId, "XÓA ĐĂNG KÝ ĐI MUỘN VỀ SỚM", obj, _param.ORG_ID)
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteCostCenter")
            Throw ex
        End Try
    End Function
#End Region

#Region "Cham cong may"
    Public Function Init_TimeTImesheetMachines(ByVal _param As ParamDTO,
                                               ByVal log As UserLog,
                                               ByVal p_fromdate As Date,
                                               ByVal p_enddate As Date,
                                               ByVal P_ORG_ID As Decimal,
                                               ByVal lstEmployee As List(Of Decimal?)) As Boolean
        Try
            Dim obj As New AT_ACTION_LOGDTO
            Using cls As New DataAccess.NonQueryData
                Dim Period = (From w In Context.AT_PERIOD Where w.START_DATE = p_fromdate).FirstOrDefault
                obj.PERIOD_ID = Period.ID

                'cls.ExecuteSQL("DELETE FROM SE_EMPLOYEE_CHOSEN S WHERE UPPER(S.USING_USER) ='" + log.Username.ToUpper + "'")

                'Dim dDay = p_fromdate
                'Dim objNew As SE_EMPLOYEE_CHOSEN
                'For Each emp As Decimal? In lstEmployee
                '    While dDay <= p_enddate
                '        objNew = New SE_EMPLOYEE_CHOSEN
                '        objNew.EMPLOYEE_ID = emp
                '        objNew.WORKINGDAY = dDay
                '        objNew.USING_USER = log.Username.ToUpper
                '        Context.SE_EMPLOYEE_CHOSEN.AddObject(objNew)
                '        dDay = dDay.AddDays(1)
                '    End While

                'Next
                LOG_AT(_param, log, lstEmployee, "TỔNG HỢP BẢNG CÔNG GỐC", obj, P_ORG_ID)

                cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.CAL_TIME_TIMESHEET_MACHINES",
                                               New With {.P_USERNAME = log.Username.ToUpper,
                                                         .P_ORG_ID = P_ORG_ID,
                                                         .P_FROMDATE = p_fromdate,
                                                         .P_ENDDATE = p_enddate,
                                                         .P_ISDISSOLVE = _param.IS_DISSOLVE})
                Return True

            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex

        End Try
    End Function

    Public Function GetMachines(ByVal _filter As AT_TIME_TIMESHEET_MACHINETDTO,
                                     ByVal _param As ParamDTO,
                                     Optional ByRef Total As Integer = 0,
                                     Optional ByVal PageIndex As Integer = 0,
                                     Optional ByVal PageSize As Integer = Integer.MaxValue,
                                     Optional ByVal Sorts As String = "EMPLOYEE_ID, WORKINGDAY", Optional ByVal log As UserLog = Nothing) As List(Of AT_TIME_TIMESHEET_MACHINETDTO)
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username.ToUpper,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using
            Dim query = From p In Context.AT_TIME_TIMESHEET_MACHINET
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From m In Context.AT_TIME_MANUAL.Where(Function(f) f.ID = p.MANUAL_ID).DefaultIfEmpty
                        From s In Context.HU_STAFF_RANK.Where(Function(f) f.ID = e.STAFF_RANK_ID).DefaultIfEmpty
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) p.ORG_ID = f.ORG_ID And
                                                                  f.USERNAME.ToUpper = log.Username.ToUpper.ToUpper)

            'If _filter.IS_TERMINATE Then
            '    query = query.Where(Function(f) f.e.WORK_STATUS = 257 And f.e.TER_LAST_DATE >= _filter.FROM_DATE)
            'End If
            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                query = query.Where(Function(f) f.e.EMPLOYEE_CODE.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.VN_FULLNAME) Then
                query = query.Where(Function(f) f.e.FULLNAME_VN.ToLower().Contains(_filter.VN_FULLNAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                query = query.Where(Function(f) f.o.NAME_VN.ToLower().Contains(_filter.ORG_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TITLE_NAME) Then
                query = query.Where(Function(f) f.t.NAME_VN.ToLower().Contains(_filter.TITLE_NAME.ToLower()))
            End If
            If _filter.WORKINGDAY.HasValue Then
                query = query.Where(Function(f) f.p.WORKINGDAY = _filter.WORKINGDAY)
            End If
            If Not String.IsNullOrEmpty(_filter.SHIFT_CODE) Then
                query = query.Where(Function(f) f.p.SHIFT_CODE.ToLower().Contains(_filter.SHIFT_CODE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.MANUAL_CODE) Then
                query = query.Where(Function(f) f.m.CODE.ToLower().Contains(_filter.MANUAL_CODE.ToLower()))
            End If
            If _filter.SHIFTIN.HasValue Then
                query = query.Where(Function(f) f.p.VALIN1 = _filter.SHIFTIN)
            End If
            If _filter.SHIFTOUT.HasValue Then
                query = query.Where(Function(f) f.p.VALIN4 = _filter.SHIFTOUT)
            End If
            If _filter.SHIFTBACKOUT.HasValue Then
                query = query.Where(Function(f) f.p.VALIN2 = _filter.SHIFTBACKOUT)
            End If
            If _filter.SHIFTBACKIN.HasValue Then
                query = query.Where(Function(f) f.p.VALIN3 = _filter.SHIFTBACKIN)
            End If
            If _filter.WORKINGHOUR.HasValue Then
                query = query.Where(Function(f) f.p.WORKINGHOUR = _filter.WORKINGHOUR)
            End If
            If Not String.IsNullOrEmpty(_filter.LEAVE_CODE) Then
                query = query.Where(Function(f) f.p.LEAVE_CODE.ToLower().Contains(_filter.LEAVE_CODE.ToLower()))
            End If

            If _filter.FROM_DATE.HasValue Then
                query = query.Where(Function(f) f.p.WORKINGDAY >= _filter.FROM_DATE)
            End If
            If _filter.END_DATE.HasValue Then
                query = query.Where(Function(f) f.p.WORKINGDAY <= _filter.END_DATE)
            End If

            Dim lst = query.Select(Function(p) New AT_TIME_TIMESHEET_MACHINETDTO With {
                                       .ID = p.p.ID,
                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                       .VN_FULLNAME = p.e.FULLNAME_VN,
                                       .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                       .TITLE_NAME = p.t.NAME_VN,
                                       .ORG_NAME = p.o.NAME_VN,
                                       .ORG_DESC = p.o.DESCRIPTION_PATH,
                                       .STAFF_RANK_NAME = p.s.NAME,
                                       .ORG_ID = p.p.ORG_ID,
                                       .WORKINGDAY = p.p.WORKINGDAY,
                                       .SHIFT_ID = p.p.SHIFT_ID,
                                       .SHIFT_CODE = p.p.SHIFT_CODE,
                                       .MANUAL_CODE = p.m.CODE,
                                       .LATE = p.p.LATE,
                                       .WORKINGHOUR = p.p.WORKINGHOUR,
                                       .SHIFTIN = p.p.VALIN1,
                                       .SHIFTBACKOUT = p.p.VALIN2,
                                       .SHIFTBACKIN = p.p.VALIN3,
                                       .SHIFTOUT = p.p.VALIN4,
                                       .COMEBACKOUT = p.p.COMEBACKOUT})
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
#End Region

#Region "Cham cong tay"

    Public Function GetCCT(ByVal param As AT_TIME_TIMESHEET_DAILYDTO, ByVal log As UserLog) As DataSet
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataSet = cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.GET_CCT",
                                               New With {.P_USERNAME = log.Username.ToUpper,
                                                         .P_ORG_ID = param.ORG_ID,
                                                         .P_ISDISSOLVE = param.IS_DISSOLVE,
                                                         .P_PAGE_INDEX = param.PAGE_INDEX,
                                                         .P_EMPLOYEE_CODE = param.EMPLOYEE_CODE,
                                                         .P_EMPLOYEE_NAME = param.VN_FULLNAME,
                                                         .P_ORG_NAME = param.ORG_NAME,
                                                         .P_TITLE_NAME = param.TITLE_NAME,
                                                         .P_PAGE_SIZE = param.PAGE_SIZE,
                                                         .P_PERIOD_ID = param.PERIOD_ID,
                                                         .P_TERMINATE = param.IS_TERMINATE,
                                                         .P_CUR = cls.OUT_CURSOR,
                                                         .P_CURCOUNT = cls.OUT_CURSOR}, False)
                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex

        End Try
    End Function

    Public Function GetCCT_Origin(ByVal param As AT_TIME_TIMESHEET_DAILYDTO, ByVal log As UserLog) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData = cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.GET_CCT_ORIGIN",
                                               New With {.P_USERNAME = log.Username.ToUpper,
                                                         .P_ORG_ID = param.ORG_ID,
                                                         .P_ISDISSOLVE = param.IS_DISSOLVE,
                                                         .P_EMPLOYEE_CODE = param.EMPLOYEE_CODE,
                                                         .P_EMPLOYEE_NAME = param.VN_FULLNAME,
                                                         .P_ORG_NAME = param.ORG_NAME,
                                                         .P_TITLE_NAME = param.TITLE_NAME,
                                                         .P_PERIOD_ID = param.PERIOD_ID,
                                                         .P_TERMINATE = param.IS_TERMINATE,
                                                         .P_CUR = cls.OUT_CURSOR})
                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex

        End Try
    End Function

    Public Function GetTimeSheetDailyById(ByVal obj As AT_TIME_TIMESHEET_DAILYDTO) As AT_TIME_TIMESHEET_DAILYDTO
        Try
            Dim query =
                      From e In Context.HU_EMPLOYEE
                      From p In Context.AT_TIME_TIMESHEET_DAILY.Where(Function(f) f.EMPLOYEE_ID = e.ID And f.WORKINGDAY = obj.WORKINGDAY).DefaultIfEmpty
                      From m In Context.AT_TIME_MANUAL.Where(Function(f) f.ID = p.MANUAL_ID).DefaultIfEmpty
                      From t In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID).DefaultIfEmpty
                      From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                      From s In Context.AT_SHIFT.Where(Function(f) f.ID = p.SHIFT_ID).DefaultIfEmpty
                      Where e.ID = obj.EMPLOYEE_ID
            Dim lst = query.Select(Function(p) New AT_TIME_TIMESHEET_DAILYDTO With {
                                       .ID = p.p.ID,
                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                       .VN_FULLNAME = p.e.FULLNAME_VN,
                                       .EMPLOYEE_ID = p.e.ID,
                                       .TITLE_NAME = p.t.NAME_VN,
                                       .ORG_ID = p.p.ORG_ID,
                                       .ORG_NAME = p.o.NAME_VN,
                                       .ORG_DESC = p.o.DESCRIPTION_PATH,
                                       .SHIFT_NAME = "[" & p.s.CODE & "] " & p.s.NAME_VN,
                                       .WORKINGDAY = p.p.WORKINGDAY,
                                       .SHIFT_ID = p.p.SHIFT_ID,
                                       .MANUAL_CODE = p.m.CODE,
                                       .MANUAL_ID = p.p.MANUAL_ID,
                                       .CREATED_BY = p.p.CREATED_BY,
                                       .CREATED_DATE = p.p.CREATED_DATE,
                                       .CREATED_LOG = p.p.CREATED_LOG,
                                       .MODIFIED_BY = p.p.MODIFIED_BY,
                                       .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                       .MODIFIED_LOG = p.p.MODIFIED_LOG}).FirstOrDefault
            Return lst
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function InsertLeaveSheetDaily(ByVal dtData As DataTable, ByVal log As UserLog, ByVal PeriodID As Decimal) As Boolean
        Try
            Dim startDate As Date

            Dim Period = (From w In Context.AT_PERIOD Where w.ID = PeriodID).FirstOrDefault

            Using conMng As New ConnectionManager
                Using conn As New OracleConnection(conMng.GetConnectionString())
                    Using cmd As New OracleCommand()
                        Using resource As New DataAccess.OracleCommon()
                            Try
                                conn.Open()
                                cmd.Connection = conn
                                cmd.CommandType = CommandType.StoredProcedure
                                cmd.CommandText = "PKG_ATTENDANCE_BUSINESS.INSERT_WORKSIGN_DATE"
                                cmd.Transaction = cmd.Connection.BeginTransaction()

                                For Each row As DataRow In dtData.Rows
                                    cmd.Parameters.Clear()
                                    Dim objParam = New With {.P_EMPLOYEEID = row("EMPLOYEE_ID").ToString,
                                                             .P_PERIODId = Period.ID,
                                                             .P_USERNAME = log.Username.ToUpper,
                                                             .P_D1 = Utilities.Obj2Decima(row("D1"), 0),
                                                             .P_D2 = Utilities.Obj2Decima(row("D2"), 0),
                                                             .P_D3 = Utilities.Obj2Decima(row("D3"), 0),
                                                             .P_D4 = Utilities.Obj2Decima(row("D4"), 0),
                                                             .P_D5 = Utilities.Obj2Decima(row("D5"), 0),
                                                             .P_D6 = Utilities.Obj2Decima(row("D6"), 0),
                                                             .P_D7 = Utilities.Obj2Decima(row("D7"), 0),
                                                             .P_D8 = Utilities.Obj2Decima(row("D8"), 0),
                                                             .P_D9 = Utilities.Obj2Decima(row("D9"), 0),
                                                             .P_D10 = Utilities.Obj2Decima(row("D10"), 0),
                                                             .P_D11 = Utilities.Obj2Decima(row("D11"), 0),
                                                             .P_D12 = Utilities.Obj2Decima(row("D12"), 0),
                                                             .P_D13 = Utilities.Obj2Decima(row("D13"), 0),
                                                             .P_D14 = Utilities.Obj2Decima(row("D14"), 0),
                                                             .P_D15 = Utilities.Obj2Decima(row("D15"), 0),
                                                             .P_D16 = Utilities.Obj2Decima(row("D16"), 0),
                                                             .P_D17 = Utilities.Obj2Decima(row("D17"), 0),
                                                             .P_D18 = Utilities.Obj2Decima(row("D18"), 0),
                                                             .P_D19 = Utilities.Obj2Decima(row("D19"), 0),
                                                             .P_D20 = Utilities.Obj2Decima(row("D20"), 0),
                                                             .P_D21 = Utilities.Obj2Decima(row("D21"), 0),
                                                             .P_D22 = Utilities.Obj2Decima(row("D22"), 0),
                                                             .P_D23 = Utilities.Obj2Decima(row("D23"), 0),
                                                             .P_D24 = Utilities.Obj2Decima(row("D24"), 0),
                                                             .P_D25 = Utilities.Obj2Decima(row("D25"), 0),
                                                             .P_D26 = Utilities.Obj2Decima(row("D26"), 0),
                                                             .P_D27 = Utilities.Obj2Decima(row("D27"), 0),
                                                             .P_D28 = Utilities.Obj2Decima(row("D28"), 0),
                                                             .P_D29 = Utilities.Obj2Decima(row("D29"), 0),
                                                             .P_D30 = Utilities.Obj2Decima(row("D30"), 0),
                                                             .P_D31 = Utilities.Obj2Decima(row("D31"), 0)}

                                    If objParam IsNot Nothing Then
                                        For Each info As PropertyInfo In objParam.GetType().GetProperties()
                                            Dim bOut As Boolean = False
                                            Dim para = resource.GetParameter(info.Name, info.GetValue(objParam, Nothing), bOut)
                                            If para IsNot Nothing Then
                                                cmd.Parameters.Add(para)
                                            End If
                                        Next
                                    End If
                                    cmd.ExecuteNonQuery()
                                Next

                                cmd.CommandText = "PKG_ATTENDANCE_BUSINESS.UPDATE_LEAVESHEET_DAILY"
                                cmd.Parameters.Clear()
                                Dim objParam1 = New With {.P_STARTDATE = Period.START_DATE.Value,
                                                         .P_ENDDATE = Period.END_DATE.Value,
                                                         .P_USERNAME = log.Username.ToUpper}

                                If objParam1 IsNot Nothing Then
                                    For Each info As PropertyInfo In objParam1.GetType().GetProperties()
                                        Dim bOut As Boolean = False
                                        Dim para = resource.GetParameter(info.Name, info.GetValue(objParam1, Nothing), bOut)
                                        If para IsNot Nothing Then
                                            cmd.Parameters.Add(para)
                                        End If
                                    Next
                                End If

                                cmd.ExecuteNonQuery()

                                cmd.Transaction.Commit()
                            Catch ex As Exception
                                WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
                                cmd.Transaction.Rollback()
                                Throw ex
                                Return False
                            Finally
                                'Dispose all resource
                                cmd.Dispose()
                                conn.Close()
                                conn.Dispose()
                            End Try
                        End Using
                    End Using
                End Using
            End Using
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
            Return False
        End Try
    End Function

    Public Function ModifyLeaveSheetDaily(ByVal objLeave As AT_TIME_TIMESHEET_DAILYDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Try
            Dim emp = (From e In Context.HU_EMPLOYEE Where e.ID = objLeave.EMPLOYEE_ID).FirstOrDefault
            Dim Period = (From w In Context.AT_PERIOD Where w.START_DATE <= objLeave.WORKINGDAY And objLeave.WORKINGDAY <= w.END_DATE).FirstOrDefault

            Dim TimeSheetDaily = (From r In Context.AT_TIME_TIMESHEET_DAILY Where r.EMPLOYEE_ID = emp.ID And r.WORKINGDAY = objLeave.WORKINGDAY).FirstOrDefault
            Dim manual_code As AT_TIME_MANUAL
            If TimeSheetDaily IsNot Nothing AndAlso TimeSheetDaily.MANUAL_ID IsNot Nothing Then
                manual_code = (From m In Context.AT_TIME_MANUAL.Where(Function(f) f.ID = TimeSheetDaily.MANUAL_ID)).FirstOrDefault
            End If

            Dim lstEmployee As New List(Of Decimal?)
            lstEmployee.Add(emp.ID)
            Dim obj As New AT_ACTION_LOGDTO
            obj.EMPLOYEE_ID = emp.ID
            If manual_code IsNot Nothing Then
                obj.OLD_VALUE = manual_code.CODE
            End If
            obj.NEW_VALUE = objLeave.MANUAL_CODE
            obj.PERIOD_ID = Period.ID
            LOG_AT(New ParamDTO, log, lstEmployee, "CHỈNH SỬA XỬ LÝ DỮ LIỆU CHẤM CÔNG", obj, Nothing)
            TimeSheetDaily.MANUAL_ID = objLeave.MANUAL_ID

            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
#End Region

#Region "Bang tong hop lam them"

    Public Function Cal_TimeTImesheet_OT(ByVal _param As ParamDTO,
                                         ByVal log As UserLog,
                                         ByVal p_period_id As Decimal?,
                                         ByVal P_ORG_ID As Decimal,
                                         ByVal lstEmployee As List(Of Decimal?)) As Boolean
        Try
            'Using cls As New DataAccess.NonQueryData
            '    cls.ExecuteSQL("DELETE FROM SE_EMPLOYEE_CHOSEN S WHERE  UPPER(S.USING_USER) ='" + log.Username.ToUpper + "'")
            'End Using
            'For Each emp As Decimal? In lstEmployee
            '    Dim objNew As New SE_EMPLOYEE_CHOSEN
            '    objNew.EMPLOYEE_ID = emp
            '    objNew.USING_USER = log.Username.ToUpper
            '    Context.SE_EMPLOYEE_CHOSEN.AddObject(objNew)
            'Next
            Dim obj As New AT_ACTION_LOGDTO
            obj.PERIOD_ID = p_period_id
            LOG_AT(_param, log, lstEmployee, "TỔNG HỢP CÔNG LÀM THÊM GIỜ", obj, P_ORG_ID)
            Using cls As New DataAccess.NonQueryData
                cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.CAL_TIMETIMESHEET_OT",
                                               New With {.P_USERNAME = log.Username.ToUpper,
                                                         .P_ORG_ID = P_ORG_ID,
                                                         .P_PERIOD_ID = p_period_id,
                                                         .P_ISDISSOLVE = _param.IS_DISSOLVE})
                Return True
            End Using

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex

        End Try
    End Function

    Public Function GetSummaryOT(ByVal param As AT_TIME_TIMESHEET_OTDTO, ByVal log As UserLog) As DataSet
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataSet = cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.GET_SUMMARY_OT",
                                               New With {.P_USERNAME = log.Username.ToUpper,
                                                         .P_ORG_ID = param.ORG_ID,
                                                         .P_ISDISSOLVE = param.IS_DISSOLVE,
                                                         .P_PAGE_INDEX = param.PAGE_INDEX,
                                                         .P_EMPLOYEE_CODE = param.EMPLOYEE_CODE,
                                                         .P_PAGE_SIZE = param.PAGE_SIZE,
                                                         .P_PERIOD_ID = param.PERIOD_ID,
                                                         .P_EMPLOYEE_NAME = param.VN_FULLNAME,
                                                         .P_ORG_NAME = param.ORG_NAME,
                                                         .P_TITLE_NAME = param.TITLE_NAME,
                                                         .P_STAFF_RANK_NAME = param.STAFF_RANK_NAME,
                                                         .P_CUR = cls.OUT_CURSOR,
                                                         .P_CURCOUNT = cls.OUT_CURSOR}, False)
                Return dtData
            End Using

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex

        End Try
    End Function

    Public Function Cal_TimeTImesheet_NB(ByVal _param As ParamDTO,
                                         ByVal log As UserLog,
                                         ByVal p_period_id As Decimal?,
                                         ByVal P_ORG_ID As Decimal,
                                         ByVal lstEmployee As List(Of Decimal?)) As Boolean
        Try
            Dim obj As New AT_ACTION_LOGDTO
            obj.PERIOD_ID = p_period_id
            LOG_AT(_param, log, lstEmployee, "TỔNG HỢP CÔNG NGHỈ BÙ", obj, P_ORG_ID)
            Using cls As New DataAccess.NonQueryData
                cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.CAL_TIMETIMESHEET_NB",
                                               New With {.P_USERNAME = log.Username.ToUpper,
                                                         .P_ORG_ID = P_ORG_ID,
                                                         .P_PERIOD_ID = p_period_id,
                                                         .P_ISDISSOLVE = _param.IS_DISSOLVE})
                Return True
            End Using

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex

        End Try
    End Function

    Public Function GetSummaryNB(ByVal param As AT_TIME_TIMESHEET_NBDTO, ByVal log As UserLog) As DataSet
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataSet = cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.GET_SUMMARY_NB",
                                               New With {.P_USERNAME = log.Username.ToUpper,
                                                         .P_ORG_ID = param.ORG_ID,
                                                         .P_ISDISSOLVE = param.IS_DISSOLVE,
                                                         .P_PAGE_INDEX = param.PAGE_INDEX,
                                                         .P_EMPLOYEE_CODE = param.EMPLOYEE_CODE,
                                                         .P_PAGE_SIZE = param.PAGE_SIZE,
                                                         .P_PERIOD_ID = param.PERIOD_ID,
                                                         .P_EMPLOYEE_NAME = param.VN_FULLNAME,
                                                         .P_ORG_NAME = param.ORG_NAME,
                                                         .P_TITLE_NAME = param.TITLE_NAME,
                                                         .P_STAFF_RANK_NAME = param.STAFF_RANK_NAME,
                                                         .P_CUR = cls.OUT_CURSOR,
                                                         .P_CURCOUNT = cls.OUT_CURSOR}, False)
                Return dtData
            End Using

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex

        End Try
    End Function

    Public Function GetTimeSheetOtById(ByVal obj As AT_TIME_TIMESHEET_OTDTO) As AT_TIME_TIMESHEET_OTDTO
        Try
            Dim query = From p In Context.AT_TIME_TIMESHEET_OT
                      From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                      From t In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID).DefaultIfEmpty
                      From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                      Where p.ID = obj.ID
            Dim lst = query.Select(Function(p) New AT_TIME_TIMESHEET_OTDTO With {
                                       .ID = p.p.ID,
                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                       .VN_FULLNAME = p.e.FULLNAME_VN,
                                       .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                       .TITLE_NAME = p.t.NAME_VN,
                                       .PERIOD_ID = p.p.PERIOD_ID,
                                       .ORG_NAME = p.o.NAME_VN,
                                       .ORG_ID = p.p.ORG_ID,
                                       .NUMBER_FACTOR_PAY = p.p.NUMBER_FACTOR_PAY,
                                       .NUMBER_FACTOR_CP = p.p.NUMBER_FACTOR_CP,
                                       .BACKUP_MONTH_BEFFORE = p.p.BACKUP_MONTH_BEFORE,
                                       .CREATED_BY = p.p.CREATED_BY,
                                       .CREATED_DATE = p.p.CREATED_DATE,
                                       .CREATED_LOG = p.p.CREATED_LOG,
                                       .MODIFIED_BY = p.p.MODIFIED_BY,
                                       .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                       .MODIFIED_LOG = p.p.MODIFIED_LOG}).FirstOrDefault
            Return lst
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function InsertLeaveSheetOt(ByVal objTimeSheetDaily As AT_TIME_TIMESHEET_OTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTimeSheetData As New AT_TIME_TIMESHEET_OT
        Try
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ModifyLeaveSheetOt(ByVal objLeave As AT_TIME_TIMESHEET_OTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Try
            Dim emp = (From e In Context.HU_EMPLOYEE Where e.EMPLOYEE_CODE.Equals(objLeave.EMPLOYEE_CODE)).FirstOrDefault
            Dim TimeSheetDaily = (From r In Context.AT_TIME_TIMESHEET_OT Where r.EMPLOYEE_ID = emp.ID And r.PERIOD_ID = objLeave.PERIOD_ID).FirstOrDefault
            TimeSheetDaily.NUMBER_FACTOR_PAY = objLeave.NUMBER_FACTOR_PAY
            TimeSheetDaily.NUMBER_FACTOR_CP = objLeave.NUMBER_FACTOR_CP
            TimeSheetDaily.BACKUP_MONTH_BEFORE = objLeave.BACKUP_MONTH_BEFFORE
            Dim congNB = Utilities.Obj2Decima((TimeSheetDaily.NUMBER_FACTOR_CP + TimeSheetDaily.BACKUP_MONTH_BEFORE) / 8)
            TimeSheetDaily.CONGHIBU = Math.Round(congNB, 1)
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

#End Region

#Region "Tổng hợp công"

    Public Function GetTimeSheet(ByVal _filter As AT_TIME_TIMESHEET_MONTHLYDTO,
                                     ByVal _param As ParamDTO,
                                     Optional ByRef Total As Integer = 0,
                                     Optional ByVal PageIndex As Integer = 0,
                                     Optional ByVal PageSize As Integer = Integer.MaxValue,
                                     Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_TIME_TIMESHEET_MONTHLYDTO)
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username.ToUpper,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using
            Dim query = From p In Context.AT_TIME_TIMESHEET_MONTHLY
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID).DefaultIfEmpty
                        From s In Context.HU_STAFF_RANK.Where(Function(f) f.ID = p.STAFF_RANK_ID).DefaultIfEmpty
                        From w In Context.HU_WORKING.Where(Function(f) f.ID = p.DECISION_ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = w.ORG_ID).DefaultIfEmpty
                        From po In Context.AT_PERIOD.Where(Function(f) f.ID = p.PERIOD_ID)
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) p.ORG_ID = f.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper)
                        Where p.PERIOD_ID = _filter.PERIOD_ID

            Dim lst = query.Select(Function(p) New AT_TIME_TIMESHEET_MONTHLYDTO With {
                                       .ID = p.p.ID,
                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                       .VN_FULLNAME = p.e.FULLNAME_VN,
                                       .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                       .TITLE_NAME = p.t.NAME_VN,
                                       .ORG_NAME = p.o.NAME_VN,
                                       .ORG_DESC = p.o.DESCRIPTION_PATH,
                                       .ORG_ID = p.p.ORG_ID,
                                       .PERIOD_ID = p.po.ID,
                                       .PERIOD_STANDARD = p.po.PERIOD_STANDARD,
                                       .STAFF_RANK_NAME = p.s.NAME,
                                       .DECISION_START = p.p.DECISION_START,
                                       .DECISION_END = p.p.DECISION_END,
                                       .WORKING_X = p.p.WORKING_X,
                                       .WORKING_F = p.p.WORKING_F,
                                       .WORKING_E = p.p.WORKING_E,
                                       .WORKING_A = p.p.WORKING_A,
                                       .WORKING_H = p.p.WORKING_H,
                                       .WORKING_D = p.p.WORKING_D,
                                       .WORKING_C = p.p.WORKING_C,
                                       .WORKING_T = p.p.WORKING_T,
                                       .WORKING_Q = p.p.WORKING_Q,
                                       .WORKING_N = p.p.WORKING_N,
                                       .WORKING_P = p.p.WORKING_P,
                                       .WORKING_L = p.p.WORKING_L,
                                       .WORKING_R = p.p.WORKING_R,
                                       .WORKING_S = p.p.WORKING_S,
                                       .WORKING_B = p.p.WORKING_B,
                                       .WORKING_K = p.p.WORKING_K,
                                       .WORKING_J = p.p.WORKING_J,
                                       .TOTAL_WORKING_XJ = p.p.TOTAL_WORKING_XJ,
                                       .WORKING_TS = p.p.WORKING_TS,
                                       .WORKING_O = p.p.WORKING_O,
                                       .WORKING_V = p.p.WORKING_V,
                                       .TOTAL_TS_V = p.p.TOTAL_TS_V,
                                       .WORKING_ADD = p.p.WORKING_ADD,
                                       .TOTAL_WORKING = p.p.TOTAL_WORKING,
                                       .TOTAL_OFF = p.p.TOTAL_OFF,
                                       .WORK_STATUS = p.e.WORK_STATUS,
                                       .TER_LAST_DATE = p.e.TER_LAST_DATE,
                                       .CREATED_BY = p.p.CREATED_BY,
                                       .CREATED_DATE = p.p.CREATED_DATE,
                                       .CREATED_LOG = p.p.CREATED_LOG,
                                       .MODIFIED_BY = p.p.MODIFIED_BY,
                                       .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                       .MODIFIED_LOG = p.p.MODIFIED_LOG})

            'If _filter.IS_TERMINATE Then
            '    lst = lst.Where(Function(f) f.WORK_STATUS = 257)
            '    If _filter.WORKINGDAY.HasValue Then
            '        lst = lst.Where(Function(f) f.TER_LAST_DATE <= _filter.WORKINGDAY)
            '    End If
            'End If
            'Dim dateNow = Date.Now.Date
            'If Not _filter.IS_TERMINATE Then
            '    lst = lst.Where(Function(f) f.WORK_STATUS <> 257 Or (f.WORK_STATUS = 257 And f.TER_LAST_DATE >= dateNow) Or f.WORK_STATUS Is Nothing)
            'End If
            If _filter.EMPLOYEE_ID.HasValue Then
                lst = lst.Where(Function(f) f.EMPLOYEE_ID = _filter.EMPLOYEE_ID)
            End If
            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                lst = lst.Where(Function(f) f.EMPLOYEE_CODE.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower()) Or f.VN_FULLNAME.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.VN_FULLNAME) Then
                lst = lst.Where(Function(f) f.VN_FULLNAME.ToLower().Contains(_filter.VN_FULLNAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                lst = lst.Where(Function(f) f.ORG_NAME.ToLower().Contains(_filter.ORG_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TITLE_NAME) Then
                lst = lst.Where(Function(f) f.TITLE_NAME.ToLower().Contains(_filter.TITLE_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.STAFF_RANK_NAME) Then
                lst = lst.Where(Function(f) f.STAFF_RANK_NAME.ToLower().Contains(_filter.STAFF_RANK_NAME.ToLower()))
            End If
            If _filter.WORKING_A.HasValue Then
                lst = lst.Where(Function(f) f.WORKING_A = _filter.WORKING_A)
            End If
            If _filter.WORKING_B.HasValue Then
                lst = lst.Where(Function(f) f.WORKING_B = _filter.WORKING_B)
            End If
            If _filter.WORKING_C.HasValue Then
                lst = lst.Where(Function(f) f.WORKING_C = _filter.WORKING_C)
            End If
            If _filter.WORKING_D.HasValue Then
                lst = lst.Where(Function(f) f.WORKING_D = _filter.WORKING_D)
            End If
            If _filter.WORKING_E.HasValue Then
                lst = lst.Where(Function(f) f.WORKING_E = _filter.WORKING_E)
            End If
            If _filter.WORKING_F.HasValue Then
                lst = lst.Where(Function(f) f.WORKING_F = _filter.WORKING_F)
            End If
            If _filter.WORKING_H.HasValue Then
                lst = lst.Where(Function(f) f.WORKING_H = _filter.WORKING_H)
            End If
            If _filter.WORKING_J.HasValue Then
                lst = lst.Where(Function(f) f.WORKING_J = _filter.WORKING_J)
            End If
            If _filter.WORKING_K.HasValue Then
                lst = lst.Where(Function(f) f.WORKING_K = _filter.WORKING_K)
            End If
            If _filter.WORKING_L.HasValue Then
                lst = lst.Where(Function(f) f.WORKING_L = _filter.WORKING_L)
            End If
            If _filter.WORKING_N.HasValue Then
                lst = lst.Where(Function(f) f.WORKING_N = _filter.WORKING_N)
            End If
            If _filter.WORKING_O.HasValue Then
                lst = lst.Where(Function(f) f.WORKING_O = _filter.WORKING_O)
            End If
            If _filter.WORKING_F.HasValue Then
                lst = lst.Where(Function(f) f.WORKING_P = _filter.WORKING_P)
            End If
            If _filter.WORKING_Q.HasValue Then
                lst = lst.Where(Function(f) f.WORKING_Q = _filter.WORKING_Q)
            End If
            If _filter.WORKING_R.HasValue Then
                lst = lst.Where(Function(f) f.WORKING_R = _filter.WORKING_R)
            End If
            If _filter.WORKING_S.HasValue Then
                lst = lst.Where(Function(f) f.WORKING_S = _filter.WORKING_S)
            End If
            If _filter.WORKING_T.HasValue Then
                lst = lst.Where(Function(f) f.WORKING_T = _filter.WORKING_T)
            End If
            If _filter.WORKING_TS.HasValue Then
                lst = lst.Where(Function(f) f.WORKING_TS = _filter.WORKING_TS)
            End If
            If _filter.WORKING_V.HasValue Then
                lst = lst.Where(Function(f) f.WORKING_V = _filter.WORKING_V)
            End If
            If _filter.WORKING_X.HasValue Then
                lst = lst.Where(Function(f) f.WORKING_X = _filter.WORKING_X)
            End If
            If _filter.TOTAL_TS_V.HasValue Then
                lst = lst.Where(Function(f) f.TOTAL_TS_V = _filter.TOTAL_TS_V)
            End If
            If _filter.TOTAL_WORKING.HasValue Then
                lst = lst.Where(Function(f) f.TOTAL_WORKING = _filter.TOTAL_WORKING)
            End If
            If _filter.TOTAL_WORKING_XJ.HasValue Then
                lst = lst.Where(Function(f) f.TOTAL_WORKING_XJ = _filter.TOTAL_WORKING_XJ)
            End If
            If _filter.TOTAL_OFF.HasValue Then
                lst = lst.Where(Function(f) f.TOTAL_OFF = _filter.TOTAL_OFF)
            End If
            If _filter.WORKING_ADD.HasValue Then
                lst = lst.Where(Function(f) f.WORKING_ADD = _filter.WORKING_ADD)
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function CAL_TIME_TIMESHEET_MONTHLY(ByVal _param As ParamDTO, ByVal lstEmployee As List(Of Decimal?), ByVal log As UserLog) As Boolean
        Try
            'Using cls As New DataAccess.NonQueryData
            '    cls.ExecuteSQL("DELETE FROM SE_EMPLOYEE_CHOSEN S WHERE  UPPER(S.USING_USER) ='" + log.Username.ToUpper + "'")
            'End Using
            'Dim dDay = Date.Now
            'For Each emp As Decimal? In lstEmployee
            '    Dim objNew As New SE_EMPLOYEE_CHOSEN
            '    objNew.EMPLOYEE_ID = emp
            '    objNew.USING_USER = log.Username.ToUpper
            '    objNew.WORKINGDAY = dDay
            '    dDay = dDay.AddDays(1)
            '    Context.SE_EMPLOYEE_CHOSEN.AddObject(objNew)
            'Next
            'Context.SaveChanges()
            Dim obj As New AT_ACTION_LOGDTO
            obj.PERIOD_ID = _param.PERIOD_ID
            LOG_AT(_param, log, lstEmployee, "TỔNG HỢP BẢNG CÔNG TỔNG HỢP", obj, _param.ORG_ID)
            Using cls As New DataAccess.NonQueryData
                cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.CAL_TIME_TIMESHEET_MONTHLY",
                                               New With {.P_USERNAME = log.Username,
                                                         .P_PERIOD_ID = _param.PERIOD_ID,
                                                         .P_ORG_ID = _param.ORG_ID,
                                                         .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex

        End Try
    End Function

    Public Function ValidateTimesheet(ByVal _validate As AT_TIME_TIMESHEET_MONTHLYDTO, ByVal sType As String, ByVal log As UserLog)
        Try
            Select Case sType
                Case "BEYOND_STANDARD"
                    Using cls As New DataAccess.QueryData
                        cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                         New With {.P_USERNAME = log.Username.ToUpper,
                                                   .P_ORGID = _validate.ORG_ID,
                                                   .P_ISDISSOLVE = _validate.IS_DISSOLVE})
                    End Using
                    Dim query = (From p In Context.AT_TIME_TIMESHEET_MONTHLY
                                From po In Context.AT_PERIOD.Where(Function(f) f.ID = p.PERIOD_ID)
                                From k In Context.SE_CHOSEN_ORG.Where(Function(f) p.ORG_ID = f.ORG_ID And
                                                                          f.USERNAME.ToUpper = log.Username.ToUpper)
                                Where p.PERIOD_ID = _validate.PERIOD_ID And
                                (p.TOTAL_WORKING IsNot Nothing AndAlso
                                 (p.TOTAL_WORKING - If(p.WORKING_V Is Nothing, 0, p.WORKING_V)) > po.PERIOD_STANDARD))
                    Return (query.Count = 0)

            End Select
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

#End Region

#Region "Lam thêm"
    Public Function GetRegisterOT(ByVal _filter As AT_REGISTER_OTDTO,
                                     ByVal _param As ParamDTO,
                                     Optional ByRef Total As Integer = 0,
                                     Optional ByVal PageIndex As Integer = 0,
                                     Optional ByVal PageSize As Integer = Integer.MaxValue,
                                     Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_REGISTER_OTDTO)
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username.ToUpper,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using
            Dim query = From p In Context.AT_REGISTER_OT
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From c In Context.HU_STAFF_RANK.Where(Function(f) f.ID = e.STAFF_RANK_ID).DefaultIfEmpty
                        From ot In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.HS_OT And f.TYPE_CODE = "HS_OT").DefaultIfEmpty
                        From meal_ot In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.MEALTIME_HS_OT And f.TYPE_CODE = "HS_OT").DefaultIfEmpty
                        From typeot In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TYPE_OT And f.TYPE_CODE = "TYPE_OT").DefaultIfEmpty
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) e.ORG_ID = f.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper)
                        Where p.TYPE_INPUT = _filter.TYPE_INPUT()

            Dim lst = query.Select(Function(p) New AT_REGISTER_OTDTO With {
                                       .ID = p.p.ID,
                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                       .VN_FULLNAME = p.e.FULLNAME_VN,
                                       .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                       .TITLE_NAME = p.t.NAME_VN,
                                       .ORG_NAME = p.o.NAME_VN,
                                       .ORG_DESC = p.o.DESCRIPTION_PATH,
                                       .TYPE_OT_NAME = p.typeot.NAME_VN,
                                       .ORG_ID = p.e.ORG_ID,
                                       .IS_NB = p.p.IS_NB,
                                       .WORKINGDAY = p.p.WORKINGDAY,
                                       .FROM_HOUR = p.p.FROM_HOUR,
                                       .TO_HOUR = p.p.TO_HOUR,
                                       .BREAK_HOUR = p.p.BREAK_HOUR,
                                       .STAFF_RANK_NAME = p.c.NAME,
                                       .NOTE = p.p.NOTE,
                                       .HS_OT_NAME = p.ot.CODE,
                                       .WORK_STATUS = p.e.WORK_STATUS,
                                       .TER_LAST_DATE = p.e.TER_LAST_DATE,
                                       .IS_RICE = p.p.IS_RICE,
                                       .MEALTIME_NUMBER = p.p.MEALTIME_NUMBER,
                                       .MEALTIME_HS_OT = p.p.MEALTIME_HS_OT,
                                       .MEALTIME_HS_OT_NAME = p.meal_ot.CODE,
                                       .HOUR_10 = p.p.HOUR_10,
                                       .HOUR_15 = p.p.HOUR_15,
                                       .HOUR_20 = p.p.HOUR_20,
                                       .HOUR_27 = p.p.HOUR_27,
                                       .HOUR_30 = p.p.HOUR_30,
                                       .HOUR_39 = p.p.HOUR_39,
                                       .IS_LAW = p.p.IS_LAW,
                                       .CREATED_BY = p.p.CREATED_BY,
                                       .CREATED_DATE = p.p.CREATED_DATE,
                                       .CREATED_LOG = p.p.CREATED_LOG,
                                       .MODIFIED_BY = p.p.MODIFIED_BY,
                                       .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                       .MODIFIED_LOG = p.p.MODIFIED_LOG})


            If _filter.IS_TERMINATE Then
                lst = lst.Where(Function(f) f.WORK_STATUS = 257)
                If _filter.WORKINGDAY.HasValue Then
                    lst = lst.Where(Function(f) f.TER_LAST_DATE <= _filter.WORKINGDAY)
                Else
                    lst = lst.Where(Function(f) f.TER_LAST_DATE <= Date.Now)
                End If
            End If
            'Dim dateNow = Date.Now.Date
            'If Not _filter.IS_TERMINATE Then
            '    lst = lst.Where(Function(f) f.WORK_STATUS <> 257 Or (f.WORK_STATUS = 257 And f.TER_LAST_DATE >= dateNow) Or f.WORK_STATUS Is Nothing)
            'End If
            If _filter.FROM_DATE.HasValue Then
                lst = lst.Where(Function(f) f.WORKINGDAY >= _filter.FROM_DATE)
            End If
            If _filter.END_DATE.HasValue Then
                lst = lst.Where(Function(f) f.WORKINGDAY <= _filter.END_DATE)
            End If
            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                lst = lst.Where(Function(f) f.EMPLOYEE_CODE.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.VN_FULLNAME) Then
                lst = lst.Where(Function(f) f.VN_FULLNAME.ToLower().Contains(_filter.VN_FULLNAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                lst = lst.Where(Function(f) f.ORG_NAME.ToLower().Contains(_filter.ORG_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TITLE_NAME) Then
                lst = lst.Where(Function(f) f.TITLE_NAME.ToLower().Contains(_filter.TITLE_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.STAFF_RANK_NAME) Then
                lst = lst.Where(Function(f) f.STAFF_RANK_NAME.ToLower().Contains(_filter.STAFF_RANK_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.HS_OT_NAME) Then
                lst = lst.Where(Function(f) f.HS_OT_NAME.ToLower().Contains(_filter.HS_OT_NAME.ToLower()))
            End If
            If _filter.WORKINGDAY.HasValue Then
                lst = lst.Where(Function(f) f.WORKINGDAY = _filter.WORKINGDAY)
            End If
            If _filter.FROM_HOUR.HasValue Then
                lst = lst.Where(Function(f) f.FROM_HOUR = _filter.FROM_HOUR)
            End If
            If _filter.TO_HOUR.HasValue Then
                lst = lst.Where(Function(f) f.TO_HOUR = _filter.TO_HOUR)
            End If
            If _filter.IS_NB.HasValue Then
                lst = lst.Where(Function(f) f.IS_NB = _filter.IS_NB)
            End If

            If _filter.IS_RICE.HasValue Then
                lst = lst.Where(Function(f) f.IS_RICE = _filter.IS_RICE)
            End If

            If _filter.IS_LAW.HasValue Then
                lst = lst.Where(Function(f) f.IS_LAW = _filter.IS_LAW)
            End If

            If _filter.MEALTIME_NUMBER.HasValue Then
                lst = lst.Where(Function(f) f.MEALTIME_NUMBER = _filter.MEALTIME_NUMBER)
            End If

            If _filter.HOUR_10.HasValue Then
                lst = lst.Where(Function(f) f.HOUR_10 = _filter.HOUR_10)
            End If

            If _filter.HOUR_15.HasValue Then
                lst = lst.Where(Function(f) f.HOUR_15 = _filter.HOUR_15)
            End If

            If _filter.HOUR_20.HasValue Then
                lst = lst.Where(Function(f) f.HOUR_20 = _filter.HOUR_20)
            End If

            If _filter.HOUR_27.HasValue Then
                lst = lst.Where(Function(f) f.HOUR_27 = _filter.HOUR_27)
            End If

            If _filter.HOUR_30.HasValue Then
                lst = lst.Where(Function(f) f.HOUR_30 = _filter.HOUR_30)
            End If

            If _filter.HOUR_39.HasValue Then
                lst = lst.Where(Function(f) f.HOUR_39 = _filter.HOUR_39)
            End If



            If Not String.IsNullOrEmpty(_filter.MEALTIME_HS_OT_NAME) Then
                lst = lst.Where(Function(f) f.MEALTIME_HS_OT_NAME.ToLower().Contains(_filter.MEALTIME_HS_OT_NAME.ToLower()))
            End If

            If Not String.IsNullOrEmpty(_filter.NOTE) Then
                lst = lst.Where(Function(f) f.NOTE.ToLower().Contains(_filter.NOTE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TYPE_OT_NAME) Then
                lst = lst.Where(Function(f) f.TYPE_OT_NAME.ToLower().Contains(_filter.TYPE_OT_NAME.ToLower()))
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function GetRegisterById(ByVal _id As Decimal?) As AT_REGISTER_OTDTO
        Try

            Dim query = From p In Context.AT_REGISTER_OT
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        From c In Context.HU_STAFF_RANK.Where(Function(f) f.ID = e.STAFF_RANK_ID).DefaultIfEmpty
                        From ot In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.HS_OT And f.TYPE_CODE = "HS_OT").DefaultIfEmpty
                        From meal_ot In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.MEALTIME_HS_OT And f.TYPE_CODE = "HS_OT").DefaultIfEmpty
                        From typeot In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TYPE_OT And f.TYPE_CODE = "TYPE_OT").DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        Where p.ID = _id

            Dim lst = query.Select(Function(p) New AT_REGISTER_OTDTO With {
                                       .ID = p.p.ID,
                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                       .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                       .VN_FULLNAME = p.e.FULLNAME_VN,
                                       .TITLE_NAME = p.t.NAME_VN,
                                       .ORG_ID = p.e.ORG_ID,
                                       .TYPE_OT = p.typeot.ID,
                                       .ORG_NAME = p.o.NAME_VN,
                                       .WORKINGDAY = p.p.WORKINGDAY,
                                       .FROM_HOUR = p.p.FROM_HOUR,
                                       .TO_HOUR = p.p.TO_HOUR,
                                       .BREAK_HOUR = p.p.BREAK_HOUR,
                                       .STAFF_RANK_NAME = p.c.NAME,
                                       .IS_NB = p.p.IS_NB,
                                       .NOTE = p.p.NOTE,
                                       .HS_OT = p.p.HS_OT,
                                       .HS_OT_NAME = p.ot.CODE,
                                       .IS_RICE = p.p.IS_RICE,
                                       .IS_LAW = p.p.IS_LAW,
                                       .MEALTIME_NUMBER = p.p.MEALTIME_NUMBER,
                                       .MEALTIME_HS_OT = p.p.MEALTIME_HS_OT,
                                       .MEALTIME_HS_OT_NAME = p.meal_ot.NAME_VN,
                                       .CREATED_BY = p.p.CREATED_BY,
                                       .CREATED_DATE = p.p.CREATED_DATE,
                                       .CREATED_LOG = p.p.CREATED_LOG,
                                       .MODIFIED_BY = p.p.MODIFIED_BY,
                                       .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                       .MODIFIED_LOG = p.p.MODIFIED_LOG}).FirstOrDefault
            Return lst
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function GetListHsOT() As List(Of OT_OTHERLIST_DTO)
        Try
            Dim query = (From p In Context.OT_OTHER_LIST Join t In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals t.ID
                                            Where p.ACTFLG = "A" And t.CODE = "HS_OT" And p.ID <> 4236 Order By p.ID Descending
                                            Select New OT_OTHERLIST_DTO With {
                                                .ID = p.ID,
                                                .CODE = p.CODE,
                                                .NAME_EN = p.NAME_EN,
                                                .NAME_VN = p.NAME_VN,
                                                .TYPE_ID = p.TYPE_ID})

            Return query.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function InsertRegisterOT(ByVal objRegisterOT As AT_REGISTER_OTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim employee_id As Decimal?
        Dim org_id As Decimal?

        'Dim emp = (From p In Context.HU_EMPLOYEE Where p.EMPLOYEE_CODE = objRegisterOT.EMPLOYEE_CODE And p.WORK_STATUS <> 257 And p.TER_LAST_DATE > objRegisterOT.WORKINGDAY).FirstOrDefault
        Dim emp = (From e In Context.HU_EMPLOYEE
                                From w In Context.HU_WORKING.Where(Function(f) f.EMPLOYEE_ID = e.ID)
                                Where e.EMPLOYEE_CODE = objRegisterOT.EMPLOYEE_CODE And e.JOIN_DATE <= objRegisterOT.WORKINGDAY And _
                                (e.TER_EFFECT_DATE Is Nothing Or _
                                 (e.TER_EFFECT_DATE IsNot Nothing And _
                                  e.TER_EFFECT_DATE >= objRegisterOT.WORKINGDAY)) And w.EFFECT_DATE <= objRegisterOT.WORKINGDAY
                          Order By w.EFFECT_DATE Descending
                          Select w).FirstOrDefault
        If emp IsNot Nothing Then
            employee_id = emp.EMPLOYEE_ID
            org_id = emp.ORG_ID
        Else
            Exit Function
        End If
        Try
            Dim exists = (From r In Context.AT_REGISTER_OT
                          Where r.EMPLOYEE_ID = employee_id And
                          r.WORKINGDAY = objRegisterOT.WORKINGDAY And
                          objRegisterOT.FROM_HOUR < r.TO_HOUR And
                          objRegisterOT.TO_HOUR > r.FROM_HOUR And
                          r.TYPE_INPUT = objRegisterOT.TYPE_INPUT).Any
            If exists Then
                Dim obj = (From r In Context.AT_REGISTER_OT
                           Where r.EMPLOYEE_ID = employee_id And
                           r.WORKINGDAY = objRegisterOT.WORKINGDAY And
                           objRegisterOT.FROM_HOUR < r.TO_HOUR And
                           objRegisterOT.TO_HOUR > r.FROM_HOUR And
                           r.TYPE_INPUT = objRegisterOT.TYPE_INPUT).FirstOrDefault
                obj.FROM_HOUR = objRegisterOT.FROM_HOUR
                obj.TO_HOUR = objRegisterOT.TO_HOUR
                'obj.HOUR = objRegisterOT.HOUR
                obj.NOTE = objRegisterOT.NOTE
                obj.TYPE_INPUT = objRegisterOT.TYPE_INPUT
                obj.IS_NB = objRegisterOT.IS_NB
                obj.IS_RICE = objRegisterOT.IS_RICE
                obj.MEALTIME_NUMBER = objRegisterOT.MEALTIME_NUMBER
                obj.MEALTIME_HS_OT = objRegisterOT.MEALTIME_HS_OT
                obj.IS_LAW = objRegisterOT.IS_LAW
                obj.HS_OT = objRegisterOT.HS_OT
                obj.TYPE_OT = objRegisterOT.TYPE_OT
            Else
                Dim objRegisterOTData As New AT_REGISTER_OT
                objRegisterOTData.ID = Utilities.GetNextSequence(Context, Context.AT_REGISTER_OT.EntitySet.Name)
                objRegisterOTData.EMPLOYEE_ID = employee_id
                objRegisterOTData.WORKINGDAY = objRegisterOT.WORKINGDAY
                objRegisterOTData.FROM_HOUR = objRegisterOT.FROM_HOUR
                objRegisterOTData.TO_HOUR = objRegisterOT.TO_HOUR
                objRegisterOTData.NOTE = objRegisterOT.NOTE
                objRegisterOTData.TYPE_INPUT = objRegisterOT.TYPE_INPUT
                'objRegisterOTData.HOUR = objRegisterOT.HOUR
                objRegisterOTData.HS_OT = objRegisterOT.HS_OT
                objRegisterOTData.IS_NB = objRegisterOT.IS_NB
                objRegisterOTData.IS_RICE = objRegisterOT.IS_RICE
                objRegisterOTData.MEALTIME_NUMBER = objRegisterOT.MEALTIME_NUMBER
                objRegisterOTData.MEALTIME_HS_OT = objRegisterOT.MEALTIME_HS_OT
                objRegisterOTData.IS_LAW = objRegisterOT.IS_LAW
                objRegisterOTData.TYPE_OT = objRegisterOT.TYPE_OT
                Context.AT_REGISTER_OT.AddObject(objRegisterOTData)
            End If
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function InsertDataRegisterOT(ByVal objRegisterOTList As List(Of AT_REGISTER_OTDTO), ByVal objRegisterOT As AT_REGISTER_OTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim employee_id As Decimal?
        Dim org_id As Decimal?
        Dim objData As New AT_REGISTER_OTDTO
        Try
            For index = 0 To objRegisterOTList.Count - 1
                objData = objRegisterOTList(index)
                'Dim emp = (From p In Context.HU_EMPLOYEE Where p.EMPLOYEE_CODE = objRegisterOT.EMPLOYEE_CODE And p.WORK_STATUS <> 257 And p.TER_LAST_DATE > objRegisterOT.WORKINGDAY).FirstOrDefault
                Dim emp = (From e In Context.HU_EMPLOYEE
                                        From w In Context.HU_WORKING.Where(Function(f) f.EMPLOYEE_ID = e.ID)
                                        Where e.EMPLOYEE_CODE = objData.EMPLOYEE_CODE And e.JOIN_DATE <= objRegisterOT.WORKINGDAY And _
                                        (e.TER_EFFECT_DATE Is Nothing Or _
                                         (e.TER_EFFECT_DATE IsNot Nothing And _
                                          e.TER_EFFECT_DATE >= objRegisterOT.WORKINGDAY)) And w.EFFECT_DATE <= objRegisterOT.WORKINGDAY
                                  Order By w.EFFECT_DATE Descending
                                  Select w).FirstOrDefault
                If emp IsNot Nothing Then
                    employee_id = emp.EMPLOYEE_ID
                    org_id = emp.ORG_ID
                Else
                    Continue For
                End If
                Dim exists = (From r In Context.AT_REGISTER_OT
                              Where r.EMPLOYEE_ID = employee_id And
                              r.WORKINGDAY = objRegisterOT.WORKINGDAY And
                              objRegisterOT.FROM_HOUR = r.TO_HOUR And
                              objRegisterOT.TO_HOUR = r.FROM_HOUR And
                              r.TYPE_INPUT = objRegisterOT.TYPE_INPUT).Any
                If exists Then
                    Dim obj = (From r In Context.AT_REGISTER_OT
                               Where r.EMPLOYEE_ID = employee_id And
                               r.WORKINGDAY = objRegisterOT.WORKINGDAY And
                               objRegisterOT.FROM_HOUR = r.TO_HOUR And
                               objRegisterOT.TO_HOUR = r.FROM_HOUR And
                               r.TYPE_INPUT = objRegisterOT.TYPE_INPUT).FirstOrDefault

                    obj.FROM_HOUR = objRegisterOT.FROM_HOUR
                    obj.TO_HOUR = objRegisterOT.TO_HOUR
                    'obj.HOUR = objRegisterOT.HOUR
                    obj.NOTE = objRegisterOT.NOTE
                    obj.TYPE_INPUT = objRegisterOT.TYPE_INPUT
                    obj.IS_NB = objRegisterOT.IS_NB
                    obj.IS_RICE = objRegisterOT.IS_RICE
                    obj.MEALTIME_NUMBER = objRegisterOT.MEALTIME_NUMBER
                    obj.MEALTIME_HS_OT = objRegisterOT.MEALTIME_HS_OT
                    obj.IS_LAW = objRegisterOT.IS_LAW
                    obj.HS_OT = objRegisterOT.HS_OT
                    obj.TYPE_OT = objRegisterOT.TYPE_OT
                Else
                    Dim objRegisterOTData As New AT_REGISTER_OT
                    objRegisterOTData.ID = Utilities.GetNextSequence(Context, Context.AT_REGISTER_OT.EntitySet.Name)
                    objRegisterOTData.EMPLOYEE_ID = employee_id
                    objRegisterOTData.WORKINGDAY = objRegisterOT.WORKINGDAY
                    objRegisterOTData.FROM_HOUR = objRegisterOT.FROM_HOUR
                    objRegisterOTData.TO_HOUR = objRegisterOT.TO_HOUR
                    objRegisterOTData.NOTE = objRegisterOT.NOTE
                    objRegisterOTData.TYPE_INPUT = objRegisterOT.TYPE_INPUT
                    'objRegisterOTData.HOUR = objRegisterOT.HOUR
                    objRegisterOTData.HS_OT = objRegisterOT.HS_OT
                    objRegisterOTData.IS_NB = objRegisterOT.IS_NB
                    objRegisterOTData.IS_RICE = objRegisterOT.IS_RICE
                    objRegisterOTData.MEALTIME_NUMBER = objRegisterOT.MEALTIME_NUMBER
                    objRegisterOTData.MEALTIME_HS_OT = objRegisterOT.MEALTIME_HS_OT
                    objRegisterOTData.IS_LAW = objRegisterOT.IS_LAW
                    objRegisterOTData.TYPE_OT = objRegisterOT.TYPE_OT
                    Context.AT_REGISTER_OT.AddObject(objRegisterOTData)
                End If
                Context.SaveChanges(log)
            Next
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ModifyRegisterOT(ByVal objRegisterOT As AT_REGISTER_OTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objRegisterOTData As New AT_REGISTER_OT With {.ID = objRegisterOT.ID}
        Try
            Dim exists = (From r In Context.AT_REGISTER_OT Where r.ID = objRegisterOT.ID).Any
            If exists Then
                Dim obj = (From r In Context.AT_REGISTER_OT Where r.ID = objRegisterOT.ID).FirstOrDefault
                obj.WORKINGDAY = objRegisterOT.WORKINGDAY
                obj.FROM_HOUR = objRegisterOT.FROM_HOUR
                obj.TO_HOUR = objRegisterOT.TO_HOUR
                obj.HS_OT = objRegisterOT.HS_OT
                obj.TYPE_INPUT = objRegisterOT.TYPE_INPUT
                obj.IS_NB = objRegisterOT.IS_NB
                obj.IS_RICE = objRegisterOT.IS_RICE
                obj.MEALTIME_NUMBER = objRegisterOT.MEALTIME_NUMBER
                obj.MEALTIME_HS_OT = objRegisterOT.MEALTIME_HS_OT
                obj.IS_LAW = objRegisterOT.IS_LAW
                'obj.HOUR = objRegisterOT.HOUR
                obj.NOTE = objRegisterOT.NOTE
                obj.TYPE_OT = objRegisterOT.TYPE_OT
            Else
                Return False
            End If
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

    End Function

    Public Function ValidateRegisterOT(ByVal _validate As AT_REGISTER_OTDTO)
        Dim query
        Try
            If _validate.WORKINGDAY.HasValue Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.AT_REGISTER_OT
                             Where p.WORKINGDAY = _validate.WORKINGDAY And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.AT_REGISTER_OT
                             Where p.WORKINGDAY = _validate.WORKINGDAY).FirstOrDefault
                End If
                Return (query Is Nothing)
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function DeleteRegisterOT(ByVal lstID As List(Of Decimal),
                                     ByVal _param As ParamDTO,
                                     ByVal period_id As Decimal,
                                     ByVal listEmployeeId As List(Of Decimal?),
                                     ByVal log As UserLog) As Boolean
        Dim lstl As List(Of AT_REGISTER_OT)
        Try
            If listEmployeeId Is Nothing Then
                listEmployeeId = New List(Of Decimal?)
            End If
            Dim obj As New AT_ACTION_LOGDTO
            obj.PERIOD_ID = period_id

            lstl = (From p In Context.AT_REGISTER_OT Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstl.Count - 1
                listEmployeeId.Add(lstl(index).EMPLOYEE_ID)
                Context.AT_REGISTER_OT.DeleteObject(lstl(index))
            Next

            Context.SaveChanges(log)
            LOG_AT(_param, log, listEmployeeId, "XÓA ĐĂNG KÝ LÀM THÊM", obj, _param.ORG_ID)

            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteCostCenter")
            Throw ex
        End Try
    End Function

    Public Function CheckImporAddNewtOT(ByVal objRegisterOT As AT_REGISTER_OTDTO) As Boolean
        Dim employee_id As Decimal?
        Dim org_id As Decimal?
        Dim emp = (From e In Context.HU_EMPLOYEE
                   From w In Context.HU_WORKING.Where(Function(f) f.EMPLOYEE_ID = e.ID)
                   Where e.EMPLOYEE_CODE = objRegisterOT.EMPLOYEE_CODE And e.JOIN_DATE <= objRegisterOT.WORKINGDAY And _
                   (e.TER_EFFECT_DATE Is Nothing Or _
                    (e.TER_EFFECT_DATE IsNot Nothing And _
                     e.TER_EFFECT_DATE >= objRegisterOT.WORKINGDAY)) And w.EFFECT_DATE <= objRegisterOT.WORKINGDAY
             Order By w.EFFECT_DATE Descending
                          Select w).FirstOrDefault
        If emp IsNot Nothing Then
            employee_id = emp.EMPLOYEE_ID
            org_id = emp.ORG_ID
        Else
            Exit Function
        End If
        Try
            Dim exists

            If objRegisterOT.ID IsNot Nothing Then
                exists = (From r In Context.AT_REGISTER_OT
                          Where r.EMPLOYEE_ID = employee_id And
                          r.WORKINGDAY = objRegisterOT.WORKINGDAY And
                          objRegisterOT.FROM_HOUR < r.TO_HOUR And
                          objRegisterOT.TO_HOUR > r.FROM_HOUR And
                          r.TYPE_INPUT = objRegisterOT.TYPE_INPUT And
                          r.ID <> objRegisterOT.ID).Any
            Else
                exists = (From r In Context.AT_REGISTER_OT
                          Where r.EMPLOYEE_ID = employee_id And
                          r.WORKINGDAY = objRegisterOT.WORKINGDAY And
                          objRegisterOT.FROM_HOUR < r.TO_HOUR And
                          objRegisterOT.TO_HOUR > r.FROM_HOUR And
                          r.TYPE_INPUT = objRegisterOT.TYPE_INPUT).Any
            End If
            If exists Then ' có dữ liệu trả lại false không cho phép đăng ký tiếp 
                Return False
            Else 'không  có dữ liệu trả lại true cho phép đăng ký tiếp 
                Return True
            End If

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function CheckDataListImportAddNew(ByVal objRegisterOTList As List(Of AT_REGISTER_OTDTO), ByVal objRegisterOT As AT_REGISTER_OTDTO, ByRef strEmployeeCode As String) As Boolean
        Dim employee_id As Decimal?
        Dim org_id As Decimal?
        Dim objData As New AT_REGISTER_OTDTO
        Try
            For index = 0 To objRegisterOTList.Count - 1
                objData = objRegisterOTList(index)
                'Dim emp = (From p In Context.HU_EMPLOYEE Where p.EMPLOYEE_CODE = objRegisterOT.EMPLOYEE_CODE And p.WORK_STATUS <> 257 And p.TER_LAST_DATE > objRegisterOT.WORKINGDAY).FirstOrDefault
                Dim emp = (From e In Context.HU_EMPLOYEE
                                        From w In Context.HU_WORKING.Where(Function(f) f.EMPLOYEE_ID = e.ID)
                                        Where e.EMPLOYEE_CODE = objData.EMPLOYEE_CODE And e.JOIN_DATE <= objRegisterOT.WORKINGDAY And _
                                        (e.TER_EFFECT_DATE Is Nothing Or _
                                         (e.TER_EFFECT_DATE IsNot Nothing And _
                                          e.TER_EFFECT_DATE >= objRegisterOT.WORKINGDAY)) And w.EFFECT_DATE <= objRegisterOT.WORKINGDAY
                                  Order By w.EFFECT_DATE Descending
                                  Select w).FirstOrDefault
                If emp IsNot Nothing Then
                    employee_id = emp.EMPLOYEE_ID
                    org_id = emp.ORG_ID
                Else
                    Continue For
                End If
                Dim exists = (From r In Context.AT_REGISTER_OT
                              Where r.EMPLOYEE_ID = employee_id And
                              r.WORKINGDAY = objRegisterOT.WORKINGDAY And
                              objRegisterOT.FROM_HOUR < r.TO_HOUR And
                              objRegisterOT.TO_HOUR > r.FROM_HOUR And
                              r.TYPE_INPUT = objRegisterOT.TYPE_INPUT).Any
                If exists Then
                    strEmployeeCode = strEmployeeCode & objData.EMPLOYEE_CODE & ","
                End If
            Next
            If Not strEmployeeCode.Equals("") Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function



#End Region

#Region "Khai bao cong com"
    Public Function GetDelareRice(ByVal _filter As AT_TIME_RICEDTO,
                                     ByVal _param As ParamDTO,
                                     Optional ByRef Total As Integer = 0,
                                     Optional ByVal PageIndex As Integer = 0,
                                     Optional ByVal PageSize As Integer = Integer.MaxValue,
                                     Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_TIME_RICEDTO)
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username.ToUpper,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using
            Dim query = From p In Context.AT_TIME_RICE
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        From c In Context.HU_STAFF_RANK.Where(Function(f) f.ID = e.STAFF_RANK_ID).DefaultIfEmpty
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) e.ORG_ID = f.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper)

            Dim lst = query.Select(Function(p) New AT_TIME_RICEDTO With {
                                       .ID = p.p.ID,
                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                       .VN_FULLNAME = p.e.FULLNAME_VN,
                                       .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                       .TITLE_NAME = p.t.NAME_VN,
                                       .ORG_NAME = p.o.NAME_VN,
                                       .ORG_DESC = p.o.DESCRIPTION_PATH,
                                       .ORG_ID = p.p.ORG_ID,
                                       .WORKINGDAY = p.p.WORKINGDAY,
                                       .ACTFLG = p.p.ACTFLG,
                                       .PRICE = p.p.PRICE,
                                       .STAFF_RANK_NAME = p.c.NAME,
                                       .WORK_STATUS = p.e.WORK_STATUS,
                                       .TER_LAST_DATE = p.e.TER_LAST_DATE,
                                       .CREATED_BY = p.p.CREATED_BY,
                                       .CREATED_DATE = p.p.CREATED_DATE,
                                       .CREATED_LOG = p.p.CREATED_LOG,
                                       .MODIFIED_BY = p.p.MODIFIED_BY,
                                       .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                       .MODIFIED_LOG = p.p.MODIFIED_LOG})

            Dim dateNow = Date.Now.Date
            If _filter.IS_TERMINATE Then
                lst = lst.Where(Function(f) f.WORK_STATUS = 257 And f.TER_LAST_DATE <= dateNow)
            End If

            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                lst = lst.Where(Function(f) f.EMPLOYEE_CODE.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.VN_FULLNAME) Then
                lst = lst.Where(Function(f) f.VN_FULLNAME.ToLower().Contains(_filter.VN_FULLNAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                lst = lst.Where(Function(f) f.ORG_NAME.ToLower().Contains(_filter.ORG_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TITLE_NAME) Then
                lst = lst.Where(Function(f) f.TITLE_NAME.ToLower().Contains(_filter.TITLE_NAME.ToLower()))
            End If
            If _filter.WORKINGDAY.HasValue Then
                lst = lst.Where(Function(f) f.WORKINGDAY = _filter.WORKINGDAY)
            End If
            'If _filter.IS_TERMINATE Then
            '    lst = lst.Where(Function(f) f.e.WORK_STATUS = 257)
            '    If _filter.WORKINGDAY.HasValue Then
            '        lst = lst.Where(Function(f) f.e.TER_LAST_DATE <= _filter.WORKINGDAY)
            '    End If
            'End If
            If _filter.FROM_DATE.HasValue Then
                lst = lst.Where(Function(f) f.WORKINGDAY >= _filter.FROM_DATE)
            End If
            If _filter.END_DATE.HasValue Then
                lst = lst.Where(Function(f) f.WORKINGDAY <= _filter.END_DATE)
            End If
            If _filter.PRICE.HasValue Then
                lst = lst.Where(Function(f) f.PRICE = _filter.PRICE)
            End If
            If Not String.IsNullOrEmpty(_filter.STAFF_RANK_NAME) Then
                lst = lst.Where(Function(f) f.STAFF_RANK_NAME.ToLower().Contains(_filter.STAFF_RANK_NAME.ToLower()))
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ActiveDelareRice(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstData As List(Of AT_TIME_RICE)
        Try
            lstData = (From p In Context.AT_TIME_RICE Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                lstData(index).ACTFLG = bActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function GetDelareRiceById(ByVal _id As Decimal?) As AT_TIME_RICEDTO
        Try

            Dim query = From p In Context.AT_TIME_RICE
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        From c In Context.HU_STAFF_RANK.Where(Function(f) f.ID = e.STAFF_RANK_ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        Where p.ID = _id

            Dim lst = query.Select(Function(p) New AT_TIME_RICEDTO With {
                                       .ID = p.p.ID,
                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                       .VN_FULLNAME = p.e.FULLNAME_VN,
                                       .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                       .TITLE_NAME = p.t.NAME_VN,
                                       .ORG_NAME = p.o.NAME_VN,
                                       .ORG_ID = p.p.ORG_ID,
                                       .WORKINGDAY = p.p.WORKINGDAY,
                                       .ACTFLG = p.p.ACTFLG,
                                       .PRICE = p.p.PRICE,
                                       .STAFF_RANK_NAME = p.c.NAME,
                                       .NOTE = p.p.NOTE,
                                       .CREATED_BY = p.p.CREATED_BY,
                                       .CREATED_DATE = p.p.CREATED_DATE,
                                       .CREATED_LOG = p.p.CREATED_LOG,
                                       .MODIFIED_BY = p.p.MODIFIED_BY,
                                       .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                       .MODIFIED_LOG = p.p.MODIFIED_LOG}).FirstOrDefault
            Return lst
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function InsertDelareRice(ByVal objDelareRice As AT_TIME_RICEDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim employee_id As Decimal?
        Dim org_id As Decimal?
        Try
            Dim exists = (From r In Context.AT_TIME_RICE Where r.EMPLOYEE_ID = objDelareRice.EMPLOYEE_ID And r.WORKINGDAY = objDelareRice.WORKINGDAY).Any

            If exists Then
                Dim obj = (From r In Context.AT_TIME_RICE Where r.EMPLOYEE_ID = objDelareRice.EMPLOYEE_ID And r.WORKINGDAY = objDelareRice.WORKINGDAY).FirstOrDefault
                obj.PRICE = objDelareRice.PRICE
                obj.WORKINGDAY = objDelareRice.WORKINGDAY
            Else
                Dim objDelareRiceData As New AT_TIME_RICE
                objDelareRiceData.ID = Utilities.GetNextSequence(Context, Context.AT_TIME_RICE.EntitySet.Name)
                Dim emp = (From p In Context.HU_EMPLOYEE Where p.ID = objDelareRice.EMPLOYEE_ID).FirstOrDefault
                objDelareRiceData.EMPLOYEE_ID = emp.ID
                employee_id = emp.ID
                org_id = emp.ORG_ID
                objDelareRiceData.WORKINGDAY = objDelareRice.WORKINGDAY
                objDelareRiceData.ORG_ID = emp.ORG_ID
                objDelareRiceData.PRICE = objDelareRice.PRICE
                objDelareRiceData.STAFF_RANK_ID = objDelareRice.STAFF_RANK_ID
                objDelareRiceData.TITLE_ID = objDelareRice.TITLE_ID
                objDelareRiceData.NOTE = objDelareRice.NOTE
                Context.AT_TIME_RICE.AddObject(objDelareRiceData)
            End If
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function InsertDelareRiceList(ByVal objDelareRiceList As List(Of AT_TIME_RICEDTO), ByVal objDelareRice As AT_TIME_RICEDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objData As New AT_TIME_RICEDTO
        Try
            For index = 0 To objDelareRiceList.Count - 1
                objData = objDelareRiceList(index)
                Dim exists = (From r In Context.AT_TIME_RICE Where r.EMPLOYEE_ID = objData.EMPLOYEE_ID And r.WORKINGDAY = objDelareRice.WORKINGDAY).Any
                If exists Then
                    Dim obj = (From r In Context.AT_TIME_RICE Where r.EMPLOYEE_ID = objData.EMPLOYEE_ID And r.WORKINGDAY = objDelareRice.WORKINGDAY).FirstOrDefault
                    obj.PRICE = objDelareRice.PRICE
                    obj.WORKINGDAY = objDelareRice.WORKINGDAY
                Else
                    Dim objDelareRiceData As New AT_TIME_RICE
                    objDelareRiceData.ID = Utilities.GetNextSequence(Context, Context.AT_TIME_RICE.EntitySet.Name)
                    objDelareRiceData.EMPLOYEE_ID = objData.EMPLOYEE_ID
                    objDelareRiceData.ORG_ID = objData.ORG_ID ' trường hợp này phải lưu org hiện tại của nhân viên
                    objDelareRiceData.WORKINGDAY = objDelareRice.WORKINGDAY
                    objDelareRiceData.PRICE = objDelareRice.PRICE
                    objDelareRiceData.STAFF_RANK_ID = objDelareRice.STAFF_RANK_ID
                    objDelareRiceData.TITLE_ID = objDelareRice.TITLE_ID
                    objDelareRiceData.NOTE = objDelareRice.NOTE
                    Context.AT_TIME_RICE.AddObject(objDelareRiceData)
                End If
                Context.SaveChanges(log)
            Next
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ValidateDelareRice(ByVal _validate As AT_TIME_RICEDTO)
        Dim query
        Try
            Dim exists = (From r In Context.AT_TIME_RICE Where r.ID = _validate.ID And r.EMPLOYEE_ID = _validate.EMPLOYEE_ID And
                                                                                          r.WORKINGDAY = _validate.WORKINGDAY).Any

            If _validate.WORKINGDAY.HasValue Then
                If exists And _validate.ID <> 0 Then
                    query = (From p In Context.AT_TIME_RICE
                             Where p.ID <> _validate.ID And p.WORKINGDAY = _validate.WORKINGDAY And p.EMPLOYEE_ID = _validate.EMPLOYEE_ID).Any
                Else
                    query = (From p In Context.AT_TIME_RICE
                             Where p.EMPLOYEE_ID = _validate.EMPLOYEE_ID And p.WORKINGDAY = _validate.WORKINGDAY).Any
                End If
                If query Then
                    Return True
                Else
                    Return False
                End If
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ModifyDelareRice(ByVal objDelareRice As AT_TIME_RICEDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objDelareRiceData As New AT_TIME_RICE With {.ID = objDelareRice.ID}
        Try
            Dim exists = (From r In Context.AT_TIME_RICE Where r.ID = objDelareRice.ID).Any
            If exists Then
                Dim obj = (From r In Context.AT_TIME_RICE Where r.ID = objDelareRice.ID).FirstOrDefault
                obj.ORG_ID = objDelareRice.ORG_ID
                obj.PRICE = objDelareRice.PRICE
                obj.NOTE = objDelareRice.NOTE
            Else
                Return False
            End If
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

    End Function

    Public Function DeleteDelareRice(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstl As List(Of AT_TIME_RICE)
        Try
            lstl = (From p In Context.AT_TIME_RICE Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstl.Count - 1
                Context.AT_TIME_RICE.DeleteObject(lstl(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteCostCenter")
            Throw ex
        End Try
    End Function
#End Region

#Region "Khai báo điều chỉnh thâm niên phép"
    Public Function GetDelareEntitlementNB(ByVal _filter As AT_DECLARE_ENTITLEMENTDTO,
                                     ByVal _param As ParamDTO,
                                     Optional ByRef Total As Integer = 0,
                                     Optional ByVal PageIndex As Integer = 0,
                                     Optional ByVal PageSize As Integer = Integer.MaxValue,
                                     Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_DECLARE_ENTITLEMENTDTO)
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username.ToUpper,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using
            Dim query = From p In Context.AT_DECLARE_ENTITLEMENT
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From c In Context.HU_STAFF_RANK.Where(Function(f) f.ID = e.STAFF_RANK_ID).DefaultIfEmpty
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) e.ORG_ID = f.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper)

            Dim lst = query.Select(Function(p) New AT_DECLARE_ENTITLEMENTDTO With {
                                       .ID = p.p.ID,
                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                       .VN_FULLNAME = p.e.FULLNAME_VN,
                                       .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                       .TITLE_NAME = p.t.NAME_VN,
                                       .ORG_NAME = p.o.NAME_VN,
                                       .ORG_DESC = p.o.DESCRIPTION_PATH,
                                       .ORG_ID = p.e.ORG_ID,
                                       .YEAR = p.p.YEAR,
                                       .YEAR_NB = p.p.YEAR_NB,
                                       .YEAR_ENTITLEMENT = p.p.YEAR_ENTITLEMENT,
                                       .STAFF_RANK_NAME = p.c.NAME,
                                       .ADJUST_MONTH_TN = p.p.ADJUST_MONTH_TN,
                                       .REMARK_TN = p.p.REMARK_TN,
                                       .ADJUST_ENTITLEMENT = p.p.ADJUST_ENTITLEMENT,
                                       .ADJUST_ENTITLEMENT_PREV = p.p.ADJUST_ENTITLEMENT_PREV,
                                       .ADJUST_MONTH_ENTITLEMENT = p.p.ADJUST_MONTH_ENTITLEMENT,
                                       .REMARK_ENTITLEMENT = p.p.REMARK_ENTITLEMENT,
                                       .START_MONTH_TN = p.p.START_MONTH_TN,
                                       .START_MONTH_EXTEND = p.p.START_MONTH_EXTEND,
                                       .ADJUST_NB = p.p.ADJUST_NB,
                                       .ADJUST_NB_PREV = p.p.ADJUST_NB_PREV,
                                       .START_MONTH_NB = p.p.START_MONTH_NB,
                                       .REMARK_NB = p.p.REMARK_NB,
                                       .MONTH_EXTENSION_NB = p.p.MONTH_EXTENSION_NB,
                                       .COM_PAY = p.p.COM_PAY,
                                       .ENT_PAY = p.p.ENT_PAY,
                                       .COM_PAY_PREV = p.p.COM_PAY_PREV,
                                       .ENT_PAY_PREV = p.p.ENT_PAY_PREV,
                                       .WORK_STATUS = p.e.WORK_STATUS,
                                       .TER_LAST_DATE = p.e.TER_LAST_DATE,
                                       .CREATED_BY = p.p.CREATED_BY,
                                       .CREATED_DATE = p.p.CREATED_DATE,
                                       .CREATED_LOG = p.p.CREATED_LOG,
                                       .MODIFIED_BY = p.p.MODIFIED_BY,
                                       .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                       .MODIFIED_LOG = p.p.MODIFIED_LOG})

            'If _filter.IS_TERMINATE Then
            '    query = query.Where(Function(f) f.e.WORK_STATUS = 257 And f.e.TER_LAST_DATE <= Date.Now)
            'End If
            Dim dateNow = Date.Now.Date
            If Not _filter.IS_TERMINATE Then
                lst = lst.Where(Function(f) f.WORK_STATUS <> 257 Or (f.WORK_STATUS = 257 And f.TER_LAST_DATE >= dateNow) Or f.WORK_STATUS Is Nothing)
            End If

            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                lst = lst.Where(Function(f) f.EMPLOYEE_CODE.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower()) Or f.VN_FULLNAME.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.VN_FULLNAME) Then
                lst = lst.Where(Function(f) f.VN_FULLNAME.ToLower().Contains(_filter.VN_FULLNAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                lst = lst.Where(Function(f) f.ORG_NAME.ToLower().Contains(_filter.ORG_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TITLE_NAME) Then
                lst = lst.Where(Function(f) f.TITLE_NAME.ToLower().Contains(_filter.TITLE_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.STAFF_RANK_NAME) Then
                lst = lst.Where(Function(f) f.STAFF_RANK_NAME.ToLower().Contains(_filter.STAFF_RANK_NAME.ToLower()))
            End If
            If _filter.ADJUST_MONTH_TN.HasValue Then
                lst = lst.Where(Function(f) f.ADJUST_MONTH_TN = _filter.ADJUST_MONTH_TN)
            End If
            If _filter.ADJUST_ENTITLEMENT.HasValue Then
                lst = lst.Where(Function(f) f.ADJUST_ENTITLEMENT = _filter.ADJUST_ENTITLEMENT)
            End If
            If _filter.ADJUST_MONTH_ENTITLEMENT.HasValue Then
                lst = lst.Where(Function(f) f.ADJUST_MONTH_ENTITLEMENT = _filter.ADJUST_MONTH_ENTITLEMENT)
            End If

            If _filter.START_MONTH_TN.HasValue Then
                lst = lst.Where(Function(f) f.START_MONTH_TN = _filter.START_MONTH_TN)
            End If
            If _filter.YEAR.HasValue Then
                lst = lst.Where(Function(f) f.YEAR = _filter.YEAR)
            End If
            If _filter.START_MONTH_EXTEND.HasValue Then
                lst = lst.Where(Function(f) f.START_MONTH_EXTEND = _filter.START_MONTH_EXTEND)
            End If
            If Not String.IsNullOrEmpty(_filter.STAFF_RANK_NAME) Then
                lst = lst.Where(Function(f) f.STAFF_RANK_NAME.ToLower().Contains(_filter.STAFF_RANK_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.REMARK_TN) Then
                lst = lst.Where(Function(f) f.REMARK_TN.ToLower().Contains(_filter.REMARK_TN.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.REMARK_ENTITLEMENT) Then
                lst = lst.Where(Function(f) f.REMARK_ENTITLEMENT.ToLower().Contains(_filter.REMARK_ENTITLEMENT.ToLower()))
            End If
            If _filter.YEAR_NB.HasValue Then
                lst = lst.Where(Function(f) f.YEAR_NB = _filter.YEAR_NB)
            End If
            If _filter.YEAR_ENTITLEMENT.HasValue Then
                lst = lst.Where(Function(f) f.YEAR_ENTITLEMENT = _filter.YEAR_ENTITLEMENT)
            End If
            If _filter.MONTH_EXTENSION_NB.HasValue Then
                lst = lst.Where(Function(f) f.MONTH_EXTENSION_NB = _filter.MONTH_EXTENSION_NB)
            End If
            If _filter.COM_PAY.HasValue Then
                lst = lst.Where(Function(f) f.COM_PAY = _filter.COM_PAY)
            End If
            If _filter.ENT_PAY.HasValue Then
                lst = lst.Where(Function(f) f.ENT_PAY = _filter.ENT_PAY)
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ActiveDelareEntitlementNB(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstData As List(Of AT_DECLARE_ENTITLEMENT)
        Try

            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function GetDelareEntitlementNBById(ByVal _id As Decimal?) As AT_DECLARE_ENTITLEMENTDTO
        Try

            Dim query = From p In Context.AT_DECLARE_ENTITLEMENT
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        From c In Context.HU_STAFF_RANK.Where(Function(f) f.ID = e.STAFF_RANK_ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        Where p.ID = _id

            Dim lst = query.Select(Function(p) New AT_DECLARE_ENTITLEMENTDTO With {
                                       .ID = p.p.ID,
                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                       .VN_FULLNAME = p.e.FULLNAME_VN,
                                       .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                       .TITLE_NAME = p.t.NAME_VN,
                                       .ORG_NAME = p.o.NAME_VN,
                                       .ORG_ID = p.e.ORG_ID,
                                       .YEAR = p.p.YEAR,
                                       .YEAR_NB = p.p.YEAR_NB,
                                       .YEAR_ENTITLEMENT = p.p.YEAR_ENTITLEMENT,
                                       .STAFF_RANK_NAME = p.c.NAME,
                                       .ADJUST_MONTH_TN = p.p.ADJUST_MONTH_TN,
                                       .REMARK_TN = p.p.REMARK_TN,
                                       .ADJUST_ENTITLEMENT = p.p.ADJUST_ENTITLEMENT,
                                       .ADJUST_ENTITLEMENT_PREV = p.p.ADJUST_ENTITLEMENT_PREV,
                                       .ADJUST_MONTH_ENTITLEMENT = p.p.ADJUST_MONTH_ENTITLEMENT,
                                       .REMARK_ENTITLEMENT = p.p.REMARK_ENTITLEMENT,
                                       .START_MONTH_TN = p.p.START_MONTH_TN,
                                       .START_MONTH_EXTEND = p.p.START_MONTH_EXTEND,
                                       .ADJUST_NB = p.p.ADJUST_NB,
                                       .ADJUST_NB_PREV = p.p.ADJUST_NB_PREV,
                                       .START_MONTH_NB = p.p.START_MONTH_NB,
                                       .REMARK_NB = p.p.REMARK_NB,
                                       .MONTH_EXTENSION_NB = p.p.MONTH_EXTENSION_NB,
                                       .COM_PAY = p.p.COM_PAY,
                                       .ENT_PAY = p.p.ENT_PAY,
                                       .COM_PAY_PREV = p.p.COM_PAY_PREV,
                                       .ENT_PAY_PREV = p.p.ENT_PAY_PREV,
                                       .CREATED_BY = p.p.CREATED_BY,
                                       .CREATED_DATE = p.p.CREATED_DATE,
                                       .CREATED_LOG = p.p.CREATED_LOG,
                                       .MODIFIED_BY = p.p.MODIFIED_BY,
                                       .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                       .MODIFIED_LOG = p.p.MODIFIED_LOG}).FirstOrDefault
            Return lst
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function InsertDelareEntitlementNB(ByVal objDelareEntitlementNB As AT_DECLARE_ENTITLEMENTDTO, ByVal log As UserLog, ByRef gID As Decimal, ByRef checkMonthNB As Boolean, ByRef checkMonthNP As Boolean) As Boolean
        Try
            ' check nghỉ bù chỉ được gia hạn 1 lần trong năm
            If objDelareEntitlementNB.MONTH_EXTENSION_NB IsNot Nothing Then
                If objDelareEntitlementNB.ID IsNot Nothing Then ' trường hợp sửa phải kiểm tra khác id hiện tại
                    checkMonthNB = (From t In Context.AT_DECLARE_ENTITLEMENT Where t.EMPLOYEE_ID = objDelareEntitlementNB.EMPLOYEE_ID And t.MONTH_EXTENSION_NB IsNot Nothing And t.ID <> objDelareEntitlementNB.ID And t.YEAR = objDelareEntitlementNB.YEAR).Any
                Else
                    checkMonthNB = (From t In Context.AT_DECLARE_ENTITLEMENT Where t.EMPLOYEE_ID = objDelareEntitlementNB.EMPLOYEE_ID And t.MONTH_EXTENSION_NB IsNot Nothing And t.YEAR = objDelareEntitlementNB.YEAR).Any
                End If
            End If
            ' check nghỉ phép chỉ được gia hạn 1 lần trong năm
            If objDelareEntitlementNB.START_MONTH_EXTEND IsNot Nothing Then
                If objDelareEntitlementNB.ID IsNot Nothing Then ' trường hợp sửa phải kiểm tra khác id hiện tại
                    checkMonthNP = (From t In Context.AT_DECLARE_ENTITLEMENT Where t.EMPLOYEE_ID = objDelareEntitlementNB.EMPLOYEE_ID And t.START_MONTH_EXTEND IsNot Nothing And t.ID <> objDelareEntitlementNB.ID And t.YEAR = objDelareEntitlementNB.YEAR).Any
                Else
                    checkMonthNP = (From t In Context.AT_DECLARE_ENTITLEMENT Where t.EMPLOYEE_ID = objDelareEntitlementNB.EMPLOYEE_ID And t.START_MONTH_EXTEND IsNot Nothing And t.YEAR = objDelareEntitlementNB.YEAR).Any
                End If
            End If
            If checkMonthNB = False And checkMonthNP = False Then
                Dim exists = (From t In Context.AT_DECLARE_ENTITLEMENT Where t.ID = objDelareEntitlementNB.ID).Any
                If exists Then
                    Dim obj = (From r In Context.AT_DECLARE_ENTITLEMENT Where r.ID = objDelareEntitlementNB.ID And r.EMPLOYEE_ID = objDelareEntitlementNB.EMPLOYEE_ID).FirstOrDefault
                    obj.ADJUST_MONTH_TN = objDelareEntitlementNB.ADJUST_MONTH_TN
                    obj.ADJUST_MONTH_ENTITLEMENT = objDelareEntitlementNB.ADJUST_MONTH_ENTITLEMENT
                    obj.ADJUST_ENTITLEMENT = objDelareEntitlementNB.ADJUST_ENTITLEMENT
                    obj.ADJUST_ENTITLEMENT_PREV = objDelareEntitlementNB.ADJUST_ENTITLEMENT_PREV
                    obj.START_MONTH_TN = objDelareEntitlementNB.START_MONTH_TN
                    obj.START_MONTH_EXTEND = objDelareEntitlementNB.START_MONTH_EXTEND
                    obj.YEAR = objDelareEntitlementNB.YEAR
                    obj.YEAR_NB = objDelareEntitlementNB.YEAR_NB
                    obj.YEAR_ENTITLEMENT = objDelareEntitlementNB.YEAR_ENTITLEMENT
                    obj.REMARK_TN = objDelareEntitlementNB.REMARK_TN
                    obj.REMARK_ENTITLEMENT = objDelareEntitlementNB.REMARK_ENTITLEMENT
                    obj.ADJUST_NB = objDelareEntitlementNB.ADJUST_NB
                    obj.ADJUST_NB_PREV = objDelareEntitlementNB.ADJUST_NB_PREV
                    obj.START_MONTH_NB = objDelareEntitlementNB.START_MONTH_NB
                    obj.REMARK_NB = objDelareEntitlementNB.REMARK_NB
                    obj.MONTH_EXTENSION_NB = objDelareEntitlementNB.MONTH_EXTENSION_NB
                    obj.COM_PAY = objDelareEntitlementNB.COM_PAY
                    obj.ENT_PAY = objDelareEntitlementNB.ENT_PAY
                    obj.COM_PAY_PREV = objDelareEntitlementNB.COM_PAY_PREV
                    obj.ENT_PAY_PREV = objDelareEntitlementNB.ENT_PAY_PREV
                Else
                    Dim objDelareEntitlementNBData As New AT_DECLARE_ENTITLEMENT
                    objDelareEntitlementNBData.ID = Utilities.GetNextSequence(Context, Context.AT_DECLARE_ENTITLEMENT.EntitySet.Name)
                    objDelareEntitlementNBData.EMPLOYEE_ID = objDelareEntitlementNB.EMPLOYEE_ID
                    objDelareEntitlementNBData.ADJUST_MONTH_TN = objDelareEntitlementNB.ADJUST_MONTH_TN
                    objDelareEntitlementNBData.ADJUST_MONTH_ENTITLEMENT = objDelareEntitlementNB.ADJUST_MONTH_ENTITLEMENT
                    objDelareEntitlementNBData.ADJUST_ENTITLEMENT = objDelareEntitlementNB.ADJUST_ENTITLEMENT
                    objDelareEntitlementNBData.ADJUST_ENTITLEMENT_PREV = objDelareEntitlementNB.ADJUST_ENTITLEMENT_PREV
                    objDelareEntitlementNBData.YEAR = objDelareEntitlementNB.YEAR
                    objDelareEntitlementNBData.YEAR_NB = objDelareEntitlementNB.YEAR_NB
                    objDelareEntitlementNBData.YEAR_ENTITLEMENT = objDelareEntitlementNB.YEAR_ENTITLEMENT
                    objDelareEntitlementNBData.START_MONTH_TN = objDelareEntitlementNB.START_MONTH_TN
                    objDelareEntitlementNBData.START_MONTH_EXTEND = objDelareEntitlementNB.START_MONTH_EXTEND
                    objDelareEntitlementNBData.REMARK_TN = objDelareEntitlementNB.REMARK_TN
                    objDelareEntitlementNBData.REMARK_ENTITLEMENT = objDelareEntitlementNB.REMARK_ENTITLEMENT
                    objDelareEntitlementNBData.ADJUST_NB = objDelareEntitlementNB.ADJUST_NB
                    objDelareEntitlementNBData.ADJUST_NB_PREV = objDelareEntitlementNB.ADJUST_NB_PREV
                    objDelareEntitlementNBData.START_MONTH_NB = objDelareEntitlementNB.START_MONTH_NB
                    objDelareEntitlementNBData.REMARK_NB = objDelareEntitlementNB.REMARK_NB
                    objDelareEntitlementNBData.MONTH_EXTENSION_NB = objDelareEntitlementNB.MONTH_EXTENSION_NB
                    objDelareEntitlementNBData.COM_PAY = objDelareEntitlementNB.COM_PAY
                    objDelareEntitlementNBData.ENT_PAY = objDelareEntitlementNB.ENT_PAY
                    objDelareEntitlementNBData.COM_PAY_PREV = objDelareEntitlementNB.COM_PAY_PREV
                    objDelareEntitlementNBData.ENT_PAY_PREV = objDelareEntitlementNB.ENT_PAY_PREV
                    Context.AT_DECLARE_ENTITLEMENT.AddObject(objDelareEntitlementNBData)
                End If
                Context.SaveChanges(log)
                Return True
            End If

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function InsertMultipleDelareEntitlementNB(ByVal objDelareEntitlementlist As List(Of AT_DECLARE_ENTITLEMENTDTO), ByVal objDelareEntitlementNB As AT_DECLARE_ENTITLEMENTDTO, ByVal log As UserLog, ByRef gID As Decimal, ByRef checkMonthNB As Boolean, ByRef checkMonthNP As Boolean) As Boolean
        Try
            Dim objData As New AT_DECLARE_ENTITLEMENTDTO

            For index = 0 To objDelareEntitlementlist.Count - 1
                objData = objDelareEntitlementlist(index)
                ' check nghỉ bù chỉ được gia hạn 1 lần trong năm
                If objDelareEntitlementNB.MONTH_EXTENSION_NB IsNot Nothing Then
                    checkMonthNB = (From t In Context.AT_DECLARE_ENTITLEMENT Where t.EMPLOYEE_ID = objData.EMPLOYEE_ID And t.MONTH_EXTENSION_NB IsNot Nothing And t.YEAR = objDelareEntitlementNB.YEAR).Any
                End If
                ' check nghỉ phép chỉ được gia hạn 1 lần trong năm
                If objDelareEntitlementNB.START_MONTH_EXTEND IsNot Nothing Then
                    checkMonthNP = (From t In Context.AT_DECLARE_ENTITLEMENT Where t.EMPLOYEE_ID = objData.EMPLOYEE_ID And t.START_MONTH_EXTEND IsNot Nothing And t.YEAR = objDelareEntitlementNB.YEAR).Any
                End If
            Next


            If checkMonthNB = False And checkMonthNP = False Then
                For index = 0 To objDelareEntitlementlist.Count - 1
                    objData = objDelareEntitlementlist(index)
                    Dim exists = (From t In Context.AT_DECLARE_ENTITLEMENT Where t.ID = objDelareEntitlementNB.ID).Any
                    If exists Then
                        Dim obj = (From r In Context.AT_DECLARE_ENTITLEMENT Where r.ID = objDelareEntitlementNB.ID And r.EMPLOYEE_ID = objData.EMPLOYEE_ID).FirstOrDefault
                        obj.ADJUST_MONTH_TN = objDelareEntitlementNB.ADJUST_MONTH_TN
                        obj.ADJUST_MONTH_ENTITLEMENT = objDelareEntitlementNB.ADJUST_MONTH_ENTITLEMENT
                        obj.ADJUST_ENTITLEMENT = objDelareEntitlementNB.ADJUST_ENTITLEMENT
                        obj.ADJUST_ENTITLEMENT_PREV = objDelareEntitlementNB.ADJUST_ENTITLEMENT_PREV
                        obj.START_MONTH_TN = objDelareEntitlementNB.START_MONTH_TN
                        obj.START_MONTH_EXTEND = objDelareEntitlementNB.START_MONTH_EXTEND
                        obj.YEAR = objDelareEntitlementNB.YEAR
                        obj.YEAR_NB = objDelareEntitlementNB.YEAR_NB
                        obj.YEAR_ENTITLEMENT = objDelareEntitlementNB.YEAR_ENTITLEMENT
                        obj.REMARK_TN = objDelareEntitlementNB.REMARK_TN
                        obj.REMARK_ENTITLEMENT = objDelareEntitlementNB.REMARK_ENTITLEMENT
                        obj.ADJUST_NB = objDelareEntitlementNB.ADJUST_NB
                        obj.ADJUST_NB_PREV = objDelareEntitlementNB.ADJUST_NB_PREV
                        obj.START_MONTH_NB = objDelareEntitlementNB.START_MONTH_NB
                        obj.REMARK_NB = objDelareEntitlementNB.REMARK_NB
                        obj.MONTH_EXTENSION_NB = objDelareEntitlementNB.MONTH_EXTENSION_NB
                        obj.COM_PAY = objDelareEntitlementNB.COM_PAY
                        obj.ENT_PAY = objDelareEntitlementNB.ENT_PAY
                        obj.COM_PAY_PREV = objDelareEntitlementNB.COM_PAY_PREV
                        obj.ENT_PAY_PREV = objDelareEntitlementNB.ENT_PAY_PREV
                    Else
                        Dim objDelareEntitlementNBData As New AT_DECLARE_ENTITLEMENT
                        objDelareEntitlementNBData.ID = Utilities.GetNextSequence(Context, Context.AT_DECLARE_ENTITLEMENT.EntitySet.Name)
                        objDelareEntitlementNBData.EMPLOYEE_ID = objData.EMPLOYEE_ID
                        objDelareEntitlementNBData.ADJUST_MONTH_TN = objDelareEntitlementNB.ADJUST_MONTH_TN
                        objDelareEntitlementNBData.ADJUST_MONTH_ENTITLEMENT = objDelareEntitlementNB.ADJUST_MONTH_ENTITLEMENT
                        objDelareEntitlementNBData.ADJUST_ENTITLEMENT = objDelareEntitlementNB.ADJUST_ENTITLEMENT
                        objDelareEntitlementNBData.ADJUST_ENTITLEMENT_PREV = objDelareEntitlementNB.ADJUST_ENTITLEMENT_PREV
                        objDelareEntitlementNBData.YEAR = objDelareEntitlementNB.YEAR
                        objDelareEntitlementNBData.YEAR_NB = objDelareEntitlementNB.YEAR_NB
                        objDelareEntitlementNBData.YEAR_ENTITLEMENT = objDelareEntitlementNB.YEAR_ENTITLEMENT
                        objDelareEntitlementNBData.START_MONTH_TN = objDelareEntitlementNB.START_MONTH_TN
                        objDelareEntitlementNBData.START_MONTH_EXTEND = objDelareEntitlementNB.START_MONTH_EXTEND
                        objDelareEntitlementNBData.REMARK_TN = objDelareEntitlementNB.REMARK_TN
                        objDelareEntitlementNBData.REMARK_ENTITLEMENT = objDelareEntitlementNB.REMARK_ENTITLEMENT
                        objDelareEntitlementNBData.ADJUST_NB = objDelareEntitlementNB.ADJUST_NB
                        objDelareEntitlementNBData.START_MONTH_NB = objDelareEntitlementNB.START_MONTH_NB
                        objDelareEntitlementNBData.REMARK_NB = objDelareEntitlementNB.REMARK_NB
                        objDelareEntitlementNBData.MONTH_EXTENSION_NB = objDelareEntitlementNB.MONTH_EXTENSION_NB
                        objDelareEntitlementNBData.COM_PAY = objDelareEntitlementNB.COM_PAY
                        objDelareEntitlementNBData.ENT_PAY = objDelareEntitlementNB.ENT_PAY
                        objDelareEntitlementNBData.COM_PAY_PREV = objDelareEntitlementNB.COM_PAY_PREV
                        objDelareEntitlementNBData.ENT_PAY_PREV = objDelareEntitlementNB.ENT_PAY_PREV
                        Context.AT_DECLARE_ENTITLEMENT.AddObject(objDelareEntitlementNBData)
                    End If
                    Context.SaveChanges(log)
                Next
                Return True
            End If

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ImportDelareEntitlementNB(ByVal dtData As DataTable, ByVal log As UserLog, ByRef gID As Decimal, ByRef checkMonthNB As Boolean, ByRef checkMonthNP As Boolean) As Boolean
        Try
            For Each row As DataRow In dtData.Rows
                Dim employee_id As New Decimal
                Dim year As New Decimal
                employee_id = Utilities.Obj2Decima(row("EMPLOYEE_ID"))
                year = Utilities.Obj2Decima(row("YEAR"))

                ' check nghỉ bù chỉ được gia hạn 1 lần trong năm
                If row("MONTH_EXTENSION_NB") IsNot Nothing AndAlso row("MONTH_EXTENSION_NB") <> "" Then
                    checkMonthNB = (From t In Context.AT_DECLARE_ENTITLEMENT Where t.EMPLOYEE_ID = employee_id And t.MONTH_EXTENSION_NB IsNot Nothing And t.YEAR = year).Any
                End If
                ' check nghỉ phép chỉ được gia hạn 1 lần trong năm
                If row("START_MONTH_EXTEND") IsNot Nothing AndAlso row("START_MONTH_EXTEND") <> "" Then
                    checkMonthNP = (From t In Context.AT_DECLARE_ENTITLEMENT Where t.EMPLOYEE_ID = employee_id And t.START_MONTH_EXTEND IsNot Nothing And t.YEAR = year).Any
                End If
                If checkMonthNB = False And checkMonthNP = False Then
                    Dim objDelareEntitlementNBData As New AT_DECLARE_ENTITLEMENT
                    objDelareEntitlementNBData.ID = Utilities.GetNextSequence(Context, Context.AT_DECLARE_ENTITLEMENT.EntitySet.Name)
                    objDelareEntitlementNBData.EMPLOYEE_ID = Utilities.Obj2Decima(row("EMPLOYEE_ID"))
                    objDelareEntitlementNBData.ADJUST_MONTH_TN = Utilities.Obj2Decima(row("ADJUST_MONTH_TN"))
                    objDelareEntitlementNBData.ADJUST_MONTH_ENTITLEMENT = Utilities.Obj2Decima(row("ADJUST_MONTH_ENTITLEMENT"))
                    objDelareEntitlementNBData.ADJUST_ENTITLEMENT = Utilities.Obj2Decima(row("ADJUST_ENTITLEMENT"))
                    objDelareEntitlementNBData.YEAR = Utilities.Obj2Decima(row("YEAR"))
                    objDelareEntitlementNBData.YEAR_NB = Utilities.Obj2Decima(row("YEAR"))
                    objDelareEntitlementNBData.YEAR_ENTITLEMENT = Utilities.Obj2Decima(row("YEAR"))
                    objDelareEntitlementNBData.START_MONTH_TN = Utilities.Obj2Decima(row("START_MONTH_TN"))
                    objDelareEntitlementNBData.START_MONTH_EXTEND = IIf(Utilities.Obj2Decima(row("START_MONTH_EXTEND")) = 0, Nothing, Utilities.Obj2Decima(row("START_MONTH_EXTEND")))
                    objDelareEntitlementNBData.REMARK_TN = row("REMARK_TN")
                    objDelareEntitlementNBData.REMARK_ENTITLEMENT = row("REMARK_ENTITLEMENT")
                    objDelareEntitlementNBData.ADJUST_NB = Utilities.Obj2Decima(row("ADJUST_NB"))
                    objDelareEntitlementNBData.START_MONTH_NB = Utilities.Obj2Decima(row("START_MONTH_NB"))
                    objDelareEntitlementNBData.REMARK_NB = row("REMARK_NB")
                    objDelareEntitlementNBData.MONTH_EXTENSION_NB = IIf(Utilities.Obj2Decima(row("MONTH_EXTENSION_NB")) = 0, Nothing, Utilities.Obj2Decima(row("MONTH_EXTENSION_NB")))
                    objDelareEntitlementNBData.COM_PAY = Utilities.Obj2Decima(row("COM_PAY"))
                    objDelareEntitlementNBData.ENT_PAY = Utilities.Obj2Decima(row("ENT_PAY"))
                    objDelareEntitlementNBData.COM_PAY_PREV = Utilities.Obj2Decima(row("COM_PAY_PREV"))
                    objDelareEntitlementNBData.ENT_PAY_PREV = Utilities.Obj2Decima(row("ENT_PAY_PREV"))
                    Context.AT_DECLARE_ENTITLEMENT.AddObject(objDelareEntitlementNBData)
                Else
                    Return False
                End If
            Next
            Context.SaveChanges(log)
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ModifyDelareEntitlementNB(ByVal objDelareEntitlementNB As AT_DECLARE_ENTITLEMENTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objDelareEntitlementNBData As New AT_DECLARE_ENTITLEMENT With {.ID = objDelareEntitlementNB.ID}
        Try
            Dim exists = (From r In Context.AT_DECLARE_ENTITLEMENT Where r.EMPLOYEE_ID = objDelareEntitlementNB.EMPLOYEE_ID).Any
            If exists Then
                Dim obj = (From r In Context.AT_DECLARE_ENTITLEMENT Where r.EMPLOYEE_ID = objDelareEntitlementNB.EMPLOYEE_ID).FirstOrDefault
                obj.ADJUST_MONTH_TN = objDelareEntitlementNB.ADJUST_MONTH_TN
                obj.ADJUST_MONTH_ENTITLEMENT = objDelareEntitlementNB.ADJUST_MONTH_ENTITLEMENT
                obj.ADJUST_ENTITLEMENT = objDelareEntitlementNB.ADJUST_ENTITLEMENT
                obj.ADJUST_ENTITLEMENT_PREV = objDelareEntitlementNB.ADJUST_ENTITLEMENT_PREV
                obj.START_MONTH_TN = objDelareEntitlementNB.START_MONTH_TN
                obj.YEAR = objDelareEntitlementNB.YEAR
                obj.YEAR_NB = objDelareEntitlementNB.YEAR_NB
                obj.YEAR_ENTITLEMENT = objDelareEntitlementNBData.YEAR_ENTITLEMENT
                obj.START_MONTH_EXTEND = objDelareEntitlementNB.START_MONTH_EXTEND
                obj.REMARK_TN = objDelareEntitlementNB.REMARK_TN
                obj.REMARK_ENTITLEMENT = objDelareEntitlementNB.REMARK_ENTITLEMENT
                obj.MONTH_EXTENSION_NB = objDelareEntitlementNB.MONTH_EXTENSION_NB
                obj.COM_PAY = objDelareEntitlementNB.COM_PAY
                obj.ENT_PAY = objDelareEntitlementNB.ENT_PAY
                obj.COM_PAY_PREV = objDelareEntitlementNB.COM_PAY_PREV
                obj.ENT_PAY_PREV = objDelareEntitlementNB.ENT_PAY_PREV
            Else
                Return False
            End If
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function DeleteDelareEntitlementNB(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstl As List(Of AT_DECLARE_ENTITLEMENT)
        Try
            lstl = (From p In Context.AT_DECLARE_ENTITLEMENT Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstl.Count - 1
                Context.AT_DECLARE_ENTITLEMENT.DeleteObject(lstl(index))
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteCostCenter")
            Throw ex
        End Try
    End Function

    Public Function ValidateMonthThamNien(ByVal _validate As AT_DECLARE_ENTITLEMENTDTO)
        Dim query
        Try
            If _validate.START_MONTH_TN IsNot Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.AT_DECLARE_ENTITLEMENT
                             Where p.EMPLOYEE_ID = _validate.EMPLOYEE_ID _
                             And p.START_MONTH_TN = _validate.START_MONTH_TN _
                             And p.YEAR = _validate.YEAR _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.AT_DECLARE_ENTITLEMENT
                             Where p.EMPLOYEE_ID = _validate.EMPLOYEE_ID _
                             And p.START_MONTH_TN = _validate.START_MONTH_TN _
                             And p.YEAR = _validate.YEAR).FirstOrDefault
                End If
                Return (query Is Nothing)
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ValidateMonthPhepNam(ByVal _validate As AT_DECLARE_ENTITLEMENTDTO)
        Dim query
        Try
            If _validate.ADJUST_MONTH_ENTITLEMENT IsNot Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.AT_DECLARE_ENTITLEMENT
                             Where p.EMPLOYEE_ID = _validate.EMPLOYEE_ID _
                             And p.ADJUST_MONTH_ENTITLEMENT = _validate.ADJUST_MONTH_ENTITLEMENT _
                             And p.YEAR = _validate.YEAR _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.AT_DECLARE_ENTITLEMENT
                             Where p.EMPLOYEE_ID = _validate.EMPLOYEE_ID _
                             And p.ADJUST_MONTH_ENTITLEMENT = _validate.ADJUST_MONTH_ENTITLEMENT _
                             And p.YEAR = _validate.YEAR).FirstOrDefault
                End If
                Return (query Is Nothing)
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ValidateMonthNghiBu(ByVal _validate As AT_DECLARE_ENTITLEMENTDTO)
        Dim query
        Try
            If _validate.START_MONTH_NB IsNot Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.AT_DECLARE_ENTITLEMENT
                             Where p.EMPLOYEE_ID = _validate.EMPLOYEE_ID _
                             And p.START_MONTH_NB = _validate.START_MONTH_NB _
                             And p.YEAR = _validate.YEAR _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.AT_DECLARE_ENTITLEMENT
                             Where p.EMPLOYEE_ID = _validate.EMPLOYEE_ID _
                             And p.START_MONTH_NB = _validate.START_MONTH_NB _
                             And p.YEAR = _validate.YEAR).FirstOrDefault
                End If
                Return (query Is Nothing)
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

#End Region

#Region "Bang tong hop công cơm"
    Public Function Cal_TimeTImesheet_Rice(ByVal _param As ParamDTO, ByVal log As UserLog, ByVal p_period_id As Decimal?, ByVal P_ORG_ID As Decimal, ByVal lstEmployee As List(Of Decimal?)) As Boolean
        Try
            Using cls As New DataAccess.NonQueryData
                cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.CAL_TIME_TIMESHEET_RICE",
                                               New With {.P_USERNAME = log.Username.ToUpper,
                                                         .P_ORG_ID = P_ORG_ID,
                                                         .P_PERIOD_ID = p_period_id,
                                                         .P_ISDISSOLVE = _param.IS_DISSOLVE})
                Return True
            End Using

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex

        End Try
    End Function

    Public Function GetSummaryRice(ByVal param As AT_TIME_TIMESHEET_RICEDTO, ByVal log As UserLog) As DataSet
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataSet = cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.GET_SUMMARY_RICE",
                                               New With {.P_USERNAME = log.Username.ToUpper,
                                                         .P_ORG_ID = param.ORG_ID,
                                                         .P_ISDISSOLVE = param.IS_DISSOLVE,
                                                         .P_PAGE_INDEX = param.PAGE_INDEX,
                                                         .P_EMPLOYEE_CODE = param.EMPLOYEE_CODE,
                                                         .P_PAGE_SIZE = param.PAGE_SIZE,
                                                         .P_PERIOD_ID = param.PERIOD_ID,
                                                         .P_EMPLOYEE_NAME = param.VN_FULLNAME,
                                                         .P_ORG_NAME = param.ORG_NAME,
                                                         .P_TITLE_NAME = param.TITLE_NAME,
                                                         .P_STAFF_RANK_NAME = param.STAFF_RANK_NAME,
                                                         .P_CUR = cls.OUT_CURSOR,
                                                         .P_CURCOUNT = cls.OUT_CURSOR}, False)
                Return dtData
            End Using

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function GetTimeSheetRiceById(ByVal obj As AT_TIME_TIMESHEET_RICEDTO) As AT_TIME_TIMESHEET_RICEDTO
        Try
            Dim query = From p In Context.AT_TIME_TIMESHEET_RICE
                      From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                      From t In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID).DefaultIfEmpty
                      From s In Context.HU_STAFF_RANK.Where(Function(f) f.ID = e.STAFF_RANK_ID).DefaultIfEmpty
                      From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                      Where p.ID = obj.ID
            Dim lst = query.Select(Function(p) New AT_TIME_TIMESHEET_RICEDTO With {
                                       .ID = p.p.ID,
                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                       .VN_FULLNAME = p.e.FULLNAME_VN,
                                       .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                       .TITLE_NAME = p.t.NAME_VN,
                                       .PERIOD_ID = p.p.PERIOD_ID,
                                       .ORG_NAME = p.o.NAME_VN,
                                       .ORG_DESC = p.o.DESCRIPTION_PATH,
                                       .STAFF_RANK_NAME = p.s.NAME,
                                       .nday_rice = p.p.NDAY_RICE,
                                       .total_rice = p.p.TOTAL_RICE,
                                       .total_rice_declare = p.p.TOTAL_RICE_DECLARE,
                                       .total_rice_price = p.p.TOTAL_RICE_PRICE,
                                       .rice_edit = p.p.RICE_EDIT,
                                       .ORG_ID = p.p.ORG_ID,
                                       .CREATED_BY = p.p.CREATED_BY,
                                       .CREATED_DATE = p.p.CREATED_DATE,
                                       .CREATED_LOG = p.p.CREATED_LOG,
                                       .MODIFIED_BY = p.p.MODIFIED_BY,
                                       .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                       .MODIFIED_LOG = p.p.MODIFIED_LOG}).FirstOrDefault
            Return lst
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function InsertLeaveSheetRice(ByVal objTimeSheetDaily As AT_TIME_TIMESHEET_RICEDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Try
            Dim TimeSheetDaily = (From r In Context.AT_TIME_TIMESHEET_RICE Where r.EMPLOYEE_ID = objTimeSheetDaily.EMPLOYEE_ID And r.PERIOD_ID = objTimeSheetDaily.PERIOD_ID).FirstOrDefault

            If TimeSheetDaily IsNot Nothing Then
                TimeSheetDaily.RICE_EDIT = objTimeSheetDaily.rice_edit
                TimeSheetDaily.TOTAL_RICE = TimeSheetDaily.TOTAL_RICE_DECLARE + TimeSheetDaily.TOTAL_RICE_PRICE + objTimeSheetDaily.rice_edit
                Context.SaveChanges(log)
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ModifyLeaveSheetRice(ByVal objLeave As AT_TIME_TIMESHEET_RICEDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Try
            Dim TimeSheetDaily = (From r In Context.AT_TIME_TIMESHEET_RICE Where r.EMPLOYEE_ID = objLeave.EMPLOYEE_ID And r.PERIOD_ID = objLeave.PERIOD_ID).FirstOrDefault
            TimeSheetDaily.RICE_EDIT = objLeave.rice_edit
            TimeSheetDaily.TOTAL_RICE = TimeSheetDaily.TOTAL_RICE_DECLARE + TimeSheetDaily.TOTAL_RICE_PRICE + objLeave.rice_edit
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ApprovedTimeSheetRice(ByVal objLeave As AT_TIME_TIMESHEET_RICEDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Try
            Dim TimeSheetDaily = (From r In Context.AT_TIME_TIMESHEET_RICE Where r.EMPLOYEE_ID = objLeave.EMPLOYEE_ID And r.PERIOD_ID = objLeave.PERIOD_ID).FirstOrDefault
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
#End Region

#Region "Đăng ký công"

    Public Function GetLeaveSheet(ByVal _filter As AT_LEAVESHEETDTO,
                                       ByVal _param As ParamDTO,
                                       Optional ByRef Total As Integer = 0,
                                       Optional ByVal PageIndex As Integer = 0,
                                       Optional ByVal PageSize As Integer = Integer.MaxValue,
                                       Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_LEAVESHEETDTO)
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username.ToUpper,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using
            Dim query = From p In Context.AT_LEAVESHEET
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        From s In Context.HU_STAFF_RANK.Where(Function(F) F.ID = e.STAFF_RANK_ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From m In Context.AT_TIME_MANUAL.Where(Function(f) f.ID = p.MANUAL_ID).DefaultIfEmpty
                        From l1 In Context.AT_FML.Where(Function(f) f.ID = m.MORNING_ID).DefaultIfEmpty
                        From l2 In Context.AT_FML.Where(Function(f) f.ID = m.AFTERNOON_ID).DefaultIfEmpty
                        From en In Context.AT_ENTITLEMENT.Where(Function(F) F.EMPLOYEE_ID = p.EMPLOYEE_ID And p.WORKINGDAY.Value.Year = F.YEAR).DefaultIfEmpty
                        From nb In Context.AT_COMPENSATORY.Where(Function(F) F.EMPLOYEE_ID = p.EMPLOYEE_ID And F.YEAR = _filter.FROM_DATE.Value.Year).DefaultIfEmpty
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) e.ORG_ID = f.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper)
            If _filter.ISTEMINAL Then
                query = query.Where(Function(f) f.e.WORK_STATUS = 257)
                If _filter.WORKINGDAY.HasValue Then
                    query = query.Where(Function(f) f.e.TER_LAST_DATE <= _filter.WORKINGDAY)
                End If
            End If
            If _filter.FROM_DATE.HasValue Then
                query = query.Where(Function(f) f.p.LEAVE_FROM >= _filter.FROM_DATE)
            End If
            If _filter.END_DATE.HasValue Then
                query = query.Where(Function(f) f.p.LEAVE_TO <= _filter.END_DATE)
            End If
            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                query = query.Where(Function(f) f.e.EMPLOYEE_CODE.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower()) Or f.e.FULLNAME_VN.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.VN_FULLNAME) Then
                query = query.Where(Function(f) f.e.FULLNAME_VN.ToLower().Contains(_filter.VN_FULLNAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                query = query.Where(Function(f) f.o.NAME_VN.ToLower().Contains(_filter.ORG_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TITLE_NAME) Then
                query = query.Where(Function(f) f.t.NAME_VN.ToLower().Contains(_filter.TITLE_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.STAFF_RANK_NAME) Then
                query = query.Where(Function(f) f.s.NAME.ToLower().Contains(_filter.STAFF_RANK_NAME.ToLower()))
            End If
            If _filter.BALANCE_NOW.HasValue Then
                query = query.Where(Function(f) f.p.BALANCE_NOW = _filter.BALANCE_NOW)
            End If
            If _filter.NGHIBUCONLAI.HasValue Then
                query = query.Where(Function(f) f.nb.CUR_HAVE = _filter.NGHIBUCONLAI)
            End If
            If _filter.LEAVE_FROM.HasValue Then
                query = query.Where(Function(f) f.p.LEAVE_FROM = _filter.LEAVE_FROM)
            End If
            If _filter.LEAVE_TO.HasValue Then
                query = query.Where(Function(f) f.p.LEAVE_TO = _filter.LEAVE_TO)
            End If
            If Not String.IsNullOrEmpty(_filter.MANUAL_NAME) Then
                query = query.Where(Function(f) f.m.NAME.ToLower().Contains(_filter.MANUAL_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.MORNING_NAME) Then
                query = query.Where(Function(f) f.l1.NAME_VN.ToLower().Contains(_filter.MORNING_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.AFTERNOON_NAME) Then
                query = query.Where(Function(f) f.l2.NAME_VN.ToLower().Contains(_filter.AFTERNOON_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.NOTE) Then
                query = query.Where(Function(f) f.p.NOTE.ToLower().Contains(_filter.NOTE.ToLower()))
            End If
            Dim lst = query.Select(Function(p) New AT_LEAVESHEETDTO With {
                                                                       .ID = p.p.ID,
                                                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                                                       .VN_FULLNAME = p.e.FULLNAME_VN,
                                                                       .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                                                       .TITLE_NAME = p.t.NAME_VN,
                                                                       .STAFF_RANK_ID = p.e.STAFF_RANK_ID,
                                                                       .STAFF_RANK_NAME = p.s.NAME,
                                                                       .ORG_NAME = p.o.NAME_VN,
                                                                       .ORG_DESC = p.o.DESCRIPTION_PATH,
                                                                       .ORG_ID = p.e.ORG_ID,
                                                                       .LEAVE_FROM = p.p.LEAVE_FROM,
                                                                       .LEAVE_TO = p.p.LEAVE_TO,
                                                                       .MANUAL_NAME = p.m.NAME,
                                                                       .MANUAL_ID = p.p.MANUAL_ID,
                                                                       .MORNING_ID = p.m.MORNING_ID,
                                                                       .AFTERNOON_ID = p.m.AFTERNOON_ID,
                                                                       .MORNING_NAME = p.l1.NAME_VN,
                                                                       .AFTERNOON_NAME = p.l2.NAME_VN,
                                                                       .BALANCE_NOW = p.en.CUR_HAVE,
                                                                       .NGHIBUCONLAI = p.nb.CUR_HAVE,
                                                                       .WORKINGDAY = p.p.WORKINGDAY,
                                                                       .NOTE = p.p.NOTE,
                                                                       .CREATED_BY = p.p.CREATED_BY,
                                                                       .CREATED_DATE = p.p.CREATED_DATE,
                                                                       .CREATED_LOG = p.p.CREATED_LOG,
                                                                       .MODIFIED_BY = p.p.MODIFIED_BY,
                                                                       .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                                                       .MODIFIED_LOG = p.p.MODIFIED_LOG})
            Dim sql = (From T In lst
                            Group T By
                              T.LEAVE_FROM,
                              T.LEAVE_TO,
                              T.EMPLOYEE_ID
                             Into g = Group
                            Select
                              ID = CType(g.Max(Function(p) p.ID), Decimal?),
                              EMPLOYEE_CODE = g.Max(Function(p) p.EMPLOYEE_CODE),
                              VN_FULLNAME = g.Max(Function(p) p.VN_FULLNAME),
                              TITLE_NAME = g.Max(Function(p) p.TITLE_NAME),
                              STAFF_RANK_NAME = g.Max(Function(p) p.STAFF_RANK_NAME),
                              STAFF_RANK_ID = g.Max(Function(p) p.STAFF_RANK_ID),
                              ORG_NAME = g.Max(Function(p) p.ORG_NAME),
                              ORG_DESC = g.Max(Function(p) p.ORG_DESC),
                              ORG_ID = CType(g.Max(Function(p) p.ORG_ID), Decimal?),
                              WORKINGDAY = CType(g.Max(Function(p) p.WORKINGDAY), DateTime?),
                              MANUAL_ID = g.Max(Function(p) p.MANUAL_ID),
                              MANUAL_NAME = g.Max(Function(p) p.MANUAL_NAME),
                              MORNING_ID = g.Max(Function(p) p.MORNING_ID),
                              AFTERNOON_ID = g.Max(Function(p) p.AFTERNOON_ID),
                              MORNING_NAME = g.Max(Function(p) p.MORNING_NAME),
                              AFTERNOON_NAME = g.Max(Function(p) p.AFTERNOON_NAME),
                              BALANCE_NOW = g.Max(Function(p) p.BALANCE_NOW),
                              NGHIBUCONLAI = g.Max(Function(p) p.NGHIBUCONLAI),
                              NOTE = g.Max(Function(p) p.NOTE),
                              CREATED_DATE = CType(g.Max(Function(p) p.CREATED_DATE), DateTime?),
                              CREATED_BY = g.Max(Function(p) p.CREATED_BY),
                              CREATED_LOG = g.Max(Function(p) p.CREATED_LOG),
                              MODIFIED_DATE = CType(g.Max(Function(p) p.MODIFIED_DATE), DateTime?),
                              MODIFIED_BY = g.Max(Function(p) p.MODIFIED_BY),
                              MODIFIED_LOG = g.Max(Function(p) p.MODIFIED_LOG),
                              EMPLOYEE_ID,
                              LEAVE_FROM,
                              LEAVE_TO).ToList

            Dim ls = sql.Select(Function(f) New AT_LEAVESHEETDTO With {
                                                                       .ID = f.ID,
                                                                       .EMPLOYEE_CODE = f.EMPLOYEE_CODE,
                                                                       .VN_FULLNAME = f.VN_FULLNAME,
                                                                       .EMPLOYEE_ID = f.EMPLOYEE_ID,
                                                                       .TITLE_NAME = f.TITLE_NAME,
                                                                       .STAFF_RANK_ID = f.STAFF_RANK_ID,
                                                                       .STAFF_RANK_NAME = f.STAFF_RANK_NAME,
                                                                       .ORG_NAME = f.ORG_NAME,
                                                                       .ORG_DESC = f.ORG_DESC,
                                                                       .ORG_ID = f.ORG_ID,
                                                                       .LEAVE_FROM = f.LEAVE_FROM,
                                                                       .LEAVE_TO = f.LEAVE_TO,
                                                                       .MANUAL_ID = f.MANUAL_ID,
                                                                       .MANUAL_NAME = f.MANUAL_NAME,
                                                                       .MORNING_ID = f.MORNING_ID,
                                                                       .AFTERNOON_ID = f.AFTERNOON_ID,
                                                                       .MORNING_NAME = f.MORNING_NAME,
                                                                       .AFTERNOON_NAME = f.AFTERNOON_NAME,
                                                                       .BALANCE_NOW = f.BALANCE_NOW,
                                                                       .NGHIBUCONLAI = f.NGHIBUCONLAI,
                                                                       .WORKINGDAY = f.WORKINGDAY,
                                                                       .NOTE = f.NOTE,
                                                                       .CREATED_BY = f.CREATED_BY,
                                                                       .CREATED_DATE = f.CREATED_DATE,
                                                                       .CREATED_LOG = f.CREATED_LOG,
                                                                       .MODIFIED_BY = f.MODIFIED_BY,
                                                                       .MODIFIED_DATE = f.MODIFIED_DATE,
                                                                       .MODIFIED_LOG = f.MODIFIED_LOG
                                                                        }).AsQueryable

            ls = ls.OrderBy(Sorts)
            Total = ls.Count
            ls = ls.Skip(PageIndex * PageSize).Take(PageSize)
            Return ls.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function GetPhepNam(ByVal _id As Decimal?, ByVal _year As Decimal?) As AT_ENTITLEMENTDTO
        Try

            Dim query = From p In Context.AT_ENTITLEMENT
                        Where p.EMPLOYEE_ID = _id And p.YEAR = _year
                        Order By p.ID Descending
            Dim lst = query.Select(Function(p) New AT_ENTITLEMENTDTO With {
                                       .EMPLOYEE_ID = p.EMPLOYEE_ID,
                                       .CAL_DATE = p.CAL_DATE,
                                       .CUR_HAVE = p.CUR_HAVE,
                                       .TOTAL_HAVE = p.TOTAL_HAVE,
                                       .CUR_REMAIN = p.CUR_HAVE}).FirstOrDefault
            Return lst
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function GetTotalDAY(ByVal P_EMPLOYEE_ID As Integer,
                                ByVal P_TYPE_MANUAL As Integer,
                                ByVal P_FROM_DATE As Date,
                                ByVal P_TO_DATE As Date) As DataTable
        Using cls As New DataAccess.QueryData
            Dim dtData As DataTable = cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.CALCULATOR_DAY",
                                           New With {.P_FROM_DATE = P_FROM_DATE,
                                                     .P_TO_DATE = P_TO_DATE,
                                                     .p_EMPLOYEE_ID = P_EMPLOYEE_ID,
                                                     .p_TYPE_MANUAL = P_TYPE_MANUAL,
                                                     .P_CUR = cls.OUT_CURSOR})
            Return dtData
        End Using
        Return Nothing
    End Function

    Public Function GetCAL_DAY_LEAVE_OLD(ByVal P_EMPLOYEE_ID As Integer,
                                        ByVal P_FROM_DATE As Date,
                                        ByVal P_TO_DATE As Date) As DataTable
        Using cls As New DataAccess.QueryData
            Dim dtData As DataTable = cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.CAL_DAY_LEAVE_OLD",
                                           New With {.P_FROM_DATE = P_FROM_DATE,
                                                     .P_TO_DATE = P_TO_DATE,
                                                     .p_EMPLOYEE_ID = P_EMPLOYEE_ID,
                                                     .P_CUR = cls.OUT_CURSOR})
            Return dtData
        End Using
        Return Nothing
    End Function

    Public Function GetTotalPHEPNAM(ByVal P_EMPLOYEE_ID As Integer,
                                      ByVal Date_cal As Date,
                                      ByVal P_TYPE_LEAVE_ID As Integer) As DataTable
        Using cls As New DataAccess.QueryData
            Dim dtData As DataTable = cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.GET_TOTAL_PHEPNAM",
                                           New With {.P_EMPLOYEE_ID = P_EMPLOYEE_ID,
                                                     .DATE_CAL = Date_cal,
                                                     .P_TYPE_LEAVE_ID = P_TYPE_LEAVE_ID,
                                                     .P_CUR = cls.OUT_CURSOR})
            Return dtData
        End Using
        Return Nothing
    End Function

    Public Function GetTotalPHEPBU(ByVal P_EMPLOYEE_ID As Integer,
                                    ByVal Date_cal As Date,
                                    ByVal P_TYPE_LEAVE_ID As Integer) As DataTable
        Using cls As New DataAccess.QueryData
            Dim dtData As DataTable = cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.GET_TOTAL_PHEPBU",
                                           New With {.P_EMPLOYEE_ID = P_EMPLOYEE_ID,
                                                     .DATE_CAL = Date_cal,
                                                     .P_TYPE_LEAVE_ID = P_TYPE_LEAVE_ID,
                                                     .P_CUR = cls.OUT_CURSOR})
            Return dtData
        End Using
        Return Nothing
    End Function

    Public Function GetNghiBu(ByVal _id As Decimal?, ByVal _year As Decimal?) As AT_COMPENSATORYDTO
        Try

            Dim query = From p In Context.AT_COMPENSATORY
                        Where p.EMPLOYEE_ID = _id And p.YEAR = _year
            Dim lst = query.Select(Function(p) New AT_COMPENSATORYDTO With {
                                       .EMPLOYEE_ID = p.EMPLOYEE_ID,
                                       .CAL_DATE = p.CAL_DATE,
                                       .TOTAL_HAVE = p.TOTAL_HAVE,
                                       .CUR_HAVE = p.CUR_HAVE}).FirstOrDefault
            Return lst
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function GetPHEPBUCONLAI(ByVal lstEmpID As List(Of AT_LEAVESHEETDTO), ByVal _year As Decimal?) As List(Of AT_LEAVESHEETDTO)
        Dim objData As New List(Of AT_LEAVESHEETDTO)
        Try
            For index = 0 To lstEmpID.Count - 1
                Dim employeeID As Decimal = lstEmpID(index).EMPLOYEE_ID
                Dim query = From e In Context.HU_EMPLOYEE
                            From o In Context.HU_ORGANIZATION.Where(Function(F) F.ID = e.ORG_ID).DefaultIfEmpty
                            From t In Context.HU_TITLE.Where(Function(F) F.ID = e.TITLE_ID).DefaultIfEmpty
                            From p In Context.AT_ENTITLEMENT.Where(Function(F) F.EMPLOYEE_ID = e.ID And F.YEAR = _year).DefaultIfEmpty
                            From b In Context.AT_COMPENSATORY.Where(Function(F) F.EMPLOYEE_ID = e.ID And F.YEAR = _year).DefaultIfEmpty
                            Where e.ID = employeeID
                If query IsNot Nothing Then
                    Dim lst = query.Select(Function(p) New AT_LEAVESHEETDTO With {
                                          .EMPLOYEE_ID = p.e.ID,
                                          .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                          .VN_FULLNAME = p.e.FULLNAME_VN,
                                          .TITLE_NAME = p.t.NAME_VN,
                                          .ORG_ID = p.e.ORG_ID,
                                          .ORG_NAME = p.o.NAME_VN,
                                          .BALANCE_NOW = p.p.CUR_HAVE,
                                          .NBCL = p.b.CUR_HAVE}).FirstOrDefault
                    objData.Add(lst)
                End If
            Next

            Return objData
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function GetLeaveById(ByVal _id As Decimal?) As AT_LEAVESHEETDTO
        Try

            Dim query = From p In Context.AT_LEAVESHEET
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From s In Context.HU_STAFF_RANK.Where(Function(F) F.ID = e.STAFF_RANK_ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From m In Context.AT_TIME_MANUAL.Where(Function(F) F.ID = p.MANUAL_ID).DefaultIfEmpty
                        From en In Context.AT_ENTITLEMENT.Where(Function(F) F.EMPLOYEE_ID = p.EMPLOYEE_ID And p.WORKINGDAY.Value.Year = F.YEAR).DefaultIfEmpty()
                        From nb In Context.AT_COMPENSATORY.Where(Function(F) F.EMPLOYEE_ID = p.EMPLOYEE_ID And p.WORKINGDAY.Value.Year = F.YEAR).DefaultIfEmpty()
                        Where p.ID = _id

            Dim lst = query.Select(Function(p) New AT_LEAVESHEETDTO With {
                                                                    .ID = p.p.ID,
                                                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                                                       .VN_FULLNAME = p.e.FULLNAME_VN,
                                                                       .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                                                       .TITLE_NAME = p.t.NAME_VN,
                                                                       .STAFF_RANK_ID = p.e.STAFF_RANK_ID,
                                                                       .STAFF_RANK_NAME = p.s.NAME,
                                                                       .ORG_NAME = p.o.NAME_VN,
                                                                       .ORG_ID = p.e.ORG_ID,
                                                                       .LEAVE_FROM = p.p.LEAVE_FROM,
                                                                       .LEAVE_TO = p.p.LEAVE_TO,
                                                                       .MANUAL_ID = p.p.MANUAL_ID,
                                                                       .MANUAL_NAME = p.m.NAME,
                                                                       .MORNING_ID = p.m.MORNING_ID,
                                                                       .AFTERNOON_ID = p.m.AFTERNOON_ID,
                                                                       .BALANCE_NOW = p.en.CUR_HAVE,
                                                                       .NBCL = p.nb.CUR_HAVE,
                                                                       .WORKINGDAY = p.p.WORKINGDAY,
                                                                       .NOTE = p.p.NOTE,
                                                                       .CREATED_BY = p.p.CREATED_BY,
                                                                       .CREATED_DATE = p.p.CREATED_DATE,
                                                                       .CREATED_LOG = p.p.CREATED_LOG,
                                                                       .MODIFIED_BY = p.p.MODIFIED_BY,
                                                                       .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                                                       .MODIFIED_LOG = p.p.MODIFIED_LOG}).FirstOrDefault
            Return lst
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function CheckDataCheckworksign(ByVal objworksignList As List(Of AT_REGISTER_OTDTO), ByVal objRegisterOT As AT_REGISTER_OTDTO, ByRef strEmployeeCode As String) As Boolean
        Dim employee_id As Decimal?
        Dim org_id As Decimal?
        Dim objData As New AT_REGISTER_OTDTO
        Dim dtData As New DataTable
        Try
            For index = 0 To objworksignList.Count - 1
                objData = objworksignList(index)
                Dim emp = (From e In Context.HU_EMPLOYEE
                                        From w In Context.HU_WORKING.Where(Function(f) f.EMPLOYEE_ID = e.ID)
                                        Where e.EMPLOYEE_CODE = objData.EMPLOYEE_CODE And e.JOIN_DATE <= objRegisterOT.WORKINGDAY And _
                                        (e.TER_EFFECT_DATE Is Nothing Or _
                                         (e.TER_EFFECT_DATE IsNot Nothing And _
                                          e.TER_EFFECT_DATE >= objRegisterOT.WORKINGDAY)) And w.EFFECT_DATE <= objRegisterOT.WORKINGDAY
                                  Order By w.EFFECT_DATE Descending
                                  Select w).FirstOrDefault
                If emp IsNot Nothing Then
                    employee_id = emp.EMPLOYEE_ID
                    org_id = emp.ORG_ID
                Else
                    Continue For
                End If
                Using cls As New DataAccess.QueryData
                    dtData = cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.CHECK_AT_LEAVE",
                                              New With {.P_EMPLOYEE_ID = employee_id,
                                                        .P_WORKINGDAY = objRegisterOT.WORKINGDAY,
                                                        .P_CUR = cls.OUT_CURSOR})
                    If dtData.Rows.Count = 0 Then
                        dtData = cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.GET_AT_WORKSIGN",
                                             New With {.P_EMPLOYEE_ID = employee_id,
                                                       .P_WORKINGDAY = objRegisterOT.WORKINGDAY,
                                                       .P_FROM_HOUSE = objRegisterOT.FROM_HOUR,
                                                       .P_TO_HOUSE = objRegisterOT.TO_HOUR,
                                                       .P_CUR = cls.OUT_CURSOR})
                    Else
                        Return True
                    End If


                End Using
                If dtData.Rows.Count > 0 Then
                    strEmployeeCode = strEmployeeCode & objData.EMPLOYEE_CODE & ","
                End If
            Next
            If Not strEmployeeCode.Equals("") Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function CheckDataCheckworksignImport(ByVal objRegisterOT As AT_REGISTER_OTDTO) As Boolean
        Dim employee_id As Decimal?
        Dim org_id As Decimal?
        Dim emp = (From e In Context.HU_EMPLOYEE
                   From w In Context.HU_WORKING.Where(Function(f) f.EMPLOYEE_ID = e.ID)
                   Where e.EMPLOYEE_CODE = objRegisterOT.EMPLOYEE_CODE And e.JOIN_DATE <= objRegisterOT.WORKINGDAY And _
                   (e.TER_EFFECT_DATE Is Nothing Or _
                    (e.TER_EFFECT_DATE IsNot Nothing And _
                     e.TER_EFFECT_DATE >= objRegisterOT.WORKINGDAY)) And w.EFFECT_DATE <= objRegisterOT.WORKINGDAY
             Order By w.EFFECT_DATE Descending
                          Select w).FirstOrDefault
        If emp IsNot Nothing Then
            employee_id = emp.EMPLOYEE_ID
            org_id = emp.ORG_ID
        Else
            Exit Function
        End If
        Try
            Dim dtData As New DataTable
            Using cls As New DataAccess.QueryData
                dtData = cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.GET_AT_WORKSIGN",
                                          New With {.P_EMPLOYEE_ID = employee_id,
                                                    .P_WORKINGDAY = objRegisterOT.WORKINGDAY,
                                                    .P_FROM_HOUSE = objRegisterOT.FROM_HOUR,
                                                    .P_TO_HOUSE = objRegisterOT.TO_HOUR,
                                                    .P_CUR = cls.OUT_CURSOR})
            End Using
            If dtData.Rows.Count > 0 Then
                ' If exists Then ' có dữ liệu trả lại false không cho phép đăng ký tiếp 
                Return False
            Else 'không  có dữ liệu trả lại true cho phép đăng ký tiếp 
                Return True
            End If

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function Check_DataRegister_OT(ByRef _param As String, ByVal log As UserLog, ByVal Startdate As Date?, ByVal Enddate As Date?, ByVal period_id As Decimal?) As Boolean
        Try

            Dim obj As New AT_ACTION_LOGDTO
            obj.PERIOD_ID = period_id
            'LOG_AT(_param, log, period_id, "XÓA ĐĂNG KÝ LÀM THÊM", obj)
            Using cls As New DataAccess.NonQueryData
                cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.DELETE_REGISTER_OT",
                                               New With {.P_EMPLOYEE_ID = _param,
                                                         .P_WORKINGTODAY = Startdate,
                                                         .P_WORKINGENDDAY = Enddate,
                                                         .P_USERNAME = log.Username,
                                                         .P_PERIOD_ID = period_id})
            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function Check_WorkSing_default(ByVal obj As ParamDTO, ByVal log As UserLog, ByRef Employee_ID As String) As Boolean
        Try
            Using cls As New DataAccess.NonQueryData
                cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.DELETE_REGISTER_OT1",
                                               New With {.P_EMPLOYEE_ID = Employee_ID,
                                                         .P_USERNAME = log.Username,
                                                         .P_PERIOD_ID = obj.PERIOD_ID})
            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function InsertLeaveSheet(ByVal objLeave As AT_LEAVESHEETDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim fromdate As Date?
        Try
            Dim exists = (From r In Context.AT_LEAVESHEET Where r.EMPLOYEE_ID = objLeave.EMPLOYEE_ID And r.WORKINGDAY <= objLeave.LEAVE_TO And r.WORKINGDAY >= objLeave.LEAVE_FROM).Any
            If exists Then ' trường hợp nếu đã có bản ghi trùng thì xóa đi và cập nhật bản ghi mới nhất
                Dim details = (From r In Context.AT_LEAVESHEET Where r.EMPLOYEE_ID = objLeave.EMPLOYEE_ID And r.WORKINGDAY <= objLeave.LEAVE_TO And r.WORKINGDAY >= objLeave.LEAVE_FROM).ToList
                For index = 0 To details.Count - 1
                    Context.AT_LEAVESHEET.DeleteObject(details(index))
                Next
                Dim objLeaveData As AT_LEAVESHEET
                'Dim emp = (From p In Context.HU_EMPLOYEE Where p.EMPLOYEE_CODE.ToLower().Contains(objLeave.EMPLOYEE_CODE.ToLower())).FirstOrDefault
                fromdate = objLeave.LEAVE_FROM
                While fromdate <= objLeave.LEAVE_TO
                    objLeaveData = New AT_LEAVESHEET
                    objLeaveData.ID = Utilities.GetNextSequence(Context, Context.AT_LEAVESHEET.EntitySet.Name)
                    objLeaveData.WORKINGDAY = fromdate
                    Dim emp = (From e In Context.HU_EMPLOYEE
                                 From w In Context.HU_WORKING.Where(Function(f) f.EMPLOYEE_ID = e.ID)
                                 Where e.EMPLOYEE_CODE = objLeave.EMPLOYEE_CODE And e.JOIN_DATE <= objLeaveData.WORKINGDAY And _
                                 (e.TER_EFFECT_DATE Is Nothing Or _
                                  (e.TER_EFFECT_DATE IsNot Nothing And _
                                   e.TER_EFFECT_DATE >= objLeaveData.WORKINGDAY)) And w.EFFECT_DATE <= objLeaveData.WORKINGDAY
                           Order By w.EFFECT_DATE Descending
                           Select w).FirstOrDefault
                    If emp IsNot Nothing Then
                        objLeaveData.EMPLOYEE_ID = emp.EMPLOYEE_ID
                        objLeaveData.LEAVE_FROM = objLeave.LEAVE_FROM
                        objLeaveData.LEAVE_TO = objLeave.LEAVE_TO
                        objLeaveData.MANUAL_ID = objLeave.MANUAL_ID
                        objLeaveData.AFTERNOON_ID = objLeave.AFTERNOON_ID
                        objLeaveData.MORNING_ID = objLeave.MORNING_ID
                        objLeaveData.NOTE = objLeave.NOTE
                        fromdate = fromdate.Value.AddDays(1)
                        Context.AT_LEAVESHEET.AddObject(objLeaveData)
                    Else
                        fromdate = fromdate.Value.AddDays(1)
                    End If

                End While
                Context.SaveChanges(log)
            Else
                Dim objLeaveData As AT_LEAVESHEET
                fromdate = objLeave.LEAVE_FROM
                While fromdate <= objLeave.LEAVE_TO
                    objLeaveData = New AT_LEAVESHEET
                    objLeaveData.ID = Utilities.GetNextSequence(Context, Context.AT_LEAVESHEET.EntitySet.Name)
                    objLeaveData.WORKINGDAY = fromdate
                    Dim emp = (From e In Context.HU_EMPLOYEE
                               From w In Context.HU_WORKING.Where(Function(f) f.EMPLOYEE_ID = e.ID)
                               Where e.EMPLOYEE_CODE = objLeave.EMPLOYEE_CODE And e.JOIN_DATE <= objLeaveData.WORKINGDAY And _
                               (e.TER_EFFECT_DATE Is Nothing Or _
                                (e.TER_EFFECT_DATE IsNot Nothing And _
                                 e.TER_EFFECT_DATE >= objLeaveData.WORKINGDAY)) And w.EFFECT_DATE <= objLeaveData.WORKINGDAY
                         Order By w.EFFECT_DATE Descending
                         Select w).FirstOrDefault
                    If emp IsNot Nothing Then
                        objLeaveData.EMPLOYEE_ID = emp.EMPLOYEE_ID
                        objLeaveData.LEAVE_FROM = objLeave.LEAVE_FROM
                        objLeaveData.LEAVE_TO = objLeave.LEAVE_TO
                        objLeaveData.MANUAL_ID = objLeave.MANUAL_ID
                        objLeaveData.AFTERNOON_ID = objLeave.AFTERNOON_ID
                        objLeaveData.MORNING_ID = objLeave.MORNING_ID
                        objLeaveData.NOTE = objLeave.NOTE
                        fromdate = fromdate.Value.AddDays(1)
                        Context.AT_LEAVESHEET.AddObject(objLeaveData)
                    Else
                        fromdate = fromdate.Value.AddDays(1)
                    End If

                End While
            End If
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function InsertLeaveSheetList(ByVal objLeaveList As List(Of AT_LEAVESHEETDTO), ByVal objLeave As AT_LEAVESHEETDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim fromdate As Date?
        Dim fromdateNext As Date?
        Dim objDataList As New AT_LEAVESHEETDTO
        Try
            For iteam = 0 To objLeaveList.Count - 1
                objDataList = objLeaveList(iteam)

                Dim exists = (From r In Context.AT_LEAVESHEET Where r.EMPLOYEE_ID = objDataList.EMPLOYEE_ID And r.WORKINGDAY <= objLeave.LEAVE_TO And r.WORKINGDAY >= objLeave.LEAVE_FROM).Any
                If exists Then ' trường hợp nếu đã có bản ghi trùng thì xóa đi và cập nhật bản ghi mới nhất
                    Dim details = (From r In Context.AT_LEAVESHEET Where r.EMPLOYEE_ID = objDataList.EMPLOYEE_ID And r.WORKINGDAY <= objLeave.LEAVE_TO And r.WORKINGDAY >= objLeave.LEAVE_FROM).ToList
                    For index = 0 To details.Count - 1
                        Context.AT_LEAVESHEET.DeleteObject(details(index))
                    Next
                    Dim objLeaveData As AT_LEAVESHEET
                    'Dim emp = (From p In Context.HU_EMPLOYEE Where p.EMPLOYEE_CODE.ToLower().Contains(objLeave.EMPLOYEE_CODE.ToLower())).FirstOrDefault
                    fromdate = objLeave.LEAVE_FROM
                    fromdateNext = objLeave.LEAVE_FROM
                    While fromdateNext <= objLeave.LEAVE_TO
                        objLeaveData = New AT_LEAVESHEET
                        objLeaveData.ID = Utilities.GetNextSequence(Context, Context.AT_LEAVESHEET.EntitySet.Name)
                        objLeaveData.WORKINGDAY = fromdateNext
                        Dim emp = (From e In Context.HU_EMPLOYEE
                                     From w In Context.HU_WORKING.Where(Function(f) f.EMPLOYEE_ID = e.ID)
                                     Where e.EMPLOYEE_CODE = objDataList.EMPLOYEE_CODE And e.JOIN_DATE <= objLeaveData.WORKINGDAY And _
                                     (e.TER_EFFECT_DATE Is Nothing Or _
                                      (e.TER_EFFECT_DATE IsNot Nothing And _
                                       e.TER_EFFECT_DATE >= objLeaveData.WORKINGDAY)) And w.EFFECT_DATE <= objLeaveData.WORKINGDAY
                               Order By w.EFFECT_DATE Descending
                               Select w).FirstOrDefault
                        If emp IsNot Nothing Then
                            objLeaveData.EMPLOYEE_ID = emp.EMPLOYEE_ID
                            objLeaveData.LEAVE_FROM = objLeave.LEAVE_FROM
                            objLeaveData.LEAVE_TO = objLeave.LEAVE_TO
                            objLeaveData.MANUAL_ID = objLeave.MANUAL_ID
                            objLeaveData.AFTERNOON_ID = objLeave.AFTERNOON_ID
                            objLeaveData.MORNING_ID = objLeave.MORNING_ID
                            objLeaveData.NOTE = objLeave.NOTE
                            fromdateNext = fromdateNext.Value.AddDays(1)
                            Context.AT_LEAVESHEET.AddObject(objLeaveData)
                        Else
                            fromdateNext = fromdateNext.Value.AddDays(1)
                        End If
                    End While
                    Context.SaveChanges(log)
                Else
                    Dim objLeaveData As AT_LEAVESHEET
                    fromdate = objLeave.LEAVE_FROM
                    fromdateNext = objLeave.LEAVE_FROM
                    While fromdateNext <= objLeave.LEAVE_TO
                        objLeaveData = New AT_LEAVESHEET
                        objLeaveData.ID = Utilities.GetNextSequence(Context, Context.AT_LEAVESHEET.EntitySet.Name)
                        objLeaveData.WORKINGDAY = fromdateNext
                        Dim emp = (From e In Context.HU_EMPLOYEE
                                   From w In Context.HU_WORKING.Where(Function(f) f.EMPLOYEE_ID = e.ID)
                                   Where e.EMPLOYEE_CODE = objDataList.EMPLOYEE_CODE And e.JOIN_DATE <= objLeaveData.WORKINGDAY And _
                                   (e.TER_EFFECT_DATE Is Nothing Or _
                                    (e.TER_EFFECT_DATE IsNot Nothing And _
                                     e.TER_EFFECT_DATE >= objLeaveData.WORKINGDAY)) And w.EFFECT_DATE <= objLeaveData.WORKINGDAY
                             Order By w.EFFECT_DATE Descending
                             Select w).FirstOrDefault
                        If emp IsNot Nothing Then
                            objLeaveData.EMPLOYEE_ID = emp.EMPLOYEE_ID
                            objLeaveData.LEAVE_FROM = objLeave.LEAVE_FROM
                            objLeaveData.LEAVE_TO = objLeave.LEAVE_TO
                            objLeaveData.MANUAL_ID = objLeave.MANUAL_ID
                            objLeaveData.AFTERNOON_ID = objLeave.AFTERNOON_ID
                            objLeaveData.MORNING_ID = objLeave.MORNING_ID
                            objLeaveData.NOTE = objLeave.NOTE
                            fromdateNext = fromdateNext.Value.AddDays(1)
                            Context.AT_LEAVESHEET.AddObject(objLeaveData)
                        Else
                            fromdateNext = fromdateNext.Value.AddDays(1)
                        End If

                    End While
                End If
                Context.SaveChanges(log)
            Next
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ValidateLeaveSheet(ByVal _validate As AT_LEAVESHEETDTO)
        Dim query
        Try
            Dim dDay = _validate.LEAVE_FROM
            While dDay <= _validate.LEAVE_TO
                _validate.WORKINGDAY = dDay
                If _validate.WORKINGDAY.HasValue Then
                    If _validate.ID <> 0 Then
                        query = (From p In Context.AT_LEAVESHEET
                                 Where p.WORKINGDAY = _validate.WORKINGDAY And p.EMPLOYEE_ID = _validate.EMPLOYEE_ID And p.ID <> _validate.ID).Any
                    Else
                        query = (From p In Context.AT_LEAVESHEET
                                 Where p.WORKINGDAY = _validate.WORKINGDAY And p.EMPLOYEE_ID = _validate.EMPLOYEE_ID).Any
                    End If
                    If query Then
                        Return True
                    End If
                    dDay = dDay.Value.AddDays(1)
                End If
            End While
            Return False
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ModifyLeaveSheet(ByVal objLeave As AT_LEAVESHEETDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Try
            Dim leave = (From r In Context.AT_LEAVESHEET Where r.ID = objLeave.ID).FirstOrDefault
            'Dim details = (From r In Context.AT_LEAVESHEET Where r.LEAVE_FROM = leave.LEAVE_FROM And r.LEAVE_TO = leave.LEAVE_TO And r.EMPLOYEE_ID = leave.EMPLOYEE_ID).ToList
            Dim details = (From r In Context.AT_LEAVESHEET Where r.WORKINGDAY <= objLeave.LEAVE_TO And r.WORKINGDAY >= objLeave.LEAVE_FROM And r.EMPLOYEE_ID = leave.EMPLOYEE_ID).ToList
            For index = 0 To details.Count - 1
                Context.AT_LEAVESHEET.DeleteObject(details(index))
            Next

            Dim fromdate As Date?
            Dim objLeaveData As AT_LEAVESHEET
            fromdate = objLeave.LEAVE_FROM
            While fromdate <= objLeave.LEAVE_TO
                objLeaveData = New AT_LEAVESHEET
                objLeaveData.ID = Utilities.GetNextSequence(Context, Context.AT_LEAVESHEET.EntitySet.Name)
                objLeaveData.WORKINGDAY = fromdate
                Dim emp = (From e In Context.HU_EMPLOYEE
                           From w In Context.HU_WORKING.Where(Function(f) f.EMPLOYEE_ID = e.ID)
                           Where e.ID = objLeave.EMPLOYEE_ID And e.JOIN_DATE <= objLeaveData.WORKINGDAY And _
                           (e.TER_EFFECT_DATE Is Nothing Or _
                            (e.TER_EFFECT_DATE IsNot Nothing And _
                             e.TER_EFFECT_DATE >= objLeaveData.WORKINGDAY)) And w.EFFECT_DATE <= objLeaveData.WORKINGDAY
                     Order By w.EFFECT_DATE Descending
                     Select w).FirstOrDefault
                If emp IsNot Nothing Then
                    objLeaveData.EMPLOYEE_ID = emp.EMPLOYEE_ID
                    objLeaveData.LEAVE_FROM = objLeave.LEAVE_FROM
                    objLeaveData.LEAVE_TO = objLeave.LEAVE_TO
                    objLeaveData.MANUAL_ID = objLeave.MANUAL_ID
                    objLeaveData.AFTERNOON_ID = objLeave.AFTERNOON_ID
                    objLeaveData.MORNING_ID = objLeave.MORNING_ID
                    objLeaveData.NOTE = objLeave.NOTE
                    fromdate = fromdate.Value.AddDays(1)
                    Context.AT_LEAVESHEET.AddObject(objLeaveData)
                Else
                    fromdate = fromdate.Value.AddDays(1)
                End If

            End While
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

    End Function

    Public Function DeleteLeaveSheet(ByVal lstID As List(Of AT_LEAVESHEETDTO),
                                     ByVal _param As ParamDTO,
                                     ByVal period_id As Decimal,
                                     ByVal listEmployeeId As List(Of Decimal?),
                                     ByVal log As UserLog) As Boolean
        Dim lstl As AT_LEAVESHEET
        Dim id As Decimal = 0
        Try
            If listEmployeeId Is Nothing Then
                listEmployeeId = New List(Of Decimal?)
            End If
            Dim obj As New AT_ACTION_LOGDTO

            obj.PERIOD_ID = period_id
            For index = 0 To lstID.Count - 1
                id = lstID(index).ID
                lstl = (From p In Context.AT_LEAVESHEET Where id = p.ID).FirstOrDefault
                Dim details = (From r In Context.AT_LEAVESHEET Where r.LEAVE_FROM = lstl.LEAVE_FROM And r.LEAVE_TO = lstl.LEAVE_TO And r.EMPLOYEE_ID = lstl.EMPLOYEE_ID).ToList
                listEmployeeId.Add(lstl.EMPLOYEE_ID)
                For index1 = 0 To details.Count - 1
                    Context.AT_LEAVESHEET.DeleteObject(details(index1))
                Next
            Next

            Context.SaveChanges(log)
            LOG_AT(_param, log, listEmployeeId, "XÓA ĐĂNG KÝ NGHỈ", obj, _param.ORG_ID)

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function checkLeaveImport(ByVal dtData As DataTable) As DataTable
        Dim dtDataError As DataTable = dtData.Clone
        Dim dtDataSave As New DataTable
        Dim dtDataUserPHEPNAM As New DataTable
        Dim dtDataUserPHEPBU As New DataTable
        Dim totalPhep As Object
        Dim totalBu As Decimal = 0
        Dim rowError As DataRow
        Dim rowDataSave As DataRow
        Dim isError As Boolean = False
        Dim at_entilement As New AT_ENTITLEMENTDTO
        Dim at_compensatory As New AT_COMPENSATORYDTO
        Dim employee_id As Decimal?
        Dim org_id As Decimal?
        Dim irow = 8
        Try
            dtDataSave.Columns.Add("EMPLOYEE_CODE")
            dtDataSave.Columns.Add("MANUAL_ID")
            dtDataSave.Columns.Add("MORNING_ID")
            dtDataSave.Columns.Add("AFTERNOON_ID")
            dtDataSave.Columns.Add("TOTAL_DAY_ENT", GetType(Decimal)) ' tổng ngày nghỉ phép
            dtDataSave.Columns.Add("TOTAL_DAY_COM", GetType(Decimal)) ' tổng ngày nghỉ bù

            For Each row As DataRow In dtData.Rows
                Dim empCode As String = row("EMPLOYEE_CODE")
                Dim fromDate As Date = Date.Parse(row("LEAVE_FROM"))
                Dim toDate As Date = Date.Parse(row("LEAVE_TO"))
                Dim manualId As Decimal = Utilities.Obj2Decima(row("MANUAL_ID"))
                Dim monningId As Decimal = Utilities.Obj2Decima(row("MORNING_ID"))
                Dim afternoonId As Decimal = Utilities.Obj2Decima(row("AFTERNOON_ID"))
                Dim emp = (From e In Context.HU_EMPLOYEE
                               From w In Context.HU_WORKING.Where(Function(f) f.EMPLOYEE_ID = e.ID)
                               Where e.EMPLOYEE_CODE = empCode And e.JOIN_DATE <= fromDate And _
                               (e.TER_EFFECT_DATE Is Nothing Or _
                                (e.TER_EFFECT_DATE IsNot Nothing And _
                                 e.TER_EFFECT_DATE >= fromDate)) And w.EFFECT_DATE <= fromDate
                         Order By w.EFFECT_DATE Descending
                         Select w).FirstOrDefault
                If emp IsNot Nothing Then
                    employee_id = emp.EMPLOYEE_ID
                    org_id = emp.ORG_ID
                Else
                    Exit For
                End If

                ' tính tổng số ngày nghỉ của 1 nv trừ thứ 7 và chủ nhật.
                rowDataSave = dtDataSave.NewRow
                rowDataSave("EMPLOYEE_CODE") = empCode
                rowDataSave("MANUAL_ID") = manualId
                rowDataSave("MORNING_ID") = monningId
                rowDataSave("AFTERNOON_ID") = afternoonId
                rowDataSave("TOTAL_DAY_ENT") = If(monningId = afternoonId, GetTotalDAY(employee_id, 251, fromDate, toDate).Rows(0)(0).ToString, Utilities.Obj2Decima(GetTotalDAY(employee_id, 251, fromDate, toDate).Rows(0)(0) / 2))
                rowDataSave("TOTAL_DAY_COM") = If(monningId = afternoonId, GetTotalDAY(employee_id, 255, fromDate, toDate).Rows(0)(0).ToString, Utilities.Obj2Decima(GetTotalDAY(employee_id, 255, fromDate, toDate).Rows(0)(0) / 2))
                dtDataSave.Rows.Add(rowDataSave)


                ' khởi tạo dòng trong datatable lỗi.
                rowError = dtDataError.NewRow
                '1. lấy phép năm đã đăng ký.
                dtDataUserPHEPNAM = GetTotalPHEPNAM(employee_id, Date.Parse(row("LEAVE_FROM")), Utilities.Obj2Decima(row("MANUAL_ID")))
                '2. lấy phép năm được phép nghỉ trong năm.
                at_entilement = GetPhepNam(employee_id, Date.Parse(row("LEAVE_FROM")).Year)
                '3 phép bù được phép nghỉ trong năm.
                at_compensatory = GetNghiBu(employee_id, Date.Parse(row("LEAVE_FROM")).Year)
                '4. tổng số ngày đăng ký của nhân viên trên file import.
                'totalDayRes = GetTotalDAY(employee_id, 251, Date.Parse(row("LEAVE_FROM")), Date.Parse(row("LEAVE_TO")))
                totalPhep = dtDataSave.Compute("SUM(TOTAL_DAY_ENT)", "EMPLOYEE_CODE = " & empCode & " AND (MORNING_ID = 251 OR AFTERNOON_ID = 251)")

                ' nếu là kiểu đăng ký nghỉ phép
                If Utilities.Obj2Decima(row("MORNING_ID")) = 251 Or Utilities.Obj2Decima(row("AFTERNOON_ID")) = 251 Then
                    If dtDataUserPHEPNAM IsNot Nothing And at_entilement IsNot Nothing Then
                        If at_entilement.TOTAL_HAVE.Value - (dtDataUserPHEPNAM.Rows(0)(0) + totalPhep) < -3 Then
                            rowError("MANUAL_NAME") = "Tổng số ngày nghỉ phép của bạn trong năm nay đã vượt quá mức cho phép."
                            isError = True
                        End If
                    End If
                    'ElseIf Utilities.Obj2Decima(row("MORNING_ID")) = 251 Or Utilities.Obj2Decima(row("AFTERNOON_ID")) = 251 Then
                    '    If dtDataUserPHEPNAM IsNot Nothing And at_entilement IsNot Nothing Then
                    '        If at_entilement.TOTAL_HAVE.Value - (dtDataUserPHEPNAM.Rows(0)(0) + totalPhep / 2) < -3 Then
                    '            rowError("MANUAL_NAME") = "Tổng số ngày nghỉ phép của bạn trong năm nay đã vượt quá mức cho phép."
                    '            isError = True
                    '        End If
                    '    End If
                End If
                ' nếu là kiểu đăng ký nghỉ bù
                dtDataUserPHEPBU = GetTotalPHEPBU(employee_id, Date.Parse(row("LEAVE_FROM")), Utilities.Obj2Decima(row("MANUAL_ID")))
                'totalDayRes = GetTotalDAY(employee_id, 255, Date.Parse(row("LEAVE_FROM")), Date.Parse(row("LEAVE_TO")))
                totalBu = Utilities.Obj2Decima(dtDataSave.Compute("SUM(TOTAL_DAY_COM)", "EMPLOYEE_CODE = " & empCode & " AND (MORNING_ID = 255 OR AFTERNOON_ID = 255)"))

                If Utilities.Obj2Decima(row("MORNING_ID")) = 255 Or Utilities.Obj2Decima(row("AFTERNOON_ID")) = 255 Then
                    If dtDataUserPHEPBU IsNot Nothing And at_compensatory IsNot Nothing Then
                        If at_compensatory.TOTAL_HAVE.Value - (dtDataUserPHEPBU.Rows(0)(0) + totalBu) < 0 Then
                            rowError("MANUAL_NAME") = "Tổng số ngày nghỉ bù của bạn đã vượt quá mức cho phép."
                            isError = True
                        End If
                    End If
                    'ElseIf Utilities.Obj2Decima(row("MORNING_ID")) = 255 Or Utilities.Obj2Decima(row("AFTERNOON_ID")) = 255 Then
                    '    If dtDataUserPHEPBU IsNot Nothing And at_compensatory IsNot Nothing Then
                    '        If at_compensatory.TOTAL_HAVE.Value - (dtDataUserPHEPBU.Rows(0)(0) + totalBu / 2) < 0 Then
                    '            rowError("MANUAL_NAME") = "Tổng số ngày nghỉ bù của bạn đã vượt quá mức cho phép."
                    '            isError = True
                    '        End If
                    '    End If
                End If
                If isError Then
                    rowError("STT") = irow
                    If rowError("EMPLOYEE_CODE").ToString = "" Then
                        rowError("EMPLOYEE_CODE") = row("EMPLOYEE_CODE").ToString
                    End If
                    rowError("VN_FULLNAME") = row("VN_FULLNAME").ToString
                    rowError("ORG_NAME") = row("ORG_NAME").ToString
                    rowError("ORG_PATH") = row("ORG_PATH").ToString
                    rowError("TITLE_NAME") = row("TITLE_NAME").ToString
                    dtDataError.Rows.Add(rowError)
                End If
                irow = irow + 1
                isError = False
            Next
            Return dtDataError
        Catch ex As Exception
            Throw ex
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
        End Try
    End Function

#End Region

#Region "NGHI BU"
    Public Function CALCULATE_ENTITLEMENT_NB(ByVal param As ParamDTO, ByVal listEmployeeId As List(Of Decimal?), ByVal log As UserLog) As Boolean
        Try
            Dim obj As New AT_ACTION_LOGDTO
            obj.PERIOD_ID = param.PERIOD_ID
            Using cls As New DataAccess.NonQueryData
                cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.CALL_ENTITLEMENT_NB",
                                               New With {.P_USERNAME = log.Username.ToUpper,
                                                         .P_ORG_ID = param.ORG_ID,
                                                         .P_PERIOD = param.PERIOD_ID,
                                                         .P_ISDISSOLVE = param.IS_DISSOLVE})
            End Using
            LOG_AT(param, log, listEmployeeId, "TỔNG HỢP NGHỈ BÙ", obj, param.ORG_ID)
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex

        End Try
    End Function

    Public Function GetNB(ByVal _filter As AT_COMPENSATORYDTO,
                                      ByVal _param As ParamDTO,
                                      Optional ByRef Total As Integer = 0,
                                      Optional ByVal PageIndex As Integer = 0,
                                      Optional ByVal PageSize As Integer = Integer.MaxValue,
                                      Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_COMPENSATORYDTO)
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username.ToUpper,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using
            Dim query = From ot In Context.AT_COMPENSATORY
                        From period In Context.AT_PERIOD.Where(Function(f) f.ID = ot.PERIOD_ID)
                        From E In Context.HU_EMPLOYEE.Where(Function(f) f.ID = ot.EMPLOYEE_ID)
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = E.ORG_ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = E.TITLE_ID).DefaultIfEmpty()
                        From c In Context.HU_STAFF_RANK.Where(Function(f) f.ID = E.STAFF_RANK_ID).DefaultIfEmpty()
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) E.ORG_ID = f.ORG_ID And
                                                                  f.USERNAME.ToUpper = log.Username.ToUpper)
                        Where ot.YEAR = _filter.YEAR

            Dim dateStart = _filter.END_DATE
            If Not _filter.ISTEMINAL Then
                query = query.Where(Function(f) f.E.WORK_STATUS Is Nothing Or f.E.WORK_STATUS <> 257 Or
                                    (f.E.WORK_STATUS = 257 And f.E.TER_LAST_DATE >= dateStart))
            End If

            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                query = query.Where(Function(f) f.E.EMPLOYEE_CODE.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.FULLNAME_VN) Then
                query = query.Where(Function(f) f.E.FULLNAME_VN.ToLower().Contains(_filter.FULLNAME_VN.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                query = query.Where(Function(f) f.o.NAME_VN.ToLower().Contains(_filter.ORG_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TITLE_NAME_VN) Then
                query = query.Where(Function(f) f.t.NAME_VN.ToLower().Contains(_filter.TITLE_NAME_VN.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.STAFF_RANK_NAME) Then
                query = query.Where(Function(f) f.c.NAME.ToLower().Contains(_filter.STAFF_RANK_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.PERIOD_NAME) Then
                query = query.Where(Function(f) f.period.PERIOD_NAME.ToLower().Contains(_filter.PERIOD_NAME.ToLower()))
            End If
            If _filter.CUR_USED1.HasValue Then
                query = query.Where(Function(f) f.ot.CUR_USED1 = _filter.CUR_USED1)
            End If
            If _filter.CUR_USED2.HasValue Then
                query = query.Where(Function(f) f.ot.CUR_USED2 = _filter.CUR_USED2)
            End If
            If _filter.CUR_USED3.HasValue Then
                query = query.Where(Function(f) f.ot.CUR_USED3 = _filter.CUR_USED3)
            End If
            If _filter.CUR_USED4.HasValue Then
                query = query.Where(Function(f) f.ot.CUR_USED4 = _filter.CUR_USED4)
            End If
            If _filter.CUR_USED5.HasValue Then
                query = query.Where(Function(f) f.ot.CUR_USED5 = _filter.CUR_USED5)
            End If
            If _filter.CUR_USED6.HasValue Then
                query = query.Where(Function(f) f.ot.CUR_USED6 = _filter.CUR_USED6)
            End If
            If _filter.CUR_USED7.HasValue Then
                query = query.Where(Function(f) f.ot.CUR_USED7 = _filter.CUR_USED7)
            End If
            If _filter.CUR_USED8.HasValue Then
                query = query.Where(Function(f) f.ot.CUR_USED8 = _filter.CUR_USED8)
            End If
            If _filter.CUR_USED9.HasValue Then
                query = query.Where(Function(f) f.ot.CUR_USED9 = _filter.CUR_USED9)
            End If
            If _filter.CUR_USED10.HasValue Then
                query = query.Where(Function(f) f.ot.CUR_USED10 = _filter.CUR_USED10)
            End If
            If _filter.CUR_USED11.HasValue Then
                query = query.Where(Function(f) f.ot.CUR_USED11 = _filter.CUR_USED11)
            End If
            If _filter.CUR_USED12.HasValue Then
                query = query.Where(Function(f) f.ot.CUR_USED12 = _filter.CUR_USED12)
            End If
            If _filter.CUR_HAVE.HasValue Then
                query = query.Where(Function(f) f.ot.CUR_HAVE = _filter.CUR_HAVE)
            End If
            If _filter.AL_T1.HasValue Then
                query = query.Where(Function(f) f.ot.AL_T1 = _filter.AL_T1)
            End If
            If _filter.AL_T2.HasValue Then
                query = query.Where(Function(f) f.ot.AL_T2 = _filter.AL_T2)
            End If
            If _filter.AL_T3.HasValue Then
                query = query.Where(Function(f) f.ot.AL_T3 = _filter.AL_T3)
            End If
            If _filter.AL_T4.HasValue Then
                query = query.Where(Function(f) f.ot.AL_T4 = _filter.AL_T4)
            End If
            If _filter.AL_T5.HasValue Then
                query = query.Where(Function(f) f.ot.AL_T5 = _filter.AL_T5)
            End If
            If _filter.AL_T6.HasValue Then
                query = query.Where(Function(f) f.ot.AL_T6 = _filter.AL_T6)
            End If
            If _filter.AL_T7.HasValue Then
                query = query.Where(Function(f) f.ot.AL_T7 = _filter.AL_T7)
            End If
            If _filter.AL_T8.HasValue Then
                query = query.Where(Function(f) f.ot.AL_T8 = _filter.AL_T8)
            End If
            If _filter.AL_T9.HasValue Then
                query = query.Where(Function(f) f.ot.AL_T9 = _filter.AL_T9)
            End If
            If _filter.AL_T10.HasValue Then
                query = query.Where(Function(f) f.ot.AL_T10 = _filter.AL_T10)
            End If
            If _filter.AL_T11.HasValue Then
                query = query.Where(Function(f) f.ot.AL_T11 = _filter.AL_T11)
            End If
            If _filter.AL_T12.HasValue Then
                query = query.Where(Function(f) f.ot.AL_T12 = _filter.AL_T12)
            End If
            If _filter.CUR_HAVE.HasValue Then
                query = query.Where(Function(f) f.ot.CUR_HAVE = _filter.CUR_HAVE)
            End If
            If _filter.CUR_USED.HasValue Then
                query = query.Where(Function(f) f.ot.CUR_USED = _filter.CUR_USED)
            End If
            If _filter.TOTAL_CUR_HAVE.HasValue Then
                query = query.Where(Function(f) f.ot.TOTAL_HAVE = _filter.TOTAL_CUR_HAVE)
            End If

            Dim lst = query.Select(Function(p) New AT_COMPENSATORYDTO With {
                                       .ID = p.ot.ID,
                                       .EMPLOYEE_ID = p.ot.EMPLOYEE_ID,
                                       .FULLNAME_VN = p.E.FULLNAME_VN,
                                       .EMPLOYEE_CODE = p.E.EMPLOYEE_CODE,
                                       .ORG_NAME = p.o.NAME_VN,
                                       .ORG_DESC = p.o.DESCRIPTION_PATH,
                                       .TITLE_NAME_VN = p.t.NAME_VN,
                                       .STAFF_RANK_NAME = p.c.NAME,
                                       .PERIOD_NAME = p.period.PERIOD_NAME,
                                       .AL_T1 = p.ot.AL_T1,
                                       .AL_T2 = p.ot.AL_T2,
                                       .AL_T3 = p.ot.AL_T3,
                                       .AL_T4 = p.ot.AL_T4,
                                       .AL_T5 = p.ot.AL_T5,
                                       .AL_T6 = p.ot.AL_T6,
                                       .AL_T7 = p.ot.AL_T7,
                                       .AL_T8 = p.ot.AL_T8,
                                       .AL_T9 = p.ot.AL_T9,
                                       .AL_T10 = p.ot.AL_T10,
                                       .AL_T11 = p.ot.AL_T11,
                                       .AL_T12 = p.ot.AL_T12,
                                       .PREV_HAVE = p.ot.PREV_HAVE,
                                       .CUR_USED1 = p.ot.CUR_USED1 + p.ot.PREV_USED1,
                                       .CUR_USED2 = p.ot.CUR_USED2 + p.ot.PREV_USED2,
                                       .CUR_USED3 = p.ot.CUR_USED3 + p.ot.PREV_USED3,
                                       .CUR_USED4 = p.ot.CUR_USED4 + p.ot.PREV_USED4,
                                       .CUR_USED5 = p.ot.CUR_USED5 + p.ot.PREV_USED5,
                                       .CUR_USED6 = p.ot.CUR_USED6 + p.ot.PREV_USED6,
                                       .CUR_USED7 = p.ot.CUR_USED7 + p.ot.PREV_USED7,
                                       .CUR_USED8 = p.ot.CUR_USED8 + p.ot.PREV_USED8,
                                       .CUR_USED9 = p.ot.CUR_USED9 + p.ot.PREV_USED9,
                                       .CUR_USED10 = p.ot.CUR_USED10 + p.ot.PREV_USED10,
                                       .CUR_USED11 = p.ot.CUR_USED11 + p.ot.PREV_USED11,
                                       .CUR_USED12 = p.ot.CUR_USED12 + p.ot.PREV_USED12,
                                       .CUR_HAVE = p.ot.CUR_HAVE,
                                       .CUR_USED = p.ot.CUR_USED,
                                       .TOTAL_CUR_HAVE = p.ot.TOTAL_HAVE,
                                       .CREATED_DATE = p.ot.CREATED_DATE})

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
#End Region

#Region "Quan ly vao ra"
    Public Function GetDataInout(ByVal _filter As AT_DATAINOUTDTO,
                                      ByVal _param As ParamDTO,
                                      Optional ByRef Total As Integer = 0,
                                      Optional ByVal PageIndex As Integer = 0,
                                      Optional ByVal PageSize As Integer = Integer.MaxValue,
                                      Optional ByVal Sorts As String = "EMPLOYEE_CODE, WORKINGDAY", Optional ByVal log As UserLog = Nothing) As List(Of AT_DATAINOUTDTO)
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username.ToUpper,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using
            Dim query = From p In Context.AT_DATA_INOUT
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) e.ORG_ID = f.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper)
                        From s In Context.HU_STAFF_RANK.Where(Function(f) e.STAFF_RANK_ID = f.ID).DefaultIfEmpty

            If _filter.IS_TERMINATE Then
                query = query.Where(Function(f) f.e.WORK_STATUS = 257)
                If _filter.END_DATE.HasValue Then
                    query = query.Where(Function(f) f.e.TER_LAST_DATE <= _filter.END_DATE)
                End If
            End If
            If _filter.FROM_DATE.HasValue Then
                query = query.Where(Function(f) f.p.WORKINGDAY >= _filter.FROM_DATE)
            End If
            If _filter.END_DATE.HasValue Then
                query = query.Where(Function(f) f.p.WORKINGDAY <= _filter.END_DATE)
            End If
            If _filter.WORKINGDAY.HasValue Then
                query = query.Where(Function(f) f.p.WORKINGDAY = _filter.WORKINGDAY)
            End If
            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                query = query.Where(Function(f) f.e.EMPLOYEE_CODE.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.VN_FULLNAME) Then
                query = query.Where(Function(f) f.e.FULLNAME_VN.ToLower().Contains(_filter.VN_FULLNAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TITLE_NAME) Then
                query = query.Where(Function(f) f.t.NAME_VN.ToLower().Contains(_filter.TITLE_NAME.ToLower()))
            End If

            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                query = query.Where(Function(f) f.o.NAME_VN.ToLower().Contains(_filter.ORG_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.STAFF_RANK_NAME) Then
                query = query.Where(Function(f) f.s.NAME.ToLower().Contains(_filter.STAFF_RANK_NAME.ToLower()))
            End If
            Dim lst = query.Select(Function(p) New AT_DATAINOUTDTO With {
                                       .ID = p.p.ID,
                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                       .VN_FULLNAME = p.e.FULLNAME_VN,
                                       .TITLE_NAME = p.t.NAME_VN,
                                       .STAFF_RANK_ID = p.e.STAFF_RANK_ID,
                                       .STAFF_RANK_NAME = p.s.NAME,
                                       .ORG_NAME = p.o.NAME_VN,
                                       .ORG_DESC = p.o.DESCRIPTION_PATH,
                                       .WORKINGDAY = p.p.WORKINGDAY,
                                       .VALIN1 = p.p.VALIN1,
                                       .VALIN2 = p.p.VALIN2,
                                       .VALIN3 = p.p.VALIN3,
                                       .VALIN4 = p.p.VALIN4,
                                       .VALIN5 = p.p.VALIN5,
                                       .VALIN6 = p.p.VALIN6,
                                       .VALIN7 = p.p.VALIN7,
                                       .VALIN8 = p.p.VALIN8,
                                       .VALIN9 = p.p.VALIN9,
                                       .VALIN10 = p.p.VALIN10,
                                       .VALIN11 = p.p.VALIN11,
                                       .VALIN12 = p.p.VALIN12,
                                       .VALIN13 = p.p.VALIN13,
                                       .VALIN14 = p.p.VALIN14,
                                       .VALIN15 = p.p.VALIN15,
                                       .VALIN16 = p.p.VALIN16,
                                       .CREATED_DATE = p.p.CREATED_DATE})
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function GetiTimeByEmployeeCode(ByVal objid As Decimal?) As Decimal?
        Try
            Return (From e In Context.HU_EMPLOYEE
                    Where e.ID = objid
                    Select e.ITIME_ID).FirstOrDefault
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
        End Try
    End Function

    Public Function InsertDataInout(ByVal lstDataInout As List(Of AT_DATAINOUTDTO), ByVal fromDate As Date, ByVal toDate As Date,
                                    ByVal log As UserLog) As Boolean
        Dim objDataInoutData As New AT_DATA_INOUT
        Try
            'THEM DU LIEU VAO 
            Dim itime = GetiTimeByEmployeeCode(lstDataInout(0).EMPLOYEE_ID)
            If itime IsNot Nothing OrElse itime <> 0 Then
                Using conMng As New ConnectionManager
                    Using conn As New OracleConnection(conMng.GetConnectionString())
                        Using cmd As New OracleCommand()
                            Try
                                conn.Open()
                                cmd.Connection = conn
                                cmd.CommandType = CommandType.StoredProcedure
                                cmd.CommandText = "PKG_ATTENDANCE_BUSINESS.INSERT_TIME_CARD"
                                cmd.Transaction = cmd.Connection.BeginTransaction()
                                For Each objDataInout In lstDataInout
                                    cmd.Parameters.Clear()
                                    Using resource As New DataAccess.OracleCommon()
                                        Dim objParam = New With {.P_TIMEID = itime,
                                                                 .P_VALTIME = objDataInout.VALIN1,
                                                                 .P_USERNAME = log.Username.ToUpper}

                                        If objParam IsNot Nothing Then
                                            For Each info As PropertyInfo In objParam.GetType().GetProperties()
                                                Dim bOut As Boolean = False
                                                Dim para = resource.GetParameter(info.Name, info.GetValue(objParam, Nothing), bOut)
                                                If para IsNot Nothing Then
                                                    cmd.Parameters.Add(para)
                                                End If
                                            Next
                                        End If
                                        cmd.ExecuteNonQuery()
                                    End Using
                                Next
                                cmd.Transaction.Commit()
                            Catch ex As Exception
                                cmd.Transaction.Rollback()
                            Finally
                                'Dispose all resource
                                cmd.Dispose()
                                conn.Close()
                                conn.Dispose()
                            End Try
                        End Using
                    End Using
                End Using

                ' UPDATE DATA TO AT_DATAINOUTDTO
                Using cls As New DataAccess.QueryData
                    cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.UPDATE_DATAINOUT",
                                                   New With {.P_ITIMEID = itime,
                                                             .P_USERNAME = log.Username.ToUpper,
                                                             .P_FROMDATE = fromDate,
                                                             .P_ENDDATE = toDate})
                End Using
            End If

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ModifyDataInout(ByVal objDataInout As AT_DATAINOUTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objDataInoutData As New AT_DATA_INOUT With {.ID = objDataInout.ID}
        Try
            Context.AT_DATA_INOUT.Attach(objDataInoutData)

            Context.SaveChanges(log)
            gID = objDataInoutData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

    End Function

    Public Function DeleteDataInout(ByVal lstDataInout() As AT_DATAINOUTDTO) As Boolean
        Dim lstDataInoutData As List(Of AT_DATA_INOUT)
        Dim lstIDDataInout As List(Of Decimal?) = (From p In lstDataInout.ToList Select p.ID).ToList
        Dim empId As Decimal?
        Dim iTime As Decimal?
        Try
            ' xoa du lieu ben bang tai du lieu may cham cong
            lstDataInoutData = (From p In Context.AT_DATA_INOUT Where lstIDDataInout.Contains(p.ID)).ToList
            empId = (From p In Context.AT_DATA_INOUT Where lstIDDataInout.Contains(p.ID)).FirstOrDefault.EMPLOYEE_ID
            ' lay ma quet the
            iTime = GetiTimeByEmployeeCode(empId)

            Dim swipe = (From p In Context.AT_SWIPE_DATA.Where(Function(f) f.ITIME_ID = iTime)).ToList
            For index = 0 To swipe.Count - 1
                Context.AT_SWIPE_DATA.DeleteObject(swipe(index))
            Next

            For index = 0 To lstDataInoutData.Count - 1
                Context.AT_DATA_INOUT.DeleteObject(lstDataInoutData(index))
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

    End Function

    Public Function DeleteDataInoutById(ByVal id As Decimal?) As Boolean
        Dim lstDataInoutData As AT_DATA_INOUT
        Try
            lstDataInoutData = (From p In Context.AT_DATA_INOUT Where p.ID = id).FirstOrDefault
            Context.AT_DATA_INOUT.DeleteObject(lstDataInoutData)
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

    End Function

#End Region

#Region "PHEP NAM"
    Public Function CALCULATE_ENTITLEMENT(ByVal param As ParamDTO, ByVal listEmployeeId As List(Of Decimal?), ByVal log As UserLog) As Boolean
        Try
            Dim obj As New AT_ACTION_LOGDTO
            obj.PERIOD_ID = param.PERIOD_ID
            Using cls As New DataAccess.NonQueryData
                cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.CALL_ENTITLEMENT",
                                               New With {.P_USERNAME = log.Username.ToUpper,
                                                         .P_ORG_ID = param.ORG_ID,
                                                         .P_PERIOD_ID = param.PERIOD_ID,
                                                         .P_ISDISSOLVE = param.IS_DISSOLVE})
            End Using

            LOG_AT(param, log, listEmployeeId, "TỔNG HỢP NGHỈ PHÉP", obj, param.ORG_ID)

            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex

        End Try
    End Function

    Public Function GetEntitlement(ByVal _filter As AT_ENTITLEMENTDTO,
                                  ByVal _param As ParamDTO,
                                      Optional ByRef Total As Integer = 0,
                                      Optional ByVal PageIndex As Integer = 0,
                                      Optional ByVal PageSize As Integer = Integer.MaxValue,
                                      Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_ENTITLEMENTDTO)
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username.ToUpper,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using
            Dim query = From en In Context.AT_ENTITLEMENT
                        From E In Context.HU_EMPLOYEE.Where(Function(f) f.ID = en.EMPLOYEE_ID)
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = E.ORG_ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = E.TITLE_ID).DefaultIfEmpty
                        From c In Context.HU_STAFF_RANK.Where(Function(f) f.ID = E.STAFF_RANK_ID).DefaultIfEmpty
                        From p In Context.AT_PERIOD.Where(Function(f) f.ID = en.PERIOD_ID).DefaultIfEmpty
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) E.ORG_ID = f.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper)
                        Where en.YEAR = _filter.YEAR

            Dim dateStart = _filter.END_DATE
            If Not _filter.IS_TERMINATE Then
                query = query.Where(Function(f) f.E.WORK_STATUS Is Nothing Or f.E.WORK_STATUS <> 257 Or
                                  (f.E.WORK_STATUS = 257 And f.E.TER_LAST_DATE >= dateStart))
            End If

            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                query = query.Where(Function(f) f.E.EMPLOYEE_CODE.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.FULLNAME_VN) Then
                query = query.Where(Function(f) f.E.FULLNAME_VN.ToLower().Contains(_filter.FULLNAME_VN.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TITLE_NAME_VN) Then
                query = query.Where(Function(f) f.t.NAME_VN.ToLower().Contains(_filter.TITLE_NAME_VN.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                query = query.Where(Function(f) f.o.NAME_VN.ToLower().Contains(_filter.ORG_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.STAFF_RANK_NAME) Then
                query = query.Where(Function(f) f.c.NAME.ToLower().Contains(_filter.STAFF_RANK_NAME.ToLower()))
            End If
            If _filter.JOIN_DATE_STATE.HasValue Then
                query = query.Where(Function(f) f.E.JOIN_DATE_STATE = _filter.JOIN_DATE_STATE)
            End If
            If Not String.IsNullOrEmpty(_filter.PERIOD_NAME) Then
                query = query.Where(Function(f) f.p.PERIOD_NAME.ToLower().Contains(_filter.PERIOD_NAME.ToLower()))
            End If
            If _filter.WORKING_TIME_HAVE.HasValue Then
                query = query.Where(Function(f) f.en.WORKING_TIME_HAVE = _filter.WORKING_TIME_HAVE)
            End If
            If _filter.BALANCE_WORKING_TIME.HasValue Then
                query = query.Where(Function(f) f.en.BALANCE_WORKING_TIME = _filter.BALANCE_WORKING_TIME)
            End If
            If _filter.PREV_HAVE.HasValue Then
                query = query.Where(Function(f) f.en.PREV_HAVE = _filter.PREV_HAVE)
            End If
            If _filter.EXPIREDATE.HasValue Then
                query = query.Where(Function(f) f.en.EXPIREDATE = _filter.EXPIREDATE)
            End If
            If _filter.CUR_USED1.HasValue Then
                query = query.Where(Function(f) f.en.CUR_USED1 = _filter.CUR_USED1)
            End If
            If _filter.CUR_USED2.HasValue Then
                query = query.Where(Function(f) f.en.CUR_USED2 = _filter.CUR_USED2)
            End If
            If _filter.CUR_USED3.HasValue Then
                query = query.Where(Function(f) f.en.CUR_USED3 = _filter.CUR_USED3)
            End If
            If _filter.CUR_USED4.HasValue Then
                query = query.Where(Function(f) f.en.CUR_USED4 = _filter.CUR_USED4)
            End If
            If _filter.CUR_USED5.HasValue Then
                query = query.Where(Function(f) f.en.CUR_USED5 = _filter.CUR_USED5)
            End If
            If _filter.CUR_USED6.HasValue Then
                query = query.Where(Function(f) f.en.CUR_USED6 = _filter.CUR_USED6)
            End If
            If _filter.CUR_USED7.HasValue Then
                query = query.Where(Function(f) f.en.CUR_USED7 = _filter.CUR_USED7)
            End If
            If _filter.CUR_USED8.HasValue Then
                query = query.Where(Function(f) f.en.CUR_USED8 = _filter.CUR_USED8)
            End If
            If _filter.CUR_USED9.HasValue Then
                query = query.Where(Function(f) f.en.CUR_USED9 = _filter.CUR_USED9)
            End If
            If _filter.CUR_USED10.HasValue Then
                query = query.Where(Function(f) f.en.CUR_USED10 = _filter.CUR_USED10)
            End If
            If _filter.CUR_USED11.HasValue Then
                query = query.Where(Function(f) f.en.CUR_USED11 = _filter.CUR_USED11)
            End If
            If _filter.CUR_USED12.HasValue Then
                query = query.Where(Function(f) f.en.CUR_USED12 = _filter.CUR_USED12)
            End If
            If _filter.CUR_USED.HasValue Then
                query = query.Where(Function(f) f.en.CUR_USED = _filter.CUR_USED)
            End If
            If _filter.CUR_HAVE.HasValue Then
                query = query.Where(Function(f) f.en.CUR_HAVE = _filter.CUR_HAVE)
            End If
            If _filter.TOTAL_CUR_HAVE.HasValue Then
                query = query.Where(Function(f) f.en.TOTAL_HAVE = _filter.TOTAL_CUR_HAVE)
            End If

            Dim lst = query.Select(Function(p) New AT_ENTITLEMENTDTO With {
                                       .ID = p.en.ID,
                                       .EMPLOYEE_ID = p.en.EMPLOYEE_ID,
                                       .FULLNAME_VN = p.E.FULLNAME_VN,
                                       .EMPLOYEE_CODE = p.E.EMPLOYEE_CODE,
                                       .TITLE_NAME_VN = p.t.NAME_VN,
                                       .STAFF_RANK_NAME = p.c.NAME,
                                       .ORG_NAME = p.o.NAME_VN,
                                       .ORG_DESC = p.o.DESCRIPTION_PATH,
                                       .JOIN_DATE_STATE = p.E.JOIN_DATE_STATE,
                                       .PERIOD_NAME = p.p.PERIOD_NAME,
                                       .WORKING_TIME_HAVE = p.en.WORKING_TIME_HAVE,
                                       .PREV_HAVE = p.en.PREV_HAVE,
                                       .EXPIREDATE = p.en.EXPIREDATE,
                                       .BALANCE_WORKING_TIME = p.en.BALANCE_WORKING_TIME,
                                       .TOTAL_CUR_HAVE = p.en.TOTAL_HAVE,
                                       .CUR_USED1 = p.en.CUR_USED1 + p.en.PREV_USED1,
                                       .CUR_USED2 = p.en.CUR_USED2 + p.en.PREV_USED2,
                                       .CUR_USED3 = p.en.CUR_USED3 + p.en.PREV_USED3,
                                       .CUR_USED4 = p.en.CUR_USED4 + p.en.PREV_USED4,
                                       .CUR_USED5 = p.en.CUR_USED5 + p.en.PREV_USED5,
                                       .CUR_USED6 = p.en.CUR_USED6 + p.en.PREV_USED6,
                                       .CUR_USED7 = p.en.CUR_USED7 + p.en.PREV_USED7,
                                       .CUR_USED8 = p.en.CUR_USED8 + p.en.PREV_USED8,
                                       .CUR_USED9 = p.en.CUR_USED9 + p.en.PREV_USED9,
                                       .CUR_USED10 = p.en.CUR_USED10 + p.en.PREV_USED10,
                                       .CUR_USED11 = p.en.CUR_USED11 + p.en.PREV_USED11,
                                       .CUR_USED12 = p.en.CUR_USED12 + p.en.PREV_USED12,
                                       .CUR_USED = p.en.CUR_USED,
                                       .CUR_HAVE = p.en.CUR_HAVE,
                                       .CUR_HAVE1 = p.en.CUR_HAVE1,
                                       .CUR_HAVE2 = p.en.CUR_HAVE2,
                                       .CUR_HAVE3 = p.en.CUR_HAVE3,
                                       .CUR_HAVE4 = p.en.CUR_HAVE4,
                                       .CUR_HAVE5 = p.en.CUR_HAVE5,
                                       .CUR_HAVE6 = p.en.CUR_HAVE6,
                                       .CUR_HAVE7 = p.en.CUR_HAVE7,
                                       .CUR_HAVE8 = p.en.CUR_HAVE8,
                                       .CUR_HAVE9 = p.en.CUR_HAVE9,
                                       .CUR_HAVE10 = p.en.CUR_HAVE10,
                                       .CUR_HAVE11 = p.en.CUR_HAVE11,
                                       .CUR_HAVE12 = p.en.CUR_HAVE12,
                                       .CREATED_DATE = p.en.CREATED_DATE,
                                       .CREATED_BY = p.en.CREATED_BY})
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
#End Region

#Region "WorkSign"
    Public Function GET_WORKSIGN(ByVal param As AT_WORKSIGNDTO, ByVal log As UserLog) As DataSet
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataSet = cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.GET_WORKSIGN",
                                               New With {.P_USERNAME = log.Username.ToUpper,
                                                         .P_ORG_ID = param.ORG_ID,
                                                         .P_ISDISSOLVE = param.IS_DISSOLVE,
                                                         .P_PAGE_INDEX = param.PAGE_INDEX,
                                                         .P_EMPLOYEE_CODE = param.EMPLOYEE_CODE,
                                                         .P_PAGE_SIZE = param.PAGE_SIZE,
                                                         .P_PERIOD_ID = param.PERIOD_ID,
                                                         .P_CUR = cls.OUT_CURSOR,
                                                         .P_CURCOUNT = cls.OUT_CURSOR}, False)
                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
        Return Nothing
    End Function

    Public Function InsertWorkSignByImport(ByVal dtData As DataTable,
                                           ByVal period_id As Decimal,
                                           ByVal log As UserLog, ByRef lstEmp As String) As Boolean
        Try
            Dim Period = (From w In Context.AT_PERIOD Where w.ID = period_id).FirstOrDefault

            Using conMng As New ConnectionManager
                Using conn As New OracleConnection(conMng.GetConnectionString())
                    Using cmd As New OracleCommand()
                        Using resource As New DataAccess.OracleCommon()
                            Try
                                conn.Open()
                                cmd.Connection = conn
                                cmd.Transaction = cmd.Connection.BeginTransaction()
                                cmd.CommandType = CommandType.StoredProcedure
                                cmd.CommandText = "PKG_ATTENDANCE_BUSINESS.INSERT_WORKSIGN_DATE"

                                For Each row As DataRow In dtData.Rows
                                    cmd.Parameters.Clear()
                                    Dim objParam = New With {.P_EMPLOYEEID = row("EMPLOYEE_ID").ToString,
                                                             .P_PERIODId = period_id,
                                                             .P_USERNAME = log.Username.ToUpper,
                                                             .P_D1 = Utilities.Obj2Decima(row("D1"), Nothing),
                                                             .P_D2 = Utilities.Obj2Decima(row("D2"), Nothing),
                                                             .P_D3 = Utilities.Obj2Decima(row("D3"), Nothing),
                                                             .P_D4 = Utilities.Obj2Decima(row("D4"), Nothing),
                                                             .P_D5 = Utilities.Obj2Decima(row("D5"), Nothing),
                                                             .P_D6 = Utilities.Obj2Decima(row("D6"), Nothing),
                                                             .P_D7 = Utilities.Obj2Decima(row("D7"), Nothing),
                                                             .P_D8 = Utilities.Obj2Decima(row("D8"), Nothing),
                                                             .P_D9 = Utilities.Obj2Decima(row("D9"), Nothing),
                                                             .P_D10 = Utilities.Obj2Decima(row("D10"), Nothing),
                                                             .P_D11 = Utilities.Obj2Decima(row("D11"), Nothing),
                                                             .P_D12 = Utilities.Obj2Decima(row("D12"), Nothing),
                                                             .P_D13 = Utilities.Obj2Decima(row("D13"), Nothing),
                                                             .P_D14 = Utilities.Obj2Decima(row("D14"), Nothing),
                                                             .P_D15 = Utilities.Obj2Decima(row("D15"), Nothing),
                                                             .P_D16 = Utilities.Obj2Decima(row("D16"), Nothing),
                                                             .P_D17 = Utilities.Obj2Decima(row("D17"), Nothing),
                                                             .P_D18 = Utilities.Obj2Decima(row("D18"), Nothing),
                                                             .P_D19 = Utilities.Obj2Decima(row("D19"), Nothing),
                                                             .P_D20 = Utilities.Obj2Decima(row("D20"), Nothing),
                                                             .P_D21 = Utilities.Obj2Decima(row("D21"), Nothing),
                                                             .P_D22 = Utilities.Obj2Decima(row("D22"), Nothing),
                                                             .P_D23 = Utilities.Obj2Decima(row("D23"), Nothing),
                                                             .P_D24 = Utilities.Obj2Decima(row("D24"), Nothing),
                                                             .P_D25 = Utilities.Obj2Decima(row("D25"), Nothing),
                                                             .P_D26 = Utilities.Obj2Decima(row("D26"), Nothing),
                                                             .P_D27 = Utilities.Obj2Decima(row("D27"), Nothing),
                                                             .P_D28 = Utilities.Obj2Decima(row("D28"), Nothing),
                                                             .P_D29 = Utilities.Obj2Decima(row("D29"), Nothing),
                                                             .P_D30 = Utilities.Obj2Decima(row("D30"), Nothing),
                                                             .P_D31 = Utilities.Obj2Decima(row("D31"), Nothing)}

                                    If objParam IsNot Nothing Then
                                        For Each info As PropertyInfo In objParam.GetType().GetProperties()
                                            Dim bOut As Boolean = False
                                            Dim para = resource.GetParameter(info.Name, info.GetValue(objParam, Nothing), bOut)
                                            If para IsNot Nothing Then
                                                cmd.Parameters.Add(para)
                                            End If
                                        Next
                                    End If
                                    cmd.ExecuteNonQuery()
                                Next

                                cmd.CommandText = "PKG_ATTENDANCE_BUSINESS.IMPORT_WORKSIGN_DATE"
                                cmd.Parameters.Clear()
                                Dim objParam1 = New With {.P_STARTDATE = Period.START_DATE.Value,
                                                         .P_ENDDATE = Period.END_DATE.Value,
                                                         .P_USERNAME = log.Username.ToUpper}

                                If objParam1 IsNot Nothing Then
                                    For Each info As PropertyInfo In objParam1.GetType().GetProperties()
                                        Dim bOut As Boolean = False
                                        Dim para = resource.GetParameter(info.Name, info.GetValue(objParam1, Nothing), bOut)
                                        If para IsNot Nothing Then
                                            cmd.Parameters.Add(para)
                                        End If
                                    Next
                                End If

                                cmd.ExecuteNonQuery()

                                cmd.Transaction.Commit()
                                Check_DataRegister_OT(lstEmp, log, objParam1.P_STARTDATE, objParam1.P_ENDDATE, period_id)
                            Catch ex As Exception
                                cmd.Transaction.Rollback()
                                WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
                                Throw ex
                            Finally
                                'Dispose all resource
                                cmd.Dispose()
                                conn.Close()
                                conn.Dispose()
                            End Try
                        End Using
                    End Using
                End Using
            End Using

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function InsertWorkSign(ByVal objWorkSigns As List(Of AT_WORKSIGNDTO), ByVal objWork As AT_WORKSIGNDTO, ByVal p_fromdate As Date, ByVal p_endDate As Date?, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objWorkSign As New AT_WORKSIGNDTO
        Dim p_fromDateBefor As Date = p_fromdate
        Try
            For index = 0 To objWorkSigns.Count - 1
                objWorkSign = objWorkSigns(index)
                p_fromdate = p_fromDateBefor
                While p_fromdate <= p_endDate
                    Dim objWorkSignData As New AT_WORKSIGN
                    ' kiem tra da ton tai chua
                    Dim exist = (From c In Context.AT_WORKSIGN
                                 Where c.EMPLOYEE_ID = objWorkSign.EMPLOYEE_ID And _
                                 c.WORKINGDAY = p_fromdate).Any
                    If exist Then

                        Dim query = (From c In Context.AT_WORKSIGN
                                     Where c.EMPLOYEE_ID = objWorkSign.EMPLOYEE_ID And _
                                     c.WORKINGDAY = p_fromdate).FirstOrDefault

                        Dim shiftIDU = (From f In Context.AT_SHIFT Where f.ID = objWork.SHIFT_ID Select f).FirstOrDefault
                        Dim shiftOffu = (From f In Context.AT_SHIFT Where f.CODE = "OFF" Select f).FirstOrDefault
                        If p_fromdate.DayOfWeek = DayOfWeek.Sunday And Not String.IsNullOrEmpty(shiftOffu.ID) Then
                            If shiftIDU.SUNDAY.HasValue Then
                                query.SHIFT_ID = shiftOffu.ID
                            Else
                                query.SHIFT_ID = objWork.SHIFT_ID
                            End If
                        ElseIf p_fromdate.DayOfWeek = DayOfWeek.Saturday And shiftIDU.SATURDAY IsNot Nothing Then
                            query.SHIFT_ID = shiftIDU.SATURDAY
                        Else
                            query.SHIFT_ID = objWork.SHIFT_ID
                        End If
                        Context.SaveChanges(log)
                        p_fromdate = p_fromdate.AddDays(1)
                        Continue While
                    End If
                    objWorkSignData.ID = Utilities.GetNextSequence(Context, Context.AT_WORKSIGN.EntitySet.Name)
                    objWorkSignData.EMPLOYEE_ID = objWorkSign.EMPLOYEE_ID
                    objWorkSignData.WORKINGDAY = p_fromdate

                    Dim shiftId = (From f In Context.AT_SHIFT Where f.ID = objWork.SHIFT_ID Select f).FirstOrDefault
                    Dim shiftOff = (From f In Context.AT_SHIFT Where f.CODE = "OFF" Select f).FirstOrDefault
                    If p_fromdate.DayOfWeek = DayOfWeek.Sunday And Not String.IsNullOrEmpty(shiftOff.ID) Then
                        If shiftId.SUNDAY.HasValue Then
                            objWorkSignData.SHIFT_ID = shiftOff.ID
                        Else
                            objWorkSignData.SHIFT_ID = objWork.SHIFT_ID
                        End If
                    ElseIf p_fromdate.DayOfWeek = DayOfWeek.Saturday And shiftId.SATURDAY IsNot Nothing Then
                        objWorkSignData.SHIFT_ID = shiftId.SATURDAY
                    Else
                        objWorkSignData.SHIFT_ID = objWork.SHIFT_ID
                    End If
                    objWorkSignData.PERIOD_ID = objWork.PERIOD_ID
                    Context.AT_WORKSIGN.AddObject(objWorkSignData)
                    Context.SaveChanges(log)
                    p_fromdate = p_fromdate.AddDays(1)
                    gID = objWorkSignData.ID
                End While
            Next
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ValidateWorkSign(ByVal _validate As AT_WORKSIGNDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.AT_WORKSIGN
                             Where p.SHIFT_ID = _validate.SHIFT_ID _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.AT_WORKSIGN
                             Where p.SHIFT_ID = _validate.SHIFT_ID).FirstOrDefault
                End If

                Return (query Is Nothing)
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function CheckOffInMonth(ByVal _param As ParamDTO, Optional ByVal log As UserLog = Nothing) As Boolean
        Try
            Dim Period = (From w In Context.AT_PERIOD Where w.ID = _param.PERIOD_ID).FirstOrDefault
            Dim TotalSunday As Decimal
            Dim is_check As Boolean = False
            Dim objWorkSign As New AT_TIME_TIMESHEET_MONTHLYDTO
            ' lay so ngay off trong thang do
            Dim firstDay = New DateTime(Period.YEAR, Period.MONTH, 1)
            Dim day29 = firstDay.AddDays(28)
            Dim day30 = firstDay.AddDays(29)
            Dim day31 = firstDay.AddDays(30)
            If (day29.Month = Period.MONTH AndAlso day29.DayOfWeek = DayOfWeek.Sunday) _
                OrElse (day30.Month = Period.MONTH AndAlso day30.DayOfWeek = DayOfWeek.Sunday) _
                OrElse (day31.Month = Period.MONTH AndAlso day31.DayOfWeek = DayOfWeek.Sunday) Then
                TotalSunday = 5
            Else
                TotalSunday = 4
            End If

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username.ToUpper,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using
            Dim query = From p In Context.AT_TIME_TIMESHEET_MONTHLY
                         From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                         From t In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID).DefaultIfEmpty
                         From s In Context.HU_STAFF_RANK.Where(Function(f) f.ID = p.STAFF_RANK_ID).DefaultIfEmpty
                         From w In Context.HU_WORKING.Where(Function(f) f.ID = p.DECISION_ID).DefaultIfEmpty
                         From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = w.ORG_ID).DefaultIfEmpty
                         From po In Context.AT_PERIOD.Where(Function(f) f.ID = p.PERIOD_ID)
                         From k In Context.SE_CHOSEN_ORG.Where(Function(f) p.ORG_ID = f.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper)
                         Where p.PERIOD_ID = _param.PERIOD_ID

            Dim lst = query.Select(Function(p) New AT_TIME_TIMESHEET_MONTHLYDTO With {
                                       .ID = p.p.ID,
                                       .TOTAL_OFF = p.p.TOTAL_OFF,
                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE}).ToList
            For index = 0 To lst.Count - 1
                objWorkSign = lst(index)
                If objWorkSign.TOTAL_OFF > TotalSunday Then
                    is_check = True
                End If
            Next
            Return is_check
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function CheckOffInMonthTable(ByVal dtData As DataTable,
                                   ByVal p_period_id As Decimal,
                                   ByRef dtDataError As DataTable) As Boolean
        Try
            Dim Period = (From w In Context.AT_PERIOD Where w.ID = p_period_id).FirstOrDefault
            Dim TotalSunday As Decimal
            Dim TotalDayOffRes As Decimal
            Dim firstDay = New DateTime(Period.YEAR, Period.MONTH, 1)
            Dim employee_id As New Decimal?
            Dim day29 = firstDay.AddDays(28)
            Dim day30 = firstDay.AddDays(29)
            Dim day31 = firstDay.AddDays(30)

            If (day29.Month = Period.MONTH AndAlso day29.DayOfWeek = DayOfWeek.Sunday) _
                OrElse (day30.Month = Period.MONTH AndAlso day30.DayOfWeek = DayOfWeek.Sunday) _
                OrElse (day31.Month = Period.MONTH AndAlso day31.DayOfWeek = DayOfWeek.Sunday) Then
                TotalSunday = 5
            Else
                TotalSunday = 4
            End If
            dtDataError = dtData.Clone
            For Each row As DataRow In dtData.Rows
                employee_id = Utilities.Obj2Decima(row("EMPLOYEE_ID"))
                Dim objData = (From w In Context.AT_WORKSIGN _
                               Where w.EMPLOYEE_ID = employee_id _
                               And w.SHIFT_ID = 81 _
                               And w.WORKINGDAY >= Period.START_DATE _
                               And w.WORKINGDAY <= Period.END_DATE).ToList
                For Each col As DataColumn In dtData.Columns
                    If row(col) = "OFF" Then
                        TotalDayOffRes = TotalDayOffRes + 1
                    End If
                Next
                If objData.Count + TotalDayOffRes > TotalSunday Then
                    row("D1") = "Nhân viên đăng ký vượt công OFF trong tháng"
                    dtDataError.ImportRow(row)
                End If
            Next
            If dtDataError.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ModifyWorkSign(ByVal objWorkSign As AT_WORKSIGNDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objWorkSignData As New AT_WORKSIGN With {.ID = objWorkSign.ID}
        Try
            Context.AT_WORKSIGN.Attach(objWorkSignData)
            objWorkSignData.EMPLOYEE_ID = objWorkSign.EMPLOYEE_ID
            objWorkSignData.WORKINGDAY = objWorkSign.WORKINGDAY
            objWorkSignData.PERIOD_ID = objWorkSign.PERIOD_ID
            objWorkSignData.SHIFT_ID = objWorkSign.SHIFT_ID
            Context.SaveChanges(log)
            gID = objWorkSignData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

    End Function

    Public Function DeleteWorkSign(ByVal lstWorkSign() As AT_WORKSIGNDTO) As Boolean
        Dim lstWorkSignData As List(Of AT_WORKSIGN)
        Dim lstIDWorkSign As List(Of Decimal) = (From p In lstWorkSign.ToList Select p.ID).ToList
        Try

            lstWorkSignData = (From p In Context.AT_WORKSIGN Where lstIDWorkSign.Contains(p.ID)).ToList
            For index = 0 To lstWorkSignData.Count - 1
                Context.AT_WORKSIGN.DeleteObject(lstWorkSignData(index))
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

    End Function

    Public Function Del_WorkSign_ByEmp(ByVal employee_id As Decimal, ByVal p_From As Date, ByVal p_to As Date) As Boolean
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.DELETE_WORKSIGN",
                                               New With {.P_EMPLOYEE_ID = employee_id,
                                                         .P_FROM = p_From,
                                                         .P_TO = p_to}, False)
                Return True
            End Using
            Return False

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex

        End Try
    End Function

    Public Function GETSIGNDEFAULT(ByVal param As ParamDTO, ByVal log As UserLog) As DataTable
        Try

            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.GETSIGNDEFAULT",
                                               New With {.P_USERNAME = log.Username.ToUpper,
                                                         .P_ORG_ID = param.ORG_ID,
                                                         .P_PERIOD_ID = param.PERIOD_ID,
                                                         .P_ISDISSOLVE = param.IS_DISSOLVE,
                                                         .P_CUR = cls.OUT_CURSOR})
                Return dtData
            End Using
            Return Nothing
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex

        End Try


    End Function

    Public Function GET_WORKSIGN_BYEMP(ByVal Emp_ID As Decimal, ByVal working_day As DateTime) As AT_WORKSIGNDTO
        Try
            Dim query = From p In Context.AT_WORKSIGN
                         From s In Context.AT_SHIFT.Where(Function(f) f.ID = p.SHIFT_ID).DefaultIfEmpty
                         Where p.EMPLOYEE_ID = Emp_ID And p.WORKINGDAY = working_day

            Dim lst = query.Select(Function(p) New AT_WORKSIGNDTO With {
                                        .ID = p.p.ID,
                                        .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                        .WORKINGDAY = p.p.WORKINGDAY,
                                        .SHIFT_ID = p.p.SHIFT_ID,
                                        .CODE = p.s.CODE}).FirstOrDefault
            Return lst

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
        Return Nothing
    End Function
#End Region

#Region "TAI DU LIEU MAY CHAM CONG"

    Public Function GetSwipeData(ByVal _filter As AT_SWIPE_DATADTO,
                                    ByVal PageIndex As Integer,
                                       ByVal PageSize As Integer,
                                       ByRef Total As Integer,
                                       Optional ByVal Sorts As String = "iTime_id, VALTIME desc") As List(Of AT_SWIPE_DATADTO)
        Try
            Dim query = From p In Context.AT_SWIPE_DATA
                        From s In Context.AT_TERMINALS.Where(Function(f) f.ID = p.TERMINAL_ID).DefaultIfEmpty
                        Select New AT_SWIPE_DATADTO With {
                                       .ID = p.ID,
                                       .TERMINAL_NAME = s.TERMINAL_NAME,
                                       .ITIME_ID = p.ITIME_ID,
                                       .ITIME_ID_S = p.ITIME_ID.Value,
                                       .WORKINGDAY = p.WORKINGDAY,
                                       .VALTIME = p.VALTIME,
                                       .IS_IMPORT = p.IS_IMPORT}

            Dim lst = query

            If _filter.TERMINAL_NAME <> "" Then
                lst = lst.Where(Function(f) f.TERMINAL_NAME.ToUpper.Contains(_filter.TERMINAL_NAME.ToUpper))
            End If
            If _filter.ITIME_ID.HasValue Then
                lst = lst.Where(Function(f) f.ITIME_ID = _filter.ITIME_ID)
            End If
            If _filter.ITIME_ID_S <> "" Then
                If IsNumeric(_filter.ITIME_ID_S) Then
                    lst = lst.Where(Function(f) f.ITIME_ID = _filter.ITIME_ID_S)
                Else
                    lst = lst.Where(Function(f) f.ITIME_ID = 0)
                End If
            End If
            If _filter.WORKINGDAY.HasValue Then
                lst = lst.Where(Function(f) f.WORKINGDAY = _filter.WORKINGDAY)
            End If
            If _filter.IS_IMPORT.HasValue Then
                lst = lst.Where(Function(f) f.IS_IMPORT = _filter.IS_IMPORT)
            End If
            If _filter.FROM_DATE.HasValue Then
                lst = lst.Where(Function(f) f.WORKINGDAY >= _filter.FROM_DATE)
            End If
            If _filter.END_DATE.HasValue Then
                lst = lst.Where(Function(f) f.WORKINGDAY <= _filter.END_DATE)
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function GetSwipeDataMeal(ByVal _filter As AT_SWIPE_DATA_MEALDTO,
                                    ByVal PageIndex As Integer,
                                       ByVal PageSize As Integer,
                                       ByRef Total As Integer,
                                       Optional ByVal Sorts As String = "iTime_id, VALTIME desc") As List(Of AT_SWIPE_DATA_MEALDTO)
        Try
            Dim query = From p In Context.AT_SWIPE_DATA_MEAL
                        From s In Context.AT_TERMINALS_MEAL.Where(Function(f) f.ID = p.TERMINAL_ID).DefaultIfEmpty
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ITIME_ID = p.ITIME_ID And f.TER_EFFECT_DATE Is Nothing).DefaultIfEmpty
                        Select New AT_SWIPE_DATA_MEALDTO With {
                                       .ID = p.ID,
                                       .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                                       .EMPLOYEE_NAME = e.FULLNAME_VN,
                                       .TERMINAL_NAME = s.TERMINAL_NAME,
                                       .ITIME_ID = p.ITIME_ID,
                                       .ITIME_ID_S = p.ITIME_ID.Value,
                                       .WORKINGDAY = p.WORKINGDAY,
                                       .VALTIME = p.VALTIME,
                                       .IS_IMPORT = p.IS_IMPORT}

            Dim lst = query

            If _filter.TERMINAL_NAME <> "" Then
                lst = lst.Where(Function(f) f.TERMINAL_NAME.ToUpper.Contains(_filter.TERMINAL_NAME.ToUpper))
            End If
            If _filter.EMPLOYEE_CODE <> "" Then
                lst = lst.Where(Function(f) f.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If
            If _filter.EMPLOYEE_NAME <> "" Then
                lst = lst.Where(Function(f) f.EMPLOYEE_NAME.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper))
            End If
            If _filter.ITIME_ID.HasValue Then
                lst = lst.Where(Function(f) f.ITIME_ID = _filter.ITIME_ID)
            End If
            If _filter.ITIME_ID_S <> "" Then
                If IsNumeric(_filter.ITIME_ID_S) Then
                    lst = lst.Where(Function(f) f.ITIME_ID = _filter.ITIME_ID_S)
                Else
                    lst = lst.Where(Function(f) f.ITIME_ID = 0)
                End If
            End If
            If _filter.WORKINGDAY.HasValue Then
                lst = lst.Where(Function(f) f.WORKINGDAY = _filter.WORKINGDAY)
            End If
            If _filter.IS_IMPORT.HasValue Then
                lst = lst.Where(Function(f) f.IS_IMPORT = _filter.IS_IMPORT)
            End If
            If _filter.FROM_DATE.HasValue Then
                lst = lst.Where(Function(f) f.WORKINGDAY >= _filter.FROM_DATE)
            End If
            If _filter.END_DATE.HasValue Then
                lst = lst.Where(Function(f) f.WORKINGDAY <= _filter.END_DATE)
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ImportSwipeDataAuto(ByVal ls_AT_SWIPE_DATADTO As List(Of AT_SWIPE_DATADTO),
                                         ByVal log As UserLog,
                                        Optional ByVal isMeal As Boolean = False
                                         ) As Boolean
        If Not isMeal Then
            Return SwipeDataAuto(ls_AT_SWIPE_DATADTO, log)
        Else
            Return SwipeDataMealAuto(ls_AT_SWIPE_DATADTO, log)
        End If
    End Function

    Function SwipeDataAuto(ByVal ls_AT_SWIPE_DATADTO As List(Of AT_SWIPE_DATADTO),
                                          ByVal log As UserLog)
        Dim endDate As Date?
        Dim fromDate As Date?
        Try
            Using conMng As New ConnectionManager
                Using conn As New OracleConnection(conMng.GetConnectionString())
                    Using cmd As New OracleCommand()
                        Try
                            conn.Open()
                            cmd.Connection = conn
                            cmd.CommandType = CommandType.StoredProcedure
                            cmd.CommandText = "PKG_ATTENDANCE_BUSINESS.INSERT_TIME_CARD_AUTO"
                            cmd.Transaction = cmd.Connection.BeginTransaction()
                            For index = 0 To ls_AT_SWIPE_DATADTO.Count - 1
                                cmd.Parameters.Clear()
                                Dim objDataInout = ls_AT_SWIPE_DATADTO(index)
                                If endDate Is Nothing Then
                                    endDate = objDataInout.VALTIME.Value.Date
                                Else
                                    If objDataInout.VALTIME.Value.Date > endDate Then
                                        endDate = objDataInout.VALTIME.Value.Date
                                    End If
                                End If

                                If fromDate Is Nothing Then
                                    fromDate = objDataInout.VALTIME.Value.Date
                                Else
                                    If objDataInout.VALTIME.Value.Date < fromDate Then
                                        fromDate = objDataInout.VALTIME.Value.Date
                                    End If
                                End If

                                Using resource As New DataAccess.OracleCommon()
                                    Dim objParam = New With {.P_TIMEID = objDataInout.ITIME_ID,
                                                             .P_TERMINAL_ID = objDataInout.TERMINAL_ID,
                                                             .P_VALTIME = objDataInout.VALTIME,
                                                             .P_USERNAME = log.Username}

                                    If objParam IsNot Nothing Then
                                        For Each info As PropertyInfo In objParam.GetType().GetProperties()
                                            Dim bOut As Boolean = False
                                            Dim para = resource.GetParameter(info.Name, info.GetValue(objParam, Nothing), bOut)
                                            If para IsNot Nothing Then
                                                cmd.Parameters.Add(para)
                                            End If
                                        Next
                                    End If
                                    cmd.ExecuteNonQuery()
                                End Using
                            Next
                            cmd.Transaction.Commit()
                        Catch ex As Exception
                            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
                            Throw ex
                            cmd.Transaction.Rollback()
                        Finally
                            'Dispose all resource
                            cmd.Dispose()
                            conn.Close()
                            conn.Dispose()
                        End Try
                    End Using
                End Using
            End Using
            If endDate IsNot Nothing And fromDate IsNot Nothing Then
                Using cls As New DataAccess.QueryData
                    cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.UPDATE_DATAINOUT",
                                                   New With {.P_ITIMEID = 0,
                                                             .P_USERNAME = log.Username,
                                                             .P_FROMDATE = fromDate,
                                                             .P_ENDDATE = endDate})
                End Using

            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Function SwipeDataMealAuto(ByVal ls_AT_SWIPE_DATADTO As List(Of AT_SWIPE_DATADTO), ByVal log As UserLog)
        Dim endDate As Date?
        Dim fromDate As Date?
        Try
            Using conMng As New ConnectionManager
                Using conn As New OracleConnection(conMng.GetConnectionString())
                    Using cmd As New OracleCommand()
                        Try
                            conn.Open()
                            cmd.Connection = conn
                            cmd.CommandType = CommandType.StoredProcedure
                            cmd.CommandText = "PKG_ATTENDANCE_BUSINESS.INSERT_TIME_CARD_MEAL_AUTO"
                            cmd.Transaction = cmd.Connection.BeginTransaction()
                            For index = 0 To ls_AT_SWIPE_DATADTO.Count - 1
                                cmd.Parameters.Clear()
                                Dim objDataInout = ls_AT_SWIPE_DATADTO(index)
                                If endDate Is Nothing Then
                                    endDate = objDataInout.VALTIME.Value.Date
                                Else
                                    If objDataInout.VALTIME.Value.Date > endDate Then
                                        endDate = objDataInout.VALTIME.Value.Date
                                    End If
                                End If

                                If fromDate Is Nothing Then
                                    fromDate = objDataInout.VALTIME.Value.Date
                                Else
                                    If objDataInout.VALTIME.Value.Date < fromDate Then
                                        fromDate = objDataInout.VALTIME.Value.Date
                                    End If
                                End If

                                Using resource As New DataAccess.OracleCommon()
                                    Dim objParam = New With {.P_TIMEID = objDataInout.ITIME_ID,
                                                             .P_TERMINAL_ID = objDataInout.TERMINAL_ID,
                                                             .P_VALTIME = objDataInout.VALTIME,
                                                             .P_USERNAME = log.Username}

                                    If objParam IsNot Nothing Then
                                        For Each info As PropertyInfo In objParam.GetType().GetProperties()
                                            Dim bOut As Boolean = False
                                            Dim para = resource.GetParameter(info.Name, info.GetValue(objParam, Nothing), bOut)
                                            If para IsNot Nothing Then
                                                cmd.Parameters.Add(para)
                                            End If
                                        Next
                                    End If
                                    cmd.ExecuteNonQuery()
                                End Using
                            Next
                            cmd.Transaction.Commit()
                        Catch ex As Exception
                            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
                            Throw ex
                            cmd.Transaction.Rollback()
                        Finally
                            'Dispose all resource
                            cmd.Dispose()
                            conn.Close()
                            conn.Dispose()
                        End Try
                    End Using
                End Using
            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function InsertSwipeDataImport(ByVal lstData As List(Of AT_SWIPE_DATADTO),
                                          ByVal log As UserLog,
                                         ByVal isMeal As Boolean) As Boolean
        If Not isMeal Then
            Return SwipeDataImport(lstData, log)
        Else
            Return SwipeDataMealImport(lstData, log)
        End If
    End Function

    Function SwipeDataImport(ByVal lstData As List(Of AT_SWIPE_DATADTO),
                             ByVal log As UserLog)
        Try
            Dim fromDate As Date?
            Dim endDate As New Date?
            Using conMng As New ConnectionManager
                Using conn As New OracleConnection(conMng.GetConnectionString())
                    Using cmd As New OracleCommand()
                        Try
                            conn.Open()
                            cmd.Connection = conn
                            cmd.CommandType = CommandType.StoredProcedure
                            cmd.CommandText = "PKG_ATTENDANCE_BUSINESS.INSERT_TIME_CARD"
                            cmd.Transaction = cmd.Connection.BeginTransaction()
                            For Each objDataInout In lstData
                                If endDate Is Nothing Then
                                    endDate = objDataInout.WORKINGDAY
                                Else
                                    If objDataInout.WORKINGDAY > endDate Then
                                        endDate = objDataInout.WORKINGDAY
                                    End If
                                End If

                                If fromDate Is Nothing Then
                                    fromDate = objDataInout.WORKINGDAY
                                Else
                                    If objDataInout.WORKINGDAY < fromDate Then
                                        fromDate = objDataInout.WORKINGDAY
                                    End If
                                End If
                                cmd.Parameters.Clear()
                                Using resource As New DataAccess.OracleCommon()
                                    Dim objParam = New With {.P_TIMEID = objDataInout.ITIME_ID,
                                                             .P_TERMINAL_ID = objDataInout.TERMINAL_ID,
                                                             .P_VALTIME = objDataInout.VALTIME,
                                                             .P_USERNAME = log.Username.ToUpper}

                                    If objParam IsNot Nothing Then
                                        For Each info As PropertyInfo In objParam.GetType().GetProperties()
                                            Dim bOut As Boolean = False
                                            Dim para = resource.GetParameter(info.Name, info.GetValue(objParam, Nothing), bOut)
                                            If para IsNot Nothing Then
                                                cmd.Parameters.Add(para)
                                            End If
                                        Next
                                    End If
                                    cmd.ExecuteNonQuery()
                                End Using
                            Next
                            cmd.Transaction.Commit()
                        Catch ex As Exception
                            cmd.Transaction.Rollback()
                        Finally
                            'Dispose all resource
                            cmd.Dispose()
                            conn.Close()
                            conn.Dispose()
                        End Try
                    End Using
                End Using
            End Using

            ' UPDATE DATA TO AT_DATAINOUTDTO
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.UPDATE_DATAINOUT",
                                               New With {.P_ITIMEID = 0,
                                                         .P_USERNAME = log.Username.ToUpper,
                                                         .P_FROMDATE = fromDate,
                                                         .P_ENDDATE = endDate})
            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Function SwipeDataMealImport(ByVal lstData As List(Of AT_SWIPE_DATADTO),
                             ByVal log As UserLog)
        Try
            Dim fromDate As Date?
            Dim endDate As New Date?
            Using conMng As New ConnectionManager
                Using conn As New OracleConnection(conMng.GetConnectionString())
                    Using cmd As New OracleCommand()
                        Try
                            conn.Open()
                            cmd.Connection = conn
                            cmd.CommandType = CommandType.StoredProcedure
                            cmd.CommandText = "PKG_ATTENDANCE_BUSINESS.INSERT_TIME_CARD_MEAL"
                            cmd.Transaction = cmd.Connection.BeginTransaction()
                            For Each objDataInout In lstData
                                If endDate Is Nothing Then
                                    endDate = objDataInout.WORKINGDAY
                                Else
                                    If objDataInout.WORKINGDAY > endDate Then
                                        endDate = objDataInout.WORKINGDAY
                                    End If
                                End If

                                If fromDate Is Nothing Then
                                    fromDate = objDataInout.WORKINGDAY
                                Else
                                    If objDataInout.WORKINGDAY < fromDate Then
                                        fromDate = objDataInout.WORKINGDAY
                                    End If
                                End If
                                cmd.Parameters.Clear()
                                Using resource As New DataAccess.OracleCommon()
                                    Dim objParam = New With {.P_TIMEID = objDataInout.ITIME_ID,
                                                             .P_TERMINAL_ID = objDataInout.TERMINAL_ID,
                                                             .P_VALTIME = objDataInout.VALTIME,
                                                             .P_USERNAME = log.Username.ToUpper}

                                    If objParam IsNot Nothing Then
                                        For Each info As PropertyInfo In objParam.GetType().GetProperties()
                                            Dim bOut As Boolean = False
                                            Dim para = resource.GetParameter(info.Name, info.GetValue(objParam, Nothing), bOut)
                                            If para IsNot Nothing Then
                                                cmd.Parameters.Add(para)
                                            End If
                                        Next
                                    End If
                                    cmd.ExecuteNonQuery()
                                End Using
                            Next
                            cmd.Transaction.Commit()
                        Catch ex As Exception
                            cmd.Transaction.Rollback()
                        Finally
                            'Dispose all resource
                            cmd.Dispose()
                            conn.Close()
                            conn.Dispose()
                        End Try
                    End Using
                End Using
            End Using

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

#End Region

#Region "Giải trình chấm công"

    Public Function GetListExplanation(ByVal _filter As AT_TIME_TIMESHEET_DAILYDTO,
                                     ByVal _param As ParamDTO,
                                     Optional ByRef Total As Integer = 0,
                                     Optional ByVal PageIndex As Integer = 0,
                                     Optional ByVal PageSize As Integer = Integer.MaxValue,
                                     Optional ByVal Sorts As String = "EMPLOYEE_CODE desc,WORKINGDAY asc",
                                     Optional ByVal log As UserLog = Nothing) As List(Of AT_TIME_TIMESHEET_DAILYDTO)
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username.ToUpper,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using

            Dim Period = (From w In Context.AT_PERIOD Where w.ID = _param.PERIOD_ID).FirstOrDefault

            Dim query = From p In Context.AT_TIME_TIMESHEET_DAILY
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        From c In Context.HU_STAFF_RANK.Where(Function(f) f.ID = e.STAFF_RANK_ID).DefaultIfEmpty
                        From typeot In Context.AT_TIME_MANUAL.Where(Function(f) f.ID = p.MANUAL_ID).DefaultIfEmpty
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) e.ORG_ID = f.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper)
                        Where p.IS_EXPLANTION = -1 And p.WORKINGDAY >= Period.START_DATE And p.WORKINGDAY <= Period.END_DATE


            Dim lst = query.Select(Function(p) New AT_TIME_TIMESHEET_DAILYDTO With {
                                       .ID = p.p.ID,
                                       .EMPLOYEE_ID = p.e.ID,
                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                       .VN_FULLNAME = p.e.FULLNAME_VN,
                                       .TITLE_NAME = p.t.NAME_VN,
                                       .ORG_ID = p.e.ORG_ID,
                                       .TITLE_ID = p.e.TITLE_ID,
                                       .ORG_NAME = p.o.NAME_VN,
                                       .STAFF_RANK_NAME = p.c.NAME,
                                       .ORG_DESC = p.o.DESCRIPTION_PATH,
                                       .WORKINGDAY = p.p.WORKINGDAY,
                                       .LEAVE_CODE = p.typeot.CODE,
                                       .NOTE = p.p.NOTE,
                                       .WORK_STATUS = p.e.WORK_STATUS,
                                       .TER_LAST_DATE = p.e.TER_LAST_DATE,
                                       .CREATED_DATE = p.p.CREATED_DATE})


            If _filter.IS_TERMINATE Then
                lst = lst.Where(Function(f) f.WORK_STATUS = 257)
                If _filter.WORKINGDAY.HasValue Then
                    lst = lst.Where(Function(f) f.TER_LAST_DATE <= _filter.WORKINGDAY)
                Else
                    lst = lst.Where(Function(f) f.TER_LAST_DATE <= Date.Now)
                End If
            End If
            If _filter.FROM_DATE.HasValue Then
                lst = lst.Where(Function(f) f.WORKINGDAY >= _filter.FROM_DATE)
            End If
            If _filter.END_DATE.HasValue Then
                lst = lst.Where(Function(f) f.WORKINGDAY <= _filter.END_DATE)
            End If
            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                lst = lst.Where(Function(f) f.EMPLOYEE_CODE.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.VN_FULLNAME) Then
                lst = lst.Where(Function(f) f.VN_FULLNAME.ToLower().Contains(_filter.VN_FULLNAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                lst = lst.Where(Function(f) f.ORG_NAME.ToLower().Contains(_filter.ORG_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TITLE_NAME) Then
                lst = lst.Where(Function(f) f.TITLE_NAME.ToLower().Contains(_filter.TITLE_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.STAFF_RANK_NAME) Then
                lst = lst.Where(Function(f) f.STAFF_RANK_NAME.ToLower().Contains(_filter.STAFF_RANK_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.LEAVE_CODE) Then
                lst = lst.Where(Function(f) f.LEAVE_CODE.ToLower().Contains(_filter.LEAVE_CODE.ToLower()))
            End If
            If _filter.WORKINGDAY.HasValue Then
                lst = lst.Where(Function(f) f.WORKINGDAY = _filter.WORKINGDAY)
            End If
            If Not String.IsNullOrEmpty(_filter.NOTE) Then
                lst = lst.Where(Function(f) f.NOTE.ToLower().Contains(_filter.NOTE.ToLower()))
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function GetExplanationManual() As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.GET_MANUAL_LIST",
                                           New With {.P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetExplanationEmployee(ByVal _param As ParamDTO, ByVal log As UserLog) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.GET_EXPORT_EXPLANATION",
                                           New With {.P_USERNAME = log.Username,
                                                     .P_ORG_ID = _param.ORG_ID,
                                                     .P_IS_DISSOLVE = _param.IS_DISSOLVE,
                                                     .P_PERIODID = _param.PERIOD_ID,
                                                    .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ImportExplanation(ByVal dtData As DataTable, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Try
            For Each row As DataRow In dtData.Rows
                Dim employeeID = Utilities.Obj2Decima(row("EMPLOYEE_ID"), Nothing)
                Dim workingDay = ToDate(row("WORKINGDAY"))

                Dim obj = (From r In Context.AT_TIME_TIMESHEET_DAILY _
                          Where r.EMPLOYEE_ID = employeeID And
                          r.WORKINGDAY = workingDay).FirstOrDefault
                If obj Is Nothing Then
                    obj = New AT_TIME_TIMESHEET_DAILY
                    obj.ID = Utilities.GetNextSequence(Context, Context.AT_TIME_TIMESHEET_DAILY.EntitySet.Name)
                    obj.EMPLOYEE_ID = Decimal.Parse(row("EMPLOYEE_ID"))
                    obj.ORG_ID = Decimal.Parse(row("ORG_ID"))
                    obj.TITLE_ID = Decimal.Parse(row("TITLE_ID"))
                    obj.WORKINGDAY = ToDate(row("WORKINGDAY"))
                    obj.MANUAL_ID = If(row("MANUAL_ID") = "#N/A", Nothing, Utilities.Obj2Decima(row("MANUAL_ID"), Nothing))
                    obj.NOTE = row("NOTE")
                    obj.IS_EXPLANTION = True
                    Context.AT_TIME_TIMESHEET_DAILY.AddObject(obj)
                Else
                    obj.LEAVE_CODE = row("MANUAL_NAME")
                    obj.MANUAL_ID = If(row("MANUAL_ID") = "#N/A", Nothing, Utilities.Obj2Decima(row("MANUAL_ID"), Nothing))
                    obj.NOTE = row("NOTE")
                    obj.IS_EXPLANTION = True
                    Context.SaveChanges(log)
                End If
                gID = obj.ID
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".InsertPlanReg")
            Throw ex
        End Try
    End Function

#End Region

#Region "LOG"
    Public Function GetActionLog(ByVal _filter As AT_ACTION_LOGDTO,
                                        ByRef Total As Integer,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        Optional ByVal Sorts As String = "ACTION_DATE desc") As List(Of AT_ACTION_LOGDTO)

        Try
            Dim query = From p In Context.AT_ACTION_LOG
                        From e In Context.SE_USER.Where(Function(f) f.USERNAME.ToUpper = p.USERNAME.ToUpper)
                        From r In Context.AT_PERIOD.Where(Function(f) f.ID = p.PERIOD_ID).DefaultIfEmpty

            Dim lst = query.Select(Function(p) New AT_ACTION_LOGDTO With {
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

    Public Function DeleteActionLogsAT(ByVal lstDeleteIds As List(Of Decimal)) As Integer
        Dim lstData As List(Of AT_ACTION_LOG)
        Try
            lstData = (From p In Context.AT_ACTION_LOG Where lstDeleteIds.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                Context.AT_ACTION_LOG.DeleteObject(lstData(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function LOG_AT(ByVal _param As ParamDTO,
                           ByVal log As UserLog,
                           ByVal lstEmployee As List(Of Decimal?),
                           ByVal Object_Name As String,
                           ByVal action As AT_ACTION_LOGDTO,
                           ByVal org_id As Decimal?) As Boolean
        Dim ActionId As Decimal?
        Dim action_log As New AT_ACTION_LOG
        action_log.ID = Utilities.GetNextSequence(Context, Context.AT_ACTION_LOG.EntitySet.Name)
        ActionId = action_log.ID
        action_log.USERNAME = log.Username.ToUpper
        action_log.IP = log.Ip
        action_log.ACTION_NAME = log.ActionName
        action_log.ACTION_DATE = DateTime.Now
        action_log.OBJECT_NAME = Object_Name
        action_log.COMPUTER_NAME = log.ComputerName
        action_log.ORG_ID = org_id
        action_log.EMPLOYEE_ID = action.EMPLOYEE_ID
        action_log.OLD_VALUE = action.OLD_VALUE
        action_log.PERIOD_ID = action.PERIOD_ID
        action_log.NEW_VALUE = action.NEW_VALUE
        Context.AT_ACTION_LOG.AddObject(action_log)
        If lstEmployee.Count > 0 Then
            Dim action_logOrg As AT_ACTION_ORG_LOG
            For Each emp As Decimal? In lstEmployee
                action_logOrg = New AT_ACTION_ORG_LOG
                action_logOrg.ID = Utilities.GetNextSequence(Context, Context.AT_ACTION_ORG_LOG.EntitySet.Name)
                action_logOrg.EMPLOYEE_ID = emp
                action_logOrg.ACTION_LOG_ID = ActionId
                Context.AT_ACTION_ORG_LOG.AddObject(action_logOrg)
            Next
        Else
            Using cls As New DataAccess.NonQueryData
                cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.INSERT_CHOSEN_LOGORG",
                             New With {.P_USERNAME = log.Username.ToUpper,
                                       .P_ORGID = _param.ORG_ID,
                                       .P_ISDISSOLVE = _param.IS_DISSOLVE,
                                       .P_ACTION_ID = ActionId})
            End Using
        End If
        Context.SaveChanges()
        Return True
    End Function
#End Region

#Region "IPORTAL - View bảng công"

    Public Function CheckPeriod(ByVal PeriodId As Integer, ByVal EmployeeId As Decimal) As Boolean
        Try
            Dim emp As HU_EMPLOYEE
            emp = (From p In Context.HU_EMPLOYEE Where p.ID = EmployeeId).FirstOrDefault

            Dim query = (From p In Context.AT_ORG_PERIOD
                         Where p.PERIOD_ID = PeriodId And p.ORG_ID = emp.ORG_ID).FirstOrDefault


            If query IsNot Nothing Then

                If query.STATUSCOLEX = 0 Then
                    Return query.STATUSCOLEX = 0
                Else
                    Return -1
                End If
            Else
                Return (query Is Nothing)
            End If
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

    End Function

    Public Function GetTimeSheetPortal(ByVal _filter As AT_TIME_TIMESHEET_DAILYDTO) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData = cls.ExecuteStore("PKG_ATTENDANCE_PORTAL.GET_TIMESHEET_DETAIL",
                                               New With {.P_PERIOD_ID = _filter.PERIOD_ID,
                                                         .P_EMPLOYEE_ID = _filter.EMPLOYEE_ID,
                                                         .P_DATA = cls.OUT_CURSOR})
                Return dtData
            End Using

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

#End Region
End Class
