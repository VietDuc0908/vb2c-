Imports PayrollBusiness.ServiceContracts
Imports PayrollDAL
Imports Framework.Data
Imports System.Collections.Generic

Namespace PayrollBusiness.ServiceImplementations
    Partial Public Class PayrollBusiness
        Implements ServiceContracts.IPayrollBusiness

#Region "Test Service"
        Public Function TestService(ByVal str As String) As String Implements IPayrollBusiness.TestService
            Return "Hello world " & str
        End Function
#End Region

        Public Function GetComboboxData(ByRef cbxData As ComboBoxDataDTO) As Boolean Implements IPayrollBusiness.GetComboboxData
            Using rep As New PayrollRepository
                Try
                    Return rep.GetComboboxData(cbxData)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function CheckExistInDatabase(ByVal lstID As List(Of Decimal), ByVal table As TABLE_NAME) As Boolean _
            Implements IPayrollBusiness.CheckExistInDatabase
            Using rep As New PayrollRepository
                Try
                    Return rep.CheckExistInDatabase(lstID, table)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function


        Function IS_PERIODSTATUS(ByVal _param As PA_ParamDTO, ByVal log As UserLog) As Boolean _
            Implements ServiceContracts.IPayrollBusiness.IS_PERIODSTATUS
            Try
                Dim lst = PayrollRepositoryStatic.Instance.IS_PERIODSTATUS(_param, log)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function IS_PERIOD_COLEXSTATUS(ByVal _param As PA_ParamDTO, ByVal log As UserLog) As Boolean _
            Implements ServiceContracts.IPayrollBusiness.IS_PERIOD_COLEXSTATUS
            Try
                Dim lst = PayrollRepositoryStatic.Instance.IS_PERIOD_COLEXSTATUS(_param, log)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

    End Class
End Namespace

