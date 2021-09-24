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

#Region "SalaryRank list"

    Public Function GetSalaryRank(ByVal _filter As SalaryRankDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of SalaryRankDTO)

        Try
            Dim query = From p In Context.PA_SALARY_RANK
                        Join o In Context.PA_SALARY_LEVEL On p.SAL_LEVEL_ID Equals o.ID
                        Join q In Context.PA_SALARY_GROUP On o.SAL_GROUP_ID Equals q.ID
                        Where q.ID = _filter.SAL_GROUP_ID

            Dim lst = query.Select(Function(e) New SalaryRankDTO With {
                                       .ID = e.p.ID,
                                       .SAL_GROUP_ID = e.o.SAL_GROUP_ID,
                                       .SAL_GROUP_NAME = e.q.NAME,
                                       .SAL_LEVEL_ID = e.p.SAL_LEVEL_ID,
                                       .SAL_LEVEL_NAME = e.o.NAME,
                                       .SALARY_BASIC = e.p.SALARY_BASIC,
                                       .RANK = e.p.RANK,
                                       .REMARK = e.p.REMARK,
                                       .ACTFLG = If(e.p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                                       .CREATED_DATE = e.p.CREATED_DATE})

            If _filter.SAL_LEVEL_ID <> 0 Then
                lst = lst.Where(Function(p) p.SAL_LEVEL_ID = _filter.SAL_LEVEL_ID)
            End If

            If _filter.RANK <> 0 Then
                lst = lst.Where(Function(p) p.RANK = _filter.RANK)
            End If

            If _filter.SALARY_BASIC <> 0 Then
                lst = lst.Where(Function(p) p.SALARY_BASIC = _filter.SALARY_BASIC)
            End If
            If _filter.SAL_GROUP_NAME <> "" Then
                lst = lst.Where(Function(p) p.SAL_GROUP_NAME.ToUpper.Contains(_filter.SAL_GROUP_NAME.ToUpper))
            End If
            If _filter.SAL_LEVEL_NAME <> "" Then
                lst = lst.Where(Function(p) p.SAL_LEVEL_NAME.ToUpper.Contains(_filter.SAL_LEVEL_NAME.ToUpper))
            End If
            If _filter.ACTFLG <> "" Then
                lst = lst.Where(Function(p) p.ACTFLG.ToUpper.Contains(_filter.ACTFLG.ToUpper))
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

    Public Function InsertSalaryRank(ByVal objSalaryRank As SalaryRankDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim iCount As Integer = 0
        Dim objSalaryRankData As New PA_SALARY_RANK
        Try
            objSalaryRankData.ID = Utilities.GetNextSequence(Context, Context.PA_SALARY_RANK.EntitySet.Name)
            objSalaryRankData.SAL_LEVEL_ID = objSalaryRank.SAL_LEVEL_ID
            objSalaryRankData.RANK = objSalaryRank.RANK
            objSalaryRankData.SALARY_BASIC = objSalaryRank.SALARY_BASIC
            objSalaryRankData.REMARK = objSalaryRank.REMARK
            objSalaryRankData.ACTFLG = objSalaryRank.ACTFLG
            objSalaryRankData.REMARK = objSalaryRank.REMARK
            Context.PA_SALARY_RANK.AddObject(objSalaryRankData)
            Context.SaveChanges(log)
            gID = objSalaryRankData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function ModifySalaryRank(ByVal objSalaryRank As SalaryRankDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objSalaryRankData As New PA_SALARY_RANK With {.ID = objSalaryRank.ID}
        Try
            Context.PA_SALARY_RANK.Attach(objSalaryRankData)
            objSalaryRankData.SAL_LEVEL_ID = objSalaryRank.SAL_LEVEL_ID
            objSalaryRankData.RANK = objSalaryRank.RANK
            objSalaryRankData.SALARY_BASIC = objSalaryRank.SALARY_BASIC
            objSalaryRankData.REMARK = objSalaryRank.REMARK
            Context.SaveChanges(log)
            gID = objSalaryRankData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function ValidateSalaryRank(ByVal _validate As SalaryRankDTO)
        Dim query
        Try
            If _validate.RANK <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.PA_SALARY_RANK
                             Where p.RANK = _validate.RANK _
                             And p.SAL_LEVEL_ID = _validate.SAL_LEVEL_ID _
                             And p.ID <> _validate.ID).SingleOrDefault
                Else
                    query = (From p In Context.PA_SALARY_RANK
                             Where p.SAL_LEVEL_ID = _validate.SAL_LEVEL_ID _
                             And p.RANK = _validate.RANK).FirstOrDefault
                End If
                Return (query Is Nothing)
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function


    Public Function ActiveSalaryRank(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstData As List(Of PA_SALARY_RANK)
        Try
            lstData = (From p In Context.PA_SALARY_RANK Where lstID.Contains(p.ID)).ToList
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


    Public Function DeleteSalaryRank(ByVal lstSalaryRank() As SalaryRankDTO) As Boolean
        Dim lstSalaryRankData As List(Of PA_SALARY_RANK)
        Dim lstIDSalaryRank As List(Of Decimal) = (From p In lstSalaryRank.ToList Select p.ID).ToList
        Try
            lstSalaryRankData = (From p In Context.PA_SALARY_RANK Where lstIDSalaryRank.Contains(p.ID)).ToList
            For idx = 0 To lstSalaryRankData.Count - 1
                Context.PA_SALARY_RANK.DeleteObject(lstSalaryRankData(idx))
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

