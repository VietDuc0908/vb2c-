Imports PayrollBusiness.ServiceContracts
Imports PayrollDAL
Imports Framework.Data

Namespace PayrollBusiness.ServiceImplementations
    Partial Public Class PayrollBusiness
        Implements ServiceContracts.IPayrollBusiness

#Region "Calculate Salary"
        Public Function Load_Calculate_Load(ByVal lstEmp As List(Of PAEmployeeCalculateDTO), ByVal OrgId As Integer, ByVal PeriodId As Integer, ByVal IsDissolve As Integer, ByVal IsLoad As Integer, ByVal log As UserLog) As Boolean Implements ServiceContracts.IPayrollBusiness.Load_Calculate_Load
            Dim rep As New PayrollRepository
            Return rep.Load_Calculate_Load(lstEmp, OrgId, PeriodId, IsDissolve, IsLoad, log)
        End Function
        Public Function Calculate_data_sum(ByVal OrgId As Integer, ByVal PeriodId As Integer, ByVal IsDissolve As Integer, ByVal IsLoad As Integer, ByVal log As UserLog) As Boolean Implements ServiceContracts.IPayrollBusiness.Calculate_data_sum
            Dim rep As New PayrollRepository
            Return rep.Calculate_data_sum(OrgId, PeriodId, IsDissolve, IsLoad, log)
        End Function
        Public Function Load_data_sum(ByVal OrgId As Integer, ByVal PeriodId As Integer, ByVal IsDissolve As Integer, ByVal IsLoad As Integer, ByVal log As UserLog) As Boolean Implements ServiceContracts.IPayrollBusiness.Load_data_sum
            Dim rep As New PayrollRepository
            Return rep.Load_data_sum(OrgId, PeriodId, IsDissolve, IsLoad, log)
        End Function
        Public Function Calculate_data_temp(ByVal OrgId As Integer, ByVal PeriodId As Integer, ByVal IsDissolve As Integer, ByVal IsLoad As Integer, ByVal log As UserLog) As Boolean Implements ServiceContracts.IPayrollBusiness.Calculate_data_temp
            Dim rep As New PayrollRepository
            Return rep.Calculate_data_temp(OrgId, PeriodId, IsDissolve, IsLoad, log)
        End Function

        Public Function GetLitsCalculate(ByVal param As PA_ParamDTO, ByVal IsLoad As Integer,
                                          ByVal PageIndex As Integer, ByVal PageSize As Integer, ByVal PageType As Integer, ByRef TotalRow As Integer,
                                     ByVal log As UserLog, Optional ByVal Sorts As String = "CREATED_DATE DESC") As DataTable Implements ServiceContracts.IPayrollBusiness.GetLitsCalculate
            Dim rep As New PayrollRepository
            Return rep.GetLitsCalculate(param, IsLoad, PageIndex, PageSize, PageType, TotalRow, log)
        End Function
        Public Function GetListSalaryVisibleCol() As List(Of PAListSalariesDTO) Implements ServiceContracts.IPayrollBusiness.GetListSalaryVisibleCol
            Dim rep As New PayrollRepository
            Return rep.GetListSalaryVisibleCol()
        End Function

        Public Function ActiveOrDeactive(ByVal _param As PA_ParamDTO,
                                     ByVal log As UserLog) As Boolean Implements ServiceContracts.IPayrollBusiness.ActiveOrDeactive
            Dim rep As New PayrollRepository
            Return rep.ActiveOrDeactive(_param, log)
        End Function

#End Region

#Region "Import Salary"

        Public Function GetImportSalary(ByVal PeriodId As Integer, ByVal OrgId As Integer, ByVal IsDissolve As Integer, ByVal EmployeeId As String,
                                     ByVal PageIndex As Integer,
                                     ByVal PageSize As Integer,
                                     ByRef TotalRow As Integer,
                                     ByVal log As UserLog,
                                        Optional ByVal Sorts As String = "CREATED_DATE DESC") As DataTable Implements ServiceContracts.IPayrollBusiness.GetImportSalary
            Try
                Dim rep As New PayrollRepository
                Return rep.GetImportSalary(PeriodId, OrgId, IsDissolve, EmployeeId, PageIndex, PageSize, TotalRow, log, Sorts)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GetSalaryList() As List(Of PAListSalariesDTO) Implements ServiceContracts.IPayrollBusiness.GetSalaryList
            Try
                Dim rep As New PayrollRepository
                Return rep.GetSalaryList()
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function SaveImport(ByVal Period As Decimal, ByVal dtData As DataTable, ByVal lstColVal As List(Of String), ByVal log As UserLog, ByRef RecordSussces As Integer) As Boolean Implements ServiceContracts.IPayrollBusiness.SaveImport
            Try
                Dim rep As New PayrollRepository
                Return rep.SaveImport(Period, dtData, lstColVal, log, RecordSussces)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region

