Imports System.Data.Objects
Imports Framework.Data
Imports LinqKit
Imports System.Data.Objects.DataClasses
Imports System.Data.Common
Imports System.Data.Entity
Imports System.Threading
Imports Framework.Data.SystemConfig

Imports Framework.Data.System.Linq.Dynamic

Imports System.Configuration
Imports System.Reflection

Partial Public Class AttendanceRepository
    Dim nvalue_id As Decimal?

#Region "List Holiday"
    Public Function GetHoliday(ByVal _filter As AT_HOLIDAYDTO,
                                 Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                 Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_HOLIDAYDTO)
        Try

            Dim query = From p In Context.AT_HOLIDAY
            Dim lst = query.Select(Function(p) New AT_HOLIDAYDTO With {
                                       .ID = p.ID,
                                       .CODE = p.CODE,
                                       .NAME_EN = p.NAME_EN,
                                       .NAME_VN = p.NAME_VN,
                                       .WORKINGDAY = p.WORKINGDAY,
                                       .YEAR = p.YEAR,
                                       .NOTE = p.NOTE,
                                       .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng Áp dụng"),
                                       .CREATED_BY = p.CREATED_BY,
                                       .CREATED_DATE = p.CREATED_DATE,
                                       .CREATED_LOG = p.CREATED_LOG,
                                       .MODIFIED_BY = p.MODIFIED_BY,
                                       .MODIFIED_DATE = p.MODIFIED_DATE,
                                       .MODIFIED_LOG = p.MODIFIED_LOG})

            If Not String.IsNullOrEmpty(_filter.CODE) Then
                lst = lst.Where(Function(f) f.CODE.ToLower().Contains(_filter.CODE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.NAME_VN) Then
                lst = lst.Where(Function(f) f.NAME_VN.ToLower().Contains(_filter.NAME_VN.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.NAME_EN) Then
                lst = lst.Where(Function(f) f.NAME_EN.ToLower().Contains(_filter.NAME_EN.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ACTFLG) Then
                lst = lst.Where(Function(f) f.ACTFLG.ToLower().Contains(_filter.ACTFLG.ToLower()))
            End If
            If _filter.WORKINGDAY.HasValue Then
                lst = lst.Where(Function(f) f.WORKINGDAY = _filter.WORKINGDAY)
            End If
            If _filter.YEAR <> 0 Then
                lst = lst.Where(Function(f) f.YEAR = _filter.YEAR)
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

    Public Function InsertHoliday(ByVal objTitle As AT_HOLIDAYDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New AT_HOLIDAY
        Dim iCount As Integer = 0
        Try
            objTitleData.ID = Utilities.GetNextSequence(Context, Context.AT_HOLIDAY.EntitySet.Name)
            objTitleData.CODE = objTitle.CODE.Trim
            'objTitleData.NAME_EN = objTitle.NAME_EN.Trim
            objTitleData.NAME_VN = objTitle.NAME_VN.Trim
            objTitleData.WORKINGDAY = objTitle.WORKINGDAY
            objTitleData.YEAR = objTitle.YEAR
            objTitleData.NOTE = objTitle.NOTE
            objTitleData.ACTFLG = objTitle.ACTFLG
            objTitleData.CREATED_BY = objTitle.CREATED_BY
            objTitleData.CREATED_DATE = objTitle.CREATED_DATE
            objTitleData.CREATED_LOG = objTitle.CREATED_LOG
            objTitleData.MODIFIED_BY = objTitle.MODIFIED_BY
            objTitleData.MODIFIED_DATE = objTitle.MODIFIED_DATE
            objTitleData.MODIFIED_LOG = objTitle.MODIFIED_LOG
            Context.AT_HOLIDAY.AddObject(objTitleData)
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

    End Function

    Public Function ValidateHoliday(ByVal _validate As AT_HOLIDAYDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.AT_HOLIDAY
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.AT_HOLIDAY
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper).FirstOrDefault
                End If
                Return (query Is Nothing)
            ElseIf _validate.WORKINGDAY.HasValue Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.AT_HOLIDAY
                             Where p.WORKINGDAY = _validate.WORKINGDAY _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.AT_HOLIDAY
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

    Public Function ModifyHoliday(ByVal objTitle As AT_HOLIDAYDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New AT_HOLIDAY With {.ID = objTitle.ID}
        Try
            objTitleData = (From p In Context.AT_HOLIDAY Where p.ID = objTitleData.ID).SingleOrDefault
            objTitleData.ID = objTitle.ID
            objTitleData.CODE = objTitle.CODE.Trim
            'objTitleData.NAME_EN = objTitle.NAME_EN.Trim
            objTitleData.NAME_VN = objTitle.NAME_VN.Trim
            objTitleData.WORKINGDAY = objTitle.WORKINGDAY
            objTitleData.YEAR = objTitle.YEAR
            objTitleData.NOTE = objTitle.NOTE
            objTitleData.CREATED_BY = objTitle.CREATED_BY
            objTitleData.CREATED_DATE = objTitle.CREATED_DATE
            objTitleData.CREATED_LOG = objTitle.CREATED_LOG
            objTitleData.MODIFIED_BY = objTitle.MODIFIED_BY
            objTitleData.MODIFIED_DATE = objTitle.MODIFIED_DATE
            objTitleData.MODIFIED_LOG = objTitle.MODIFIED_LOG
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

    End Function

    Public Function ActiveHoliday(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstData As List(Of AT_HOLIDAY)
        Try
            lstData = (From p In Context.AT_HOLIDAY Where lstID.Contains(p.ID)).ToList
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

    Public Function DeleteHoliday(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstHolidayData As List(Of AT_HOLIDAY)
        Try
            lstHolidayData = (From p In Context.AT_HOLIDAY Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstHolidayData.Count - 1
                Context.AT_HOLIDAY.DeleteObject(lstHolidayData(index))
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

#Region "List Holiday gerenal"
    Public Function GetHolidayGerenal(ByVal _filter As AT_HOLIDAY_GENERALDTO,
                                        Optional ByVal PageIndex As Integer = 0,
                                        Optional ByVal PageSize As Integer = Integer.MaxValue,
                                        Optional ByRef Total As Integer = 0,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_HOLIDAY_GENERALDTO)
        Try

            Dim query = From p In Context.AT_HOLIDAY_GENERAL

            Dim lst = query.Select(Function(p) New AT_HOLIDAY_GENERALDTO With {
                                       .ID = p.ID,
                                       .CODE = p.CODE,
                                       .NAME_EN = p.NAME_EN,
                                       .NAME_VN = p.NAME_VN,
                                       .WORKINGDAY = p.WORKINGDAY,
                                       .NOTE = p.NOTE,
                                       .YEAR = p.YEAR,
                                       .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng Áp dụng"),
                                       .CREATED_BY = p.CREATED_BY,
                                       .CREATED_DATE = p.CREATED_DATE,
                                       .CREATED_LOG = p.CREATED_LOG,
                                       .MODIFIED_BY = p.MODIFIED_BY,
                                       .MODIFIED_DATE = p.MODIFIED_DATE,
                                       .MODIFIED_LOG = p.MODIFIED_LOG})

            If Not String.IsNullOrEmpty(_filter.CODE) Then
                lst = lst.Where(Function(f) f.CODE.ToLower().Contains(_filter.CODE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.NAME_VN) Then
                lst = lst.Where(Function(f) f.NAME_VN.ToLower().Contains(_filter.NAME_VN.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.NAME_EN) Then
                lst = lst.Where(Function(f) f.NAME_EN.ToLower().Contains(_filter.NAME_EN.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ACTFLG) Then
                lst = lst.Where(Function(f) f.ACTFLG.ToLower().Contains(_filter.ACTFLG.ToLower()))
            End If
            If _filter.WORKINGDAY.HasValue Then
                lst = lst.Where(Function(f) f.WORKINGDAY = _filter.WORKINGDAY)
            End If
            If _filter.YEAR.HasValue Then
                lst = lst.Where(Function(f) f.YEAR = _filter.YEAR)
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

    Public Function InsertHolidayGerenal(ByVal objTitle As AT_HOLIDAY_GENERALDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New AT_HOLIDAY_GENERAL
        Dim iCount As Integer = 0
        Try
            objTitleData.ID = Utilities.GetNextSequence(Context, Context.AT_HOLIDAY_GENERAL.EntitySet.Name)
            objTitleData.CODE = objTitle.CODE.Trim
            objTitleData.NAME_EN = objTitle.NAME_VN.Trim
            objTitleData.NAME_VN = objTitle.NAME_VN.Trim
            objTitleData.WORKINGDAY = objTitle.WORKINGDAY
            objTitleData.NOTE = objTitle.NOTE
            objTitleData.ACTFLG = objTitle.ACTFLG
            objTitleData.YEAR = objTitle.YEAR
            objTitleData.CREATED_BY = objTitle.CREATED_BY
            objTitleData.CREATED_DATE = objTitle.CREATED_DATE
            objTitleData.CREATED_LOG = objTitle.CREATED_LOG
            objTitleData.MODIFIED_BY = objTitle.MODIFIED_BY
            objTitleData.MODIFIED_DATE = objTitle.MODIFIED_DATE
            objTitleData.MODIFIED_LOG = objTitle.MODIFIED_LOG
            Context.AT_HOLIDAY_GENERAL.AddObject(objTitleData)
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

    End Function

    Public Function ValidateHolidayGerenal(ByVal _validate As AT_HOLIDAY_GENERALDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.AT_HOLIDAY_GENERAL
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.AT_HOLIDAY_GENERAL
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper).FirstOrDefault
                End If
                Return (query Is Nothing)
            ElseIf _validate.WORKINGDAY.HasValue Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.AT_HOLIDAY_GENERAL
                             Where p.WORKINGDAY = _validate.WORKINGDAY _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.AT_HOLIDAY_GENERAL
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

    Public Function ModifyHolidayGerenal(ByVal objTitle As AT_HOLIDAY_GENERALDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New AT_HOLIDAY_GENERAL With {.ID = objTitle.ID}
        Try
            objTitleData = (From p In Context.AT_HOLIDAY_GENERAL Where p.ID = objTitleData.ID).SingleOrDefault
            objTitleData.ID = objTitle.ID
            objTitleData.CODE = objTitle.CODE.Trim
            objTitleData.NAME_EN = objTitle.NAME_VN.Trim
            objTitleData.NAME_VN = objTitle.NAME_VN.Trim
            objTitleData.WORKINGDAY = objTitle.WORKINGDAY
            objTitleData.NOTE = objTitle.NOTE
            objTitleData.YEAR = objTitle.YEAR
            objTitleData.CREATED_BY = objTitle.CREATED_BY
            objTitleData.CREATED_DATE = objTitle.CREATED_DATE
            objTitleData.CREATED_LOG = objTitle.CREATED_LOG
            objTitleData.MODIFIED_BY = objTitle.MODIFIED_BY
            objTitleData.MODIFIED_DATE = objTitle.MODIFIED_DATE
            objTitleData.MODIFIED_LOG = objTitle.MODIFIED_LOG
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

    End Function

    Public Function ActiveHolidayGerenal(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstData As List(Of AT_HOLIDAY_GENERAL)
        Try
            lstData = (From p In Context.AT_HOLIDAY_GENERAL Where lstID.Contains(p.ID)).ToList
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

    Public Function DeleteHolidayGerenal(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstHolidayData As List(Of AT_HOLIDAY_GENERAL)
        Try
            lstHolidayData = (From p In Context.AT_HOLIDAY_GENERAL Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstHolidayData.Count - 1
                Context.AT_HOLIDAY_GENERAL.DeleteObject(lstHolidayData(index))
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

#Region "Danh mục kiểu công"
    Public Function GetSignByPage(ByVal pagecode As String) As List(Of AT_TIME_MANUALDTO)
        Dim query = From p In Context.AT_TIME_MANUAL
                    Where p.ACTFLG = "A" _
                    And p.CODE = "RDT" Or p.CODE = "RVS"
        Dim lst = query.Select(Function(p) New AT_TIME_MANUALDTO With {
                                   .ID = p.ID,
                                   .CODE = p.CODE,
                                   .NAME_VN = p.NAME})
        Return lst.ToList
    End Function

    Public Function GetAT_FML(ByVal _filter As AT_FMLDTO,
                                Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_FMLDTO)
        Try

            Dim query = From p In Context.AT_FML

            Dim lst = query.Select(Function(p) New AT_FMLDTO With {
                                       .ID = p.ID,
                                       .CODE = p.CODE,
                                       .NAME_EN = p.NAME_EN,
                                       .NAME_VN = p.NAME_VN,
                                       .EFFECT_DATE = p.EFFECT_DATE,
                                       .NOTE = p.NOTE,
                                       .IS_LEAVE = p.IS_LEAVE,
                                       .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng Áp dụng"),
                                       .CREATED_DATE = p.CREATED_DATE})

            If Not String.IsNullOrEmpty(_filter.CODE) Then
                lst = lst.Where(Function(f) f.CODE.ToLower().Contains(_filter.CODE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.NAME_VN) Then
                lst = lst.Where(Function(f) f.NAME_VN.ToLower().Contains(_filter.NAME_VN.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.NAME_EN) Then
                lst = lst.Where(Function(f) f.NAME_EN.ToLower().Contains(_filter.NAME_EN.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ACTFLG) Then
                lst = lst.Where(Function(f) f.ACTFLG.ToLower().Contains(_filter.ACTFLG.ToLower()))
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

    Public Function InsertAT_FML(ByVal objTitle As AT_FMLDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New AT_FML
        Dim iCount As Integer = 0
        Try
            objTitleData.ID = Utilities.GetNextSequence(Context, Context.AT_FML.EntitySet.Name)
            objTitleData.CODE = objTitle.CODE.Trim
            'objTitleData.NAME_EN = objTitle.NAME_EN.Trim
            objTitleData.NAME_VN = objTitle.NAME_VN.Trim
            'objTitleData.EFFECT_DATE = objTitle.EFFECT_DATE
            objTitleData.NOTE = objTitle.NOTE
            objTitleData.IS_LEAVE = objTitle.IS_LEAVE
            objTitleData.ACTFLG = objTitle.ACTFLG
            Context.AT_FML.AddObject(objTitleData)
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

    End Function

    Public Function ValidateAT_FML(ByVal _validate As AT_FMLDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.AT_FML
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.AT_FML
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper).FirstOrDefault
                End If
                Return (query Is Nothing)
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ModifyAT_FML(ByVal objTitle As AT_FMLDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New AT_FML With {.ID = objTitle.ID}
        Try
            objTitleData = (From p In Context.AT_FML Where p.ID = objTitleData.ID).SingleOrDefault
            objTitleData.ID = objTitle.ID
            objTitleData.CODE = objTitle.CODE.Trim
            'objTitleData.NAME_EN = objTitle.NAME_EN.Trim
            objTitleData.NAME_VN = objTitle.NAME_VN.Trim
            objTitleData.IS_LEAVE = objTitle.IS_LEAVE
            'objTitleData.EFFECT_DATE = objTitle.EFFECT_DATE
            objTitleData.NOTE = objTitle.NOTE
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ActiveAT_FML(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstData As List(Of AT_FML)
        Try
            lstData = (From p In Context.AT_FML Where lstID.Contains(p.ID)).ToList
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

    Public Function DeleteAT_FML(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstHolidayData As List(Of AT_FML)
        Try
            lstHolidayData = (From p In Context.AT_FML Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstHolidayData.Count - 1
                Context.AT_FML.DeleteObject(lstHolidayData(index))
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

#Region "Quy định ca"
    Public Function GetAT_GSIGN(ByVal _filter As AT_GSIGNDTO,
                                Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                       Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_GSIGNDTO)
        Try

            Dim query = From p In Context.AT_GSIGN
            Dim lst = query.Select(Function(p) New AT_GSIGNDTO With {
                                      .ID = p.ID,
                                      .CODE = p.CODE,
                                      .NAME_VN = p.NAME_VN,
                                      .SOONEST_IN = p.SOONEST_IN,
                                      .LATEST_OUT = p.LATEST_OUT,
                                      .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng Áp dụng"),
                                      .CREATED_BY = p.CREATED_BY,
                                      .CREATED_DATE = p.CREATED_DATE,
                                      .CREATED_LOG = p.CREATED_LOG,
                                      .MODIFIED_BY = p.MODIFIED_BY,
                                      .MODIFIED_DATE = p.MODIFIED_DATE,
                                      .MODIFIED_LOG = p.MODIFIED_LOG})

            If Not String.IsNullOrEmpty(_filter.CODE) Then
                lst = lst.Where(Function(f) f.CODE.ToLower().Contains(_filter.CODE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.NAME_VN) Then
                lst = lst.Where(Function(f) f.NAME_VN.ToLower().Contains(_filter.NAME_VN.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ACTFLG) Then
                lst = lst.Where(Function(f) f.ACTFLG.ToLower().Contains(_filter.ACTFLG.ToLower()))
            End If
            If _filter.SOONEST_IN.HasValue Then
                lst = lst.Where(Function(f) f.SOONEST_IN = _filter.SOONEST_IN)
            End If
            If _filter.LATEST_OUT.HasValue Then
                lst = lst.Where(Function(f) f.LATEST_OUT = _filter.LATEST_OUT)
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

    Public Function InsertAT_GSIGN(ByVal objTitle As AT_GSIGNDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New AT_GSIGN
        Dim iCount As Integer = 0
        Try
            objTitleData.ID = Utilities.GetNextSequence(Context, Context.AT_GSIGN.EntitySet.Name)
            objTitleData.CODE = objTitle.CODE.Trim
            objTitleData.NAME_VN = objTitle.NAME_VN.Trim
            objTitleData.SOONEST_IN = objTitle.SOONEST_IN
            objTitleData.LATEST_OUT = objTitle.LATEST_OUT
            objTitleData.ACTFLG = objTitle.ACTFLG
            objTitleData.CREATED_BY = objTitle.CREATED_BY
            objTitleData.CREATED_DATE = objTitle.CREATED_DATE
            objTitleData.CREATED_LOG = objTitle.CREATED_LOG
            objTitleData.MODIFIED_BY = objTitle.MODIFIED_BY
            objTitleData.MODIFIED_DATE = objTitle.MODIFIED_DATE
            objTitleData.MODIFIED_LOG = objTitle.MODIFIED_LOG
            Context.AT_GSIGN.AddObject(objTitleData)
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

    End Function

    Public Function ValidateAT_GSIGN(ByVal _validate As AT_GSIGNDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.AT_GSIGN
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.AT_GSIGN
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper).FirstOrDefault
                End If
                Return (query Is Nothing)
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ModifyAT_GSIGN(ByVal objTitle As AT_GSIGNDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New AT_GSIGN With {.ID = objTitle.ID}
        Try
            objTitleData = (From p In Context.AT_GSIGN Where p.ID = objTitleData.ID).SingleOrDefault
            objTitleData.ID = objTitle.ID
            objTitleData.CODE = objTitle.CODE.Trim
            objTitleData.NAME_VN = objTitle.NAME_VN.Trim
            objTitleData.SOONEST_IN = objTitle.SOONEST_IN
            objTitleData.LATEST_OUT = objTitle.LATEST_OUT
            objTitleData.CREATED_BY = objTitle.CREATED_BY
            objTitleData.CREATED_DATE = objTitle.CREATED_DATE
            objTitleData.CREATED_LOG = objTitle.CREATED_LOG
            objTitleData.MODIFIED_BY = objTitle.MODIFIED_BY
            objTitleData.MODIFIED_DATE = objTitle.MODIFIED_DATE
            objTitleData.MODIFIED_LOG = objTitle.MODIFIED_LOG
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ActiveAT_GSIGN(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstData As List(Of AT_GSIGN)
        Try
            lstData = (From p In Context.AT_GSIGN Where lstID.Contains(p.ID)).ToList
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

    Public Function DeleteAT_GSIGN(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstHolidayData As List(Of AT_GSIGN)
        Try
            lstHolidayData = (From p In Context.AT_GSIGN Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstHolidayData.Count - 1
                Context.AT_GSIGN.DeleteObject(lstHolidayData(index))
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

#Region "Quy định đi muộn về sớm"
    Public Function GetAT_DMVS(ByVal _filter As AT_DMVSDTO,
                                  Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                     Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_DMVSDTO)
        Try

            Dim query = From p In Context.AT_DMVS
                        From t1 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.LOAIPHAT1 And f.TYPE_ID = 1035).DefaultIfEmpty
                        From t2 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.LOAIPHAT2 And f.TYPE_ID = 1035).DefaultIfEmpty
                        From t3 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.LOAIPHAT3 And f.TYPE_ID = 1035).DefaultIfEmpty
                        From t4 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.LOAIPHAT4 And f.TYPE_ID = 1035).DefaultIfEmpty
                        From t5 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.LOAIPHAT5 And f.TYPE_ID = 1035).DefaultIfEmpty
            Dim lst = query.Select(Function(p) New AT_DMVSDTO With {
                                       .ID = p.p.ID,
                                       .CODE = p.p.CODE,
                                       .NAME_VN = p.p.NAME_VN,
                                       .MUC1 = p.p.MUC1,
                                       .LOAIPHAT1 = p.p.LOAIPHAT1,
                                       .LOAIPHAT1_NAME = p.t1.NAME_VN,
                                       .GIATRI1 = p.p.GIATRI1,
                                       .MUC2 = p.p.MUC2,
                                       .LOAIPHAT2 = p.p.LOAIPHAT2,
                                       .LOAIPHAT2_NAME = p.t2.NAME_VN,
                                       .GIATRI2 = p.p.GIATRI2,
                                       .MUC3 = p.p.MUC3,
                                       .LOAIPHAT3 = p.p.LOAIPHAT3,
                                       .LOAIPHAT3_NAME = p.t3.NAME_VN,
                                       .GIATRI3 = p.p.GIATRI3,
                                       .MUC4 = p.p.MUC4,
                                       .LOAIPHAT4 = p.p.LOAIPHAT4,
                                       .LOAIPHAT4_NAME = p.t4.NAME_VN,
                                       .GIATRI4 = p.p.GIATRI4,
                                       .MUC5 = p.p.MUC5,
                                       .LOAIPHAT5 = p.p.LOAIPHAT5,
                                       .LOAIPHAT5_NAME = p.t5.NAME_VN,
                                       .GIATRI5 = p.p.GIATRI5,
                                       .ACTFLG = If(p.p.ACTFLG = "A", "Áp dụng", "Ngừng Áp dụng"),
                                       .CREATED_BY = p.p.CREATED_BY,
                                       .CREATED_DATE = p.p.CREATED_DATE,
                                       .CREATED_LOG = p.p.CREATED_LOG,
                                       .MODIFIED_BY = p.p.MODIFIED_BY,
                                       .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                       .MODIFIED_LOG = p.p.MODIFIED_LOG})

            If Not String.IsNullOrEmpty(_filter.CODE) Then
                lst = lst.Where(Function(f) f.CODE.ToLower().Contains(_filter.CODE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.NAME_VN) Then
                lst = lst.Where(Function(f) f.NAME_VN.ToLower().Contains(_filter.NAME_VN.ToLower()))
            End If
            If _filter.MUC1.HasValue Then
                lst = lst.Where(Function(f) f.MUC1 = _filter.MUC1)
            End If
            If Not String.IsNullOrEmpty(_filter.LOAIPHAT1_NAME) Then
                lst = lst.Where(Function(f) f.NAME_VN.ToLower().Contains(_filter.LOAIPHAT1_NAME.ToLower()))
            End If
            If _filter.GIATRI1.HasValue Then
                lst = lst.Where(Function(f) f.GIATRI1 = _filter.GIATRI1)
            End If
            If _filter.MUC2.HasValue Then
                lst = lst.Where(Function(f) f.MUC2 = _filter.MUC2)
            End If
            If Not String.IsNullOrEmpty(_filter.LOAIPHAT2_NAME) Then
                lst = lst.Where(Function(f) f.NAME_VN.ToLower().Contains(_filter.LOAIPHAT2_NAME.ToLower()))
            End If
            If _filter.GIATRI2.HasValue Then
                lst = lst.Where(Function(f) f.GIATRI2 = _filter.GIATRI2)
            End If
            If _filter.MUC3.HasValue Then
                lst = lst.Where(Function(f) f.MUC3 = _filter.MUC3)
            End If
            If Not String.IsNullOrEmpty(_filter.LOAIPHAT3_NAME) Then
                lst = lst.Where(Function(f) f.NAME_VN.ToLower().Contains(_filter.LOAIPHAT3_NAME.ToLower()))
            End If
            If _filter.GIATRI3.HasValue Then
                lst = lst.Where(Function(f) f.GIATRI3 = _filter.GIATRI3)
            End If
            If _filter.MUC4.HasValue Then
                lst = lst.Where(Function(f) f.MUC4 = _filter.MUC4)
            End If
            If Not String.IsNullOrEmpty(_filter.LOAIPHAT4_NAME) Then
                lst = lst.Where(Function(f) f.NAME_VN.ToLower().Contains(_filter.LOAIPHAT4_NAME.ToLower()))
            End If
            If _filter.GIATRI4.HasValue Then
                lst = lst.Where(Function(f) f.GIATRI4 = _filter.GIATRI4)
            End If
            If _filter.MUC5.HasValue Then
                lst = lst.Where(Function(f) f.MUC5 = _filter.MUC5)
            End If
            If Not String.IsNullOrEmpty(_filter.LOAIPHAT5_NAME) Then
                lst = lst.Where(Function(f) f.NAME_VN.ToLower().Contains(_filter.LOAIPHAT5_NAME.ToLower()))
            End If
            If _filter.GIATRI5.HasValue Then
                lst = lst.Where(Function(f) f.GIATRI5 = _filter.GIATRI5)
            End If
            If Not String.IsNullOrEmpty(_filter.ACTFLG) Then
                lst = lst.Where(Function(f) f.NAME_VN.ToLower().Contains(_filter.LOAIPHAT1_NAME.ToLower()))
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

    Public Function InsertAT_DMVS(ByVal objTitle As AT_DMVSDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New AT_DMVS
        Dim iCount As Integer = 0
        Try
            objTitleData.ID = Utilities.GetNextSequence(Context, Context.AT_DMVS.EntitySet.Name)
            objTitleData.CODE = objTitle.CODE.Trim
            objTitleData.NAME_VN = objTitle.NAME_VN.Trim
            objTitleData.MUC1 = objTitle.MUC1
            objTitleData.LOAIPHAT1 = objTitle.LOAIPHAT1
            objTitleData.GIATRI1 = objTitle.GIATRI1
            objTitleData.MUC2 = objTitle.MUC2
            objTitleData.LOAIPHAT2 = objTitle.LOAIPHAT2
            objTitleData.GIATRI2 = objTitle.GIATRI2
            objTitleData.MUC3 = objTitle.MUC3
            objTitleData.LOAIPHAT3 = objTitle.LOAIPHAT3
            objTitleData.GIATRI3 = objTitle.GIATRI3
            objTitleData.MUC4 = objTitle.MUC4
            objTitleData.LOAIPHAT4 = objTitle.LOAIPHAT4
            objTitleData.GIATRI4 = objTitle.GIATRI4
            objTitleData.MUC5 = objTitle.MUC5
            objTitleData.LOAIPHAT5 = objTitle.LOAIPHAT5
            objTitleData.GIATRI5 = objTitle.GIATRI5
            objTitleData.ACTFLG = objTitle.ACTFLG
            objTitleData.CREATED_BY = objTitle.CREATED_BY
            objTitleData.CREATED_DATE = objTitle.CREATED_DATE
            objTitleData.CREATED_LOG = objTitle.CREATED_LOG
            objTitleData.MODIFIED_BY = objTitle.MODIFIED_BY
            objTitleData.MODIFIED_DATE = objTitle.MODIFIED_DATE
            objTitleData.MODIFIED_LOG = objTitle.MODIFIED_LOG
            Context.AT_DMVS.AddObject(objTitleData)
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

    End Function

    Public Function ValidateAT_DMVS(ByVal _validate As AT_DMVSDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.AT_DMVS
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.AT_DMVS
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper).FirstOrDefault
                End If
                Return (query Is Nothing)
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ModifyAT_DMVS(ByVal objTitle As AT_DMVSDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New AT_DMVS With {.ID = objTitle.ID}
        Try
            objTitleData = (From p In Context.AT_DMVS Where p.ID = objTitleData.ID).SingleOrDefault
            objTitleData.ID = objTitle.ID
            objTitleData.CODE = objTitle.CODE.Trim
            objTitleData.NAME_VN = objTitle.NAME_VN.Trim
            objTitleData.MUC1 = objTitle.MUC1
            objTitleData.LOAIPHAT1 = objTitle.LOAIPHAT1
            objTitleData.GIATRI1 = objTitle.GIATRI1
            objTitleData.MUC2 = objTitle.MUC2
            objTitleData.LOAIPHAT2 = objTitle.LOAIPHAT2
            objTitleData.GIATRI2 = objTitle.GIATRI2
            objTitleData.MUC3 = objTitle.MUC3
            objTitleData.LOAIPHAT3 = objTitle.LOAIPHAT3
            objTitleData.GIATRI3 = objTitle.GIATRI3
            objTitleData.MUC4 = objTitle.MUC4
            objTitleData.LOAIPHAT4 = objTitle.LOAIPHAT4
            objTitleData.GIATRI4 = objTitle.GIATRI4
            objTitleData.MUC5 = objTitle.MUC5
            objTitleData.LOAIPHAT5 = objTitle.LOAIPHAT5
            objTitleData.GIATRI5 = objTitle.GIATRI5
            objTitleData.CREATED_BY = objTitle.CREATED_BY
            objTitleData.CREATED_DATE = objTitle.CREATED_DATE
            objTitleData.CREATED_LOG = objTitle.CREATED_LOG
            objTitleData.MODIFIED_BY = objTitle.MODIFIED_BY
            objTitleData.MODIFIED_DATE = objTitle.MODIFIED_DATE
            objTitleData.MODIFIED_LOG = objTitle.MODIFIED_LOG
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ActiveAT_DMVS(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstData As List(Of AT_DMVS)
        Try
            lstData = (From p In Context.AT_DMVS Where lstID.Contains(p.ID)).ToList
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

    Public Function DeleteAT_DMVS(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstHolidayData As List(Of AT_DMVS)
        Try
            lstHolidayData = (From p In Context.AT_DMVS Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstHolidayData.Count - 1
                Context.AT_DMVS.DeleteObject(lstHolidayData(index))
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

#Region "Danh mục ca làm việc"
    Public Function GetAT_SHIFT(ByVal _filter As AT_SHIFTDTO,
                                  Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                    Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_SHIFTDTO)
        Try

            Dim query = From p In Context.AT_SHIFT
                        From t1 In Context.AT_SHIFT.Where(Function(f) f.ID = p.SATURDAY).DefaultIfEmpty
                        From t2 In Context.AT_TIME_MANUAL.Where(Function(f) f.ID = p.SUNDAY).DefaultIfEmpty
                        From Dsvm In Context.AT_DMVS.Where(Function(f) f.ID = p.PENALIZEA).DefaultIfEmpty
                        From mn In Context.AT_TIME_MANUAL.Where(Function(f) f.ID = p.MANUAL_ID).DefaultIfEmpty()

            Dim lst = query.Select(Function(p) New AT_SHIFTDTO With {
                                       .ID = p.p.ID,
                                       .CODE = p.p.CODE,
                                       .NAME_VN = p.p.NAME_VN,
                                       .NAME_EN = p.p.NAME_EN,
                                       .MANUAL_ID = p.p.MANUAL_ID,
                                       .MANUAL_CODE = p.mn.CODE,
                                       .MANUAL_NAME = p.mn.NAME,
                                       .PENALIZEA = p.p.PENALIZEA,
                                       .PENALIZEA_NAME = p.Dsvm.NAME_VN,
                                       .SATURDAY = p.p.SATURDAY,
                                       .SATURDAY_CODE = p.t1.CODE,
                                       .SATURDAY_NAME = p.t1.NAME_VN,
                                       .SUNDAY = p.p.SUNDAY,
                                       .SUNDAY_CODE = p.t2.CODE,
                                       .SUNDAY_NAME = p.t2.NAME,
                                       .HOURS_START = p.p.HOURS_START,
                                       .HOURS_STOP = p.p.HOURS_STOP,
                                       .BREAKS_FORM = p.p.BREAKS_FORM,
                                       .BREAKS_TO = p.p.BREAKS_TO,
                                       .ACTFLG = If(p.p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                                       .NOTE = p.p.NOTE,
                                       .IS_NOON = p.p.IS_NOON,
                                       .CREATED_DATE = p.p.CREATED_DATE})

            If Not String.IsNullOrEmpty(_filter.CODE) Then
                lst = lst.Where(Function(f) f.CODE.ToLower().Contains(_filter.CODE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.NAME_VN) Then
                lst = lst.Where(Function(f) f.NAME_VN.ToLower().Contains(_filter.NAME_VN.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.NAME_EN) Then
                lst = lst.Where(Function(f) f.NAME_EN.ToLower().Contains(_filter.NAME_EN.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.MANUAL_NAME) Then
                lst = lst.Where(Function(f) f.MANUAL_NAME.ToLower().Contains(_filter.MANUAL_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.MANUAL_CODE) Then
                lst = lst.Where(Function(f) f.MANUAL_CODE.ToLower().Contains(_filter.MANUAL_CODE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ACTFLG) Then
                lst = lst.Where(Function(f) f.ACTFLG.ToLower().Contains(_filter.ACTFLG.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.PENALIZEA_NAME) Then
                lst = lst.Where(Function(f) f.PENALIZEA_NAME.ToLower().Contains(_filter.PENALIZEA_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.SATURDAY_NAME) Then
                lst = lst.Where(Function(f) f.PENALIZEA_NAME.ToLower().Contains(_filter.SATURDAY_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.SATURDAY_CODE) Then
                lst = lst.Where(Function(f) f.SATURDAY_CODE.ToLower().Contains(_filter.SATURDAY_CODE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.SUNDAY_NAME) Then
                lst = lst.Where(Function(f) f.SUNDAY_NAME.ToLower().Contains(_filter.SUNDAY_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.SUNDAY_CODE) Then
                lst = lst.Where(Function(f) f.SUNDAY_CODE.ToLower().Contains(_filter.SUNDAY_CODE.ToLower()))
            End If
            If _filter.HOURS_START.HasValue Then
                lst = lst.Where(Function(f) f.HOURS_START = _filter.HOURS_START)
            End If
            If _filter.HOURS_STOP.HasValue Then
                lst = lst.Where(Function(f) f.HOURS_STOP = _filter.HOURS_STOP)
            End If
            If _filter.BREAKS_FORM.HasValue Then
                lst = lst.Where(Function(f) f.BREAKS_FORM = _filter.BREAKS_FORM)
            End If
            If _filter.BREAKS_TO.HasValue Then
                lst = lst.Where(Function(f) f.BREAKS_TO = _filter.BREAKS_TO)
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

    Public Function InsertAT_SHIFT(ByVal objTitle As AT_SHIFTDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New AT_SHIFT
        Dim iCount As Integer = 0
        Try
            objTitleData.ID = Utilities.GetNextSequence(Context, Context.AT_SHIFT.EntitySet.Name)
            objTitleData.CODE = objTitle.CODE.Trim
            objTitleData.NAME_VN = objTitle.NAME_VN.Trim
            'objTitleData.NAME_EN = objTitle.NAME_EN.Trim
            'objTitleData.APPLY_LAW = objTitle.APPLY_LAW
            'objTitleData.PENALIZEA = objTitle.PENALIZEA
            objTitleData.SATURDAY = objTitle.SATURDAY
            objTitleData.SUNDAY = objTitle.SUNDAY
            objTitleData.MANUAL_ID = objTitle.MANUAL_ID
            objTitleData.HOURS_START = objTitle.HOURS_START
            objTitleData.HOURS_STOP = objTitle.HOURS_STOP
            objTitleData.BREAKS_FORM = objTitle.BREAKS_FORM
            objTitleData.BREAKS_TO = objTitle.BREAKS_TO
            objTitleData.NOTE = objTitle.NOTE
            objTitleData.IS_NOON = objTitle.IS_NOON
            objTitleData.ACTFLG = objTitle.ACTFLG
            objTitleData.CREATED_BY = objTitle.CREATED_BY
            objTitleData.CREATED_DATE = objTitle.CREATED_DATE
            objTitleData.CREATED_LOG = objTitle.CREATED_LOG
            objTitleData.MODIFIED_BY = objTitle.MODIFIED_BY
            objTitleData.MODIFIED_DATE = objTitle.MODIFIED_DATE
            objTitleData.MODIFIED_LOG = objTitle.MODIFIED_LOG
            Context.AT_SHIFT.AddObject(objTitleData)
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

    End Function

    Public Function ValidateAT_SHIFT(ByVal _validate As AT_SHIFTDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.AT_SHIFT
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.AT_SHIFT
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper).FirstOrDefault
                End If
                Return (query Is Nothing)
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ModifyAT_SHIFT(ByVal objTitle As AT_SHIFTDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New AT_SHIFT With {.ID = objTitle.ID}
        Try
            objTitleData = (From p In Context.AT_SHIFT Where p.ID = objTitleData.ID).SingleOrDefault
            objTitleData.ID = objTitle.ID
            objTitleData.CODE = objTitle.CODE.Trim
            objTitleData.NAME_VN = objTitle.NAME_VN.Trim
            'objTitleData.NAME_EN = objTitle.NAME_EN.Trim
            'objTitleData.APPLY_LAW = objTitle.APPLY_LAW
            'objTitleData.PENALIZEA = objTitle.PENALIZEA
            objTitleData.SATURDAY = objTitle.SATURDAY
            objTitleData.SUNDAY = objTitle.SUNDAY
            objTitleData.MANUAL_ID = objTitle.MANUAL_ID
            objTitleData.HOURS_START = objTitle.HOURS_START
            objTitleData.HOURS_STOP = objTitle.HOURS_STOP
            objTitleData.BREAKS_FORM = objTitle.BREAKS_FORM
            objTitleData.BREAKS_TO = objTitle.BREAKS_TO
            objTitleData.NOTE = objTitle.NOTE
            objTitleData.IS_NOON = objTitle.IS_NOON
            objTitleData.CREATED_BY = objTitle.CREATED_BY
            objTitleData.CREATED_DATE = objTitle.CREATED_DATE
            objTitleData.CREATED_LOG = objTitle.CREATED_LOG
            objTitleData.MODIFIED_BY = objTitle.MODIFIED_BY
            objTitleData.MODIFIED_DATE = objTitle.MODIFIED_DATE
            objTitleData.MODIFIED_LOG = objTitle.MODIFIED_LOG
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ActiveAT_SHIFT(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstData As List(Of AT_SHIFT)
        Try
            lstData = (From p In Context.AT_SHIFT Where lstID.Contains(p.ID)).ToList
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

    Public Function DeleteAT_SHIFT(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstHolidayData As List(Of AT_SHIFT)
        Try
            lstHolidayData = (From p In Context.AT_SHIFT Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstHolidayData.Count - 1
                Context.AT_SHIFT.DeleteObject(lstHolidayData(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteCostCenter")
            Throw ex
        End Try
    End Function

    Public Function GetAT_TIME_MANUALBINCOMBO() As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_ATTENDANCE_LIST.GETAT_TIME_MANUAL",
                                           New With {.P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Thiết lập số ngày nghỉ chế độ theo đối tượng"
    Public Function GetAT_Holiday_Object(ByVal _filter As AT_HOLIDAY_OBJECTDTO,
                                  Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                   Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_HOLIDAY_OBJECTDTO)
        Try

            Dim query = From p In Context.AT_HOLIDAY_OBJECT
                        From t In Context.OT_OTHER_LIST.Where(Function(F) F.ID = p.EMP_OBJECT).DefaultIfEmpty
                        From FML In Context.AT_FML.Where(Function(F) F.ID = p.TYPE_SHIT).DefaultIfEmpty

            Dim lst = query.Select(Function(p) New AT_HOLIDAY_OBJECTDTO With {
                                       .ID = p.p.ID,
                                       .EMP_OBJECT = p.p.EMP_OBJECT,
                                       .EMP_OBJECT_NAME = p.t.NAME_VN,
                                       .TYPE_SHIT = p.p.TYPE_SHIT,
                                       .TYPE_SHIT_NAME = p.FML.NAME_VN,
                                       .SALARIED_DATES = p.p.SALARIED_DATES,
                                       .SALARIED_DATES_CB = p.p.SALARIED_DATES_CB,
                                       .EFFECT_DATE = p.p.EFFECT_DATE,
                                       .NOTE = p.p.NOTE,
                                       .ACTFLG = If(p.p.ACTFLG = "A", "Áp dụng", "Ngừng Áp dụng"),
                                       .CREATED_BY = p.p.CREATED_BY,
                                       .CREATED_DATE = p.p.CREATED_DATE,
                                       .CREATED_LOG = p.p.CREATED_LOG,
                                       .MODIFIED_BY = p.p.MODIFIED_BY,
                                       .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                       .MODIFIED_LOG = p.p.MODIFIED_LOG})

            If Not String.IsNullOrEmpty(_filter.EMP_OBJECT_NAME) Then
                lst = lst.Where(Function(f) f.EMP_OBJECT_NAME.ToLower().Contains(_filter.EMP_OBJECT_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TYPE_SHIT_NAME) Then
                lst = lst.Where(Function(f) f.TYPE_SHIT_NAME.ToLower().Contains(_filter.TYPE_SHIT_NAME.ToLower()))
            End If
            If _filter.SALARIED_DATES_CB.HasValue Then
                lst = lst.Where(Function(f) f.SALARIED_DATES_CB = _filter.SALARIED_DATES_CB)
            End If
            If _filter.SALARIED_DATES.HasValue Then
                lst = lst.Where(Function(f) f.SALARIED_DATES = _filter.SALARIED_DATES)
            End If

            If _filter.EFFECT_DATE.HasValue Then
                lst = lst.Where(Function(f) f.EFFECT_DATE = _filter.EFFECT_DATE)
            End If
            If Not String.IsNullOrEmpty(_filter.NOTE) Then
                lst = lst.Where(Function(f) f.NOTE.ToLower().Contains(_filter.NOTE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ACTFLG) Then
                lst = lst.Where(Function(f) f.ACTFLG.ToLower().Contains(_filter.ACTFLG.ToLower()))
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

    Public Function InsertAT_Holiday_Object(ByVal objTitle As AT_HOLIDAY_OBJECTDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New AT_HOLIDAY_OBJECT
        Dim iCount As Integer = 0
        Try
            objTitleData.ID = Utilities.GetNextSequence(Context, Context.AT_HOLIDAY_OBJECT.EntitySet.Name)
            objTitleData.EMP_OBJECT = objTitle.EMP_OBJECT
            objTitleData.TYPE_SHIT = objTitle.TYPE_SHIT
            objTitleData.SALARIED_DATES = objTitle.SALARIED_DATES
            objTitleData.SALARIED_DATES_CB = objTitle.SALARIED_DATES_CB
            objTitleData.EFFECT_DATE = objTitle.EFFECT_DATE
            objTitleData.NOTE = objTitle.NOTE
            objTitleData.ACTFLG = objTitle.ACTFLG
            objTitleData.CREATED_BY = objTitle.CREATED_BY
            objTitleData.CREATED_DATE = objTitle.CREATED_DATE
            objTitleData.CREATED_LOG = objTitle.CREATED_LOG
            objTitleData.MODIFIED_BY = objTitle.MODIFIED_BY
            objTitleData.MODIFIED_DATE = objTitle.MODIFIED_DATE
            objTitleData.MODIFIED_LOG = objTitle.MODIFIED_LOG
            Context.AT_HOLIDAY_OBJECT.AddObject(objTitleData)
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

    End Function

    Public Function ValidateAT_Holiday_Object(ByVal _validate As AT_HOLIDAY_OBJECTDTO)
        Dim query
        Try
            'If _validate.EMP_OBJECT <> Nothing Then
            '    If _validate.ID <> 0 Then
            '        query = (From p In Context.AT_HOLIDAY_OBJECT
            '                 Where p.EMP_OBJECT.ToUpper = _validate.EMP_OBJECT.ToUpper _
            '                 And p.ID <> _validate.ID).FirstOrDefault
            '    Else
            '        query = (From p In Context.AT_HOLIDAY_OBJECT
            '                 Where p.EMP_OBJECT.ToUpper = _validate.EMP_OBJECT.ToUpper).FirstOrDefault
            '    End If
            '    Return (query Is Nothing)
            'End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ModifyAT_Holiday_Object(ByVal objTitle As AT_HOLIDAY_OBJECTDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New AT_HOLIDAY_OBJECT With {.ID = objTitle.ID}
        Try
            objTitleData = (From p In Context.AT_HOLIDAY_OBJECT Where p.ID = objTitleData.ID).SingleOrDefault
            objTitleData.EMP_OBJECT = objTitle.EMP_OBJECT
            objTitleData.TYPE_SHIT = objTitle.TYPE_SHIT
            objTitleData.SALARIED_DATES = objTitle.SALARIED_DATES
            objTitleData.SALARIED_DATES_CB = objTitle.SALARIED_DATES_CB
            objTitleData.EFFECT_DATE = objTitle.EFFECT_DATE
            objTitleData.NOTE = objTitle.NOTE
            objTitleData.ACTFLG = objTitle.ACTFLG
            objTitleData.CREATED_BY = objTitle.CREATED_BY
            objTitleData.CREATED_DATE = objTitle.CREATED_DATE
            objTitleData.CREATED_LOG = objTitle.CREATED_LOG
            objTitleData.MODIFIED_BY = objTitle.MODIFIED_BY
            objTitleData.MODIFIED_DATE = objTitle.MODIFIED_DATE
            objTitleData.MODIFIED_LOG = objTitle.MODIFIED_LOG
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ActiveAT_Holiday_Object(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstData As List(Of AT_HOLIDAY_OBJECT)
        Try
            lstData = (From p In Context.AT_HOLIDAY_OBJECT Where lstID.Contains(p.ID)).ToList
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

    Public Function DeleteAT_Holiday_Object(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstHolidayData As List(Of AT_HOLIDAY_OBJECT)
        Try
            lstHolidayData = (From p In Context.AT_HOLIDAY_OBJECT Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstHolidayData.Count - 1
                Context.AT_HOLIDAY_OBJECT.DeleteObject(lstHolidayData(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteHolidayObject")
            Throw ex
        End Try
    End Function
#End Region

#Region "Thiết lập chấm công theo cấp nhân sự"
    Public Function GetAT_SETUP_SPECIAL(ByVal _filter As AT_SETUP_SPECIALDTO,
                                  Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                   Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_SETUP_SPECIALDTO)
        Try

            Dim query = From p In Context.AT_SETUP_SPECIAL
                        From S In Context.HU_STAFF_RANK.Where(Function(F) F.ID = p.STAFF_RANK_ID).DefaultIfEmpty
            Dim lst = query.Select(Function(p) New AT_SETUP_SPECIALDTO With {
                                      .ID = p.p.ID,
                                      .STAFF_RANK_ID = p.p.STAFF_RANK_ID,
                                      .STAFF_RANK_NAME = p.S.NAME,
                                      .NUMBER_SWIPECARD = p.p.NUMBER_SWIPECARD,
                                      .NOTE = p.p.NOTE,
                                      .ACTFLG = If(p.p.ACTFLG = "A", "Áp dụng", "Ngừng Áp dụng"),
                                      .CREATED_BY = p.p.CREATED_BY,
                                      .CREATED_DATE = p.p.CREATED_DATE,
                                      .CREATED_LOG = p.p.CREATED_LOG,
                                      .MODIFIED_BY = p.p.MODIFIED_BY,
                                      .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                      .MODIFIED_LOG = p.p.MODIFIED_LOG})

            If Not String.IsNullOrEmpty(_filter.STAFF_RANK_NAME) Then
                lst = lst.Where(Function(f) f.STAFF_RANK_NAME.ToLower().Contains(_filter.STAFF_RANK_NAME.ToLower()))
            End If
            If _filter.NUMBER_SWIPECARD.HasValue Then
                lst = lst.Where(Function(f) f.NUMBER_SWIPECARD = _filter.NUMBER_SWIPECARD)
            End If
            If Not String.IsNullOrEmpty(_filter.NOTE) Then
                lst = lst.Where(Function(f) f.NOTE.ToLower().Contains(_filter.NOTE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ACTFLG) Then
                lst = lst.Where(Function(f) f.ACTFLG.ToLower().Contains(_filter.ACTFLG.ToLower()))
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

    Public Function InsertAT_SETUP_SPECIAL(ByVal objTitle As AT_SETUP_SPECIALDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New AT_SETUP_SPECIAL
        Dim iCount As Integer = 0
        Try
            objTitleData.ID = Utilities.GetNextSequence(Context, Context.AT_SETUP_SPECIAL.EntitySet.Name)
            'objTitleData.EMPLOYEE_ID = objTitle.EMPLOYEE_ID
            'objTitleData.ORG_ID = objTitle.ORG_ID
            'objTitleData.POS_ID = objTitle.ORG_ID
            'objTitleData.DATE_FROM = objTitle.DATE_FROM
            'objTitleData.DATE_TO = objTitle.DATE_TO
            objTitleData.STAFF_RANK_ID = objTitle.STAFF_RANK_ID
            objTitleData.NUMBER_SWIPECARD = objTitle.NUMBER_SWIPECARD
            objTitleData.NOTE = objTitle.NOTE
            objTitleData.ACTFLG = objTitle.ACTFLG
            objTitleData.CREATED_BY = objTitle.CREATED_BY
            objTitleData.CREATED_DATE = objTitle.CREATED_DATE
            objTitleData.CREATED_LOG = objTitle.CREATED_LOG
            objTitleData.MODIFIED_BY = objTitle.MODIFIED_BY
            objTitleData.MODIFIED_DATE = objTitle.MODIFIED_DATE
            objTitleData.MODIFIED_LOG = objTitle.MODIFIED_LOG
            Context.AT_SETUP_SPECIAL.AddObject(objTitleData)
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

    End Function

    Public Function ValidateAT_SETUP_SPECIAL(ByVal _validate As AT_SETUP_SPECIALDTO)
        Dim query
        Try
            If _validate.STAFF_RANK_ID <> 0 Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.AT_SETUP_SPECIAL
                             Where p.STAFF_RANK_ID = _validate.STAFF_RANK_ID _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.AT_SETUP_SPECIAL
                             Where p.STAFF_RANK_ID = _validate.STAFF_RANK_ID).FirstOrDefault
                End If
                Return (query Is Nothing)
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ModifyAT_SETUP_SPECIAL(ByVal objTitle As AT_SETUP_SPECIALDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New AT_SETUP_SPECIAL With {.ID = objTitle.ID}
        Try
            objTitleData = (From p In Context.AT_SETUP_SPECIAL Where p.ID = objTitleData.ID).SingleOrDefault
            'objTitleData.ID = objTitle.ID
            'objTitleData.EMPLOYEE_ID = objTitle.EMPLOYEE_ID
            'objTitleData.ORG_ID = objTitle.ORG_ID
            'objTitleData.POS_ID = objTitle.ORG_ID
            'objTitleData.DATE_FROM = objTitle.DATE_FROM
            'objTitleData.DATE_TO = objTitle.DATE_TO
            objTitleData.STAFF_RANK_ID = objTitle.STAFF_RANK_ID
            objTitleData.NUMBER_SWIPECARD = objTitle.NUMBER_SWIPECARD
            objTitleData.NOTE = objTitle.NOTE
            objTitleData.CREATED_BY = objTitle.CREATED_BY
            objTitleData.CREATED_DATE = objTitle.CREATED_DATE
            objTitleData.CREATED_LOG = objTitle.CREATED_LOG
            objTitleData.MODIFIED_BY = objTitle.MODIFIED_BY
            objTitleData.MODIFIED_DATE = objTitle.MODIFIED_DATE
            objTitleData.MODIFIED_LOG = objTitle.MODIFIED_LOG
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ActiveAT_SETUP_SPECIAL(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstData As List(Of AT_SETUP_SPECIAL)
        Try
            lstData = (From p In Context.AT_SETUP_SPECIAL Where lstID.Contains(p.ID)).ToList
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

    Public Function DeleteAT_SETUP_SPECIAL(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lsAT_SetUp_Special As List(Of AT_SETUP_SPECIAL)
        Try
            lsAT_SetUp_Special = (From p In Context.AT_SETUP_SPECIAL Where lstID.Contains(p.ID)).ToList
            For index = 0 To lsAT_SetUp_Special.Count - 1
                Context.AT_SETUP_SPECIAL.DeleteObject(lsAT_SetUp_Special(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteAT_SetUp")
            Throw ex
        End Try
    End Function
#End Region

#Region "Thiết lập chấm công theo nhân viên"
    Public Function GetAT_SETUP_TIME_EMP(ByVal _filter As AT_SETUP_TIME_EMPDTO,
                                  Optional ByVal PageIndex As Integer = 0,
                                  Optional ByVal PageSize As Integer = Integer.MaxValue,
                                  Optional ByRef Total As Integer = 0,
                                   Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_SETUP_TIME_EMPDTO)
        Try

            Dim query = From p In Context.AT_SETUP_TIME_EMP
                        From S In Context.HU_EMPLOYEE.Where(Function(F) F.ID = p.EMPLOYEE_ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(F) S.TITLE_ID = F.ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(F) S.ORG_ID = F.ID).DefaultIfEmpty
            Dim lst = query.Select(Function(p) New AT_SETUP_TIME_EMPDTO With {
                                      .ID = p.p.ID,
                                      .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                      .EMPLOYEE_CODE = p.S.EMPLOYEE_CODE,
                                      .EMPLOYEE_NAME = p.S.FULLNAME_VN,
                                      .ORG_ID = p.S.ORG_ID,
                                      .ORG_NAME = p.o.NAME_VN,
                                      .ORG_DESC = p.o.DESCRIPTION_PATH,
                                      .TITLE_ID = p.S.TITLE_ID,
                                      .TITLE_NAME = p.t.NAME_VN,
                                      .NUMBER_SWIPECARD = p.p.NUMBER_SWIPECARD,
                                      .NOTE = p.p.NOTE,
                                      .ACTFLG = If(p.p.ACTFLG = "A", "Áp dụng", "Ngừng Áp dụng"),
                                      .CREATED_BY = p.p.CREATED_BY,
                                      .CREATED_DATE = p.p.CREATED_DATE,
                                      .CREATED_LOG = p.p.CREATED_LOG,
                                      .MODIFIED_BY = p.p.MODIFIED_BY,
                                      .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                      .MODIFIED_LOG = p.p.MODIFIED_LOG})

            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                lst = lst.Where(Function(f) f.EMPLOYEE_CODE.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_NAME) Then
                lst = lst.Where(Function(f) f.EMPLOYEE_NAME.ToLower().Contains(_filter.EMPLOYEE_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TITLE_NAME) Then
                lst = lst.Where(Function(f) f.TITLE_NAME.ToLower().Contains(_filter.TITLE_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                lst = lst.Where(Function(f) f.ORG_NAME.ToLower().Contains(_filter.ORG_NAME.ToLower()))
            End If
            If _filter.NUMBER_SWIPECARD.HasValue Then
                lst = lst.Where(Function(f) f.NUMBER_SWIPECARD = _filter.NUMBER_SWIPECARD)
            End If
            If Not String.IsNullOrEmpty(_filter.NOTE) Then
                lst = lst.Where(Function(f) f.NOTE.ToLower().Contains(_filter.NOTE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ACTFLG) Then
                lst = lst.Where(Function(f) f.ACTFLG.ToLower().Contains(_filter.ACTFLG.ToLower()))
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

    Public Function InsertAT_SETUP_TIME_EMP(ByVal objTitle As AT_SETUP_TIME_EMPDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New AT_SETUP_TIME_EMP
        Dim iCount As Integer = 0
        Try
            objTitleData.ID = Utilities.GetNextSequence(Context, Context.AT_SETUP_TIME_EMP.EntitySet.Name)
            objTitleData.EMPLOYEE_ID = objTitle.EMPLOYEE_ID
            objTitleData.NUMBER_SWIPECARD = objTitle.NUMBER_SWIPECARD
            objTitleData.NOTE = objTitle.NOTE
            objTitleData.ACTFLG = objTitle.ACTFLG
            objTitleData.CREATED_BY = objTitle.CREATED_BY
            objTitleData.CREATED_DATE = objTitle.CREATED_DATE
            objTitleData.CREATED_LOG = objTitle.CREATED_LOG
            objTitleData.MODIFIED_BY = objTitle.MODIFIED_BY
            objTitleData.MODIFIED_DATE = objTitle.MODIFIED_DATE
            objTitleData.MODIFIED_LOG = objTitle.MODIFIED_LOG
            Context.AT_SETUP_TIME_EMP.AddObject(objTitleData)
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

    End Function

    Public Function ValidateAT_SETUP_TIME_EMP(ByVal _validate As AT_SETUP_TIME_EMPDTO)
        Dim query
        Try
            If _validate.EMPLOYEE_ID <> 0 Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.AT_SETUP_TIME_EMP
                             Where p.EMPLOYEE_ID = _validate.EMPLOYEE_ID _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.AT_SETUP_TIME_EMP
                             Where p.EMPLOYEE_ID = _validate.EMPLOYEE_ID).FirstOrDefault
                End If
                Return (query Is Nothing)
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ModifyAT_SETUP_TIME_EMP(ByVal objTitle As AT_SETUP_TIME_EMPDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New AT_SETUP_TIME_EMP With {.ID = objTitle.ID}
        Try
            objTitleData = (From p In Context.AT_SETUP_TIME_EMP Where p.ID = objTitleData.ID).SingleOrDefault
            objTitleData.EMPLOYEE_ID = objTitle.EMPLOYEE_ID
            objTitleData.NUMBER_SWIPECARD = objTitle.NUMBER_SWIPECARD
            objTitleData.NOTE = objTitle.NOTE
            objTitleData.CREATED_BY = objTitle.CREATED_BY
            objTitleData.CREATED_DATE = objTitle.CREATED_DATE
            objTitleData.CREATED_LOG = objTitle.CREATED_LOG
            objTitleData.MODIFIED_BY = objTitle.MODIFIED_BY
            objTitleData.MODIFIED_DATE = objTitle.MODIFIED_DATE
            objTitleData.MODIFIED_LOG = objTitle.MODIFIED_LOG
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ActiveAT_SETUP_TIME_EMP(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstData As List(Of AT_SETUP_TIME_EMP)
        Try
            lstData = (From p In Context.AT_SETUP_TIME_EMP Where lstID.Contains(p.ID)).ToList
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

    Public Function DeleteAT_SETUP_TIME_EMP(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lsAT_SetUp_time_emp As List(Of AT_SETUP_TIME_EMP)
        Try
            lsAT_SetUp_time_emp = (From p In Context.AT_SETUP_TIME_EMP Where lstID.Contains(p.ID)).ToList
            For index = 0 To lsAT_SetUp_time_emp.Count - 1
                Context.AT_SETUP_TIME_EMP.DeleteObject(lsAT_SetUp_time_emp(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteAT_SetUp")
            Throw ex
        End Try
    End Function
#End Region

#Region "Đăng ký máy chấm công"
    Public Function GetAT_TERMINAL(ByVal _filter As AT_TERMINALSDTO,
                                 Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                   Optional ByVal Sorts As String = "CREATED_DATE desc",
                                   Optional ByVal log As UserLog = Nothing) As List(Of AT_TERMINALSDTO)
        Try

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username.ToUpper,
                                           .P_ORGID = _filter.ORG_ID,
                                           .P_ISDISSOLVE = _filter.IS_DISSOLVE})
            End Using

            Dim query = From p In Context.AT_TERMINALS
                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) p.ORG_ID = f.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper)
                        Select New AT_TERMINALSDTO With {
                                       .ID = p.ID,
                                       .TERMINAL_CODE = p.TERMINAL_CODE,
                                       .TERMINAL_NAME = p.TERMINAL_NAME,
                                       .ADDRESS_PLACE = p.ADDRESS_PLACE,
                                       .TERMINAL_IP = p.TERMINAL_IP,
                                       .ORG_ID = p.ORG_ID,
                                       .ORG_NAME = org.NAME_VN,
                                       .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng Áp dụng"),
                                       .NOTE = p.NOTE,
                                       .PASS = p.PASS,
                                       .PORT = p.PORT,
                                       .CREATED_DATE = p.CREATED_DATE,
                                       .CREATED_BY = p.CREATED_BY,
                                       .CREATED_LOG = p.CREATED_LOG,
                                       .MODIFIED_DATE = p.MODIFIED_DATE,
                                       .MODIFIED_BY = p.MODIFIED_BY,
                                       .MODIFIED_LOG = p.MODIFIED_LOG}

            Dim lst = query

            If Not String.IsNullOrEmpty(_filter.TERMINAL_CODE) Then
                lst = lst.Where(Function(f) f.TERMINAL_CODE.ToLower().Contains(_filter.TERMINAL_CODE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TERMINAL_NAME) Then
                lst = lst.Where(Function(f) f.TERMINAL_NAME.ToLower().Contains(_filter.TERMINAL_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TERMINAL_IP) Then
                lst = lst.Where(Function(f) f.TERMINAL_IP.ToLower().Contains(_filter.TERMINAL_IP.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ACTFLG) Then
                lst = lst.Where(Function(f) f.ACTFLG.ToLower().Contains(_filter.ACTFLG.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                lst = lst.Where(Function(f) f.ORG_NAME.ToLower().Contains(_filter.ORG_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.NOTE) Then
                lst = lst.Where(Function(f) f.NOTE.ToLower().Contains(_filter.NOTE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ADDRESS_PLACE) Then
                lst = lst.Where(Function(f) f.ADDRESS_PLACE.ToLower().Contains(_filter.ADDRESS_PLACE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.PASS) Then
                lst = lst.Where(Function(f) f.PASS.ToLower().Contains(_filter.PASS.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.PORT) Then
                lst = lst.Where(Function(f) f.PORT.ToLower().Contains(_filter.PORT.ToLower()))
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

    Public Function GetAT_TERMINAL_STATUS(ByVal _filter As AT_TERMINALSDTO,
                                 Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                   Optional ByVal Sorts As String = "CREATED_DATE desc",
                                   Optional ByVal log As UserLog = Nothing) As List(Of AT_TERMINALSDTO)
        Try

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username.ToUpper,
                                           .P_ORGID = 46,
                                           .P_ISDISSOLVE = 0})
            End Using


            Dim query = From p In Context.AT_TERMINALS
                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) p.ORG_ID = f.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper)
                        Where p.ACTFLG = "A"
                        Select p

            Dim lst = query.Select(Function(p) New AT_TERMINALSDTO With {
                                       .ID = p.ID,
                                       .TERMINAL_CODE = p.TERMINAL_CODE,
                                       .TERMINAL_NAME = p.TERMINAL_NAME,
                                       .ADDRESS_PLACE = p.ADDRESS_PLACE,
                                       .TERMINAL_IP = p.TERMINAL_IP,
                                       .TERMINAL_STATUS = p.TERMINAL_STATUS,
                                       .LAST_TIME_STATUS = p.LAST_TIME_STATUS,
                                       .LAST_TIME_UPDATE = p.LAST_TIME_UPDATE,
                                       .TERMINAL_ROW = p.TERMINAL_ROW,
                                       .NOTE = p.NOTE,
                                       .PASS = p.PASS,
                                       .PORT = p.PORT,
                                       .CREATED_DATE = p.CREATED_DATE})

            If Not String.IsNullOrEmpty(_filter.TERMINAL_CODE) Then
                lst = lst.Where(Function(f) f.TERMINAL_CODE.ToLower().Contains(_filter.TERMINAL_CODE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TERMINAL_STATUS) Then
                lst = lst.Where(Function(f) f.TERMINAL_STATUS.ToLower().Contains(_filter.TERMINAL_STATUS.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TERMINAL_NAME) Then
                lst = lst.Where(Function(f) f.TERMINAL_NAME.ToLower().Contains(_filter.TERMINAL_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TERMINAL_IP) Then
                lst = lst.Where(Function(f) f.TERMINAL_IP.ToLower().Contains(_filter.TERMINAL_IP.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ACTFLG) Then
                lst = lst.Where(Function(f) f.ACTFLG.ToLower().Contains(_filter.ACTFLG.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.NOTE) Then
                lst = lst.Where(Function(f) f.NOTE.ToLower().Contains(_filter.NOTE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ADDRESS_PLACE) Then
                lst = lst.Where(Function(f) f.ADDRESS_PLACE.ToLower().Contains(_filter.ADDRESS_PLACE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.PASS) Then
                lst = lst.Where(Function(f) f.PASS.ToLower().Contains(_filter.PASS.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.PORT) Then
                lst = lst.Where(Function(f) f.PORT.ToLower().Contains(_filter.PORT.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TERMINAL_STATUS) Then
                lst = lst.Where(Function(f) f.TERMINAL_STATUS.ToLower().Contains(_filter.TERMINAL_STATUS.ToLower()))
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

    Public Function InsertAT_TERMINAL(ByVal objTitle As AT_TERMINALSDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New AT_TERMINALS
        Dim iCount As Integer = 0
        Try
            objTitleData.ID = Utilities.GetNextSequence(Context, Context.AT_TERMINALS.EntitySet.Name)
            objTitleData.TERMINAL_CODE = objTitle.TERMINAL_CODE
            objTitleData.TERMINAL_NAME = objTitle.TERMINAL_NAME
            objTitleData.ADDRESS_PLACE = objTitle.ADDRESS_PLACE
            objTitleData.TERMINAL_IP = objTitle.TERMINAL_IP
            objTitleData.ACTFLG = objTitle.ACTFLG
            objTitleData.ORG_ID = objTitle.ORG_ID
            objTitleData.NOTE = objTitle.NOTE
            objTitleData.PASS = objTitle.PASS
            objTitleData.PORT = objTitle.PORT
            Context.AT_TERMINALS.AddObject(objTitleData)
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

    End Function

    Public Function ValidateAT_TERMINAL(ByVal _validate As AT_TERMINALSDTO)
        Dim query
        Try
            If _validate.TERMINAL_CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.AT_TERMINALS
                             Where p.TERMINAL_CODE.ToUpper = _validate.TERMINAL_CODE.ToUpper _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.AT_TERMINALS
                             Where p.TERMINAL_CODE.ToUpper = _validate.TERMINAL_CODE.ToUpper).FirstOrDefault
                End If
                Return (query Is Nothing)
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ModifyAT_TERMINAL(ByVal objTitle As AT_TERMINALSDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New AT_TERMINALS With {.ID = objTitle.ID}
        Try
            objTitleData = (From p In Context.AT_TERMINALS Where p.ID = objTitleData.ID).SingleOrDefault
            objTitleData.TERMINAL_CODE = objTitle.TERMINAL_CODE
            objTitleData.TERMINAL_NAME = objTitle.TERMINAL_NAME
            objTitleData.ADDRESS_PLACE = objTitle.ADDRESS_PLACE
            objTitleData.TERMINAL_IP = objTitle.TERMINAL_IP
            objTitleData.NOTE = objTitle.NOTE
            objTitleData.PASS = objTitle.PASS
            objTitleData.PORT = objTitle.PORT
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ActiveAT_TERMINAL(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstData As List(Of AT_TERMINALS)
        Try
            lstData = (From p In Context.AT_TERMINALS Where lstID.Contains(p.ID)).ToList
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

    Public Function DeleteAT_TERMINAL(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstAT_Terminal As List(Of AT_TERMINALS)
        Try
            lstAT_Terminal = (From p In Context.AT_TERMINALS Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstAT_Terminal.Count - 1
                Context.AT_TERMINALS.DeleteObject(lstAT_Terminal(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteTerminal")
            Throw ex
        End Try
    End Function
#End Region

#Region "Đăng ký chấm công mặc định"
    Public Function GetAT_SIGNDEFAULT(ByVal _filter As AT_SIGNDEFAULTDTO,
                                Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                  Optional ByVal Sorts As String = "CREATED_DATE desc",
                                  Optional ByVal log As UserLog = Nothing) As List(Of AT_SIGNDEFAULTDTO)
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username.ToUpper,
                                           .P_ORGID = 46,
                                           .P_ISDISSOLVE = 0})
            End Using

            Dim query = From p In Context.AT_SIGNDEFAULT
                        From e In Context.HU_EMPLOYEE.Where(Function(F) F.ID = p.EMPLOYEE_ID)
                        From SH In Context.AT_SHIFT.Where(Function(F) F.ID = p.SINGDEFAULE).DefaultIfEmpty
                        From ORG In Context.HU_ORGANIZATION.Where(Function(F) F.ID = e.ORG_ID).DefaultIfEmpty
                        From s In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = ORG.ID And f.USERNAME.ToUpper = log.Username.ToUpper)
                        From TI In Context.HU_TITLE.Where(Function(F) F.ID = e.TITLE_ID).DefaultIfEmpty

            Dim lst = query.Select(Function(p) New AT_SIGNDEFAULTDTO With {
                                       .ID = p.p.ID,
                                       .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                       .EMPLOYEE_NAME = p.e.FULLNAME_VN,
                                       .TITLE_ID = p.e.TITLE_ID,
                                       .TITLE_NAME = p.TI.NAME_EN,
                                       .ORG_ID = p.e.ORG_ID,
                                       .ORG_NAME = p.ORG.NAME_VN,
                                       .ORG_DESC = p.ORG.DESCRIPTION_PATH,
                                       .EFFECT_DATE_FROM = p.p.EFFECT_DATE_FROM,
                                       .EFFECT_DATE_TO = p.p.EFFECT_DATE_TO,
                                       .SINGDEFAULE = p.p.SINGDEFAULE,
                                       .SINGDEFAULF_NAME = p.SH.CODE,
                                       .NOTE = p.p.NOTE,
                                       .ACTFLG = If(p.p.ACTFLG = "A", "Áp dụng", "Ngừng Áp dụng"),
                                       .CREATED_DATE = p.p.CREATED_DATE,
                                       .CREATED_BY = p.p.CREATED_BY,
                                       .CREATED_LOG = p.p.CREATED_LOG,
                                       .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                       .MODIFIED_BY = p.p.MODIFIED_BY,
                                       .MODIFIED_LOG = p.p.MODIFIED_LOG})

            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_NAME) Then
                lst = lst.Where(Function(f) f.EMPLOYEE_NAME.ToLower().Contains(_filter.EMPLOYEE_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                lst = lst.Where(Function(f) f.EMPLOYEE_CODE.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                lst = lst.Where(Function(f) f.ORG_NAME.ToLower().Contains(_filter.ORG_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TITLE_NAME) Then
                lst = lst.Where(Function(f) f.TITLE_NAME.ToLower().Contains(_filter.TITLE_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.SINGDEFAULF_NAME) Then
                lst = lst.Where(Function(f) f.SINGDEFAULF_NAME.ToLower().Contains(_filter.SINGDEFAULF_NAME.ToLower()))
            End If
            If _filter.EFFECT_DATE_FROM.HasValue Then
                lst = lst.Where(Function(f) f.EFFECT_DATE_FROM = _filter.EFFECT_DATE_FROM)
            End If
            If _filter.EFFECT_DATE_TO.HasValue Then
                lst = lst.Where(Function(f) f.EFFECT_DATE_TO = _filter.EFFECT_DATE_TO)
            End If
            If Not String.IsNullOrEmpty(_filter.ACTFLG) Then
                lst = lst.Where(Function(f) f.ACTFLG.ToLower().Contains(_filter.ACTFLG.ToLower()))
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

    Public Function GetAT_ListShift() As DataTable
        Try

            Dim query = From p In Context.AT_SHIFT Where p.ACTFLG = "A"
                        From t1 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.SATURDAY And f.TYPE_ID = 1036).DefaultIfEmpty
                        From t2 In Context.AT_TIME_MANUAL.Where(Function(f) f.ID = p.SUNDAY).DefaultIfEmpty
                        From Dsvm In Context.AT_DMVS.Where(Function(f) f.ID = p.PENALIZEA).DefaultIfEmpty
                        From mn In Context.AT_TIME_MANUAL.Where(Function(f) f.ID = p.MANUAL_ID).DefaultIfEmpty
                        Order By p.CODE
            Dim lst = query.Select(Function(p) New AT_SHIFTDTO With {
                                       .ID = p.p.ID,
                                       .CODE = p.p.CODE,
                                       .NAME_VN = "[" & p.p.CODE & "] " & p.p.NAME_VN,
                                       .MANUAL_NAME = "[" & p.mn.CODE & "]" & p.mn.NAME,
                                       .IS_NOON = If(p.p.IS_NOON, "X", ""),
                                       .SUNDAY_NAME = p.t2.NAME,
                                       .HOURS_START = p.p.HOURS_START,
                                       .HOURS_STOP = p.p.HOURS_STOP,
                                       .BREAKS_FORM = p.p.BREAKS_FORM,
                                       .BREAKS_TO = p.p.BREAKS_TO,
                                       .NOTE = p.p.NOTE}).ToList
            Return lst.ToTable
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function GetAT_PERIOD() As DataTable
        Try

            Dim query = From p In Context.AT_PERIOD
                        Order By p.YEAR
            Dim lst = query.Select(Function(p) New AT_PERIODDTO With {
                                       .PERIOD_ID = p.ID,
                                       .PERIOD_NAME = p.PERIOD_NAME}).ToList
            Return lst.ToTable
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function GetEmployeeID(ByVal employee_code As String, ByVal period_ID As Decimal) As DataTable
        Try
            Dim Period = (From w In Context.AT_PERIOD Where w.ID = period_ID).FirstOrDefault

            Dim query = From p In Context.HU_EMPLOYEE
                        Where p.EMPLOYEE_CODE = employee_code And (p.WORK_STATUS <> 257 Or (p.WORK_STATUS = 257 And p.TER_LAST_DATE >= Period.START_DATE))
            Dim lst = query.Select(Function(p) New EmployeeDTO With {
                                       .ID = p.ID,
                                       .EMPLOYEE_CODE = p.EMPLOYEE_CODE}).ToList
            Return lst.ToTable
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function GetEmployeeIDInSign(ByVal employee_code As String) As DataTable
        Try

            Dim query = From p In Context.HU_EMPLOYEE
                        Where p.EMPLOYEE_CODE = employee_code
            Dim lst = query.Select(Function(p) New EmployeeDTO With {
                                       .ID = p.ID,
                                       .EMPLOYEE_CODE = p.EMPLOYEE_CODE}).ToList
            Return lst.ToTable
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function GetEmployeeByTimeID(ByVal time_ID As Decimal) As DataTable
        Try

            Dim query = From p In Context.HU_EMPLOYEE
                        Where p.ITIME_ID = time_ID
            Dim lst = query.Select(Function(p) New EmployeeDTO With {
                                       .ID = p.ID,
                                       .EMPLOYEE_CODE = p.EMPLOYEE_CODE}).ToList
            Return lst.ToTable
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function InsertAT_SIGNDEFAULT(ByVal objTitle As AT_SIGNDEFAULTDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New AT_SIGNDEFAULT
        Dim iCount As Integer = 0
        Try
            objTitleData.ID = Utilities.GetNextSequence(Context, Context.AT_SIGNDEFAULT.EntitySet.Name)
            objTitleData.EMPLOYEE_ID = objTitle.EMPLOYEE_ID
            objTitleData.EFFECT_DATE_FROM = objTitle.EFFECT_DATE_FROM
            objTitleData.EFFECT_DATE_TO = objTitle.EFFECT_DATE_TO
            objTitleData.SINGDEFAULE = objTitle.SINGDEFAULE
            objTitleData.NOTE = objTitle.NOTE
            objTitleData.ACTFLG = objTitle.ACTFLG
            objTitleData.CREATED_BY = objTitle.CREATED_BY
            objTitleData.CREATED_DATE = objTitle.CREATED_DATE
            objTitleData.CREATED_LOG = objTitle.CREATED_LOG
            objTitleData.MODIFIED_BY = objTitle.MODIFIED_BY
            objTitleData.MODIFIED_DATE = objTitle.MODIFIED_DATE
            objTitleData.MODIFIED_LOG = objTitle.MODIFIED_LOG
            Context.AT_SIGNDEFAULT.AddObject(objTitleData)
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

    End Function

    Public Function ModifyAT_SIGNDEFAULT(ByVal objTitle As AT_SIGNDEFAULTDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New AT_SIGNDEFAULT With {.ID = objTitle.ID}
        Try
            objTitleData = (From p In Context.AT_SIGNDEFAULT Where p.ID = objTitleData.ID).SingleOrDefault
            objTitleData.EMPLOYEE_ID = objTitle.EMPLOYEE_ID
            objTitleData.EFFECT_DATE_FROM = objTitle.EFFECT_DATE_FROM
            objTitleData.EFFECT_DATE_TO = objTitle.EFFECT_DATE_TO
            objTitleData.SINGDEFAULE = objTitle.SINGDEFAULE
            objTitleData.NOTE = objTitle.NOTE
            objTitleData.CREATED_BY = objTitle.CREATED_BY
            objTitleData.CREATED_DATE = objTitle.CREATED_DATE
            objTitleData.CREATED_LOG = objTitle.CREATED_LOG
            objTitleData.MODIFIED_BY = objTitle.MODIFIED_BY
            objTitleData.MODIFIED_DATE = objTitle.MODIFIED_DATE
            objTitleData.MODIFIED_LOG = objTitle.MODIFIED_LOG
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ValidateAT_SIGNDEFAULT(ByVal _validate As AT_SIGNDEFAULTDTO)
        Dim query
        Try
            If _validate.EFFECT_DATE_FROM IsNot Nothing And _validate.EFFECT_DATE_TO IsNot Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.AT_SIGNDEFAULT
                             Where (_validate.EFFECT_DATE_FROM <= p.EFFECT_DATE_TO And _validate.EFFECT_DATE_TO >= p.EFFECT_DATE_FROM) _
                             And p.EMPLOYEE_ID = _validate.EMPLOYEE_ID _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.AT_SIGNDEFAULT
                             Where (_validate.EFFECT_DATE_FROM <= p.EFFECT_DATE_TO And _validate.EFFECT_DATE_TO >= p.EFFECT_DATE_FROM) _
                             And p.EMPLOYEE_ID = _validate.EMPLOYEE_ID).FirstOrDefault
                End If
                Return (query Is Nothing)
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ActiveAT_SIGNDEFAULT(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstData As List(Of AT_SIGNDEFAULT)
        Try
            lstData = (From p In Context.AT_SIGNDEFAULT Where lstID.Contains(p.ID)).ToList
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

    Public Function DeleteAT_SIGNDEFAULT(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstAT_Terminal As List(Of AT_SIGNDEFAULT)
        Try
            lstAT_Terminal = (From p In Context.AT_SIGNDEFAULT Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstAT_Terminal.Count - 1
                Context.AT_SIGNDEFAULT.DeleteObject(lstAT_Terminal(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteTerminal")
            Throw ex
        End Try
    End Function
#End Region

#Region "đăng ký nghỉ trên iportal"
    Public Function GetPlanningAppointmentByEmployee(ByVal empid As Decimal, ByVal startdate As DateTime, ByVal enddate As DateTime, _
                                                    ByVal listSign As List(Of AT_TIME_MANUALDTO)) As List(Of AT_TIMESHEET_REGISTERDTO)
        Dim rtnValue As List(Of AT_TIMESHEET_REGISTERDTO)
        Dim lstSignID As List(Of Decimal)
        Try

            Dim lstValue = (From p In Context.OT_OTHER_LIST).ToList
            lstSignID = (From p In listSign Select p.ID).ToList

            Dim qr = From p In Context.AT_TIMESHEET_REGISTER
                     Join e In Context.HU_EMPLOYEE On p.HU_EMPLOYEEID Equals e.ID
                     Join sign In Context.AT_TIME_MANUAL On p.AT_SIGNID Equals sign.ID
                     Group Join rgtext In Context.AT_TIMESHEET_REGISTER On p.ID Equals rgtext.ID Into rgt_ext = Group
                     From rgtext In rgt_ext.DefaultIfEmpty
                     Where lstSignID.Contains(p.AT_SIGNID) _
                     And p.HU_EMPLOYEEID = empid _
                     And p.WORKINGDAY >= startdate _
                     And p.WORKINGDAY <= enddate
                     Order By sign.CODE


            rtnValue = (From p In qr.AsEnumerable
                        Select New AT_TIMESHEET_REGISTERDTO With {.ID = p.p.ID,
                                                         .EMPLOYEEID = p.p.HU_EMPLOYEEID,
                                                         .EMPLOYEECODE = p.e.EMPLOYEE_CODE,
                                                         .EMPLOYEENAME = p.e.FULLNAME_VN,
                                                         .SIGNID = p.sign.ID,
                                                         .SIGNTYPE = p.sign.ID,
                                                         .SIGNCODE = p.sign.CODE,
                                                         .SIGNNAME = p.sign.NAME,
                                                         .WORKINGDAY = p.p.WORKINGDAY,
                                                         .NVALUE = p.p.NVALUE,
                                                         .SVALUE = p.p.SVALUE,
                                                         .DVALUE = p.p.DVALUE,
                                                         .NVALUE_ID = p.p.NVALUE_ID,
                                                         .NOTE = p.p.NOTE}).ToList()

            Return rtnValue
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".")
        Finally

        End Try
    End Function

    Public Function CheckRegisterPortal(ByVal Emp As EmployeeDTO, ByVal ID_REGGROUP As Guid, ByVal process As String,
                                       ByVal startdate As Date, ByVal enddate As Date, _
                                      ByVal sign_code As AT_TIMESHEET_REGISTERDTO, ByRef sAction As String) As Boolean

        Try
            Dim lstEmp As New List(Of EmployeeDTO)
            lstEmp.Add(Emp)
            Select Case sAction
                Case "ExistPortal"
                    Return ExistPortal(Emp, startdate, enddate, sign_code, sAction)
            End Select

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".CheckRegisterAppointmentByEmployee")
            Throw ex
        End Try
    End Function

    Private Function ExistPortal(ByVal Emp As EmployeeDTO, ByVal startdate As Date, ByVal enddate As Date,
                                      ByVal sign_code As AT_TIMESHEET_REGISTERDTO, ByRef sAction As String) As Boolean
        Dim signID As Decimal = sign_code.SIGNID
        Dim EmpID As Decimal = Emp.ID
        Try

            Dim query = (From p In Context.AT_PORTAL_REG
                         Where EmpID = p.ID_EMPLOYEE And _
                         p.ID_SIGN = sign_code.SIGNID And _
                         p.FROM_DATE >= startdate And _
                         p.FROM_DATE <= enddate).Count

            Return If(query > 0, False, True)
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function GetRegisterAppointmentInPortalByEmployee(ByVal empid As Decimal, ByVal startdate As Date, ByVal enddate As Date,
                                                             ByVal listSign As List(Of AT_TIME_MANUALDTO), ByVal status As List(Of Short)) As List(Of AT_TIMESHEET_REGISTERDTO)
        Dim rtnValue As List(Of AT_TIMESHEET_REGISTERDTO)
        Try
            _isAvailable = False
            Dim lstValue = (From p In Context.OT_OTHER_LIST Where p.OT_OTHER_LIST_TYPE.CODE = ATConstant.CODE_TIMELEAVE).ToList
            Dim lstSignId As List(Of Decimal) = (From p In listSign Select p.ID).ToList()
            Dim query = (From p In Context.AT_PORTAL_REG
                         From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.ID_EMPLOYEE).DefaultIfEmpty
                         From AT In Context.AT_TIME_MANUAL.Where(Function(f) f.ID = p.ID_SIGN).DefaultIfEmpty
                         Where p.ID_EMPLOYEE = empid _
                         And lstSignId.Contains(p.ID_SIGN) _
                         And p.FROM_DATE >= startdate.Date _
                         And p.FROM_DATE <= enddate.Date _
                         And status.Contains(p.STATUS)
                         Select p, e, AT).ToList

            rtnValue = (From p In query
                       Select New AT_TIMESHEET_REGISTERDTO With {.ID = p.p.ID,
                                                        .EMPLOYEEID = p.e.ID,
                                                        .EMPLOYEECODE = p.e.EMPLOYEE_CODE,
                                                        .EMPLOYEENAME = p.e.FULLNAME_VN,
                                                        .WORKINGDAY = p.p.FROM_DATE,
                                                        .SIGNID = p.p.ID_SIGN,
                                                        .MORNING_ID = p.AT.MORNING_ID,
                                                        .AFTERNOON_ID = p.AT.AFTERNOON_ID,
                                                        .NVALUE = p.p.NVALUE,
                                                        .SVALUE = p.p.SVALUE,
                                                        .DVALUE = p.p.DVALUE,
                                                        .NOTE = p.p.NOTE,
                                                        .STATUS = p.p.STATUS,
                                                        .SUBJECT = If(p.p.SVALUE = "WLEO", p.p.NVALUE, Nothing) & " " & FormatRegisterAppointmentSubjectPortal(p.AT, p.p, lstValue)}).ToList
            Return rtnValue
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".GetRegisterAppointmentInPortalByEmployee")
        Finally
            _isAvailable = True
        End Try
    End Function

    Public Function GetTotalLeaveInYear(ByVal empid As Decimal, ByVal p_year As Decimal) As Decimal
        Try
            _isAvailable = False
            Dim query = (From p In Context.AT_PORTAL_REG
                         Where p.ID_EMPLOYEE = empid _
                         And p.FROM_DATE.Value.Year = p_year And (p.STATUS = 0 Or p.STATUS = 1)
                         Select p.NVALUE).Sum

            Return query
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".GetRegisterAppointmentInPortalByEmployee")
        Finally
            _isAvailable = True
        End Try
    End Function

    Public Function InsertPortalRegister(ByVal itemRegister As AT_PORTAL_REG_DTO, ByVal log As UserLog) As Boolean
        Try
            _isAvailable = False
            Dim itemInsert As AT_PORTAL_REG
            Dim groupid As Guid = Guid.NewGuid

            If itemRegister.PROCESS = ATConstant.GSIGNCODE_LEAVE Or itemRegister.PROCESS = ATConstant.GSIGNCODE_WLEO Then
                DeleteRegisterLeavePortal(itemRegister.ID_EMPLOYEE, itemRegister.FROM_DATE,
                                          itemRegister.TO_DATE, New AT_TIME_MANUALDTO With {.ID = itemRegister.ID_SIGN}, itemRegister.PROCESS)
            End If

            While itemRegister.FROM_DATE <= itemRegister.TO_DATE
                itemInsert = New AT_PORTAL_REG
                itemInsert.ID = Utilities.GetNextSequence(Context, Context.AT_PORTAL_REG.EntitySet.Name)
                itemInsert.ID_EMPLOYEE = itemRegister.ID_EMPLOYEE
                itemInsert.ID_SIGN = itemRegister.ID_SIGN
                itemInsert.FROM_DATE = itemRegister.FROM_DATE
                itemInsert.TO_DATE = itemRegister.FROM_DATE
                If itemRegister.FROM_HOUR.HasValue Then
                    itemInsert.FROM_HOUR = itemRegister.FROM_DATE.Value.Date.AddMinutes(itemRegister.FROM_HOUR.Value.TimeOfDay.TotalMinutes)
                End If
                If itemRegister.TO_HOUR.HasValue Then
                    itemInsert.TO_HOUR = itemRegister.FROM_DATE.Value.Date.AddMinutes(itemRegister.TO_HOUR.Value.TimeOfDay.TotalMinutes)
                    If itemInsert.TO_HOUR < itemInsert.FROM_HOUR Then
                        itemInsert.TO_HOUR = itemInsert.TO_HOUR.Value.AddDays(1)
                    End If
                End If
                If itemInsert.FROM_HOUR.HasValue AndAlso itemInsert.TO_HOUR.HasValue Then
                    itemInsert.HOURCOUNT = (itemInsert.TO_HOUR.Value - itemInsert.FROM_HOUR.Value).TotalHours
                End If
                'itemInsert.TO_HOUR = itemRegister.TO_HOUR
                itemInsert.NOTE = itemRegister.NOTE
                itemInsert.NOTE_AT = itemRegister.NOTE_AT
                itemInsert.REGDATE = Date.Now
                itemInsert.STATUS = itemRegister.STATUS
                itemInsert.CREATED_BY = log.Username.ToUpper
                itemInsert.CREATED_DATE = Date.Now
                itemInsert.CREATED_LOG = log.ComputerName
                itemInsert.STATUS = RegisterStatus.Regist
                itemInsert.NVALUE = itemRegister.NVALUE
                itemInsert.SVALUE = itemRegister.PROCESS
                itemInsert.NVALUE_ID = itemRegister.NVALUE_ID
                itemInsert.ID_REGGROUP = groupid
                Context.AT_PORTAL_REG.AddObject(itemInsert)

                itemRegister.FROM_DATE = itemRegister.FROM_DATE.Value.AddDays(1)
            End While
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "InsertPortalRegister")
            Throw ex
        Finally
            _isAvailable = True
        End Try
    End Function



    Public Function DeleteRegisterLeavePortal(ByVal EmpID As Decimal, ByVal startdate As Date, ByVal enddate As Date, _
                                                  ByVal sign_code As AT_TIME_MANUALDTO, ByVal process As String) As Boolean
        Dim query As List(Of AT_PORTAL_REG)
        Dim signID = If(sign_code Is Nothing, 0, sign_code.ID)
        Dim lstSignIDNotSum As New List(Of Decimal)
        Try

            query = (From p In Context.AT_PORTAL_REG
                     Where EmpID = p.ID_EMPLOYEE _
                     And p.FROM_DATE >= startdate _
                     And p.FROM_DATE <= enddate _
                     And p.ID_SIGN <> signID Select p).ToList


            If sign_code IsNot Nothing Then
                ' xóa thông tin ký hiệu đang được đăng ký mà tồn tại trong db
                Dim delShift
                ' nếu là đăng ký nghỉ thì không cần kiểm tra theo id_sign là đăng đi DMVS thì kiểm tra thêm cả id_sing
                If process = "LEAVE" Then
                    delShift = (From p In Context.AT_PORTAL_REG
                                Where p.FROM_DATE >= startdate _
                                And p.FROM_DATE <= enddate _
                                And p.ID_SIGN = signID _
                                And p.ID_EMPLOYEE = EmpID _
                                And p.SVALUE = process _
                                Select p).ToList
                Else
                    delShift = (From p In Context.AT_PORTAL_REG
                               Where p.FROM_DATE >= startdate _
                               And p.FROM_DATE <= enddate _
                               And p.ID_SIGN = signID _
                               And p.ID_EMPLOYEE = EmpID _
                               And p.SVALUE = process _
                               And p.ID_SIGN = sign_code.ID _
                               Select p).ToList
                End If

                For Each shift In delShift
                    Context.AT_PORTAL_REG.DeleteObject(shift)
                Next
            End If

            Dim startDateInc = startdate
            Do

                Dim delShift
                If process = "LEAVE" Then
                    delShift = (From p In Context.AT_PORTAL_REG
                               Where EntityFunctions.TruncateTime(p.FROM_DATE) = EntityFunctions.TruncateTime(startDateInc) _
                               And p.ID_SIGN <> signID And EmpID = p.ID_EMPLOYEE _
                               And p.STATUS <> RegisterStatus.Approved _
                               And p.SVALUE = process
                               Select p).ToList
                Else
                    delShift = (From p In Context.AT_PORTAL_REG
                              Where EntityFunctions.TruncateTime(p.FROM_DATE) = EntityFunctions.TruncateTime(startDateInc) _
                              And p.ID_SIGN <> signID And EmpID = p.ID_EMPLOYEE _
                              And p.STATUS <> RegisterStatus.Approved _
                              And p.SVALUE = process _
                              And p.ID_SIGN = sign_code.ID _
                              Select p).ToList
                End If

                For Each shift In delShift
                    Context.AT_PORTAL_REG.DeleteObject(shift)
                Next

                startDateInc = startDateInc.AddDays(1)
            Loop Until startDateInc > enddate

            Context.SaveChanges()

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteRegisterLeaveByEmployee")
            Throw ex
        End Try
    End Function

    Public Function GetHolidayByCalender(ByVal startdate As Date, ByVal enddate As Date) As List(Of Date)
        Try
            Return (From p In Context.AT_HOLIDAY
                    Where p.WORKINGDAY >= startdate And p.WORKINGDAY <= enddate _
                    And p.ACTFLG = ATConstant.ACTFLG_ACTIVE
                     Select p.WORKINGDAY.Value).ToList()
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".")
        End Try
    End Function

    Public Function DeletePortalRegisterByDate(ByVal listappointment As List(Of AT_TIMESHEET_REGISTERDTO), ByVal listSign As List(Of AT_TIME_MANUALDTO)) As Boolean
        Dim delLstObject As List(Of AT_PORTAL_REG)
        Try
            _isAvailable = False

            delLstObject = (From z In listappointment
                            From p In Context.AT_PORTAL_REG
                            From s In listSign
                            Where p.ID_EMPLOYEE = z.EMPLOYEEID And s.ID = p.ID_SIGN _
                            And p.FROM_DATE = z.WORKINGDAY And p.STATUS <> RegisterStatus.Approved
                            Select p).ToList()

            For Each delObj As AT_PORTAL_REG In delLstObject
                Context.AT_PORTAL_REG.DeleteObject(delObj)
            Next

            Context.SaveChanges()

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".DeletePortalRegisterByDate")
        Finally
            _isAvailable = True
        End Try
    End Function

    Public Function DeletePortalRegister(ByVal id As Decimal) As Boolean
        Try
            _isAvailable = False

            Dim delObj As AT_PORTAL_REG = GetObjectById(Of AT_PORTAL_REG)(id)

            If delObj IsNot Nothing Then
                Context.AT_PORTAL_REG.DeleteObject(delObj)
                Context.SaveChanges()
            End If

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        Finally
            _isAvailable = True
        End Try
    End Function

    Public Function SendRegisterToApprove(ByVal objLstRegisterId As List(Of Decimal), ByVal process As String, ByVal currentUrl As String) As String
        Dim itemIns As AT_PORTAL_APP
        Dim datecount As Decimal
        Dim objLstRegister As List(Of AT_PORTAL_REG)
        Dim groupid As Guid = Guid.NewGuid

        objLstRegister = (From p In Context.AT_PORTAL_REG
                         Where objLstRegisterId.Contains(p.ID)
                         Select p).ToList()

        If objLstRegister Is Nothing OrElse objLstRegister.Count = 0 Then Return String.Empty

        Dim listApproveUser As List(Of ApproveUserDTO) = GetApproveUsers(objLstRegister(0).ID_EMPLOYEE, process)

        If listApproveUser Is Nothing OrElse listApproveUser.Count = 0 Then
            Return "Chưa thiết lập phê duyệt hoặc người phê duyệt không tồn tại."
        End If

        For Each itm As AT_PORTAL_REG In objLstRegister
            'itm.ID_REGGROUP = groupid
            itm.STATUS = RegisterStatus.WaitForApprove
        Next

        datecount = objLstRegister.Sum(Function(rgt) rgt.NVALUE)

        ' Tiến hành tạo bản ghi phê duyệt cho bản ghi đăng ký này
        Dim idSendMail As Decimal? = Nothing
        Dim emailInform As String = ""
        For Each itemApprove In listApproveUser
            For Each i In objLstRegister.GroupBy(Function(x) New AT_PORTAL_REG With {.ID_REGGROUP = x.ID_REGGROUP})
                itemIns = New AT_PORTAL_APP With {.ID = Utilities.GetNextSequence(Context, Context.AT_PORTAL_APP.EntitySet.Name), .ID_REGGROUP = objLstRegister(0).ID_REGGROUP,
                                                  .ID_EMPLOYEE = itemApprove.EMPLOYEE_ID,
                                                  .level = itemApprove.LEVEL}

                If itemApprove.LEVEL = 1 Then
                    itemIns.APPROVE_STATUS = RegisterStatus.WaitForApprove ' cho người phê duyệt cấp 1 có trạng thái là chờ phê duyệt.
                    itemIns.ID_REGGROUP = i.Key.ID_REGGROUP
                    'idSendMail = objLstRegister(0).ID_EMPLOYEE
                    idSendMail = itemIns.ID_EMPLOYEE

                    If datecount >= itemApprove.INFORM_DATE Then
                        emailInform = itemApprove.INFORM_EMAIL
                    End If
                Else
                    itemIns.APPROVE_STATUS = RegisterStatus.Regist    ' cho các người phê duyệt khác là đang chờ đến lượt.
                End If

                Context.AT_PORTAL_APP.AddObject(itemIns)
            Next
        Next

        Context.SaveChanges()

        'Tiến hành gửi email cho cấp 1
        If idSendMail.HasValue Then
            ' Kiểm tra nhân viên thay thế
            Dim process_id = (From s In Context.SE_APP_PROCESS
                               Where s.PROCESS_CODE = process Select s).FirstOrDefault.ID

            Dim approveExt = (From p In Context.SE_APP_SETUPEXT
                               Where p.EMPLOYEE_ID = idSendMail.Value _
                               And p.PROCESS_ID = process_id _
                               And p.FROM_DATE <= Date.Today _
                               And p.TO_DATE >= Date.Today).FirstOrDefault

            SendMail(SendMailType.RegToApp, objLstRegister(0).ID_EMPLOYEE,
                     idSendMail.Value, If(approveExt Is Nothing, Nothing, approveExt.SUB_EMPLOYEE_ID),
                     process, objLstRegister(0).ID_REGGROUP, currentUrl, emailInform)
        End If

        Return String.Empty
    End Function
#End Region

#Region "Send mail"
    Private Enum SendMailType
        RegToApp
        AppToReg
        DenyToReg
        RegToAppTime
        AppToRegTime
        DenyToRegTime
        DelToWaitApp
    End Enum

    Private Function SendMail(ByVal typeSend As SendMailType, ByVal registerId As Decimal,
                              ByVal approveId As Decimal, ByVal approveExtId As Decimal?,
                              ByVal process As String, ByVal registerRecordId As Guid,
                              ByVal url As String, Optional ByVal ccEmail As String = "",
                              Optional ByVal sReason As String = "")
        Try
            'Dim config = GetConfig(ModuleID.All)
            Dim _viewname As String = ""
            Dim _from As String = "", _to As String = "", _cc As String = "", _subject As String = "", _content As String = "", _link As String = ""

            Dim config As Dictionary(Of String, String)
            config = GetConfig(ModuleID.All)
            Dim mailfrom = If(config.ContainsKey("MailFrom"), config("MailFrom"), "")


            'Dim mailfrom = ConfigurationManager.AppSettings("EMAIL_FROM")
            If mailfrom = "" Then
                Return False
            End If

            Dim _listField As Dictionary(Of String, String) = _
                New Dictionary(Of String, String) From {{"[HR_User_Email]", ""}, _
                                                        {"[Request_User_Id]", ""}, _
                                                        {"[Request_User_Email]", ""}, _
                                                        {"[Request_User_Full_Name]", ""}, _
                                                        {"[Request_User_Position]", ""}, _
                                                        {"[Request_User_Location]", ""}, _
                                                        {"[Approve_User_Id]", ""}, _
                                                        {"[Approve_User_Email]", ""}, _
                                                        {"[ApproveExt_User_Email]", ""}, _
                                                        {"[Approve_User_Full_Name]", ""}, _
                                                        {"[Approve_User_Position]", ""}, _
                                                        {"[Approve_User_Location]", ""}, _
                                                        {"[Sign_Name]", ""}, _
                                                        {"[RJ_REASON]", ""}, _
                                                        {"[Start_Date]", ""}, _
                                                        {"[End_Date]", ""}, _
                                                        {"[DirectLink]", ""}
                                                       }

            ' lấy dữ liệu cho các field
            ' - Lấy dữ liệu của quy trình, Lấy [HR_User_Email]
            'Dim processInfo = Context.AT_APP_PROCESS.FirstOrDefault(Function(p) p.PROCESS_CODE = process)
            'If processInfo Is Nothing Then
            '    Return False
            'End If
            _listField("[HR_User_Email]") = ccEmail

            Dim registerRecordInfo = (From p In Context.AT_PORTAL_REG
                                      From s In Context.AT_TIME_MANUAL.Where(Function(F) F.ID = p.ID_SIGN).DefaultIfEmpty
                                     Where p.ID_REGGROUP = registerRecordId
                                     Select p, s
                                     Order By p.FROM_DATE).ToList()

            If registerRecordInfo Is Nothing Then
                Return False
            End If
            Dim sValueName As String = ""
            Select Case process
                Case ATConstant.GSIGNCODE_LEAVE
                    sValueName = "day"
                Case ATConstant.GSIGNCODE_OVERTIME
                    sValueName = "hour"
                Case ATConstant.GSIGNCODE_WLEO
                    sValueName = "hour"
            End Select
            ' - Lấy thông tin về mã đăng ký [Sign_Code]
            For Each item In registerRecordInfo
                If Not _listField("[Sign_Name]").Contains(item.s.NAME + "-") Then
                    Dim sSignName As String = item.s.NAME
                    Dim dValue As String = Format(registerRecordInfo _
                        .Where(Function(f) f.p.ID_SIGN = item.p.ID_SIGN) _
                        .Sum(Function(p) p.p.NVALUE.Value), "0.##")

                    _listField("[Sign_Name]") += item.s.NAME & "-" & dValue & " " & sValueName & ", "
                End If
            Next
            _listField("[Sign_Name]") = _listField("[Sign_Name]").Substring(0, _listField("[Sign_Name]").Length - 2)

            ' - Lấy thông tin [Start_Date]
            _listField("[Start_Date]") = registerRecordInfo(0).p.FROM_DATE.Value.ToString("dd/MM/yyyy")
            ' - Lấy thông tin [End_Date]
            _listField("[End_Date]") = registerRecordInfo(registerRecordInfo.Count - 1).p.FROM_DATE.Value.ToString("dd/MM/yyyy")


            ' - Lấy thông tin của người đăng ký
            Dim registerInfo = Context.HU_EMPLOYEE.SingleOrDefault(Function(p) p.ID = registerId)
            Dim infoEmail = Context.HU_EMPLOYEE_CV.SingleOrDefault(Function(p) p.EMPLOYEE_ID = registerId)
            If registerInfo Is Nothing Then
                Return False
            End If
            _listField("[Request_User_Id]") = registerInfo.EMPLOYEE_CODE
            _listField("[Request_User_Email]") = infoEmail.WORK_EMAIL
            _listField("[Request_User_Full_Name]") = registerInfo.FULLNAME_VN
            '_listField("[Request_User_Location]") = registerInfo.HU_ORGANIZATION.NAME_VN
            '_listField("[Request_User_Position]") = registerInfo.HU_TITLE.NAME_VN
            _listField("[RJ_REASON]") = sReason

            ' - Lấy thông tin của người phê duyệt
            Dim approveInfo
            Dim approveExtInfo
            If approveExtId Is Nothing Or approveExtId = 0 Then
                'approveInfo = Context.HU_EMPLOYEE.SingleOrDefault(Function(p) p.ID = approveId)
                approveInfo = (From p In Context.HU_EMPLOYEE
                               From c In Context.HU_EMPLOYEE_CV.Where(Function(F) F.EMPLOYEE_ID = p.ID).DefaultIfEmpty
                               From o In Context.HU_ORGANIZATION.Where(Function(F) F.ID = p.ORG_ID).DefaultIfEmpty
                               From t In Context.HU_TITLE.Where(Function(F) F.ID = p.TITLE_ID).DefaultIfEmpty
                               Where p.ID = approveId
                               Select p, c, o, t
                               Order By p.ID).ToList()
                If approveInfo Is Nothing Then
                    Return False
                End If

                For Each info In approveInfo
                    _listField("[Approve_User_Id]") = info.p.EMPLOYEE_CODE
                    _listField("[Approve_User_Email]") = info.c.WORK_EMAIL
                    _listField("[Approve_User_Full_Name]") = info.p.FULLNAME_VN
                    _listField("[Approve_User_Location]") = info.o.NAME_VN
                    _listField("[Approve_User_Position]") = info.t.NAME_VN
                Next

            Else
                'approveInfo = Context.HU_EMPLOYEE.SingleOrDefault(Function(p) p.ID = approveId)
                'approveExtInfo = Context.HU_EMPLOYEE.SingleOrDefault(Function(p) p.ID = approveExtId)

                approveInfo = (From p In Context.HU_EMPLOYEE
                              From c In Context.HU_EMPLOYEE_CV.Where(Function(F) F.EMPLOYEE_ID = p.ID).DefaultIfEmpty
                              From o In Context.HU_ORGANIZATION.Where(Function(F) F.ID = p.ORG_ID).DefaultIfEmpty
                              From t In Context.HU_TITLE.Where(Function(F) F.ID = p.TITLE_ID).DefaultIfEmpty
                              Where p.ID = approveId
                              Select p, c, o, t
                              Order By p.ID).FirstOrDefault()

                approveExtInfo = (From p In Context.HU_EMPLOYEE
                              From c In Context.HU_EMPLOYEE_CV.Where(Function(F) F.EMPLOYEE_ID = p.ID).DefaultIfEmpty
                              From o In Context.HU_ORGANIZATION.Where(Function(F) F.ID = p.ORG_ID).DefaultIfEmpty
                              From t In Context.HU_TITLE.Where(Function(F) F.ID = p.TITLE_ID).DefaultIfEmpty
                              Where p.ID = approveExtId
                              Select p, c, o, t
                              Order By p.ID).FirstOrDefault()


                _listField("[Approve_User_Id]") = approveExtInfo.EMPLOYEE_CODE
                _listField("[Approve_User_Email]") = approveExtInfo.HU_EMPLOYEE_CV.WORK_EMAIL
                _listField("[ApproveExt_User_Email]") = approveInfo.HU_EMPLOYEE_CV.WORK_EMAIL
                _listField("[Approve_User_Full_Name]") = approveExtInfo.FULLNAME_VN
                _listField("[Approve_User_Location]") = approveExtInfo.HU_ORGANIZATION.NAME_VN
                _listField("[Approve_User_Position]") = approveExtInfo.HU_TITLE.NAME_VN
            End If



            _link = url & ":" & If(config.ContainsKey("PortalPort"), config("PortalPort"), "") & "?fid=ctrl[0][1]&mid=Attendance"

            Select Case process
                Case ATConstant.GSIGNCODE_OVERTIME
                    _link = _link.Replace("[0]", "OT")
                Case ATConstant.GSIGNCODE_WLEO
                    _link = _link.Replace("[0]", "DMVS")
                Case ATConstant.GSIGNCODE_LEAVE
                    _link = _link.Replace("[0]", "Leave")
            End Select

            Select Case typeSend
                Case SendMailType.RegToApp
                    _from = mailfrom
                    _to = "[Approve_User_Email];[ApproveExt_User_Email]"
                    _cc = "[Request_User_Email];[HR_User_Email]"
                    _viewname = "AT_TIME_REGTOAPP.html"
                    _listField("[Directlink]") = _link.Replace("[1]", "Approve")
                    GetContentTemplate(_viewname, _subject, _content)
                Case SendMailType.AppToReg
                    _from = mailfrom
                    _to = "[Request_User_Email]"
                    _cc = "[Approve_User_Email];[HR_User_Email];[ApproveExt_User_Email]"
                    _viewname = "AT_TIME_APPTOREG.html"
                    _listField("[Directlink]") = _link.Replace("[1]", "Register")
                    GetContentTemplate(_viewname, _subject, _content)
                Case SendMailType.DenyToReg
                    _from = mailfrom
                    _to = "[Request_User_Email]"
                    _cc = "[Approve_User_Email];[HR_User_Email];[ApproveExt_User_Email]"
                    _viewname = "AT_TIME_DENYTOREG.html"
                    _listField("[Directlink]") = _link.Replace("[1]", "Register")
                    GetContentTemplate(_viewname, _subject, _content)
            End Select

            Return InsertMail(_from.ReplaceField(_listField), _to.ReplaceField(_listField),
                              _subject.ReplaceField(_listField), _content.ReplaceField(_listField),
                              _cc.ReplaceField(_listField), , _viewname)


        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Return False
        End Try
    End Function

    Public Function InsertMail(ByVal _from As String, ByVal _to As String,
                               ByVal _subject As String, ByVal _content As String,
                               Optional ByVal _cc As String = "", Optional ByVal _bcc As String = "",
                               Optional ByVal _viewName As String = "", Optional ByVal attachUrl As String = "") As Boolean
        Try

            If _from = String.Empty OrElse _to = String.Empty Then
                Return False
            End If

            Dim _newMail As New SE_MAIL
            _newMail.ID = Utilities.GetNextSequence(Context, Context.SE_MAIL.EntitySet.Name)
            _newMail.MAIL_FROM = _from
            _newMail.MAIL_TO = _to
            _newMail.MAIL_CC = _cc
            _newMail.MAIL_BCC = _bcc
            _newMail.SUBJECT = _subject
            _newMail.CONTENT = _content
            _newMail.VIEW_NAME = _viewName
            _newMail.ATTACHMENT = attachUrl
            _newMail.ACTFLG = "I"

            Context.SE_MAIL.AddObject(_newMail)

            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
#End Region

#Region "Phê duyệt đăng ký nghỉ"
    Public Function GetListSignCode(ByVal gSignCode As String) As List(Of AT_TIME_MANUALDTO)
        Try
            _isAvailable = False
            Return (From p In Context.AT_TIME_MANUAL.ToList
                    Group Join ol In Context.OT_OTHER_LIST On p.ID Equals ol.ID Into g_ol = Group
                    From pol In g_ol.DefaultIfEmpty
                    Where p.CODE = gSignCode _
                    And p.ACTFLG = ATConstant.ACTFLG_ACTIVE _
                    And pol.CODE = "RGT"
                    Select New AT_TIME_MANUALDTO With {.ID = p.ID,
                                              .CODE = p.CODE,
                                              .NAME_VN = p.NAME,
                                              .NOTE = p.NOTE}).ToList()
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        Finally
            _isAvailable = True
        End Try
    End Function

    Public Function ApprovePortalRegister(ByVal regID As Guid, ByVal approveId As Decimal,
                                          ByVal status As Integer, ByVal note As String,
                                          ByVal currentUrl As String, ByVal process As String,
                                             ByVal log As UserLog) As Boolean
        Try
            _isAvailable = False
            Dim approveRecord As AT_PORTAL_APP
            Dim registerRecord As List(Of AT_PORTAL_REG)
            Dim bExt As Boolean = False
            Dim approveExt As SE_APP_SETUPEXT
            Dim approveInfo As HU_EMPLOYEE
            Dim approveExtInfo As HU_EMPLOYEE

            approveRecord = (From a In Context.AT_PORTAL_APP
                                 Where a.ID_REGGROUP = regID _
                                 And a.ID_EMPLOYEE = approveId _
                                 And a.APPROVE_STATUS = 1
                                 Select a).FirstOrDefault



            ' Lấy bản ghi đang chờ phê duyệt của bản đăng ký đang phê duyệt
            If approveRecord Is Nothing Then


                ' Kiểm tra nhân viên thay thế
                'approveExt = (From p In Context.SE_APP_SETUPEXT
                '              From t In Context.SE_APP_PROCESS.Where(Function(f) p.PROCESS_ID = f.ID).DefaultIfEmpty
                '               Where p.PROCESS_ID = approveId _
                '               And t.PROCESS_CODE = process _
                '               And p.FROM_DATE <= Date.Today _
                '               And p.TO_DATE >= Date.Today).FirstOrDefault


                approveRecord = (From a In Context.AT_PORTAL_APP
                                     Where a.ID_REGGROUP = regID _
                                     And a.ID_EMPLOYEE = approveExt.EMPLOYEE_ID _
                                     And a.APPROVE_STATUS = 1
                                     Select a).FirstOrDefault

                approveExtInfo = Context.HU_EMPLOYEE.SingleOrDefault(Function(p) p.ID = approveExt.SUB_EMPLOYEE_ID)
                bExt = True
            Else
                approveInfo = Context.HU_EMPLOYEE.SingleOrDefault(Function(p) p.ID = approveRecord.ID_EMPLOYEE)
            End If


            ' cập nhật lại trạng thái của bản ghi phê duyệt
            approveRecord.APPROVE_STATUS = status
            approveRecord.APPROVE_DATE = Date.Now

            registerRecord = (From a In Context.AT_PORTAL_REG
                              Where a.ID_REGGROUP = regID
                              Select a).ToList()

            registerRecord.ForEach(Sub(x) x.NOTE &= approveRecord.level & ". " & _
                                       If(bExt, approveExtInfo.FULLNAME_VN, approveInfo.FULLNAME_VN) & ": " & note & "<br />")

            ' * nếu trạng thái bản ghi là chưa phê duyệt (-1) thì set trạng thái sang đang chờ phê duyệt (1)
            If registerRecord(0).STATUS = -1 Then
                'registerRecord.STATUS = 1
                registerRecord.ForEach(Sub(x) x.STATUS = 1)
            End If

            ' * Nếu không phê duyệt
            If status = RegisterStatus.Denied Then
                ' - Dừng và Email về cho người đăng ký.
                ' + Set trạng thái lại cho bản ghi đăng ký là không phê duyệt

                'registerRecord(0).STATUS = status
                registerRecord.ForEach(Sub(x) x.STATUS = status)

                ' + Gửi email
                SendMail(SendMailType.DenyToReg, registerRecord(0).ID_EMPLOYEE,
                         approveId, If(bExt, approveExt.SUB_EMPLOYEE_ID, Nothing), process,
                         registerRecord(0).ID_REGGROUP, currentUrl, , note)

                'Cập nhật thay đổi vào CSDL
                Context.SaveChanges()
                Return True
            End If

            ' Nếu là phê duyệt bản ghi, lấy cấp tiếp theo và cập nhật trạng thái là đang chờ phê duyệt
            Dim nextApproveRecords = From a In Context.AT_PORTAL_APP
                                   Where a.ID_REGGROUP = regID _
                                   And a.level = approveRecord.level + 1
                                   Select a


            ' * Nếu không có bản ghi nào => đây là bản ghi cuối cùng
            '   --> Cập nhật trạng thái bản đăng ký là đã phê duyệt
            If nextApproveRecords.Count() = 0 Then

                'registerRecord.STATUS = status
                registerRecord.ForEach(Sub(x) x.STATUS = status)

                '   --> Thực hiện chuyển dữ liệu bản ghi vào AT_RGT
                If Not ApprovePortalRegisterFinallize(registerRecord(0).ID_REGGROUP, log) Then
                    Return False
                End If
                ' đẩy dữ liệu sang phần bảng chấm công
                If process = "LEAVE" Then
                    INSERTAT_LEAVESHEET(registerRecord(0).ID_REGGROUP, log)
                Else
                    INSERTAT_LATE_COMBACKOUT(registerRecord(0).ID_REGGROUP, log)
                End If

                ' + Gửi email cho người đăng ký
                SendMail(SendMailType.AppToReg, registerRecord(0).ID_EMPLOYEE,
                         approveId, If(bExt, approveExt.SUB_EMPLOYEE_ID, Nothing),
                         process, registerRecord(0).ID_REGGROUP, currentUrl)
            Else
                For Each item In nextApproveRecords
                    item.APPROVE_STATUS = RegisterStatus.WaitForApprove  'Chờ phê duyệt
                Next

            End If

            'Cập nhật thay đổi vào CSDL
            Context.SaveChanges()

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        Finally
            _isAvailable = True
        End Try
    End Function

    Private Function INSERTAT_LEAVESHEET(ByVal regID As System.Guid, ByVal log As UserLog) As Boolean
        Dim item As AT_PORTAL_REG
        Dim leave As AT_LEAVESHEET
        Dim fromdate As Date?
        Dim todate As Date?
        Dim count As Decimal = 0
        Try
            Dim registerRecord = From p In Context.AT_PORTAL_REG
                                 From e In Context.HU_EMPLOYEE.Where(Function(F) F.ID = p.ID_EMPLOYEE).DefaultIfEmpty
                                 From s In Context.AT_TIME_MANUAL.Where(Function(C) C.ID = p.ID_SIGN).DefaultIfEmpty
                                 Where p.ID_REGGROUP = regID
                                 Select p, e, s

            If registerRecord Is Nothing Then
                Return False
            End If
            ' lấy ngày đầu và ngày cuối của kỳ nghỉ để đẩy sang công
            For Each i In registerRecord
                If fromdate Is Nothing Then
                    fromdate = i.p.FROM_DATE
                End If
                todate = i.p.TO_DATE
            Next


            For Each itm In registerRecord
                Dim exists = (From r In Context.AT_LEAVESHEET Where r.EMPLOYEE_ID = itm.p.ID_EMPLOYEE And r.WORKINGDAY = itm.p.FROM_DATE).Any
                If exists = False Then
                    leave = New AT_LEAVESHEET
                    leave.ID = Utilities.GetNextSequence(Context, Context.AT_LEAVESHEET.EntitySet.Name)
                    leave.EMPLOYEE_ID = itm.p.ID_EMPLOYEE
                    leave.WORKINGDAY = itm.p.FROM_DATE
                    leave.LEAVE_FROM = fromdate
                    leave.LEAVE_TO = todate
                    leave.MANUAL_ID = itm.s.ID
                    leave.NOTE = itm.p.NOTE
                    Context.AT_LEAVESHEET.AddObject(leave)
                    'itm.p.FROM_DATE = itm.p.FROM_DATE.Value.AddDays(1)
                Else
                    Dim details = (From r In Context.AT_LEAVESHEET Where r.EMPLOYEE_ID = itm.p.ID_EMPLOYEE And r.WORKINGDAY = itm.p.FROM_DATE).ToList
                    For index = 0 To details.Count - 1
                        Context.AT_LEAVESHEET.DeleteObject(details(index))
                    Next
                    leave = New AT_LEAVESHEET
                    leave.ID = Utilities.GetNextSequence(Context, Context.AT_LEAVESHEET.EntitySet.Name)
                    leave.EMPLOYEE_ID = itm.p.ID_EMPLOYEE
                    leave.WORKINGDAY = itm.p.FROM_DATE
                    leave.LEAVE_FROM = fromdate
                    leave.LEAVE_TO = todate
                    leave.MANUAL_ID = itm.s.ID
                    leave.NOTE = itm.p.NOTE_AT
                    Context.AT_LEAVESHEET.AddObject(leave)
                    'itm.p.FROM_DATE = itm.p.FROM_DATE.Value.AddDays(1)
                End If
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")

        End Try
    End Function

    Private Function INSERTAT_LATE_COMBACKOUT(ByVal regID As System.Guid, ByVal log As UserLog) As Boolean
        Dim item As AT_PORTAL_REG
        Dim leave As AT_LATE_COMBACKOUT
        Dim fromdate As Date?
        Dim todate As Date?
        Dim count As Decimal = 0
        Try
            Dim registerRecord = From p In Context.AT_PORTAL_REG
                                 From e In Context.HU_EMPLOYEE.Where(Function(F) F.ID = p.ID_EMPLOYEE).DefaultIfEmpty
                                 From s In Context.AT_TIME_MANUAL.Where(Function(C) C.ID = p.ID_SIGN).DefaultIfEmpty
                                 Where p.ID_REGGROUP = regID
                                 Select p, e, s

            If registerRecord Is Nothing Then
                Return False
            End If
            ' lấy ngày đầu và ngày cuối của kỳ nghỉ để đẩy sang công
            For Each i In registerRecord
                If fromdate Is Nothing Then
                    fromdate = i.p.FROM_DATE
                End If
                todate = i.p.TO_DATE
            Next


            For Each itm In registerRecord
                Dim exists = (From p In Context.AT_LATE_COMBACKOUT _
                             Where (p.EMPLOYEE_ID = itm.p.ID_EMPLOYEE And p.WORKINGDAY = itm.p.FROM_DATE And p.TYPE_DSVM = itm.p.ID_SIGN)).Any
                If exists = False Then
                    leave = New AT_LATE_COMBACKOUT
                    leave.ID = Utilities.GetNextSequence(Context, Context.AT_LATE_COMBACKOUT.EntitySet.Name)
                    leave.EMPLOYEE_ID = itm.p.ID_EMPLOYEE
                    leave.WORKINGDAY = itm.p.FROM_DATE
                    leave.ORG_ID = itm.e.ORG_ID
                    leave.MINUTE = itm.p.NVALUE
                    leave.TITLE_ID = itm.e.TITLE_ID
                    leave.TYPE_DSVM = itm.p.ID_SIGN
                    leave.REMARK = itm.p.NOTE
                    Context.AT_LATE_COMBACKOUT.AddObject(leave)
                Else
                    Dim details = (From p In Context.AT_LATE_COMBACKOUT _
                                    Where (p.EMPLOYEE_ID = itm.p.ID_EMPLOYEE And p.WORKINGDAY = itm.p.FROM_DATE And p.TYPE_DSVM = itm.p.ID_SIGN)).ToList
                    For index = 0 To details.Count - 1
                        Context.AT_LATE_COMBACKOUT.DeleteObject(details(index))
                    Next
                    leave = New AT_LATE_COMBACKOUT
                    leave.ID = Utilities.GetNextSequence(Context, Context.AT_LATE_COMBACKOUT.EntitySet.Name)
                    leave.EMPLOYEE_ID = itm.p.ID_EMPLOYEE
                    leave.WORKINGDAY = itm.p.FROM_DATE
                    leave.ORG_ID = itm.e.ORG_ID
                    leave.MINUTE = itm.p.NVALUE
                    leave.TITLE_ID = itm.e.TITLE_ID
                    leave.TYPE_DSVM = itm.p.ID_SIGN
                    leave.REMARK = itm.p.NOTE
                    Context.AT_LATE_COMBACKOUT.AddObject(leave)
                End If
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")

        End Try
    End Function

    Private Function ApprovePortalRegisterFinallize(ByVal regID As Guid,
                                             ByVal log As UserLog) As Boolean
        Dim itm As AT_PORTAL_REG
        Dim itemAppointment As AT_TIMESHEET_REGISTERDTO

        Try
            _isAvailable = False

            Dim registerRecord = From p In Context.AT_PORTAL_REG
                                 Where p.ID_REGGROUP = regID
                                 Select p

            If registerRecord Is Nothing Then
                Return False
            End If
            Dim listAppointment As New List(Of AT_TIMESHEET_REGISTERDTO)
            Dim lstDate As New List(Of Date)
            For Each itm In registerRecord
                nvalue_id = itm.NVALUE_ID
                If nvalue_id IsNot Nothing Then
                    Dim lstEmp As New List(Of Decimal)
                    lstEmp.Add(itm.ID_EMPLOYEE)

                    If Not lstDate.Contains(itm.FROM_DATE) Then
                        lstDate.Add(itm.FROM_DATE)
                        Dim dNValue As Double = registerRecord _
                                                .Where(Function(f) f.FROM_DATE = itm.FROM_DATE) _
                                                .Sum(Function(f) f.NVALUE.Value)
                        'DeleteRegisterLeaveByEmployee(lstEmp, itm.FROM_DATE.Value.Date, itm.FROM_DATE.Value.Date,
                        '                              New List(Of AT_FMLDTO), dNValue,
                        '                                  New AT_FMLDTO With {.ID = itm.ID_SIGN,
                        '                                      .CODE = itm.AT_FMLDTO.CODE})
                    End If
                End If

                itemAppointment = New AT_TIMESHEET_REGISTERDTO
                itemAppointment.EMPLOYEEID = itm.ID_EMPLOYEE
                itemAppointment.NOTE = itm.NOTE
                itemAppointment.NVALUE = itm.NVALUE
                itemAppointment.SIGNID = itm.ID_SIGN
                'itemAppointment.SIGNNAME = itm.AT_SIGN.CODE
                itemAppointment.WORKINGDAY = itm.FROM_DATE
                itemAppointment.NVALUE_ID = itm.NVALUE_ID
                itemAppointment.DEXT1 = itm.FROM_HOUR
                itemAppointment.DEXT2 = itm.TO_HOUR
                itemAppointment.IS_INSERT = True

                listAppointment.Add(itemAppointment)
            Next

            Dim lstDup = CreateRegisterAppointment(listAppointment, log)

            If lstDup IsNot Nothing AndAlso lstDup.Count > 0 Then
                Return False
            End If

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        Finally
            _isAvailable = True
        End Try
    End Function

    Public Function CreateRegisterAppointment(ByVal listappointment As List(Of AT_TIMESHEET_REGISTERDTO),
                                              ByVal log As UserLog,
                                             Optional ByVal isPlan As Boolean = False,
                                             Optional ByVal lstWorking As List(Of HU_WORKING) = Nothing) As List(Of AT_TIMESHEET_REGISTERDTO)
        Dim insObject As AT_RGT
        Dim rtnValue As List(Of AT_TIMESHEET_REGISTERDTO)
        Dim working As HU_WORKING
        Dim employee As HU_EMPLOYEE
        'Dim signex As AT_SIGNEXT
        Dim listId As New List(Of Decimal)

        Try
            _isAvailable = False
            Dim lstEmp = (From p In listappointment Where p.IS_INSERT = True Select p.EMPLOYEEID).ToList
            Dim startdate = listappointment.Where(Function(f) f.IS_INSERT = True).Min(Function(p) p.WORKINGDAY)
            Dim enddate = listappointment.Where(Function(f) f.IS_INSERT = True).Max(Function(p) p.WORKINGDAY)

            If listappointment.Where(Function(f) f.IS_INSERT = True).Count > 0 Then
                If Not isPlan Then
                    lstWorking = (From p In Context.HU_WORKING
                                  Where lstEmp.Contains(p.EMPLOYEE_ID) _
                                  And ((startdate >= p.EFFECT_DATE _
                                        And (Not p.EXPIRE_DATE.HasValue OrElse startdate <= p.EXPIRE_DATE)) _
                                   Or (enddate >= p.EFFECT_DATE And (Not p.EXPIRE_DATE.HasValue OrElse enddate <= p.EXPIRE_DATE))) _
                                     Select p).ToList
                End If

            End If

            For Each item As AT_TIMESHEET_REGISTERDTO In listappointment
                If item.IS_INSERT = True Then
                    insObject = New AT_RGT
                    insObject.ID = Utilities.GetNextSequence(Context, Context.AT_RGT.EntitySet.Name)
                    insObject.PLNID = item.PLNID
                    insObject.HU_EMPLOYEEID = item.EMPLOYEEID
                    insObject.AT_SIGNID = item.SIGNID
                    insObject.WORKINGDAY = item.WORKINGDAY
                    insObject.NVALUE = item.NVALUE
                    insObject.SVALUE = item.SVALUE
                    If item.DVALUE IsNot Nothing Then
                        insObject.DVALUE = item.DVALUE
                    Else : insObject.DVALUE = Nothing
                    End If
                    insObject.NVALUE_ID = item.NVALUE_ID
                    working = (From p In lstWorking
                               Where p.EMPLOYEE_ID = insObject.HU_EMPLOYEEID And _
                               p.EFFECT_DATE <= insObject.WORKINGDAY
                               Order By p.EFFECT_DATE Descending
                               Select p).FirstOrDefault()

                    If working IsNot Nothing Then
                        insObject.ORGID = working.ORG_ID
                        insObject.POSID = working.TITLE_ID
                    Else
                        employee = (From p In Context.HU_EMPLOYEE Where p.ID = insObject.HU_EMPLOYEEID Select p).FirstOrDefault()
                        insObject.ORGID = employee.ORG_ID
                        insObject.POSID = employee.TITLE_ID
                    End If

                    insObject.ISAP = IIf(item.ISAP, ATConstant.ACTFLG_ACTIVE, ATConstant.ACTFLG_DEACTIVE)

                    Context.AT_RGT.AddObject(insObject)
                    listId.Add(insObject.ID)
                End If
            Next
            If log Is Nothing Then
                Context.SaveChanges()
            Else
                Context.SaveChanges(log)
            End If
            Return Nothing
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".CreateRegisterAppointment")
        Finally
            _isAvailable = True
        End Try
    End Function

    Public Function GetListWaitingForApprove(ByVal approveId As Decimal, ByVal process As String, ByVal filter As ATRegSearchDTO) As List(Of AT_PORTAL_REG_DTO)
        Dim fromDate As Date = Date.MinValue
        Dim toDate As Date = Date.MaxValue
        Dim signId As Decimal? = Nothing
        Dim empIdName As String = ""
        Dim listChangeReturn As New List(Of AT_PORTAL_REG_DTO)
        Dim curObjReg As AT_PORTAL_REG_DTO
        Dim objExistRegGroup As AT_PORTAL_REG_DTO
        Try
            _isAvailable = False

            If filter IsNot Nothing Then
                If filter.FromDate.HasValue Then
                    fromDate = filter.FromDate.Value
                End If

                If filter.ToDate.HasValue Then
                    toDate = filter.ToDate.Value
                End If

                If filter.SignId.HasValue Then
                    signId = filter.SignId.Value
                End If

                If filter.EmployeeIdName <> "" Then
                    empIdName = filter.EmployeeIdName.ToUpper
                End If
            End If

            'Lấy danh sách các bản ghi đang chờ người này phê duyệt
            'And f.CODE = process _
            'And f.IS_LEAVE = If(process = "LEAVE", -1, 2) _
            Dim list As List(Of AT_PORTAL_REG_DTO) = (From r In Context.AT_PORTAL_APP
                                                            From p In Context.AT_PORTAL_REG.Where(Function(F) F.ID_REGGROUP = r.ID_REGGROUP).DefaultIfEmpty
                                                            From e In Context.HU_EMPLOYEE.Where(Function(C) C.ID = p.ID_EMPLOYEE).DefaultIfEmpty
                                                            From f In Context.AT_TIME_MANUAL.Where(Function(T) T.ID = p.ID_SIGN).DefaultIfEmpty
                                                            Where (r.ID_EMPLOYEE = approveId _
                                                                   And (Not signId.HasValue Or p.ID_SIGN = signId.Value) _
                                                                   And (empIdName = "" OrElse (e.EMPLOYEE_CODE.ToUpper.Contains(empIdName) OrElse e.FULLNAME_VN.ToUpper.Contains(empIdName))) _
                                                                   And (p.TO_DATE.Value <= toDate And p.FROM_DATE.Value >= fromDate)) _
                                                                   And p.SVALUE = process _
                                                               Order By r.APPROVE_DATE Descending, p.FROM_DATE, r.ID_REGGROUP, f.CODE _
                                                               Select New AT_PORTAL_REG_DTO With {.ID = p.ID,
                                                                                                  .ID_EMPLOYEE = p.ID_EMPLOYEE,
                                                                                                  .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                                                                                                  .EMPLOYEE_NAME = e.FULLNAME_VN,
                                                                                                  .ID_SIGN = p.ID_SIGN,
                                                                                                  .GSIGN_CODE = f.CODE,
                                                                                                  .REGDATE = p.REGDATE,
                                                                                                  .FROM_DATE = p.FROM_DATE,
                                                                                                  .TO_DATE = p.TO_DATE,
                                                                                                  .FROM_HOUR = p.FROM_HOUR,
                                                                                                  .TO_HOUR = p.TO_HOUR,
                                                                                                  .DAYCOUNT = p.DAYCOUNT,
                                                                                                  .HOURCOUNT = p.HOURCOUNT,
                                                                                                  .NOTE = p.NOTE,
                                                                                                  .STATUS = r.APPROVE_STATUS,
                                                                                                  .APP_DATE = r.APPROVE_DATE,
                                                                                                  .APP_LEVEL = r.level,
                                                                                                  .SIGN_CODE = f.CODE,
                                                                                                  .SIGN_NAME = f.CODE & " - " & f.NAME,
                                                                                                  .NVALUE = p.NVALUE,
                                                                                                  .ID_REGGROUP = r.ID_REGGROUP}).ToList
            Dim lst = list.Select(Function(p) New AT_PORTAL_REG_DTO With {.ID = p.ID,
                                                                                .ID_EMPLOYEE = p.ID_EMPLOYEE,
                                                                                .EMPLOYEE_CODE = p.EMPLOYEE_CODE,
                                                                                .EMPLOYEE_NAME = p.EMPLOYEE_NAME,
                                                                                .ID_SIGN = p.ID_SIGN,
                                                                                .GSIGN_CODE = p.GSIGN_CODE,
                                                                                .REGDATE = p.REGDATE,
                                                                                .FROM_DATE = p.FROM_DATE,
                                                                                .TO_DATE = p.TO_DATE,
                                                                                .FROM_HOUR = p.FROM_HOUR,
                                                                                .TO_HOUR = p.TO_HOUR,
                                                                                .DAYCOUNT = p.DAYCOUNT,
                                                                                .HOURCOUNT = p.HOURCOUNT,
                                                                                .NOTE = p.NOTE,
                                                                                .STATUS = p.STATUS,
                                                                                .APP_DATE = p.APP_DATE,
                                                                                .APP_LEVEL = p.APP_LEVEL,
                                                                                .SIGN_CODE = p.SIGN_CODE,
                                                                                .SIGN_NAME = p.SIGN_NAME,
                                                                                .NVALUE = p.NVALUE,
                                                                                .ID_REGGROUP = p.ID_REGGROUP})

            Dim sql = (From T In lst
                            Group T By
                              T.ID,
                              T.FROM_DATE,
                              T.TO_DATE
                             Into g = Group
                            Select
                              ID = CType(g.Max(Function(p) p.ID), Decimal?),
                              EMPLOYEE_ID = CType(g.Max(Function(p) p.ID_EMPLOYEE), Decimal?),
                              EMPLOYEE_CODE = g.Max(Function(p) p.EMPLOYEE_CODE),
                              VN_FULLNAME = g.Max(Function(p) p.EMPLOYEE_NAME),
                              ID_SIGN = CType(g.Max(Function(p) p.ID_SIGN), Decimal?),
                              GSIGN_CODE = g.Max(Function(p) p.GSIGN_CODE),
                              REGDATE = CType(g.Max(Function(p) p.REGDATE), DateTime?),
                              FROM_DATE = CType(g.Max(Function(p) p.FROM_DATE), DateTime?),
                              TO_DATE = CType(g.Max(Function(p) p.TO_DATE), DateTime?),
                              FROM_HOUR = CType(g.Max(Function(p) p.FROM_HOUR), DateTime?),
                              TO_HOUR = CType(g.Max(Function(p) p.TO_HOUR), DateTime?),
                              DAYCOUNT = CType(g.Max(Function(p) p.DAYCOUNT), Decimal?),
                              HOURCOUNT = CType(g.Max(Function(p) p.HOURCOUNT), Decimal?),
                              NOTE = g.Max(Function(p) p.NOTE),
                              STATUS = g.Max(Function(p) p.STATUS),
                              APP_DATE = CType(g.Max(Function(p) p.APP_DATE), DateTime?),
                              APP_LEVEL = g.Max(Function(p) p.APP_LEVEL),
                              SIGN_CODE = g.Max(Function(p) p.SIGN_CODE),
                              SIGN_NAME = g.Max(Function(p) p.SIGN_NAME),
                              NVALUE = g.Max(Function(p) p.NVALUE),
                              ID_REGGROUP = g.Max(Function(p) p.ID_REGGROUP)
                              ).ToList

            Dim listReturn As List(Of AT_PORTAL_REG_DTO) = sql.Select(Function(f) New AT_PORTAL_REG_DTO With {.ID = f.ID,
                                                                                .ID_EMPLOYEE = f.EMPLOYEE_ID,
                                                                                .EMPLOYEE_CODE = f.EMPLOYEE_CODE,
                                                                                .EMPLOYEE_NAME = f.VN_FULLNAME,
                                                                                .ID_SIGN = f.ID_SIGN,
                                                                                .GSIGN_CODE = f.GSIGN_CODE,
                                                                                .REGDATE = f.REGDATE,
                                                                                .FROM_DATE = f.FROM_DATE,
                                                                                .TO_DATE = f.TO_DATE,
                                                                                .FROM_HOUR = f.FROM_HOUR,
                                                                                .TO_HOUR = f.TO_HOUR,
                                                                                .DAYCOUNT = f.DAYCOUNT,
                                                                                .HOURCOUNT = f.HOURCOUNT,
                                                                                .NOTE = f.NOTE,
                                                                                .STATUS = f.STATUS,
                                                                                .APP_DATE = f.APP_DATE,
                                                                                .APP_LEVEL = f.APP_LEVEL,
                                                                                .SIGN_CODE = f.SIGN_CODE,
                                                                                .SIGN_NAME = f.SIGN_NAME,
                                                                                .NVALUE = f.NVALUE,
                                                                                .ID_REGGROUP = f.ID_REGGROUP
                                 }).ToList

            ' Lấy danh sách những người mà người này được phê duyệt thay thế
            Dim listAppExt = (From p In Context.SE_APP_SETUPEXT
                              From c In Context.SE_APP_PROCESS.Where(Function(f) f.ID = p.PROCESS_ID).DefaultIfEmpty
                             Where p.SUB_EMPLOYEE_ID = approveId _
                             And p.FROM_DATE <= Date.Today _
                             And Date.Today <= p.TO_DATE _
                             And c.PROCESS_CODE = process
                             Select p).ToList

            For Each setupEx In listAppExt

                Dim empId As Decimal = setupEx.EMPLOYEE_ID

                Dim listAppEx As List(Of AT_PORTAL_REG_DTO) = (From r In Context.AT_PORTAL_APP
                                                               Join p In Context.AT_PORTAL_REG On r.ID_REGGROUP Equals p.ID_REGGROUP
                                                               Join e In Context.HU_EMPLOYEE On p.ID_EMPLOYEE Equals e.ID
                                                               Where (r.ID_EMPLOYEE = empId _
                                                                      And (p.REGDATE.Value <= toDate And p.REGDATE.Value >= fromDate))
                                                                      Order By r.APPROVE_DATE Descending, p.FROM_DATE, e.EMPLOYEE_CODE
                                                                  Select New AT_PORTAL_REG_DTO With {.ID = p.ID,
                                                                                                     .ID_EMPLOYEE = p.ID_EMPLOYEE,
                                                                                                     .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                                                                                                     .EMPLOYEE_NAME = e.FULLNAME_VN,
                                                                                                     .ID_SIGN = p.ID_SIGN,
                                                                                                     .GSIGN_CODE = "2",
                                                                                                     .REGDATE = p.REGDATE,
                                                                                                     .FROM_DATE = p.FROM_DATE,
                                                                                                     .TO_DATE = p.TO_DATE,
                                                                                                     .FROM_HOUR = p.FROM_HOUR,
                                                                                                     .TO_HOUR = p.TO_HOUR,
                                                                                                     .DAYCOUNT = p.DAYCOUNT,
                                                                                                     .HOURCOUNT = p.HOURCOUNT,
                                                                                                     .NOTE = p.NOTE,
                                                                                                     .STATUS = r.APPROVE_STATUS,
                                                                                                     .APP_DATE = r.APPROVE_DATE,
                                                                                                     .APP_LEVEL = r.level,
                                                                                                     .SIGN_CODE = "2",
                                                                                                     .SIGN_NAME = "ABC",
                                                                                                     .NVALUE = p.NVALUE,
                                                                                                     .ID_REGGROUP = p.ID_REGGROUP}).ToList

                If listAppEx.Count > 0 Then
                    listReturn.AddRange(listAppEx)
                End If
            Next

            For Each curObjReg In listReturn
                If curObjReg.ID_REGGROUP IsNot Nothing Then
                    objExistRegGroup = (From p In listChangeReturn
                                      Where p.ID_REGGROUP = curObjReg.ID_REGGROUP _
                                      And p.STATUS = curObjReg.STATUS
                                      Select p).SingleOrDefault()

                    If objExistRegGroup IsNot Nothing Then
                        If Not (objExistRegGroup.SIGN_NAME + "<br />").Contains(curObjReg.SIGN_NAME + "<br />") Then
                            If curObjReg.GSIGN_CODE = ATConstant.GSIGNCODE_LEAVE Then
                                objExistRegGroup.SIGN_NAME += "<br />" & curObjReg.SIGN_NAME
                                objExistRegGroup.DISPLAY += "<br />" & curObjReg.SIGN_CODE + " - " + _
                                    Format((From p In listReturn
                                            Where (p.ID_REGGROUP = objExistRegGroup.ID_REGGROUP _
                                                   And p.STATUS = objExistRegGroup.STATUS _
                                                   And p.SIGN_NAME = curObjReg.SIGN_NAME)
                                               Select p.NVALUE.Value).Sum, "0.##")
                            End If

                            If curObjReg.GSIGN_CODE = ATConstant.GSIGNCODE_WLEO Then
                                objExistRegGroup.SIGN_NAME += "<br />" & curObjReg.SIGN_NAME
                                objExistRegGroup.DISPLAY += "<br />" & curObjReg.SIGN_CODE + " - " + _
                                    Format((From p In listReturn
                                            Where p.ID_REGGROUP = objExistRegGroup.ID_REGGROUP _
                                            And p.STATUS = objExistRegGroup.STATUS _
                                            And p.SIGN_NAME = curObjReg.SIGN_NAME
                                            Select p.NVALUE.Value).Sum, "0.##")
                            End If
                        End If
                        If curObjReg.GSIGN_CODE = ATConstant.GSIGNCODE_OVERTIME Then
                            objExistRegGroup.DISPLAY += "<br />" + _
                                curObjReg.FROM_DATE.Value.ToString("dd/MM/yyyy") + ": " + _
                                curObjReg.FROM_HOUR.Value.ToString("HH:mm") + "-" + curObjReg.TO_HOUR.Value.ToString("HH:mm")
                        End If
                        objExistRegGroup.DAYCOUNT += Format(If(curObjReg.NVALUE Is Nothing, 0, curObjReg.NVALUE), "0.##")
                        objExistRegGroup.HOURCOUNT += Format(If(curObjReg.HOURCOUNT Is Nothing, 0, curObjReg.HOURCOUNT), "0.##")
                        If objExistRegGroup.FROM_DATE > curObjReg.FROM_DATE Then
                            objExistRegGroup.FROM_DATE = curObjReg.FROM_DATE
                        Else : objExistRegGroup.TO_DATE = curObjReg.FROM_DATE
                        End If
                    Else
                        curObjReg.DAYCOUNT = Format(If(curObjReg.NVALUE Is Nothing, 0, curObjReg.NVALUE), "0.##")
                        curObjReg.HOURCOUNT = Format(If(curObjReg.HOURCOUNT Is Nothing, 0, curObjReg.HOURCOUNT), "0.##")
                        Select Case curObjReg.GSIGN_CODE
                            Case ATConstant.GSIGNCODE_OVERTIME
                                curObjReg.DISPLAY = curObjReg.FROM_DATE.Value.ToString("dd/MM/yyyy") + ": " + _
                                curObjReg.FROM_HOUR.Value.ToString("HH:mm") + "-" + curObjReg.TO_HOUR.Value.ToString("HH:mm")
                            Case ATConstant.GSIGNCODE_LEAVE
                                curObjReg.DISPLAY = curObjReg.SIGN_CODE + " - " + Format((From p In listReturn
                                                                                           Where (p.ID_REGGROUP = curObjReg.ID_REGGROUP _
                                                                                                  And p.STATUS = curObjReg.STATUS _
                                                                                                  And p.SIGN_NAME = curObjReg.SIGN_NAME)
                                                                                              Select p.NVALUE.Value).Sum, "0.##")
                            Case ATConstant.GSIGNCODE_WLEO
                                curObjReg.DISPLAY = curObjReg.SIGN_CODE + " - " + Format((From p In listReturn
                                                                                           Where (p.ID_REGGROUP = curObjReg.ID_REGGROUP _
                                                                                                  And p.STATUS = curObjReg.STATUS _
                                                                                                  And p.SIGN_NAME = curObjReg.SIGN_NAME)
                                                                                              Select p.NVALUE.Value).Sum, "0.##")
                        End Select

                        listChangeReturn.Add(curObjReg)
                    End If
                End If
            Next


            Return (From p In listChangeReturn Order By p.APP_DATE Descending, p.FROM_DATE, p.EMPLOYEE_CODE).ToList

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        Finally
            _isAvailable = True
        End Try
    End Function

    Public Function GetEmployeeList() As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PORTAL_LEAVE.GET_EMPLOYEE_LIST",
                                                    New With {.P_CUR = cls.OUT_CURSOR})
                Return dtData
            End Using

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function GetLeaveDay(ByVal dDate As Date) As DataTable
        Try

            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PORTAL_LEAVE.GET_LEAVE_DAY",
                                                    New With {.P_DATE = dDate,
                                                              .P_CUR = cls.OUT_CURSOR})
                Return dtData
            End Using

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
#End Region

#Region "Thiết lập kiểu công"

    Public Function GetAT_TIME_MANUAL(ByVal _filter As AT_TIME_MANUALDTO,
                                       Optional ByVal PageIndex As Integer = 0,
                                       Optional ByVal PageSize As Integer = Integer.MaxValue,
                                       Optional ByRef Total As Integer = 0,
                                       Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_TIME_MANUALDTO)
        Try

            Dim query = From p In Context.AT_TIME_MANUAL
                        From f1 In Context.AT_FML.Where(Function(F) F.ID = p.MORNING_ID).DefaultIfEmpty
                        From f2 In Context.AT_FML.Where(Function(F) F.ID = p.AFTERNOON_ID).DefaultIfEmpty
            Dim lst = query.Select(Function(p) New AT_TIME_MANUALDTO With {
                                       .ID = p.p.ID,
                                       .CODE = p.p.CODE,
                                       .NAME_VN = p.p.NAME,
                                       .MORNING_ID = p.p.MORNING_ID,
                                       .MORNING_NAME = p.f1.NAME_VN,
                                       .AFTERNOON_ID = p.p.AFTERNOON_ID,
                                       .AFTERNOON_NAME = p.f2.NAME_VN,
                                       .NOTE = p.p.NOTE,
                                       .ACTFLG = If(p.p.ACTFLG = "A", "Áp dụng", "Ngừng Áp dụng"),
                                       .IS_PAID_RICE = p.p.IS_PAID_RICE,
                                       .CREATED_BY = p.p.CREATED_BY,
                                       .CREATED_DATE = p.p.CREATED_DATE,
                                       .CREATED_LOG = p.p.CREATED_LOG,
                                       .MODIFIED_BY = p.p.MODIFIED_BY,
                                       .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                       .MODIFIED_LOG = p.p.MODIFIED_LOG})


            If Not String.IsNullOrEmpty(_filter.CODE) Then
                lst = lst.Where(Function(f) f.CODE.ToLower().Contains(_filter.CODE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.NAME_VN) Then
                lst = lst.Where(Function(f) f.NAME_VN.ToLower().Contains(_filter.NAME_VN.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.MORNING_NAME) Then
                lst = lst.Where(Function(f) f.MORNING_NAME.ToLower().Contains(_filter.MORNING_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.AFTERNOON_NAME) Then
                lst = lst.Where(Function(f) f.AFTERNOON_NAME.ToLower().Contains(_filter.AFTERNOON_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ACTFLG) Then
                lst = lst.Where(Function(f) f.ACTFLG.ToLower().Contains(_filter.ACTFLG.ToLower()))
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

    Public Function GetAT_TIME_MANUALById(ByVal _id As Decimal?) As AT_TIME_MANUALDTO
        Try

            Dim query = From p In Context.AT_TIME_MANUAL
                        Where p.ID = _id

            Dim ls = query.Select(Function(p) New AT_TIME_MANUALDTO With {.ID = p.ID,
                                                                       .MORNING_ID = p.MORNING_ID,
                                                                       .AFTERNOON_ID = p.AFTERNOON_ID}).FirstOrDefault
            Return ls
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function InsertAT_TIME_MANUAL(ByVal objTitle As AT_TIME_MANUALDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New AT_TIME_MANUAL
        Dim iCount As Integer = 0
        Try
            objTitleData.ID = Utilities.GetNextSequence(Context, Context.AT_TIME_MANUAL.EntitySet.Name)
            objTitleData.CODE = objTitle.CODE.Trim
            'objTitleData.NAME_EN = objTitle.NAME_EN.Trim
            objTitleData.NAME = objTitle.NAME_VN.Trim
            objTitleData.MORNING_ID = objTitle.MORNING_ID
            objTitleData.AFTERNOON_ID = objTitle.AFTERNOON_ID
            objTitleData.NOTE = objTitle.NOTE
            objTitleData.ACTFLG = objTitle.ACTFLG
            objTitleData.CREATED_BY = objTitle.CREATED_BY
            objTitleData.CREATED_DATE = objTitle.CREATED_DATE
            objTitleData.IS_PAID_RICE = objTitle.IS_PAID_RICE
            objTitleData.CREATED_LOG = objTitle.CREATED_LOG
            objTitleData.MODIFIED_BY = objTitle.MODIFIED_BY
            objTitleData.MODIFIED_DATE = objTitle.MODIFIED_DATE
            objTitleData.MODIFIED_LOG = objTitle.MODIFIED_LOG
            Context.AT_TIME_MANUAL.AddObject(objTitleData)
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

    End Function

    Public Function ValidateAT_TIME_MANUAL(ByVal _validate As AT_TIME_MANUALDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.AT_TIME_MANUAL
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.AT_TIME_MANUAL
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper).FirstOrDefault
                End If
                Return (query Is Nothing)
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ModifyAT_TIME_MANUAL(ByVal objTitle As AT_TIME_MANUALDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New AT_TIME_MANUAL With {.ID = objTitle.ID}
        Try
            objTitleData = (From p In Context.AT_TIME_MANUAL Where p.ID = objTitleData.ID).SingleOrDefault
            objTitleData.CODE = objTitle.CODE.Trim
            'objTitleData.NAME_EN = objTitle.NAME_EN.Trim
            objTitleData.NAME = objTitle.NAME_VN.Trim
            objTitleData.MORNING_ID = objTitle.MORNING_ID
            objTitleData.AFTERNOON_ID = objTitle.AFTERNOON_ID
            objTitleData.NOTE = objTitle.NOTE
            objTitleData.CREATED_BY = objTitle.CREATED_BY
            objTitleData.CREATED_DATE = objTitle.CREATED_DATE
            objTitleData.CREATED_LOG = objTitle.CREATED_LOG
            objTitleData.IS_PAID_RICE = objTitle.IS_PAID_RICE
            objTitleData.MODIFIED_BY = objTitle.MODIFIED_BY
            objTitleData.MODIFIED_DATE = objTitle.MODIFIED_DATE
            objTitleData.MODIFIED_LOG = objTitle.MODIFIED_LOG
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

    End Function

    Public Function ActiveAT_TIME_MANUAL(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstData As List(Of AT_TIME_MANUAL)
        Try
            lstData = (From p In Context.AT_TIME_MANUAL Where lstID.Contains(p.ID)).ToList
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

    Public Function DeleteAT_TIME_MANUAL(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstHolidayData As List(Of AT_TIME_MANUAL)
        Try
            lstHolidayData = (From p In Context.AT_TIME_MANUAL Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstHolidayData.Count - 1
                Context.AT_TIME_MANUAL.DeleteObject(lstHolidayData(index))
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteTime_Manual")
            Throw ex
        End Try
    End Function

    Public Function GetDataImportCO() As DataTable
        Using cls As New DataAccess.QueryData
            Dim dtData As DataTable = cls.ExecuteStore("PKG_ATTENDANCE_LIST.GET_TIME_MANUAL_IMPORT",
                                           New With {.CUR = cls.OUT_CURSOR})
            Return dtData
        End Using
        Return Nothing
    End Function
#End Region

#Region "Danh mục tham số hệ thống phần itime"
    Public Function GetListParamItime(ByVal _filter As AT_LISTPARAM_SYSTEAMDTO,
                                        Optional ByVal PageIndex As Integer = 0,
                                        Optional ByVal PageSize As Integer = Integer.MaxValue,
                                        Optional ByRef Total As Integer = 0,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_LISTPARAM_SYSTEAMDTO)
        Try

            Dim query = From p In Context.AT_LIST_PARAM_SYSTEM
                        From i In Context.HU_STAFF_RANK.Where(Function(F) F.ID = p.RANK_PAY_OT).DefaultIfEmpty

            Dim lst = query.Select(Function(p) New AT_LISTPARAM_SYSTEAMDTO With {
                                       .ID = p.p.ID,
                                       .CODE = p.p.CODE,
                                       .NAME = p.p.NAME,
                                       .EFFECT_DATE_FROM = p.p.EFFECT_DATE_FROM,
                                       .EFFECT_DATE_TO_NB = p.p.EFFECT_DATE_TO_NB,
                                       .TO_LEAVE_YEAR = p.p.TO_LEAVE_YEAR,
                                       .RANK_PAY_OT = p.p.RANK_PAY_OT,
                                       .RANK_PAY_OT_NAME = p.i.NAME,
                                       .HOUR_CAL_OT = p.p.HOUR_CAL_OT,
                                       .HOUR_MAX_OT = p.p.HOUR_MAX_OT,
                                       .FACTOR_OT = p.p.FACTOR_OT,
                                       .CREATE_BY_SHOW = p.p.CREATE_BY_SHOW,
                                       .CREATE_DATE_SHOW = p.p.CREATE_DATE_SHOW,
                                       .ACTFLG = If(p.p.ACTFLG = "A", "Áp dụng", "Ngừng Áp dụng"),
                                       .NOTE = p.p.NOTE,
                                       .MAX_DAY_OT = p.p.MAX_DAY_OT,
                                       .CREATED_BY = p.p.CREATED_BY,
                                       .CREATED_DATE = p.p.CREATED_DATE,
                                       .CREATED_LOG = p.p.CREATED_LOG,
                                       .MODIFIED_BY = p.p.MODIFIED_BY,
                                       .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                       .MODIFIED_LOG = p.p.MODIFIED_LOG})

            If Not String.IsNullOrEmpty(_filter.CODE) Then
                lst = lst.Where(Function(f) f.CODE.ToLower().Contains(_filter.CODE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.NAME) Then
                lst = lst.Where(Function(f) f.NAME.ToLower().Contains(_filter.NAME.ToLower()))
            End If
            If _filter.EFFECT_DATE_FROM.HasValue Then
                lst = lst.Where(Function(f) f.EFFECT_DATE_FROM = _filter.EFFECT_DATE_FROM)
            End If
            If _filter.EFFECT_DATE_TO_NB.HasValue Then
                lst = lst.Where(Function(f) f.EFFECT_DATE_TO_NB = _filter.EFFECT_DATE_TO_NB)
            End If
            If _filter.TO_LEAVE_YEAR.HasValue Then
                lst = lst.Where(Function(f) f.TO_LEAVE_YEAR = _filter.TO_LEAVE_YEAR)
            End If
            If _filter.RANK_PAY_OT <> 0 Then
                lst = lst.Where(Function(f) f.RANK_PAY_OT = _filter.RANK_PAY_OT)
            End If
            If _filter.HOUR_CAL_OT <> 0 Then
                lst = lst.Where(Function(f) f.HOUR_CAL_OT = _filter.HOUR_CAL_OT)
            End If
            If _filter.HOUR_MAX_OT <> 0 Then
                lst = lst.Where(Function(f) f.HOUR_MAX_OT = _filter.HOUR_MAX_OT)
            End If
            If _filter.FACTOR_OT <> 0 Then
                lst = lst.Where(Function(f) f.FACTOR_OT = _filter.FACTOR_OT)
            End If
            If _filter.MAX_DAY_OT <> 0 Then
                lst = lst.Where(Function(f) f.MAX_DAY_OT = _filter.MAX_DAY_OT)
            End If
            If Not String.IsNullOrEmpty(_filter.NOTE) Then
                lst = lst.Where(Function(f) f.NOTE.ToLower().Contains(_filter.NOTE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.CREATE_BY_SHOW) Then
                lst = lst.Where(Function(f) f.CREATE_BY_SHOW.ToLower().Contains(_filter.CREATE_BY_SHOW.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ACTFLG) Then
                lst = lst.Where(Function(f) f.ACTFLG.ToLower().Contains(_filter.ACTFLG.ToLower()))
            End If
            If _filter.CREATE_DATE_SHOW.HasValue Then
                lst = lst.Where(Function(f) f.CREATE_DATE_SHOW = _filter.CREATE_DATE_SHOW)
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

    Public Function InsertListParamItime(ByVal objData As AT_LISTPARAM_SYSTEAMDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New AT_LIST_PARAM_SYSTEM
        Dim iCount As Integer = 0
        Try
            objTitleData.ID = Utilities.GetNextSequence(Context, Context.AT_LIST_PARAM_SYSTEM.EntitySet.Name)
            objTitleData.CODE = objData.CODE.Trim
            objTitleData.NAME = objData.NAME.Trim
            objTitleData.EFFECT_DATE_FROM = objData.EFFECT_DATE_FROM
            objTitleData.EFFECT_DATE_TO_NB = objData.EFFECT_DATE_TO_NB
            objTitleData.TO_LEAVE_YEAR = objData.TO_LEAVE_YEAR
            objTitleData.RANK_PAY_OT = objData.RANK_PAY_OT
            objTitleData.HOUR_CAL_OT = objData.HOUR_CAL_OT
            objTitleData.HOUR_MAX_OT = objData.HOUR_MAX_OT
            objTitleData.FACTOR_OT = objData.FACTOR_OT
            objTitleData.CREATE_BY_SHOW = objData.CREATE_BY_SHOW
            objTitleData.CREATE_DATE_SHOW = objData.CREATE_DATE_SHOW
            objTitleData.NOTE = objData.NOTE
            objTitleData.ACTFLG = objData.ACTFLG
            objTitleData.CREATED_BY = objData.CREATED_BY
            objTitleData.CREATED_DATE = objData.CREATED_DATE
            objTitleData.CREATED_LOG = objData.CREATED_LOG
            objTitleData.MODIFIED_BY = objData.MODIFIED_BY
            objTitleData.MODIFIED_DATE = objData.MODIFIED_DATE
            objTitleData.MODIFIED_LOG = objData.MODIFIED_LOG
            Context.AT_LIST_PARAM_SYSTEM.AddObject(objTitleData)
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

    End Function

    Public Function ValidateListParamItime(ByVal _validate As AT_LISTPARAM_SYSTEAMDTO)
        Dim query
        Try
            If _validate.EFFECT_DATE_FROM IsNot Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.AT_LIST_PARAM_SYSTEM
                             Where p.EFFECT_DATE_FROM = _validate.EFFECT_DATE_FROM _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.AT_LIST_PARAM_SYSTEM
                             Where p.EFFECT_DATE_FROM = _validate.EFFECT_DATE_FROM).FirstOrDefault
                End If
                Return (query Is Nothing)
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ModifyListParamItime(ByVal objData As AT_LISTPARAM_SYSTEAMDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New AT_LIST_PARAM_SYSTEM With {.ID = objData.ID}
        Try
            objTitleData = (From p In Context.AT_LIST_PARAM_SYSTEM Where p.ID = objTitleData.ID).SingleOrDefault
            objTitleData.CODE = objData.CODE.Trim
            objTitleData.NAME = objData.NAME.Trim
            objTitleData.EFFECT_DATE_FROM = objData.EFFECT_DATE_FROM
            objTitleData.EFFECT_DATE_TO_NB = objData.EFFECT_DATE_TO_NB
            objTitleData.TO_LEAVE_YEAR = objData.TO_LEAVE_YEAR
            objTitleData.RANK_PAY_OT = objData.RANK_PAY_OT
            objTitleData.HOUR_CAL_OT = objData.HOUR_CAL_OT
            objTitleData.HOUR_MAX_OT = objData.HOUR_MAX_OT
            objTitleData.FACTOR_OT = objData.FACTOR_OT
            objTitleData.CREATE_BY_SHOW = objData.CREATE_BY_SHOW
            objTitleData.CREATE_DATE_SHOW = objData.CREATE_DATE_SHOW
            objTitleData.NOTE = objData.NOTE
            objTitleData.ACTFLG = objData.ACTFLG
            objTitleData.CREATED_BY = objData.CREATED_BY
            objTitleData.CREATED_DATE = objData.CREATED_DATE
            objTitleData.CREATED_LOG = objData.CREATED_LOG
            objTitleData.MODIFIED_BY = objData.MODIFIED_BY
            objTitleData.MODIFIED_DATE = objData.MODIFIED_DATE
            objTitleData.MODIFIED_LOG = objData.MODIFIED_LOG
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

    End Function

    Public Function ActiveListParamItime(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstData As List(Of AT_LIST_PARAM_SYSTEM)
        Try
            lstData = (From p In Context.AT_LIST_PARAM_SYSTEM Where lstID.Contains(p.ID)).ToList
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

    Public Function DeleteListParamItime(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstHolidayData As List(Of AT_LIST_PARAM_SYSTEM)
        Try
            lstHolidayData = (From p In Context.AT_LIST_PARAM_SYSTEM Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstHolidayData.Count - 1
                Context.AT_LIST_PARAM_SYSTEM.DeleteObject(lstHolidayData(index))
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

#Region ""
    Public Function AutoGenCode(ByVal firstChar As String, ByVal tableName As String, ByVal colName As String) As String
        Try
            Dim str As String
            Dim Sql = "SELECT NVL(MAX(" & colName & "), '" & firstChar & "000') FROM " & tableName & " WHERE " & colName & " LIKE '" & firstChar & "%'"
            str = Context.ExecuteStoreQuery(Of String)(Sql).FirstOrDefault
            If str = "" Then
                Return firstChar & "001"
            End If
            Dim number = Decimal.Parse(str.Substring(str.IndexOf(firstChar) + firstChar.Length))
            number = number + 1
            Dim lastChar = Format(number, "000")
            Return firstChar & lastChar
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function CheckExistInDatabase(ByVal lstID As List(Of Decimal), ByVal table As AttendanceCommon.TABLE_NAME) As Boolean
        Dim isExist As Boolean = False
        Dim strListID As String = lstID.Select(Function(x) x.ToString).Aggregate(Function(x, y) x & "," & y)
        Dim count As Decimal = 0
        Try
            Select Case table
                Case AttendanceCommon.TABLE_NAME.AT_FML
                    isExist = Execute_ExistInDatabase("AT_TIME_MANUAL", "MORNING_ID", strListID)
                    If Not isExist Then
                        Return isExist
                    End If
                    isExist = Execute_ExistInDatabase("AT_TIME_MANUAL", "AFTERNOON_ID", strListID)
                    If Not isExist Then
                        Return isExist
                    End If
                Case AttendanceCommon.TABLE_NAME.AT_SHIFT
                    isExist = Execute_ExistInDatabase("AT_WORKSIGN", "SHIFT_ID", strListID)
                    If Not isExist Then
                        Return isExist
                    End If
                Case AttendanceCommon.TABLE_NAME.AT_TIME_MANUAL
                    isExist = Execute_ExistInDatabase("AT_TIME_TIMESHEET_MACHINET", "MANUAL_ID", strListID)
                    If Not isExist Then
                        Return isExist
                    End If

                Case AttendanceCommon.TABLE_NAME.AT_HOLIDAY
                Case AttendanceCommon.TABLE_NAME.AT_LIST_PARAM_SYSTEM
                Case AttendanceCommon.TABLE_NAME.AT_SETUP_SPECIAL
                Case AttendanceCommon.TABLE_NAME.AT_SETUP_TIME_EMP
                Case AttendanceCommon.TABLE_NAME.AT_GSIGN
                Case AttendanceCommon.TABLE_NAME.AT_DMVS
            End Select
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Return False
        End Try
    End Function

    Public Function CheckExistInDatabaseAT_SIGNDEFAULT(ByVal lstID As List(Of Decimal), ByVal lstWorking As List(Of Date), ByVal lstShift As List(Of Decimal), ByVal table As AttendanceCommon.TABLE_NAME) As Boolean
        Dim isExist As Boolean = False
        Dim strListID As String = lstID.Select(Function(x) x.ToString).Aggregate(Function(x, y) x & "," & y)
        Dim strListWorking As Date = lstWorking.Select(Function(x) x.ToString).Aggregate(Function(x, y) x & "," & y)
        Dim strListShift As String = lstShift.Select(Function(x) x.ToString).Aggregate(Function(x, y) x & "," & y)
        Dim count As Decimal = 0
        Try
            Select Case table
                Case AttendanceCommon.TABLE_NAME.AT_SIGNDEFAULT
                    Dim number As New Decimal
                    Dim Sql = "SELECT COUNT(*) FROM AT_WORKSIGN WHERE EMPLOYEE_ID IN (" & strListID & ") AND SHIFT_ID IN (" & strListShift & ")"
                    number = Context.ExecuteStoreQuery(Of Decimal)(Sql).FirstOrDefault

                    If number > 0 Then
                        Return False
                    End If
            End Select
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Return False
        End Try
    End Function

    Private Function Execute_ExistInDatabase(ByVal tableName As String, ByVal colName As String, ByVal strListID As String)
        Try
            Dim count As Decimal = 0
            Dim Sql = "SELECT COUNT(" & colName & ") FROM " & tableName & " WHERE " & colName & " IN (" & strListID & ")"
            count = Context.ExecuteStoreQuery(Of Decimal)(Sql).FirstOrDefault
            If count > 0 Then
                Return False
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
#End Region

End Class