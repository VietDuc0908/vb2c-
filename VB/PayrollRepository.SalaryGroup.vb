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

#Region "SalaryGroup list"

    Public Function GetSalaryGroup(ByVal _filter As SalaryGroupDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of SalaryGroupDTO)

        Try
            Dim query = From p In Context.PA_SALARY_GROUP

            If _filter.CODE <> "" Then
                query = query.Where(Function(p) p.CODE.ToUpper.Contains(_filter.CODE.ToUpper))
            End If

            If _filter.NAME <> "" Then
                query = query.Where(Function(p) p.NAME.ToUpper.Contains(_filter.NAME.ToUpper))
            End If

            If _filter.EFFECT_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.EFFECT_DATE = _filter.EFFECT_DATE)
            End If

            Dim lst = query.Select(Function(p) New SalaryGroupDTO With {
                                       .ID = p.ID,
                                       .CODE = p.CODE,
                                       .NAME = p.NAME,
                                       .REMARK = p.REMARK,
                                       .EFFECT_DATE = p.EFFECT_DATE,
                                       .CREATED_DATE = p.CREATED_DATE})


            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try


    End Function

    Public Function GetEffectSalaryGroup() As SalaryGroupDTO
        Try
            Dim query = From p In Context.PA_SALARY_GROUP
            Where p.EFFECT_DATE <= Date.Now
                 Order By p.EFFECT_DATE Descending, p.CREATED_DATE Descending

            Dim EffectSalaryGroup = query.Select(Function(p) New SalaryGroupDTO With {
                                       .ID = p.ID,
                                       .CODE = p.CODE,
                                       .NAME = p.NAME,
                                       .REMARK = p.REMARK,
                                       .EFFECT_DATE = p.EFFECT_DATE,
                                       .CREATED_DATE = p.CREATED_DATE}).FirstOrDefault

            Return EffectSalaryGroup
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try


    End Function

    Public Function InsertSalaryGroup(ByVal objSalaryGroup As SalaryGroupDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim iCount As Integer = 0
        Dim objSalaryGroupData As New PA_SALARY_GROUP
        Try
            objSalaryGroupData.ID = Utilities.GetNextSequence(Context, Context.PA_SALARY_GROUP.EntitySet.Name)
            objSalaryGroupData.CODE = objSalaryGroup.CODE.Trim
            objSalaryGroupData.NAME = objSalaryGroup.NAME.Trim
            objSalaryGroupData.EFFECT_DATE = objSalaryGroup.EFFECT_DATE
            objSalaryGroupData.REMARK = objSalaryGroup.REMARK
            Context.PA_SALARY_GROUP.AddObject(objSalaryGroupData)
            Context.SaveChanges(log)


            Context.SaveChanges(log)
            gID = objSalaryGroupData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function ValidateSalaryGroup(ByVal _validate As SalaryGroupDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.PA_SALARY_GROUP
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.ID <> _validate.ID).SingleOrDefault
                Else
                    query = (From p In Context.PA_SALARY_GROUP
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

    Public Function ModifySalaryGroup(ByVal objSalaryGroup As SalaryGroupDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objSalaryGroupData As New PA_SALARY_GROUP With {.ID = objSalaryGroup.ID}
        Try
            Context.PA_SALARY_GROUP.Attach(objSalaryGroupData)
            objSalaryGroupData.CODE = objSalaryGroup.CODE.Trim
            objSalaryGroupData.NAME = objSalaryGroup.NAME.Trim
            objSalaryGroupData.EFFECT_DATE = objSalaryGroup.EFFECT_DATE
            objSalaryGroupData.REMARK = objSalaryGroup.REMARK

            Context.SaveChanges(log)
            gID = objSalaryGroupData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function DeleteSalaryGroup(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstSalaryGroupData As List(Of PA_SALARY_GROUP)
        Try
            lstSalaryGroupData = (From p In Context.PA_SALARY_GROUP Where lstID.Contains(p.ID)).ToList

            For idx = 0 To lstSalaryGroupData.Count - 1
                For Each item In lstSalaryGroupData(idx).PA_SALARY_LEVEL
                    For Each item1 In item.PA_SALARY_RANK
                        Context.PA_SALARY_RANK.DeleteObject(item1)
                    Next
                    Context.PA_SALARY_LEVEL.DeleteObject(item)
                Next

                Context.PA_SALARY_GROUP.DeleteObject(lstSalaryGroupData(idx))
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