#Region "IPORTAL - View phiếu lương"
        Public Function GetPayrollSheetSum(ByVal PeriodId As Integer, ByVal EmployeeId As String, ByVal log As UserLog,
                                        Optional ByVal Sorts As String = "CREATED_DATE DESC") As DataTable Implements ServiceContracts.IPayrollBusiness.GetPayrollSheetSum
            Try
                Dim rep As New PayrollRepository
                Return rep.GetPayrollSheetSum(PeriodId, EmployeeId, log, Sorts)
            Catch ex As Exception
                Throw ex
            End Try
        End Function


        Public Function CheckPeriod(ByVal PeriodId As Integer, ByVal EmployeeId As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.CheckPeriod
            Try
                Dim rep As New PayrollRepository
                Return rep.CheckPeriod(PeriodId, EmployeeId)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region

#Region "Báo cáo lương "
        Public Function GetReportById(ByVal _filter As Se_ReportDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        log As UserLog,
                                        Optional ByVal Sorts As String = "CODE ASC") As List(Of Se_ReportDTO) Implements ServiceContracts.IPayrollBusiness.GetReportById
            Using rep As New PayrollRepository
                Try

                    Dim lst = rep.GetReportById(_filter, PageIndex, PageSize, Total, log, Sorts)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ExportReport(ByVal sPkgName As String,
                                     ByVal sMoth As Decimal,
                                     ByVal sYear As Decimal,
                                     ByVal sOrgId As String,
                                     ByVal IsDissolve As Decimal,
                                     ByVal log As UserLog) As DataSet Implements ServiceContracts.IPayrollBusiness.ExportReport
            Using rep As New PayrollRepository
                Try
                    Dim lst = rep.ExportReport(sPkgName, sMoth, sYear, sOrgId, IsDissolve, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ExportReport_005(ByVal sPkgName As String,
                                         ByVal sMoth As Decimal,
                                         ByVal sYear As Decimal,
                                     ByVal sOrgId As String,
                                         ByVal IsDissolve As Decimal,
                                         ByVal log As UserLog) As DataSet _
                                     Implements ServiceContracts.IPayrollBusiness.ExportReport_005
            Using rep As New PayrollRepository
                Try
                    Dim lst = rep.ExportReport_005(sPkgName, sMoth, sYear, sOrgId, IsDissolve, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ExportReport_008(ByVal sPkgName As String,
                                         ByVal sMoth As Decimal,
                                         ByVal sYear As Decimal,
                                     ByVal sOrgId As String,
                                         ByVal IsDissolve As Decimal,
                                         ByVal log As UserLog) As DataSet _
                                     Implements ServiceContracts.IPayrollBusiness.ExportReport_008
            Using rep As New PayrollRepository
                Try
                    Dim lst = rep.ExportReport_008(sPkgName, sMoth, sYear, sOrgId, IsDissolve, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ExportReport_010(ByVal sPkgName As String,
                                         ByVal sMoth As Decimal,
                                         ByVal sYear As Decimal,
                                     ByVal sOrgId As String,
                                         ByVal IsDissolve As Decimal,
                                         ByVal log As UserLog) As DataSet _
                                     Implements ServiceContracts.IPayrollBusiness.ExportReport_010
            Using rep As New PayrollRepository
                Try
                    Dim lst = rep.ExportReport_010(sPkgName, sMoth, sYear, sOrgId, IsDissolve, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ExportReport_014(ByVal sPkgName As String,
                                         ByVal sMoth As Decimal,
                                         ByVal sYear As Decimal,
                                     ByVal sOrgId As String,
                                         ByVal IsDissolve As Decimal,
                                         ByVal log As UserLog) As DataSet _
                                     Implements ServiceContracts.IPayrollBusiness.ExportReport_014
            Using rep As New PayrollRepository
                Try
                    Dim lst = rep.ExportReport_014(sPkgName, sMoth, sYear, sOrgId, IsDissolve, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ExportPhieuLuong(ByVal lstEmployee As List(Of Decimal),
                                     ByVal orgID As Decimal?,
                                     ByVal isDissolve As Decimal?,
                                     ByVal periodID As Decimal,
                                  ByVal log As UserLog) As DataTable Implements ServiceContracts.IPayrollBusiness.ExportPhieuLuong
            Using rep As New PayrollRepository
                Try
                    Dim lst = rep.ExportPhieuLuong(lstEmployee, orgID, isDissolve, periodID, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ActionSendPayslip(ByVal lstEmployee As List(Of Decimal),
                                     ByVal orgID As Decimal?,
                                     ByVal isDissolve As Decimal?,
                                     ByVal periodID As Decimal,
                                     ByVal log As UserLog) As Boolean Implements ServiceContracts.IPayrollBusiness.ActionSendPayslip
            Using rep As New PayrollRepository
                Try
                    Return rep.ActionSendPayslip(lstEmployee, orgID, isDissolve, periodID, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

#End Region

#Region "LOG"
        Function GetActionLog(ByVal _filter As PA_ACTION_LOGDTO,
                                 Optional ByRef Total As Integer = 0,
                                 Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByVal Sorts As String = "ACTION_DATE desc") As List(Of PA_ACTION_LOGDTO) Implements IPayrollBusiness.GetActionLog

            Using rep As New PayrollRepository
                Try
                    Dim lst = rep.GetActionLog(_filter, Total, PageIndex, PageSize, Sorts)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Function DeleteActionLogsPA(ByVal lstDeleteIds As List(Of Decimal)) As Integer Implements IPayrollBusiness.DeleteActionLogsPA

            Using rep As New PayrollRepository
                Try
                    Dim lst = rep.DeleteActionLogsPA(lstDeleteIds)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "Seniority"

        Function CalSeniorityProcess(ByVal _filter As PASeniorityProcessDTO, ByVal log As UserLog) As Boolean _
                                    Implements IPayrollBusiness.CalSeniorityProcess

            Using rep As New PayrollRepository
                Try
                    Dim lst = rep.CalSeniorityProcess(_filter, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Function GetSeniorityProcess(ByVal _filter As PASeniorityProcessDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer, ByVal log As UserLog,
                                        Optional ByVal Sorts As String = "EMPLOYEE_CODE") As List(Of PASeniorityProcessDTO) _
                                    Implements IPayrollBusiness.GetSeniorityProcess

            Using rep As New PayrollRepository
                Try
                    Dim lst = rep.GetSeniorityProcess(_filter, PageIndex, PageSize, Total, log, Sorts)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Function GetSeniorityProcessImport(ByVal _filter As PASeniorityProcessDTO,
                                        ByVal log As UserLog) As DataTable _
                                    Implements IPayrollBusiness.GetSeniorityProcessImport

            Using rep As New PayrollRepository
                Try
                    Dim lst = rep.GetSeniorityProcessImport(_filter, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Function SaveSeniorityProcessImport(ByVal dtData As DataTable,
                                               ByVal periodId As Decimal,
                                               ByVal log As UserLog) _
                                    Implements IPayrollBusiness.SaveSeniorityProcessImport

            Using rep As New PayrollRepository
                Try
                    Dim lst = rep.SaveSeniorityProcessImport(dtData, periodId, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Function CalSeniorityMonthly(ByVal _filter As PASeniorityMonthlyDTO, ByVal log As UserLog) As Boolean _
                                    Implements IPayrollBusiness.CalSeniorityMonthly

            Using rep As New PayrollRepository
                Try
                    Dim lst = rep.CalSeniorityMonthly(_filter, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function


        Function GetSeniorityMonthly(ByVal _filter As PASeniorityMonthlyDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer, ByVal log As UserLog,
                                        Optional ByVal Sorts As String = "EMPLOYEE_CODE") As List(Of PASeniorityMonthlyDTO) _
                                    Implements IPayrollBusiness.GetSeniorityMonthly

            Using rep As New PayrollRepository
                Try
                    Dim lst = rep.GetSeniorityMonthly(_filter, PageIndex, PageSize, Total, log, Sorts)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

#End Region


#Region "MailRemark"

        Public Function GetMailRemark(ByVal _filter As PAMailRemarkDTO,
                                   ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer, ByVal log As UserLog,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PAMailRemarkDTO) Implements ServiceContracts.IPayrollBusiness.GetMailRemark
            Try
                Dim rep As New PayrollRepository
                Return rep.GetMailRemark(_filter, PageIndex, PageSize, Total, log, Sorts)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function InsertMailRemark(ByVal objMailRemark As PAMailRemarkDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.InsertMailRemark
            Try
                Dim rep As New PayrollRepository
                Return rep.InsertMailRemark(objMailRemark, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ModifyMailRemark(ByVal objMailRemark As PAMailRemarkDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.ModifyMailRemark
            Try
                Dim rep As New PayrollRepository
                Return rep.ModifyMailRemark(objMailRemark, log, gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function DeleteMailRemark(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IPayrollBusiness.DeleteMailRemark
            Try
                Dim rep As New PayrollRepository
                Return rep.DeleteMailRemark(lstID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region


    End Class
End Namespace

