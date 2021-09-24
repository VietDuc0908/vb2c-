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


#Region "Taxation "

    Public Function GetTaxation(ByVal _filter As PATaxationDTO,
                                   ByVal PageIndex As Integer,
                                    ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PATaxationDTO)
        Try
            Dim query = From p In Context.PA_TAXATION
            Dim lst = query.Select(Function(p) New PATaxationDTO With {
                                        .ID = p.ID,
                                        .VALUE_FROM = p.VALUE_FROM,
                                        .VALUE_TO = p.VALUE_TO,
                                        .RATE = p.RATE,
                                        .EXCEPT_FAST = p.EXCEPT_FAST,
                                        .FROM_DATE = p.FROM_DATE,
                                        .TO_DATE = p.TO_DATE,
                                        .SDESC = p.SDESC,
                                        .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng Áp dụng"),
                                        .CREATED_DATE = p.CREATED_DATE})

            If _filter.VALUE_FROM.HasValue Then
                lst = lst.Where(Function(f) f.VALUE_FROM = _filter.VALUE_FROM)
            End If
            If _filter.VALUE_TO.HasValue Then
                lst = lst.Where(Function(f) f.VALUE_TO = _filter.VALUE_TO)
            End If
            If _filter.EXCEPT_FAST.HasValue Then
                lst = lst.Where(Function(f) f.EXCEPT_FAST = _filter.EXCEPT_FAST)
            End If
            If _filter.RATE.HasValue Then
                lst = lst.Where(Function(f) f.RATE = _filter.RATE)
            End If
            If _filter.FROM_DATE.HasValue Then
                lst = lst.Where(Function(f) f.FROM_DATE = _filter.FROM_DATE)
            End If
            If _filter.TO_DATE.HasValue Then
                lst = lst.Where(Function(f) f.TO_DATE = _filter.TO_DATE)
            End If
            If Not String.IsNullOrEmpty(_filter.SDESC) Then
                lst = lst.Where(Function(f) f.SDESC.ToLower().Contains(_filter.SDESC.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ACTFLG) Then
                lst = lst.Where(Function(f) f.ACTFLG.ToLower().Contains(_filter.ACTFLG.ToLower()))
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

    Public Function InsertTaxation(ByVal objTitle As PATaxationDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New PA_TAXATION
        Dim iCount As Integer = 0
        Try
            objTitleData.ID = Utilities.GetNextSequence(Context, Context.PA_TAXATION.EntitySet.Name)
            objTitleData.VALUE_FROM = objTitle.VALUE_FROM
            objTitleData.VALUE_TO = objTitle.VALUE_TO
            objTitleData.RATE = objTitle.RATE
            objTitleData.EXCEPT_FAST = objTitle.EXCEPT_FAST
            objTitleData.FROM_DATE = objTitle.FROM_DATE
            objTitleData.TO_DATE = objTitle.TO_DATE
            objTitleData.ACTFLG = "A"
            objTitleData.SDESC = objTitle.SDESC
            objTitleData.CREATED_BY = objTitle.CREATED_BY
            objTitleData.CREATED_DATE = objTitle.CREATED_DATE
            objTitleData.CREATED_LOG = objTitle.CREATED_LOG
            objTitleData.MODIFIED_BY = objTitle.MODIFIED_BY
            objTitleData.MODIFIED_DATE = objTitle.MODIFIED_DATE
            objTitleData.MODIFIED_LOG = objTitle.MODIFIED_LOG
            Context.PA_TAXATION.AddObject(objTitleData)
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function ModifyTaxation(ByVal objTitle As PATaxationDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New PA_TAXATION With {.ID = objTitle.ID}
        Try
            objTitleData = (From p In Context.PA_TAXATION Where p.ID = objTitleData.ID).SingleOrDefault
            objTitleData.ID = objTitle.ID
            objTitleData.VALUE_FROM = objTitle.VALUE_FROM
            objTitleData.VALUE_TO = objTitle.VALUE_TO
            objTitleData.RATE = objTitle.RATE
            objTitleData.EXCEPT_FAST = objTitle.EXCEPT_FAST
            objTitleData.FROM_DATE = objTitle.FROM_DATE
            objTitleData.TO_DATE = objTitle.TO_DATE
            objTitleData.SDESC = objTitle.SDESC
            objTitleData.CREATED_BY = objTitle.CREATED_BY
            objTitleData.CREATED_DATE = objTitle.CREATED_DATE
            objTitleData.CREATED_LOG = objTitle.CREATED_LOG
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function ActiveTaxation(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstData As List(Of PA_TAXATION)
        Try
            lstData = (From p In Context.PA_TAXATION Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                lstData(index).ACTFLG = bActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function DeleteTaxation(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstTaxationData As List(Of PA_TAXATION)
        Try
            lstTaxationData = (From p In Context.PA_TAXATION Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstTaxationData.Count - 1
                Context.PA_TAXATION.DeleteObject(lstTaxationData(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteTaxation")
            Throw ex
        End Try
    End Function

#End Region

#Region "Payment list"

    Public Function GetPaymentList(ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PAPaymentListDTO)
        Try
            Dim query = From p In Context.PA_PAYMENT_LIST
                        Join o In Context.PA_OBJECT_SALARY On o.ID Equals p.OBJ_PAYMENT_ID
            Dim lst = query.Select(Function(s) New PAPaymentListDTO With {
                                        .ID = s.p.ID,
                                        .CODE = s.p.CODE,
                                        .NAME = s.p.NAME,
                                        .OBJ_PAYMENT_ID = s.p.OBJ_PAYMENT_ID,
                                        .OBJ_PAYMENT_NAME_VN = s.o.NAME_EN,
                                        .OBJ_PAYMENT_NAME_EN = s.o.NAME_VN,
                                        .EFFECTIVE_DATE = s.p.EFFECTIVE_DATE,
                                        .VALUE = s.p.VALUE,
                                        .SDESC = s.p.SDESC,
                                        .ACTFLG = If(s.p.ACTFLG = "A", "Áp dụng", "Ngưng áp dụng"),
                                .CREATED_DATE = s.p.CREATED_DATE
                                     })
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function GetPaymentListAll(Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PAPaymentListDTO)
        Try

            Dim query = From p In Context.PA_PAYMENT_LIST
            Dim lst = query.Select(Function(p) New PAPaymentListDTO With {
                                        .ID = p.ID,
                                        .OBJ_PAYMENT_ID = p.OBJ_PAYMENT_ID,
                                        .CODE = p.CODE,
                                        .NAME = p.NAME,
                                        .EFFECTIVE_DATE = p.EFFECTIVE_DATE,
                                        .VALUE = p.VALUE,
                                        .SDESC = p.SDESC,
                                        .ACTFLG = p.ACTFLG
                                     })
            lst = lst.OrderBy(Sorts)
            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function InsertPaymentList(ByVal objTitle As PAPaymentListDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New PA_PAYMENT_LIST
        Dim iCount As Integer = 0
        Try
            objTitleData.ID = Utilities.GetNextSequence(Context, Context.PA_PAYMENT_LIST.EntitySet.Name)
            objTitleData.OBJ_PAYMENT_ID = objTitle.OBJ_PAYMENT_ID
            objTitleData.CODE = objTitle.CODE
            objTitleData.NAME = objTitle.NAME
            objTitleData.EFFECTIVE_DATE = objTitle.EFFECTIVE_DATE
            objTitleData.VALUE = objTitle.VALUE
            objTitleData.SDESC = objTitle.SDESC
            objTitleData.ACTFLG = objTitle.ACTFLG
            objTitleData.CREATED_BY = objTitle.CREATED_BY
            objTitleData.CREATED_DATE = objTitle.CREATED_DATE
            objTitleData.CREATED_LOG = objTitle.CREATED_LOG
            objTitleData.MODIFIED_BY = objTitle.MODIFIED_BY
            objTitleData.MODIFIED_DATE = objTitle.MODIFIED_DATE
            objTitleData.MODIFIED_LOG = objTitle.MODIFIED_LOG
            Context.PA_PAYMENT_LIST.AddObject(objTitleData)
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function ModifyPaymentList(ByVal objTitle As PAPaymentListDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New PA_PAYMENT_LIST With {.ID = objTitle.ID}
        Try
            objTitleData = (From p In Context.PA_PAYMENT_LIST Where p.ID = objTitleData.ID).SingleOrDefault
            objTitleData.ID = objTitle.ID
            objTitleData.OBJ_PAYMENT_ID = objTitle.OBJ_PAYMENT_ID
            objTitleData.EFFECTIVE_DATE = objTitle.EFFECTIVE_DATE
            objTitleData.VALUE = objTitle.VALUE
            objTitleData.SDESC = objTitle.SDESC
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
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function ActivePaymentList(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstData As List(Of PA_PAYMENT_LIST)
        Try
            lstData = (From p In Context.PA_PAYMENT_LIST Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                lstData(index).ACTFLG = bActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function DeletePaymentList(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstPaymentListData As List(Of PA_PAYMENT_LIST)
        Try
            lstPaymentListData = (From p In Context.PA_PAYMENT_LIST Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstPaymentListData.Count - 1
                Context.PA_PAYMENT_LIST.DeleteObject(lstPaymentListData(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteCostCenter")
            Throw ex
        End Try
    End Function

#End Region

#Region "Object Salary"

    Public Function GetObjectSalary(ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PAObjectSalaryDTO)
        Try

            Dim query = From p In Context.PA_OBJECT_SALARY
            Dim lst = query.Select(Function(p) New PAObjectSalaryDTO With {
                                        .ID = p.ID,
                                        .CODE = p.CODE,
                                        .NAME_VN = p.NAME_VN,
                                        .NAME_EN = p.NAME_EN,
                                        .EFFECTIVE_DATE = p.EFFECTIVE_DATE,
                                        .ACTFLG = p.ACTFLG,
                                        .SDESC = p.SDESC,
                                        .CREATED_BY = p.CREATED_BY,
                                        .CREATED_DATE = p.CREATED_DATE,
                                        .CREATED_LOG = p.CREATED_LOG,
                                        .MODIFIED_BY = p.MODIFIED_BY,
                                        .MODIFIED_DATE = p.MODIFIED_DATE,
            .MODIFIED_LOG = p.MODIFIED_LOG
                                        })
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function GetObjectSalaryAll(Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PAObjectSalaryDTO)
        Try

            Dim query = From p In Context.PA_OBJECT_SALARY
            Dim lst = query.Select(Function(p) New PAObjectSalaryDTO With {
                                        .ID = p.ID,
                                        .CODE = p.CODE,
                                        .NAME_VN = p.NAME_VN,
                                        .NAME_EN = p.NAME_EN,
                                        .EFFECTIVE_DATE = p.EFFECTIVE_DATE,
                                        .SDESC = p.SDESC,
                                        .CREATED_BY = p.CREATED_BY,
                                        .CREATED_DATE = p.CREATED_DATE,
                                        .CREATED_LOG = p.CREATED_LOG,
                                        .MODIFIED_BY = p.MODIFIED_BY,
                                        .MODIFIED_DATE = p.MODIFIED_DATE,
                                        .MODIFIED_LOG = p.MODIFIED_LOG
                                        })
            lst = lst.OrderBy(Sorts)
            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function InsertObjectSalary(ByVal objTitle As PAObjectSalaryDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New PA_OBJECT_SALARY
        Dim iCount As Integer = 0
        Try
            objTitleData.ID = Utilities.GetNextSequence(Context, Context.PA_OBJECT_SALARY.EntitySet.Name)
            objTitleData.CODE = objTitle.CODE
            objTitleData.NAME_VN = objTitle.NAME_VN
            objTitleData.NAME_EN = objTitle.NAME_EN
            objTitleData.EFFECTIVE_DATE = objTitle.EFFECTIVE_DATE
            objTitleData.SDESC = objTitle.SDESC
            objTitleData.CREATED_BY = objTitle.CREATED_BY
            objTitleData.CREATED_DATE = objTitle.CREATED_DATE
            objTitleData.CREATED_LOG = objTitle.CREATED_LOG
            objTitleData.MODIFIED_BY = objTitle.MODIFIED_BY
            objTitleData.MODIFIED_DATE = objTitle.MODIFIED_DATE
            objTitleData.MODIFIED_LOG = objTitle.MODIFIED_LOG
            Context.PA_OBJECT_SALARY.AddObject(objTitleData)
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function ValidateObjectSalary(ByVal _validate As PAObjectSalaryDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.PA_OBJECT_SALARY
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.PA_OBJECT_SALARY
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper).FirstOrDefault
                End If
                Return (query Is Nothing)
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function ModifyObjectSalary(ByVal objTitle As PAObjectSalaryDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New PA_OBJECT_SALARY With {.ID = objTitle.ID}
        Try
            objTitleData = (From p In Context.PA_OBJECT_SALARY Where p.ID = objTitleData.ID).SingleOrDefault
            objTitleData.ID = objTitle.ID
            objTitleData.CODE = objTitle.CODE
            objTitleData.NAME_VN = objTitle.NAME_VN
            objTitleData.NAME_EN = objTitle.NAME_EN
            objTitleData.EFFECTIVE_DATE = objTitle.EFFECTIVE_DATE
            objTitleData.SDESC = objTitle.SDESC
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
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function ActiveObjectSalary(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstData As List(Of PA_OBJECT_SALARY)
        Try
            lstData = (From p In Context.PA_OBJECT_SALARY Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                lstData(index).ACTFLG = bActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function DeleteObjectSalary(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstObjectSalaryData As List(Of PA_OBJECT_SALARY)
        Try
            lstObjectSalaryData = (From p In Context.PA_OBJECT_SALARY Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstObjectSalaryData.Count - 1
                Context.PA_OBJECT_SALARY.DeleteObject(lstObjectSalaryData(index))
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteCostCenter")
            Throw ex
        End Try
    End Function

#End Region

#Region "Period list"

    Public Function GetPeriodList(ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "START_DATE desc") As List(Of ATPeriodDTO)

        Try
            Dim query = From p In Context.AT_PERIOD

            Dim lst = query.Select(Function(p) New ATPeriodDTO With {
                                       .ID = p.ID,
                                       .YEAR = p.YEAR,
                                       .MONTH = p.MONTH,
                                       .PERIOD_NAME = p.PERIOD_NAME,
                                       .START_DATE = p.START_DATE,
                                       .END_DATE = p.END_DATE,
                                       .CREATED_DATE = p.CREATED_DATE,
                                       .CREATED_BY = p.CREATED_BY,
                                       .PERIOD_STANDARD = p.PERIOD_STANDARD})


            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try


    End Function

    Public Function GetPeriodbyYear(ByVal year As Decimal) As List(Of ATPeriodDTO)
        Try
            Dim query = From p In Context.AT_PERIOD Where p.YEAR = year Order By p.MONTH Ascending, p.START_DATE Ascending
            Dim Period = query.Select(Function(p) New ATPeriodDTO With {
                                       .ID = p.ID,
                                       .YEAR = p.YEAR,
                                       .MONTH = p.MONTH,
                                       .PERIOD_NAME = p.PERIOD_NAME,
                                       .START_DATE = p.START_DATE,
                                       .END_DATE = p.END_DATE,
                                       .CREATED_DATE = p.CREATED_DATE,
                                       .CREATED_BY = p.CREATED_BY,
                                       .PERIOD_STANDARD = p.PERIOD_STANDARD})


            Return Period.ToList

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function GetOrgByPeriodID(ByVal periodID As Decimal) As List(Of Decimal)
        Try
            Dim query = From p In Context.AT_ORG_PERIOD
                        Where p.PERIOD_ID = periodID
                        Select p.ORG_ID.Value

            Return query.ToList

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function InsertPeriod(ByVal objPeriod As ATPeriodDTO, ByVal objOrgPeriod As List(Of AT_ORG_PERIOD), ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim iCount As Integer = 0
        Dim objPeriodData As New AT_PERIOD
        Dim objOrgPeriodData As AT_ORG_PERIOD
        Try
            objPeriodData.ID = Utilities.GetNextSequence(Context, Context.AT_PERIOD.EntitySet.Name)
            objPeriodData.YEAR = objPeriod.YEAR
            objPeriodData.MONTH = objPeriod.MONTH
            objPeriodData.PERIOD_NAME = objPeriod.PERIOD_NAME
            objPeriodData.START_DATE = objPeriod.START_DATE
            objPeriodData.END_DATE = objPeriod.END_DATE
            objPeriodData.PERIOD_STANDARD = objPeriod.PERIOD_STANDARD
            Context.AT_PERIOD.AddObject(objPeriodData)
            'Context.SaveChanges(log)
            If objPeriodData.ID > 0 Then
                For Each obj As AT_ORG_PERIOD In objOrgPeriod
                    objOrgPeriodData = New AT_ORG_PERIOD
                    objOrgPeriodData.ID = Utilities.GetNextSequence(Context, Context.AT_ORG_PERIOD.EntitySet.Name)
                    objOrgPeriodData.ORG_ID = obj.ORG_ID
                    objOrgPeriodData.PERIOD_ID = objPeriodData.ID
                    objOrgPeriodData.STATUSCOLEX = 1
                    objOrgPeriodData.STATUSPAROX = 1
                    Context.AT_ORG_PERIOD.AddObject(objOrgPeriodData)
                Next
            End If
            Context.SaveChanges(log)
            gID = objPeriodData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function ValidateATPeriod(ByVal _validate As ATPeriodDTO) As Boolean
        Try
            If _validate.ID <> 0 Then
                If _validate.ID <> 0 Then
                    Dim query = (From p In Context.AT_ORG_PERIOD Where p.PERIOD_ID = _validate.ID).ToList
                    If query.Count > 0 Then
                        Return False
                    Else
                        Return True
                    End If
                End If

            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function ValidateATPeriodDay(ByVal _validate As ATPeriodDTO)
        Dim query
        Try
            If _validate.START_DATE IsNot Nothing And _validate.END_DATE IsNot Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.AT_PERIOD
                             Where (_validate.START_DATE <= p.END_DATE And _validate.END_DATE >= p.START_DATE) _
                             And p.YEAR = _validate.YEAR _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.AT_PERIOD
                             Where (_validate.START_DATE <= p.END_DATE And _validate.END_DATE >= p.START_DATE) _
                             And p.YEAR = _validate.YEAR).FirstOrDefault
                End If
                Return (query Is Nothing)
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function ModifyPeriod(ByVal objPeriod As ATPeriodDTO, ByVal objOrgPeriod As List(Of AT_ORG_PERIOD), ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objPeriodData As New AT_PERIOD With {.ID = objPeriod.ID}
        Dim objOrgPeriodData As AT_ORG_PERIOD
        Try
            Context.AT_PERIOD.Attach(objPeriodData)
            objPeriodData.YEAR = objPeriod.YEAR
            objPeriodData.MONTH = objPeriod.MONTH
            objPeriodData.PERIOD_NAME = objPeriod.PERIOD_NAME
            objPeriodData.START_DATE = objPeriod.START_DATE
            objPeriodData.END_DATE = objPeriod.END_DATE
            objPeriodData.PERIOD_STANDARD = objPeriod.PERIOD_STANDARD
            If objPeriodData.ID > 0 Then
                For Each ObjIns As AT_ORG_PERIOD In objOrgPeriod
                    Dim org_id = ObjIns.ORG_ID
                    Dim objDelete As List(Of AT_ORG_PERIOD) = (From p In Context.AT_ORG_PERIOD Where p.PERIOD_ID = objPeriodData.ID And p.ORG_ID = org_id).ToList
                    If objDelete.Count = 0 Then
                        objOrgPeriodData = New AT_ORG_PERIOD
                        objOrgPeriodData.ID = Utilities.GetNextSequence(Context, Context.AT_ORG_PERIOD.EntitySet.Name)
                        objOrgPeriodData.ORG_ID = ObjIns.ORG_ID
                        objOrgPeriodData.STATUSCOLEX = 1
                        objOrgPeriodData.STATUSPAROX = 1
                        objOrgPeriodData.PERIOD_ID = objPeriodData.ID
                        Context.AT_ORG_PERIOD.AddObject(objOrgPeriodData)
                    End If

                Next
            End If
            Context.SaveChanges(log)
            gID = objPeriodData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function DeletePeriod(ByVal lstPeriod As ATPeriodDTO) As Boolean
        Dim objOrgPeriod As List(Of AT_ORG_PERIOD) = (From p In Context.AT_ORG_PERIOD Where p.PERIOD_ID = lstPeriod.ID).ToList
        Dim objPeriod As List(Of AT_PERIOD) = (From p In Context.AT_PERIOD Where p.ID = lstPeriod.ID).ToList
        Try
            For Each item In objOrgPeriod
                Context.AT_ORG_PERIOD.DeleteObject(item)
            Next
            For Each item In objPeriod
                Context.AT_PERIOD.DeleteObject(item)
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

#End Region

#Region "List Salary Fomuler"

    Public Function GetAllFomulerGroup() As List(Of PAFomulerGroup)
        Try
            Dim query = From p In Context.PA_FORMULER_GROUP
                        From o In Context.OT_OTHER_LIST.Where(Function(o) o.ID = p.TYPE_PAYMENT)
                        From s In Context.PA_OBJECT_SALARY.Where(Function(s) s.ID = p.OBJ_SAL_ID)
                        Where o.TYPE_ID = 1037
            Dim obj = query.Select(Function(q) New PAFomulerGroup With
                                               {.ID = q.p.ID,
                                                .NAME_VN = q.p.NAME_VN,
                                                .NAME_EN = q.p.NAME_EN,
                                                .OBJ_SAL_ID = q.p.OBJ_SAL_ID,
                                                .OBJ_SAL_NAME = q.s.NAME_VN,
                                                .TYPE_PAYMENT = q.p.TYPE_PAYMENT,
                                                .TYPE_PAYMENT_NAME = q.o.NAME_VN,
                                                .START_DATE = q.p.START_DATE,
                                                .END_DATE = q.p.END_DATE,
                                                .SDESC = q.p.SDESC,
                                                .STATUS = q.p.STATUS,
                                                .IDX = q.p.IDX
                                                }).ToList

            Return obj
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function InsertFomulerGroup(ByVal objFomuler As PAFomulerGroup, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objData As New PA_FORMULER_GROUP
        Try
            objData.ID = Utilities.GetNextSequence(Context, Context.AT_PERIOD.EntitySet.Name)
            objData.TYPE_PAYMENT = objFomuler.TYPE_PAYMENT
            objData.OBJ_SAL_ID = objFomuler.OBJ_SAL_ID
            objData.NAME_VN = objFomuler.NAME_VN
            objData.NAME_EN = objFomuler.NAME_EN
            objData.START_DATE = objFomuler.START_DATE
            objData.END_DATE = objFomuler.END_DATE
            objData.STATUS = objFomuler.STATUS
            objData.SDESC = objFomuler.SDESC
            objData.IDX = objFomuler.IDX
            Context.PA_FORMULER_GROUP.AddObject(objData)
            Context.SaveChanges(log)
            gID = objData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function ModifyFomulerGroup(ByVal objPeriod As PAFomulerGroup, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objData As New PA_FORMULER_GROUP With {.ID = objPeriod.ID}
        Try
            Context.PA_FORMULER_GROUP.Attach(objData)
            objData.TYPE_PAYMENT = objPeriod.TYPE_PAYMENT
            objData.OBJ_SAL_ID = objPeriod.OBJ_SAL_ID
            objData.NAME_VN = objPeriod.NAME_VN
            objData.NAME_EN = objPeriod.NAME_EN
            objData.START_DATE = objPeriod.START_DATE
            objData.END_DATE = objPeriod.END_DATE
            objData.STATUS = objPeriod.STATUS
            objData.SDESC = objPeriod.SDESC
            objData.IDX = objPeriod.IDX
            Context.SaveChanges(log)
            gID = objData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function DeleteFomulerGroup(ByVal lstDelete As PAFomulerGroup) As Boolean
        Dim objData As List(Of PA_FORMULER_GROUP) = (From p In Context.PA_FORMULER_GROUP Where p.ID = lstDelete.ID).ToList
        Try
            For Each item In objData
                Context.PA_FORMULER_GROUP.DeleteObject(item)
            Next
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function GetListAllSalary(ByVal gID As Decimal) As List(Of PAFomuler)
        Try
            Dim query = From p In Context.PA_LISTSALARIES
                        From g In Context.PA_FORMULER_GROUP.Where(Function(g) g.TYPE_PAYMENT = p.TYPE_PAYMENT)
                         From f In Context.PA_FORMULER.Where(Function(f) f.GROUP_FML = g.ID And f.COL_NAME = p.COL_NAME).DefaultIfEmpty
                         Where g.ID = gID And p.IS_CALCULATE = -1 Order By p.COL_INDEX Ascending
            Dim obj = query.Select(Function(o) New PAFomuler With
                        {.ID = o.p.ID,
                         .COL_NAME = o.p.COL_NAME,
                         .NAME_VN = o.p.NAME_VN,
                         .NAME_EN = o.p.NAME_EN,
                         .COL_INDEX = o.f.INDEX_FML,
                         .FORMULER = o.f.FORMULER
                        }).ToList
            Return obj
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function GetListInputColumn(ByVal gID As Decimal) As DataTable
        Try
            Dim query = From p In Context.PA_LISTSALARIES
                        From g In Context.PA_FORMULER_GROUP.Where(Function(g) g.TYPE_PAYMENT = p.TYPE_PAYMENT)
                        Where p.STATUS = "A" And p.IS_INPUT = -1 And g.ID = gID Order By p.NAME_VN, p.COL_INDEX Ascending
            Dim obj = query.Select(Function(f) New PAListSalariesDTO With
                        {.ID = f.p.ID,
                         .COL_INDEX = f.p.COL_INDEX,
                         .COL_NAME = f.p.COL_NAME,
                         .NAME_VN = f.p.NAME_VN & " - (" & f.p.COL_NAME & ")",
                         .NAME_EN = f.p.NAME_EN
                        }).ToList
            Return obj.ToTable()
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function GetListCalculation() As List(Of OT_OTHERLIST_DTO)
        Try
            Dim query = (From p In Context.OT_OTHER_LIST Join t In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals t.ID
                                             Where p.ACTFLG = "A" And t.CODE = "CALCULATION" Order By p.CREATED_DATE Descending
                         Select New OT_OTHERLIST_DTO With {
                             .ID = p.ID,
                             .CODE = p.CODE,
                             .NAME_EN = p.NAME_EN,
                             .NAME_VN = p.NAME_VN,
                .TYPE_ID = p.TYPE_ID
                         }).ToList
            Return query
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function SaveFomuler(ByVal objData As PAFomuler, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objInsert As PA_FORMULER
        Dim iCount As Integer = 0
        Try
            objInsert = (From p In Context.PA_FORMULER Where p.COL_NAME = objData.COL_NAME And p.GROUP_FML = objData.GROUP_FML).SingleOrDefault
            If objInsert Is Nothing Then
                objInsert = New PA_FORMULER
                objInsert.ID = Utilities.GetNextSequence(Context, Context.PA_FORMULER.EntitySet.Name)
                objInsert.COL_NAME = objData.COL_NAME
                objInsert.INDEX_FML = objData.INDEX_FML
                objInsert.GROUP_FML = objData.GROUP_FML
                objInsert.FORMULER = objData.FORMULER
                objInsert.CREATED_BY = objData.CREATED_BY
                objInsert.CREATED_DATE = objData.CREATED_DATE
                objInsert.CREATED_LOG = objData.CREATED_LOG
                objInsert.MODIFIED_BY = objData.MODIFIED_BY
                objInsert.MODIFIED_DATE = objData.MODIFIED_DATE
                objInsert.MODIFIED_LOG = objData.MODIFIED_LOG
                Context.PA_FORMULER.AddObject(objInsert)
            Else
                objInsert.COL_NAME = objData.COL_NAME
                objInsert.INDEX_FML = objData.INDEX_FML
                objInsert.GROUP_FML = objData.GROUP_FML
                objInsert.FORMULER = objData.FORMULER
                objInsert.CREATED_BY = objData.CREATED_BY
                objInsert.CREATED_DATE = objData.CREATED_DATE
                objInsert.CREATED_LOG = objData.CREATED_LOG
                objInsert.MODIFIED_BY = objData.MODIFIED_BY
                objInsert.MODIFIED_DATE = objData.MODIFIED_DATE
                objInsert.MODIFIED_LOG = objData.MODIFIED_LOG
            End If
            Context.SaveChanges(log)
            gID = objInsert.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function CheckFomuler(ByVal sCol As String, ByVal sFormuler As String, ByVal objID As Decimal) As Boolean
        Try
            Using cls As New DataAccess.NonQueryData
                Dim sql As String = ""
                Select Case objID
                    Case 4122
                        sql = "UPDATE TEMP_CALCULATE S SET S." & sCol & " = " & sFormuler
                    Case 4123
                        sql = "UPDATE TEMP_CALCULATE_SUM S SET S." & sCol & " = " & sFormuler
                End Select
                cls.ExecuteSQL(sql)
            End Using
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function ActiveFolmulerGroup(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As Decimal) As Boolean
        Dim lstData As PA_FORMULER_GROUP
        Try
            For Each id As Decimal In lstID
                lstData = (From p In Context.PA_FORMULER_GROUP Where p.ID = id).SingleOrDefault
                lstData.STATUS = bActive
                Context.SaveChanges(log)
            Next
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

#End Region

#Region "Salaries List"

    Public Function GetListSalaries(ByVal _filter As PAListSalariesDTO,
                                        ByVal TypePaymentId As Integer,
                                        Optional ByVal PageIndex As Integer = 0,
                                        Optional ByVal PageSize As Integer = Integer.MaxValue,
                                        Optional ByRef Total As Integer = 0,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PAListSalariesDTO)
        Try
            Dim lst = (From p In Context.PA_LISTSALARIES
                        From o In Context.OT_OTHER_LIST.Where(Function(o) o.ID = p.TYPE_PAYMENT)
                        From ot In Context.OT_OTHER_LIST_TYPE.Where(Function(ot) ot.ID = o.TYPE_ID)
                        Where p.TYPE_PAYMENT = TypePaymentId
           Select New PAListSalariesDTO With {
                                        .ID = p.ID,
                                        .TYPE_PAYMENT = p.TYPE_PAYMENT,
                                        .COL_NAME = p.COL_NAME,
                                        .NAME_VN = p.NAME_VN,
                                        .NAME_EN = p.NAME_EN,
                                        .DATA_TYPE = p.DATA_TYPE,
                                        .COL_INDEX = p.COL_INDEX,
                                        .STATUS = If(p.STATUS = "A", "Áp dụng", "Ngừng Áp dụng"),
                                        .IS_VISIBLE = p.IS_VISIBLE,
                                        .IS_INPUT = p.IS_INPUT,
                                        .IS_CALCULATE = p.IS_CALCULATE,
                                        .IS_IMPORT = p.IS_IMPORT,
                                        .INPUT_FORMULER = p.INPUT_FORMULER,
                                        .CREATED_DATE = p.CREATED_DATE})


            If _filter.TYPE_PAYMENT.HasValue Then
                lst = lst.Where(Function(f) f.TYPE_PAYMENT = _filter.TYPE_PAYMENT)
            End If
            If Not String.IsNullOrEmpty(_filter.COL_NAME) Then
                lst = lst.Where(Function(f) f.COL_NAME.ToLower().Contains(_filter.COL_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.NAME_VN) Then
                lst = lst.Where(Function(f) f.NAME_VN.ToLower().Contains(_filter.NAME_VN.ToLower()))
            End If
            If _filter.DATA_TYPE.HasValue Then
                lst = lst.Where(Function(f) f.DATA_TYPE.Value = _filter.DATA_TYPE)
            End If
            If _filter.COL_INDEX.HasValue Then
                lst = lst.Where(Function(f) f.COL_INDEX.Value = _filter.COL_INDEX)
            End If
            If Not String.IsNullOrEmpty(_filter.STATUS) Then
                lst = lst.Where(Function(f) f.STATUS.ToLower().Contains(_filter.STATUS.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.INPUT_FORMULER) Then
                lst = lst.Where(Function(f) f.INPUT_FORMULER.ToLower().Contains(_filter.INPUT_FORMULER.ToLower()))
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

    Public Function CheckColName(ByVal COl_NAME As String, ByVal TypeID As Decimal) As Boolean
        Try
            Dim dt As New DataTable
            Using cls As New DataAccess.QueryData
                Select Case TypeID
                    Case 4122
                        dt = cls.ExecuteSQL("SELECT * FROM PA_PAYROLLSHEET_TEMP WHERE 1=0", True)
                    Case 4123
                        dt = cls.ExecuteSQL("SELECT * FROM PA_PAYROLLSHEET_SUM WHERE 1=0", True)
                End Select
            End Using
            If dt Is Nothing OrElse dt.Columns.Contains(COl_NAME) Then
                Return False
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function InsertListSalaries(ByVal objTitle As PAListSalariesDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New PA_LISTSALARIES
        Dim iCount As Integer = 0
        Try
            objTitleData.TYPE_PAYMENT = objTitle.TYPE_PAYMENT
            objTitleData.COL_NAME = objTitle.COL_NAME
            objTitleData.NAME_VN = objTitle.NAME_VN
            objTitleData.NAME_EN = objTitle.NAME_EN
            objTitleData.DATA_TYPE = objTitle.DATA_TYPE
            objTitleData.COL_INDEX = objTitle.COL_INDEX
            objTitleData.STATUS = objTitle.STATUS
            objTitleData.IS_VISIBLE = objTitle.IS_VISIBLE
            objTitleData.IS_INPUT = objTitle.IS_INPUT
            objTitleData.IS_CALCULATE = objTitle.IS_CALCULATE
            objTitleData.IS_IMPORT = objTitle.IS_IMPORT
            objTitleData.INPUT_FORMULER = objTitle.INPUT_FORMULER
            Using cls As New DataAccess.NonQueryData
                cls.ExecuteStore("PKG_PA_SETTING.ADD_COL_SALARY",
                                           New With {.COL_NAME = objTitle.COL_NAME,
                                                     .COL_TYPE = objTitle.DATA_TYPE,
                                                     .TYPE_PAYMENT = objTitle.TYPE_PAYMENT})
            End Using
            objTitleData.ID = Utilities.GetNextSequence(Context, Context.PA_LISTSALARIES.EntitySet.Name)
            Context.PA_LISTSALARIES.AddObject(objTitleData)
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function ModifyListSalaries(ByVal objTitle As PAListSalariesDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New PA_LISTSALARIES With {.ID = objTitle.ID}
        Dim old_Name As String
        Try
            objTitleData = (From p In Context.PA_LISTSALARIES Where p.ID = objTitleData.ID).SingleOrDefault
            old_Name = objTitleData.COL_NAME
            objTitleData.TYPE_PAYMENT = objTitle.TYPE_PAYMENT
            objTitleData.COL_NAME = objTitle.COL_NAME
            objTitleData.NAME_VN = objTitle.NAME_VN
            objTitleData.NAME_EN = objTitle.NAME_EN
            objTitleData.DATA_TYPE = objTitle.DATA_TYPE
            objTitleData.COL_INDEX = objTitle.COL_INDEX
            objTitleData.STATUS = objTitle.STATUS
            objTitleData.IS_VISIBLE = objTitle.IS_VISIBLE
            objTitleData.IS_INPUT = objTitle.IS_INPUT
            objTitleData.IS_CALCULATE = objTitle.IS_CALCULATE
            objTitleData.IS_IMPORT = objTitle.IS_IMPORT
            objTitleData.INPUT_FORMULER = objTitle.INPUT_FORMULER
            Using cls As New DataAccess.NonQueryData
                cls.ExecuteStore("PKG_PA_SETTING.EDIT_COL_SALARY",
                                           New With {.COL_NAME = old_Name,
                                                     .COL_NAME_NEW = objTitleData.COL_NAME,
                                                     .TYPE_PAYMENT = objTitleData.TYPE_PAYMENT})
            End Using
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function ActiveListSalaries(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstData As List(Of PA_LISTSALARIES)
        Try
            lstData = (From p In Context.PA_LISTSALARIES Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                lstData(index).STATUS = bActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function DeleteListSalaries(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstListSalariesData As List(Of PA_LISTSALARIES)
        Try
            lstListSalariesData = (From p In Context.PA_LISTSALARIES Where lstID.Contains(p.ID)).ToList
            Using cls As New DataAccess.NonQueryData
                For index = 0 To lstListSalariesData.Count - 1
                    cls.ExecuteStore("PKG_PA_SETTING.DELETE_COL_SALARY",
                                            New With {.COL_NAME = lstListSalariesData(index).COL_NAME,
                                                      .TYPE_PAYMENT = lstListSalariesData(index).TYPE_PAYMENT})
                Next
            End Using
            For index = 0 To lstListSalariesData.Count - 1
                Context.PA_LISTSALARIES.DeleteObject(lstListSalariesData(index))
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteCostCenter")
            Throw ex
        End Try
    End Function

#End Region

#Region "lunch list : Đơn giá tiền ăn trưa"

    Public Function GetPriceLunchList(ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "EFFECT_DATE desc",
                                        Optional ByVal log As UserLog = Nothing) As List(Of ATPriceLunchDTO)

        Try
            Dim query = From p In Context.PA_PRICE_LUNCH

            If log.Username.ToUpper <> "ADMIN" And log.Username.ToUpper <> "HR.ADMIN" And log.Username.ToUpper <> "SYS.ADMIN" Then
                query = query.Where(Function(f) f.CREATED_BY.ToUpper = log.Username.ToUpper)
            End If

            Dim lst = query.Select(Function(p) New ATPriceLunchDTO With {
                                       .ID = p.ID,
                                       .PRICE = p.PRICE,
                                       .EFFECT_DATE = p.EFFECT_DATE,
                                       .EXPIRE_DATE = p.EXPIRE_DATE,
                                       .REMARK = p.REMARK,
                                       .CREATED_DATE = p.CREATED_DATE,
                                       .CREATED_BY = p.CREATED_BY})


            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try


    End Function

    Public Function GetPriceLunch(ByVal year As Decimal) As List(Of ATPriceLunchDTO)
        Try
            Dim query = From p In Context.PA_PRICE_LUNCH Where p.ID = year Order By p.ID Ascending, p.EFFECT_DATE Ascending
            Dim Period = query.Select(Function(p) New ATPriceLunchDTO With {
                                       .ID = p.ID,
                                       .PRICE = p.PRICE,
                                       .EFFECT_DATE = p.EFFECT_DATE,
                                       .EXPIRE_DATE = p.EXPIRE_DATE,
                                       .REMARK = p.REMARK,
                                       .CREATED_DATE = p.CREATED_DATE,
                                       .CREATED_BY = p.CREATED_BY})


            Return Period.ToList

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function InsertPriceLunch(ByVal objPeriod As ATPriceLunchDTO, ByVal objOrgPeriod As List(Of PA_ORG_LUNCH), ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim iCount As Integer = 0
        Dim objPeriodData As New PA_PRICE_LUNCH
        Dim objOrgPeriodData As PA_ORG_LUNCH
        Try
            objPeriodData.ID = Utilities.GetNextSequence(Context, Context.PA_PRICE_LUNCH.EntitySet.Name)
            objPeriodData.PRICE = objPeriod.PRICE
            objPeriodData.EFFECT_DATE = objPeriod.EFFECT_DATE
            objPeriodData.EXPIRE_DATE = objPeriod.EXPIRE_DATE
            objPeriodData.REMARK = objPeriod.REMARK
            Context.PA_PRICE_LUNCH.AddObject(objPeriodData)
            Context.SaveChanges(log)
            If objPeriodData.ID > 0 Then
                For Each obj As PA_ORG_LUNCH In objOrgPeriod
                    objOrgPeriodData = New PA_ORG_LUNCH
                    objOrgPeriodData.ID = Utilities.GetNextSequence(Context, Context.PA_ORG_LUNCH.EntitySet.Name)
                    objOrgPeriodData.ORG_ID = obj.ORG_ID
                    objOrgPeriodData.LUNCH_ID = objPeriodData.ID
                    Context.PA_ORG_LUNCH.AddObject(objOrgPeriodData)
                    Context.SaveChanges(log)
                Next
            End If
            gID = objPeriodData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function ValidateATPriceLunch(ByVal _validate As ATPriceLunchDTO) As Boolean
        Try
            If _validate.ID <> 0 Then
                If _validate.ID <> 0 Then
                    Dim query = (From p In Context.PA_ORG_LUNCH Where p.LUNCH_ID = _validate.ID).ToList
                    If query.Count > 0 Then
                        Return False
                    Else
                        Return True
                    End If
                End If

            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function ValidateATPriceLunchOrg(ByVal _validate As ATPriceLunchDTO)
        Dim query
        Try

            If _validate.ID <> 0 Then
                query = (From p In Context.PA_PRICE_LUNCH
                         Where ((p.EFFECT_DATE <= _validate.EFFECT_DATE And p.EXPIRE_DATE >= _validate.EXPIRE_DATE) _
                                Or (p.EFFECT_DATE <= _validate.EFFECT_DATE And p.EXPIRE_DATE >= _validate.EFFECT_DATE) _
                                Or (p.EFFECT_DATE <= _validate.EXPIRE_DATE And p.EXPIRE_DATE >= _validate.EXPIRE_DATE) _
                                ) _
                         And p.ID <> _validate.ID).FirstOrDefault
            Else
                query = (From p In Context.PA_PRICE_LUNCH
                         Where (p.EFFECT_DATE <= _validate.EFFECT_DATE And p.EXPIRE_DATE >= _validate.EXPIRE_DATE) _
                                Or (p.EFFECT_DATE <= _validate.EFFECT_DATE And p.EXPIRE_DATE >= _validate.EFFECT_DATE) _
                                Or (p.EFFECT_DATE <= _validate.EXPIRE_DATE And p.EXPIRE_DATE >= _validate.EXPIRE_DATE)).FirstOrDefault
            End If
            Return (query Is Nothing)

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function ModifyPriceLunch(ByVal objPeriod As ATPriceLunchDTO, ByVal objOrgPeriod As List(Of PA_ORG_LUNCH), ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objPeriodData As New PA_PRICE_LUNCH With {.ID = objPeriod.ID}
        Dim objOrgPeriodData As PA_ORG_LUNCH
        Try
            Context.PA_PRICE_LUNCH.Attach(objPeriodData)
            objPeriodData.PRICE = objPeriod.PRICE
            objPeriodData.EFFECT_DATE = objPeriod.EFFECT_DATE
            objPeriodData.EXPIRE_DATE = objPeriod.EXPIRE_DATE
            objPeriodData.REMARK = objPeriod.REMARK
            If objPeriodData.ID > 0 Then

                If objOrgPeriod IsNot Nothing Then
                    Dim objDelete As List(Of PA_ORG_LUNCH) = (From p In Context.PA_ORG_LUNCH Where p.LUNCH_ID = objPeriodData.ID).ToList
                    For Each obj As PA_ORG_LUNCH In objDelete
                        Context.PA_ORG_LUNCH.DeleteObject(obj)
                    Next
                End If

                For Each ObjIns As PA_ORG_LUNCH In objOrgPeriod
                    objOrgPeriodData = New PA_ORG_LUNCH
                    objOrgPeriodData.ID = Utilities.GetNextSequence(Context, Context.PA_ORG_LUNCH.EntitySet.Name)
                    objOrgPeriodData.ORG_ID = ObjIns.ORG_ID
                    objOrgPeriodData.LUNCH_ID = objPeriodData.ID
                    Context.PA_ORG_LUNCH.AddObject(objOrgPeriodData)
                Next
            End If
            Context.SaveChanges(log)
            gID = objPeriodData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function DeletePriceLunch(ByVal lstPeriod As ATPriceLunchDTO) As Boolean
        Dim objOrgPeriod As List(Of PA_ORG_LUNCH) = (From p In Context.PA_ORG_LUNCH Where p.LUNCH_ID = lstPeriod.ID).ToList
        Dim objPeriod As List(Of PA_PRICE_LUNCH) = (From p In Context.PA_PRICE_LUNCH Where p.ID = lstPeriod.ID).ToList
        Try
            For Each item In objOrgPeriod
                Context.PA_ORG_LUNCH.DeleteObject(item)
            Next
            For Each item In objPeriod
                Context.PA_PRICE_LUNCH.DeleteObject(item)
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function GetOrgByLunchID(ByVal lunchID As Decimal) As List(Of Decimal)
        Try
            Dim query = From p In Context.PA_ORG_LUNCH
                        Where p.LUNCH_ID = lunchID
                        Select p.ORG_ID.Value

            Return query.ToList

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function



#End Region

#Region "don gia tien an trua theo nhan vien"



    Public Function GetPA_EMP_LUNCH(ByVal _filter As PA_EMP_LUNCHDTO,
                                      ByVal _param As PA_ParamDTO,
                                     Optional ByRef Total As Integer = 0,
                                     Optional ByVal PageIndex As Integer = 0,
                                     Optional ByVal PageSize As Integer = Integer.MaxValue,
                                     Optional ByVal Sorts As String = "EFFECT_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of PA_EMP_LUNCHDTO)

        Try

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username.ToUpper,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using

            Dim query = From p In Context.PA_EMP_LUNCH
                        From e In Context.HU_EMPLOYEE.Where(Function(o) o.ID = p.EMPLOYEE_ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(o) o.ID = p.ORG_ID).DefaultIfEmpty
                         From t In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                         From k In Context.SE_CHOSEN_ORG.Where(Function(f) e.ORG_ID = f.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper)


            Dim lst = query.Select(Function(p) New PA_EMP_LUNCHDTO With {
                                       .ID = p.p.ID,
                                       .ORG_ID = p.p.ORG_ID,
                                       .ORG_NAME = p.o.NAME_VN,
                                       .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                       .EMPLOYEE_NAME = p.e.FULLNAME_VN,
                                       .TITLE_NAME = p.t.NAME_VN,
                                       .EFFECT_DATE = p.p.EFFECT_DATE,
                                       .EXPIRE_DATE = p.p.EXPIRE_DATE,
                                       .PRICE = p.p.PRICE,
                                       .REMARK = p.p.REMARK,
                                       .CREATED_BY = p.p.CREATED_BY,
                                       .CREATED_DATE = p.p.CREATED_DATE})


            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                lst = lst.Where(Function(f) f.EMPLOYEE_CODE.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_NAME) Then
                lst = lst.Where(Function(f) f.EMPLOYEE_NAME.ToLower().Contains(_filter.EMPLOYEE_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                lst = lst.Where(Function(f) f.ORG_NAME.ToLower().Contains(_filter.ORG_NAME.ToLower()))
            End If
            If _filter.EFFECT_DATE.HasValue Then
                lst = lst.Where(Function(f) f.EFFECT_DATE = _filter.EFFECT_DATE)
            End If
            If _filter.EXPIRE_DATE.HasValue Then
                lst = lst.Where(Function(f) f.EXPIRE_DATE = _filter.EXPIRE_DATE)
            End If
            If Not String.IsNullOrEmpty(_filter.REMARK) Then
                lst = lst.Where(Function(f) f.REMARK.ToLower().Contains(_filter.REMARK.ToLower()))
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

    Public Function GetPA_EMP_LUNCHbyID(ByVal _filter As PA_EMP_LUNCHDTO) As PA_EMP_LUNCHDTO
        Try


            Dim query = From p In Context.PA_EMP_LUNCH
                           From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID).DefaultIfEmpty
                           From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                           From t In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                           Where p.ID = _filter.ID


            Dim lst = query.Select(Function(p) New PA_EMP_LUNCHDTO With {
                                       .ID = p.p.ID,
                                       .ORG_ID = p.p.ORG_ID,
                                       .ORG_NAME = p.o.NAME_VN,
                                       .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                       .EMPLOYEE_NAME = p.e.FULLNAME_VN,
                                       .TITLE_NAME = p.t.NAME_VN,
                                       .EFFECT_DATE = p.p.EFFECT_DATE,
                                       .EXPIRE_DATE = p.p.EXPIRE_DATE,
                                       .PRICE = p.p.PRICE,
                                       .REMARK = p.p.REMARK,
                                       .CREATED_BY = p.p.CREATED_BY,
                                       .CREATED_DATE = p.p.CREATED_DATE})
            Return lst.FirstOrDefault
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function InsertPA_EMP_LUNCH(ByVal lst As List(Of PA_EMP_LUNCHDTO), ByVal objEmp As PA_EMP_LUNCHDTO,
                                  ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        Try
            For Each obj In lst
                Dim objAdd As New PA_EMP_LUNCH
                objAdd.ID = Utilities.GetNextSequence(Context, Context.PA_EMP_LUNCH.EntitySet.Name)
                objAdd.ORG_ID = obj.ORG_ID
                objAdd.LUNCH_ID = obj.LUNCH_ID
                objAdd.EMPLOYEE_ID = obj.EMPLOYEE_ID
                objAdd.EXPIRE_DATE = objEmp.EXPIRE_DATE
                objAdd.EFFECT_DATE = objEmp.EFFECT_DATE
                objAdd.REMARK = objEmp.REMARK
                objAdd.PRICE = objEmp.PRICE
                Context.PA_EMP_LUNCH.AddObject(objAdd)
                Context.SaveChanges(log)
            Next


            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function ModifyPA_EMP_LUNCH(ByVal obj As PA_EMP_LUNCHDTO,
                              ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        Try

            Dim objAdd As New PA_EMP_LUNCH

            objAdd.ID = obj.ID
            objAdd.ORG_ID = obj.ORG_ID
            objAdd.LUNCH_ID = obj.LUNCH_ID
            objAdd.EMPLOYEE_ID = obj.EMPLOYEE_ID
            objAdd.EXPIRE_DATE = obj.EXPIRE_DATE
            objAdd.EFFECT_DATE = obj.EFFECT_DATE
            objAdd.REMARK = obj.REMARK
            objAdd.PRICE = obj.PRICE
            'Context.PA_EMP_LUNCH.AddObject(objAdd)
            Context.SaveChanges(log)

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function DeletePA_EMP_LUNCH(ByVal lstID As List(Of Decimal)) As Boolean
        Try
            Dim lstl As List(Of PA_EMP_LUNCH)
            lstl = (From p In Context.PA_EMP_LUNCH Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstl.Count - 1
                Context.PA_EMP_LUNCH.DeleteObject(lstl(index))
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

