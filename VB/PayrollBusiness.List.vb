Imports PayrollBusiness.ServiceContracts
Imports PayrollDAL
Imports Framework.Data

Namespace PayrollBusiness.ServiceImplementations
    Partial Public Class PayrollBusiness
        Implements ServiceContracts.IPayrollBusiness

#Region "Taxation List"

        Public Function GetTaxation(ByVal _filter As PATaxationDTO,
                                   ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PATaxationDTO) Implements ServiceContracts.IPayrollBusiness.GetTaxation
            Try
                Dim rep As New PayrollRepository
                Return rep.GetTaxation(_filter, PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function InsertTaxation(ByVal objTaxation As PATaxationDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.InsertTaxation
            Try
                Dim rep As New PayrollRepository
                Return rep.InsertTaxation(objTaxation, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ModifyTaxation(ByVal objTaxation As PATaxationDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.ModifyTaxation
            Try
                Dim rep As New PayrollRepository
                Return rep.ModifyTaxation(objTaxation, log, gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ActiveTaxation(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean Implements ServiceContracts.IPayrollBusiness.ActiveTaxation
            Try
                Dim rep As New PayrollRepository
                Return rep.ActiveTaxation(lstID, log, bActive)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function DeleteTaxation(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IPayrollBusiness.DeleteTaxation
            Try
                Dim rep As New PayrollRepository
                Return rep.DeleteTaxation(lstID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

#Region "Payment list"

        Public Function GetPaymentList(ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PAPaymentListDTO) Implements ServiceContracts.IPayrollBusiness.GetPaymentList
            Try
                Dim rep As New PayrollRepository
                Return rep.GetPaymentList(PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception

                Throw ex
            End Try
        End Function
        Public Function GetPaymentListAll(Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PAPaymentListDTO) Implements ServiceContracts.IPayrollBusiness.GetPaymentListAll
            Try
                Dim rep As New PayrollRepository
                Return rep.GetPaymentListAll(Sorts)
            Catch ex As Exception

                Throw ex
            End Try
        End Function
        Public Function InsertPaymentList(ByVal objPaymentList As PAPaymentListDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.InsertPaymentList
            Try
                Dim rep As New PayrollRepository
                Return rep.InsertPaymentList(objPaymentList, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ModifyPaymentList(ByVal objPaymentList As PAPaymentListDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.ModifyPaymentList
            Try
                Dim rep As New PayrollRepository
                Return rep.ModifyPaymentList(objPaymentList, log, gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ActivePaymentList(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean Implements ServiceContracts.IPayrollBusiness.ActivePaymentList
            Try
                Dim rep As New PayrollRepository
                Return rep.ActivePaymentList(lstID, log, bActive)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function DeletePaymentList(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IPayrollBusiness.DeletePaymentList
            Try
                Dim rep As New PayrollRepository
                Return rep.DeletePaymentList(lstID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

#Region "ObjectSalary"

        Public Function GetObjectSalary(ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PAObjectSalaryDTO) Implements ServiceContracts.IPayrollBusiness.GetObjectSalary
            Try
                Dim rep As New PayrollRepository
                Return rep.GetObjectSalary(PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception

                Throw ex
            End Try
        End Function
        Public Function GetObjectSalaryAll(Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PAObjectSalaryDTO) Implements ServiceContracts.IPayrollBusiness.GetObjectSalaryAll
            Try
                Dim rep As New PayrollRepository
                Return rep.GetObjectSalaryAll(Sorts)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function InsertObjectSalary(ByVal objObjectSalary As PAObjectSalaryDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.InsertObjectSalary
            Try
                Dim rep As New PayrollRepository
                Return rep.InsertObjectSalary(objObjectSalary, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ValidateObjectSalary(ByVal objObjectSalary As PAObjectSalaryDTO) As Boolean Implements ServiceContracts.IPayrollBusiness.ValidateObjectSalary
            Try
                Dim rep As New PayrollRepository
                Return rep.ValidateObjectSalary(objObjectSalary)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ModifyObjectSalary(ByVal objObjectSalary As PAObjectSalaryDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.ModifyObjectSalary
            Try
                Dim rep As New PayrollRepository
                Return rep.ModifyObjectSalary(objObjectSalary, log, gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ActiveObjectSalary(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean Implements ServiceContracts.IPayrollBusiness.ActiveObjectSalary
            Try
                Dim rep As New PayrollRepository
                Return rep.ActiveObjectSalary(lstID, log, bActive)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function DeleteObjectSalary(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IPayrollBusiness.DeleteObjectSalary
            Try
                Dim rep As New PayrollRepository
                Return rep.DeleteObjectSalary(lstID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

#Region "Period List"
        Public Function GetPeriodList(ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "START_DATE desc") As List(Of ATPeriodDTO) Implements ServiceContracts.IPayrollBusiness.GetPeriodList
            Try
                Dim lst = PayrollRepositoryStatic.Instance.GetPeriodList(PageIndex, PageSize, Total, Sorts)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetPeriodbyYear(ByVal year As Decimal) As List(Of ATPeriodDTO) Implements ServiceContracts.IPayrollBusiness.GetPeriodbyYear
            Try
                Dim lst = PayrollRepositoryStatic.Instance.GetPeriodbyYear(year)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetOrgByPeriodID(ByVal periodID As Decimal) As List(Of Decimal) _
            Implements ServiceContracts.IPayrollBusiness.GetOrgByPeriodID
            Try
                Dim lst = PayrollRepositoryStatic.Instance.GetOrgByPeriodID(periodID)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function InsertPeriod(ByVal objPeriod As ATPeriodDTO, ByVal objOrgPeriod As List(Of AT_ORG_PERIOD), ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.InsertPeriod
            Try
                Return PayrollRepositoryStatic.Instance.InsertPeriod(objPeriod, objOrgPeriod, log, gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ValidateATPeriod(ByVal objPeriod As ATPeriodDTO) As Boolean Implements ServiceContracts.IPayrollBusiness.ValidateATPeriod
            Try
                Return PayrollRepositoryStatic.Instance.ValidateATPeriod(objPeriod)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ValidateATPeriodDay(ByVal objPeriod As ATPeriodDTO) As Boolean Implements ServiceContracts.IPayrollBusiness.ValidateATPeriodDay
            Try
                Return PayrollRepositoryStatic.Instance.ValidateATPeriodDay(objPeriod)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ModifyPeriod(ByVal objPeriod As ATPeriodDTO, ByVal objOrgPeriod As List(Of AT_ORG_PERIOD), ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.ModifyPeriod
            Try
                Return PayrollRepositoryStatic.Instance.ModifyPeriod(objPeriod, objOrgPeriod, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function DeletePeriod(ByVal lstPeriod As ATPeriodDTO) As Boolean Implements ServiceContracts.IPayrollBusiness.DeletePeriod
            Try
                Return PayrollRepositoryStatic.Instance.DeletePeriod(lstPeriod)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

#Region "List Salary"
        Public Function GetAllFomulerGroup() As List(Of PAFomulerGroup) Implements ServiceContracts.IPayrollBusiness.GetAllFomulerGroup
            Try
                Return PayrollRepositoryStatic.Instance.GetAllFomulerGroup()
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function InsertFomulerGroup(ByVal objPeriod As PAFomulerGroup, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.InsertFomulerGroup
            Try
                Return PayrollRepositoryStatic.Instance.InsertFomulerGroup(objPeriod, log, gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ModifyFomulerGroup(ByVal objPeriod As PAFomulerGroup, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.ModifyFomulerGroup
            Try
                Return PayrollRepositoryStatic.Instance.ModifyFomulerGroup(objPeriod, log, gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function DeleteFomulerGroup(ByVal lstDelete As PAFomulerGroup) As Boolean Implements ServiceContracts.IPayrollBusiness.DeleteFomulerGroup
            Try
                Return PayrollRepositoryStatic.Instance.DeleteFomulerGroup(lstDelete)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GetListAllSalary(ByVal gID As Decimal) As List(Of PAFomuler) Implements ServiceContracts.IPayrollBusiness.GetListAllSalary
            Try
                Return PayrollRepositoryStatic.Instance.GetListAllSalary(gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetListInputColumn(ByVal gID As Decimal) As DataTable Implements ServiceContracts.IPayrollBusiness.GetListInputColumn
            Try
                Return PayrollRepositoryStatic.Instance.GetListInputColumn(gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GetListCalculation() As List(Of OT_OTHERLIST_DTO) Implements ServiceContracts.IPayrollBusiness.GetListCalculation
            Try
                Return PayrollRepositoryStatic.Instance.GetListCalculation()
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function CheckFomuler(ByVal sCol As String, ByVal sFormuler As String, ByVal objID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.CheckFomuler
            Try
                Return PayrollRepositoryStatic.Instance.CheckFomuler(sCol, sFormuler, objID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function SaveFomuler(ByVal objData As PAFomuler, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.SaveFomuler
            Try
                Return PayrollRepositoryStatic.Instance.SaveFomuler(objData, log, gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ActiveFolmulerGroup(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.ActiveFolmulerGroup
            Try
                Return PayrollRepositoryStatic.Instance.ActiveFolmulerGroup(lstID, log, bActive)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region

#Region "Salary list"

        Public Function GetListSalaries(ByVal _filter As PAListSalariesDTO,
                                        ByVal TypePaymentId As Integer,
                                        Optional ByVal PageIndex As Integer = 0,
                                        Optional ByVal PageSize As Integer = Integer.MaxValue,
                                        Optional ByRef Total As Integer = 0,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PAListSalariesDTO) Implements ServiceContracts.IPayrollBusiness.GetListSalaries
            Try
                Dim rep As New PayrollRepository
                Return rep.GetListSalaries(_filter, TypePaymentId, PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception

                Throw ex
            End Try
        End Function
        Public Function CheckColName(ByVal COl_NAME As String, ByVal TypeID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.CheckColName
            Try
                Dim rep As New PayrollRepository
                Return rep.CheckColName(COl_NAME, TypeID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function InsertListSalaries(ByVal objListSalaries As PAListSalariesDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.InsertListSalaries
            Try
                Dim rep As New PayrollRepository
                Return rep.InsertListSalaries(objListSalaries, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ModifyListSalaries(ByVal objListSalaries As PAListSalariesDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.ModifyListSalaries
            Try
                Dim rep As New PayrollRepository
                Return rep.ModifyListSalaries(objListSalaries, log, gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ActiveListSalaries(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean Implements ServiceContracts.IPayrollBusiness.ActiveListSalaries
            Try
                Dim rep As New PayrollRepository
                Return rep.ActiveListSalaries(lstID, log, bActive)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function DeleteListSalaries(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IPayrollBusiness.DeleteListSalaries
            Try
                Dim rep As New PayrollRepository
                Return rep.DeleteListSalaries(lstID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

#Region "lunch list : Đơn giá tiền ăn trưa"
        Public Function GetPriceLunchList(ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "EFFECT_DATE desc",
                                        Optional ByVal log As UserLog = Nothing) As List(Of ATPriceLunchDTO) Implements ServiceContracts.IPayrollBusiness.GetPriceLunchList
            Try
                Dim lst = PayrollRepositoryStatic.Instance.GetPriceLunchList(PageIndex, PageSize, Total, Sorts, log)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetPriceLunch(ByVal year As Decimal) As List(Of ATPriceLunchDTO) Implements ServiceContracts.IPayrollBusiness.GetPriceLunch
            Try
                Dim lst = PayrollRepositoryStatic.Instance.GetPriceLunch(year)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function InsertPriceLunch(ByVal objPeriod As ATPriceLunchDTO, ByVal objOrgPeriod As List(Of PA_ORG_LUNCH), ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.InsertPriceLunch
            Try
                Return PayrollRepositoryStatic.Instance.InsertPriceLunch(objPeriod, objOrgPeriod, log, gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ValidateATPriceLunch(ByVal _validate As ATPriceLunchDTO) As Boolean Implements ServiceContracts.IPayrollBusiness.ValidateATPriceLunch
            Try
                Return PayrollRepositoryStatic.Instance.ValidateATPriceLunch(_validate)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ValidateATPriceLunchOrg(ByVal _validate As ATPriceLunchDTO) As Boolean Implements ServiceContracts.IPayrollBusiness.ValidateATPriceLunchOrg
            Try
                Return PayrollRepositoryStatic.Instance.ValidateATPriceLunchOrg(_validate)
            Catch ex As Exception
                Throw ex
            End Try
        End Function


        Public Function ModifyPriceLunch(ByVal objPeriod As ATPriceLunchDTO, ByVal objOrgPeriod As List(Of PA_ORG_LUNCH), ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.ModifyPriceLunch
            Try
                Return PayrollRepositoryStatic.Instance.ModifyPriceLunch(objPeriod, objOrgPeriod, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function DeletePriceLunch(ByVal lstPeriod As ATPriceLunchDTO) As Boolean Implements ServiceContracts.IPayrollBusiness.DeletePriceLunch
            Try
                Return PayrollRepositoryStatic.Instance.DeletePriceLunch(lstPeriod)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetOrgByLunchID(ByVal lunchID As Decimal) As List(Of Decimal) Implements ServiceContracts.IPayrollBusiness.GetOrgByLunchID
            Try
                Return PayrollRepositoryStatic.Instance.GetOrgByLunchID(lunchID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region

#Region "tien an trua theo nhan vien"

        Public Function GetPA_EMP_LUNCH(ByVal _filter As PA_EMP_LUNCHDTO,
                                      ByVal _param As PA_ParamDTO,
                                     Optional ByRef Total As Integer = 0,
                                     Optional ByVal PageIndex As Integer = 0,
                                     Optional ByVal PageSize As Integer = Integer.MaxValue,
                                     Optional ByVal Sorts As String = "EFFECT_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of PA_EMP_LUNCHDTO) Implements ServiceContracts.IPayrollBusiness.GetPA_EMP_LUNCH
            Try
                Dim lst = PayrollRepositoryStatic.Instance.GetPA_EMP_LUNCH(_filter, _param, Total, PageIndex, PageSize, Sorts, log)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetPA_EMP_LUNCHbyID(ByVal _filter As PA_EMP_LUNCHDTO) As PA_EMP_LUNCHDTO Implements ServiceContracts.IPayrollBusiness.GetPA_EMP_LUNCHbyID
            Try
                Dim lst = PayrollRepositoryStatic.Instance.GetPA_EMP_LUNCHbyID(_filter)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function InsertPA_EMP_LUNCH(ByVal lst As List(Of PA_EMP_LUNCHDTO), ByVal objEmp As PA_EMP_LUNCHDTO,
                                  ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.InsertPA_EMP_LUNCH

            Try
                Dim lstEmp = PayrollRepositoryStatic.Instance.InsertPA_EMP_LUNCH(lst, objEmp, log, gID)
                Return lstEmp
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ModifyPA_EMP_LUNCH(ByVal obj As PA_EMP_LUNCHDTO,
                                  ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.ModifyPA_EMP_LUNCH
            Try
                Dim lst = PayrollRepositoryStatic.Instance.ModifyPA_EMP_LUNCH(obj, log, gID)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function DeletePA_EMP_LUNCH(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IPayrollBusiness.DeletePA_EMP_LUNCH
            Try
                Dim lst = PayrollRepositoryStatic.Instance.DeletePA_EMP_LUNCH(lstID)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region
    End Class
End Namespace

