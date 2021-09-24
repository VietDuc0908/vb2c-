Imports PayrollBusiness.ServiceContracts
Imports PayrollDAL
Imports Framework.Data

Namespace PayrollBusiness.ServiceImplementations
    Partial Public Class PayrollBusiness
        Implements ServiceContracts.IPayrollBusiness

#Region "Hold Salary"

        Public Function GetHoldSalaryList(ByVal PeriodId As Integer, ByVal OrgId As Integer, ByVal IsDissolve As Integer, ByVal log As UserLog,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PAHoldSalaryDTO) Implements ServiceContracts.IPayrollBusiness.GetHoldSalaryList
            Try
                Dim rep As New PayrollRepository
                Return rep.GetHoldSalaryList(PeriodId, OrgId, IsDissolve, log, PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function InsertHoldSalary(ByVal objPeriod As List(Of PAHoldSalaryDTO), ByVal log As UserLog) As Boolean Implements ServiceContracts.IPayrollBusiness.InsertHoldSalary
            Try
                Dim rep As New PayrollRepository
                Return rep.InsertHoldSalary(objPeriod, log)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function DeleteHoldSalary(ByVal lstDelete As List(Of Decimal)) As Boolean Implements ServiceContracts.IPayrollBusiness.DeleteHoldSalary
            Try
                Dim rep As New PayrollRepository
                Return rep.DeleteHoldSalary(lstDelete)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region


    End Class
End Namespace

