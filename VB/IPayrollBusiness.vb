Imports PayrollDAL
Imports Framework.Data
' NOTE: You can use the "Rename" command on the context menu to change the interface name "IService1" in both code and config file together.
Namespace PayrollBusiness.ServiceContracts
    <ServiceContract()>
    Public Interface IPayrollBusiness

#Region "Common"


        <OperationContract()>
        Function CheckExistInDatabase(ByVal lstID As List(Of Decimal), ByVal table As TABLE_NAME) As Boolean


        <OperationContract()>
        Function IS_PERIODSTATUS(ByVal _param As PA_ParamDTO, ByVal log As UserLog) As Boolean


        <OperationContract()>
        Function IS_PERIOD_COLEXSTATUS(ByVal _param As PA_ParamDTO, ByVal log As UserLog) As Boolean

#End Region

#Region "Calculate Salary"
        <OperationContract()>
        Function Load_Calculate_Load(ByVal lstEmp As List(Of PAEmployeeCalculateDTO), ByVal OrgId As Integer, ByVal PeriodId As Integer, ByVal IsDissolve As Integer, ByVal IsLoad As Integer, ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function Load_data_sum(ByVal OrgId As Integer, ByVal PeriodId As Integer, ByVal IsDissolve As Integer, ByVal IsLoad As Integer, ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function Calculate_data_sum(ByVal OrgId As Integer, ByVal PeriodId As Integer, ByVal IsDissolve As Integer, ByVal IsLoad As Integer, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function Calculate_data_temp(ByVal OrgId As Integer, ByVal PeriodId As Integer, ByVal IsDissolve As Integer, ByVal IsLoad As Integer, ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function GetListSalaryVisibleCol() As List(Of PAListSalariesDTO)
        <OperationContract()>
        Function GetLitsCalculate(ByVal param As PA_ParamDTO, ByVal IsLoad As Integer,
                                   ByVal PageIndex As Integer, ByVal PageSize As Integer, ByVal PageType As Integer, ByRef TotalRow As Integer,
                                    ByVal log As UserLog, Optional ByVal Sorts As String = "CREATED_DATE DESC") As DataTable
        <OperationContract()>
        Function ActiveOrDeactive(ByVal _param As PA_ParamDTO,
                                     ByVal log As UserLog) As Boolean
#End Region

#Region "Import Salary"
        <OperationContract()>
        Function GetImportSalary(ByVal PeriodId As Integer, ByVal OrgId As Integer, ByVal IsDissolve As Integer, ByVal EmployeeId As String,
                                     ByVal PageIndex As Integer,
                                     ByVal PageSize As Integer,
                                     ByRef TotalRow As Integer,
                                     ByVal log As UserLog,
                                        Optional ByVal Sorts As String = "CREATED_DATE DESC") As DataTable
        <OperationContract()>
        Function GetSalaryList() As List(Of PAListSalariesDTO)
        <OperationContract()>
        Function SaveImport(ByVal Period As Decimal, ByVal dtData As DataTable, ByVal lstColVal As List(Of String), ByVal log As UserLog, ByRef RecordSussces As Integer) As Boolean
#End Region

#Region "Hold Salary"

        <OperationContract()>
        Function GetHoldSalaryList(ByVal PeriodId As Integer, ByVal OrgId As Integer, ByVal IsDissolve As Integer, ByVal log As UserLog,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PAHoldSalaryDTO)

        <OperationContract()>
        Function InsertHoldSalary(ByVal objPeriod As List(Of PAHoldSalaryDTO), ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function DeleteHoldSalary(ByVal lstDelete As List(Of Decimal)) As Boolean
#End Region

#Region "Taxation List"

        <OperationContract()>
        Function GetTaxation(ByVal _filter As PATaxationDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PATaxationDTO)

        <OperationContract()>
        Function InsertTaxation(ByVal objTaxation As PATaxationDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyTaxation(ByVal objTaxation As PATaxationDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveTaxation(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean

        <OperationContract()>
        Function DeleteTaxation(ByVal lstID As List(Of Decimal)) As Boolean
#End Region

#Region "Payment List"

        <OperationContract()>
        Function GetPaymentList(ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PAPaymentListDTO)
        <OperationContract()>
        Function GetPaymentListAll(Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PAPaymentListDTO)
        <OperationContract()>
        Function InsertPaymentList(ByVal objPaymentList As PAPaymentListDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyPaymentList(ByVal objPaymentList As PAPaymentListDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActivePaymentList(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean

        <OperationContract()>
        Function DeletePaymentList(ByVal lstID As List(Of Decimal)) As Boolean
#End Region

#Region "Object Salary"
        <OperationContract()>
        Function GetObjectSalary(ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PAObjectSalaryDTO)
        <OperationContract()>
        Function GetObjectSalaryAll(Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PAObjectSalaryDTO)
        <OperationContract()>
        Function InsertObjectSalary(ByVal objObjectSalary As PAObjectSalaryDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateObjectSalary(ByVal objObjectSalary As PAObjectSalaryDTO) As Boolean

        <OperationContract()>
        Function ModifyObjectSalary(ByVal objObjectSalary As PAObjectSalaryDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveObjectSalary(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean

        <OperationContract()>
        Function DeleteObjectSalary(ByVal lstID As List(Of Decimal)) As Boolean
#End Region

#Region "Period List"
        <OperationContract()>
        Function GetPeriodList(ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "START_DATE desc") As List(Of ATPeriodDTO)

        <OperationContract()>
        Function GetPeriodbyYear(ByVal year As Decimal) As List(Of ATPeriodDTO)

        <OperationContract()>
        Function GetOrgByPeriodID(ByVal periodID As Decimal) As List(Of Decimal)

        <OperationContract()>
        Function InsertPeriod(ByVal objPeriod As ATPeriodDTO, ByVal objOrgPeriod As List(Of AT_ORG_PERIOD), ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyPeriod(ByVal objPeriod As ATPeriodDTO, ByVal objOrgPeriod As List(Of AT_ORG_PERIOD), ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DeletePeriod(ByVal lstPeriod As ATPeriodDTO) As Boolean

        <OperationContract()>
        Function ValidateATPeriod(ByVal objPeriod As ATPeriodDTO) As Boolean

        <OperationContract()>
        Function ValidateATPeriodDay(ByVal objPeriod As ATPeriodDTO) As Boolean
#End Region

#Region "List Salary"
        <OperationContract()>
        Function DeleteFomulerGroup(ByVal lstDelete As PAFomulerGroup) As Boolean
        <OperationContract()>
        Function ModifyFomulerGroup(ByVal objPeriod As PAFomulerGroup, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function InsertFomulerGroup(ByVal objPeriod As PAFomulerGroup, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function GetAllFomulerGroup() As List(Of PAFomulerGroup)
        <OperationContract()>
        Function GetListAllSalary(ByVal gID As Decimal) As List(Of PAFomuler)
        <OperationContract()>
        Function GetListInputColumn(ByVal gID As Decimal) As DataTable
        <OperationContract()>
        Function GetListCalculation() As List(Of OT_OTHERLIST_DTO)
        <OperationContract()>
        Function SaveFomuler(ByVal objData As PAFomuler, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function CheckFomuler(ByVal sCol As String, ByVal sFormuler As String, ByVal objID As Decimal) As Boolean
        <OperationContract()>
        Function ActiveFolmulerGroup(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As Decimal) As Boolean
#End Region

#Region "Salary Group"
        <OperationContract()>
        Function GetSalaryGroup(ByVal _filter As SalaryGroupDTO, ByVal PageIndex As Integer,
                             ByVal PageSize As Integer,
                             ByRef Total As Integer,
                             Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of SalaryGroupDTO)
        <OperationContract()>
        Function InsertSalaryGroup(ByVal objSalaryGroup As SalaryGroupDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ValidateSalaryGroup(ByVal objSalaryGroup As SalaryGroupDTO) As Boolean
        <OperationContract()>
        Function ModifySalaryGroup(ByVal objSalaryGroup As SalaryGroupDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function DeleteSalaryGroup(ByVal lstID As List(Of Decimal)) As Boolean
        <OperationContract()>
        Function GetEffectSalaryGroup() As SalaryGroupDTO

#End Region

#Region "Salary Level"

        <OperationContract()>
        Function GetSalaryLevel(ByVal _filter As SalaryLevelDTO, ByVal PageIndex As Integer,
                             ByVal PageSize As Integer,
                             ByRef Total As Integer,
                             Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of SalaryLevelDTO)
        <OperationContract()>
        Function InsertSalaryLevel(ByVal objSalaryLevel As SalaryLevelDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ValidateSalaryLevel(ByVal objSalaryLevel As SalaryLevelDTO) As Boolean
        <OperationContract()>
        Function ModifySalaryLevel(ByVal objSalaryLevel As SalaryLevelDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveSalaryLevel(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean

        <OperationContract()>
        Function DeleteSalaryLevel(ByVal lstSalaryLevel() As SalaryLevelDTO) As Boolean

#End Region

#Region "Salary Rank"

        <OperationContract()>
        Function GetSalaryRank(ByVal _filter As SalaryRankDTO, ByVal PageIndex As Integer,
                             ByVal PageSize As Integer,
                             ByRef Total As Integer,
                             Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of SalaryRankDTO)
        <OperationContract()>
        Function InsertSalaryRank(ByVal objSalaryRank As SalaryRankDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ValidateSalaryRank(ByVal objSalaryRank As SalaryRankDTO) As Boolean
        <OperationContract()>
        Function ModifySalaryRank(ByVal objSalaryRank As SalaryRankDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveSalaryRank(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean

        <OperationContract()>
        Function DeleteSalaryRank(ByVal lstSalaryRank() As SalaryRankDTO) As Boolean

#End Region

#Region "Salary List"

        <OperationContract()>
        Function GetListSalaries(ByVal _filter As PAListSalariesDTO,
                                        ByVal TypePaymentId As Integer,
                                        Optional ByVal PageIndex As Integer = 0,
                                        Optional ByVal PageSize As Integer = Integer.MaxValue,
                                        Optional ByRef Total As Integer = 0,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PAListSalariesDTO)
        <OperationContract()>
        Function CheckColName(ByVal COl_NAME As String, ByVal TypeID As Decimal) As Boolean
        <OperationContract()>
        Function InsertListSalaries(ByVal objListSalaries As PAListSalariesDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyListSalaries(ByVal objListSalaries As PAListSalariesDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveListSalaries(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean

        <OperationContract()>
        Function DeleteListSalaries(ByVal lstID As List(Of Decimal)) As Boolean
#End Region

#Region "lunch list : Đơn giá tiền ăn trưa"
        <OperationContract()>
        Function GetPriceLunchList(ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "EFFECT_DATE desc",
                                        Optional ByVal log As UserLog = Nothing) As List(Of ATPriceLunchDTO)
        <OperationContract()>
        Function GetPriceLunch(ByVal year As Decimal) As List(Of ATPriceLunchDTO)

        <OperationContract()>
        Function InsertPriceLunch(ByVal objPeriod As ATPriceLunchDTO, ByVal objOrgPeriod As List(Of PA_ORG_LUNCH), ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ModifyPriceLunch(ByVal objPeriod As ATPriceLunchDTO, ByVal objOrgPeriod As List(Of PA_ORG_LUNCH), ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function DeletePriceLunch(ByVal lstPeriod As ATPriceLunchDTO) As Boolean
        <OperationContract()>
        Function ValidateATPriceLunch(ByVal _validate As ATPriceLunchDTO) As Boolean
        <OperationContract()>
        Function ValidateATPriceLunchOrg(ByVal _validate As ATPriceLunchDTO) As Boolean
        <OperationContract()>
        Function GetOrgByLunchID(ByVal lunchID As Decimal) As List(Of Decimal)

#End Region

#Region "tien an trua theo nhan vien"
        <OperationContract()>
        Function GetPA_EMP_LUNCH(ByVal _filter As PA_EMP_LUNCHDTO,
                                      ByVal _param As PA_ParamDTO,
                                     Optional ByRef Total As Integer = 0,
                                     Optional ByVal PageIndex As Integer = 0,
                                     Optional ByVal PageSize As Integer = Integer.MaxValue,
                                     Optional ByVal Sorts As String = "EFFECT_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of PA_EMP_LUNCHDTO)

        <OperationContract()>
        Function GetPA_EMP_LUNCHbyID(ByVal _filter As PA_EMP_LUNCHDTO) As PA_EMP_LUNCHDTO

        <OperationContract()>
        Function InsertPA_EMP_LUNCH(ByVal lst As List(Of PA_EMP_LUNCHDTO), ByVal objEmp As PA_EMP_LUNCHDTO,
                                  ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyPA_EMP_LUNCH(ByVal obj As PA_EMP_LUNCHDTO,
                             ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DeletePA_EMP_LUNCH(ByVal lstID As List(Of Decimal)) As Boolean
#End Region

#Region "Test services"
        <OperationContract()>
        Function TestService(ByVal str As String) As String
#End Region

#Region "get Combobox"
        <OperationContract()>
        Function GetComboboxData(ByRef cbxData As ComboBoxDataDTO) As Boolean
#End Region

#Region "IPORTAL - View phiếu lương"
        <OperationContract()>
        Function GetPayrollSheetSum(ByVal PeriodId As Integer, ByVal EmployeeId As String, ByVal log As UserLog,
                                        Optional ByVal Sorts As String = "CREATED_DATE DESC") As DataTable

        <OperationContract()>
        Function CheckPeriod(ByVal PeriodId As Integer, ByVal EmployeeId As Decimal) As Boolean
#End Region

#Region "Báo cáo lương"
        <OperationContract()>
        Function GetReportById(ByVal _filter As Se_ReportDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        ByVal log As UserLog,
                                        Optional ByVal Sorts As String = "CODE ASC") As List(Of Se_ReportDTO)
        <OperationContract()>
        Function ExportReport(ByVal sPkgName As String,
                              ByVal sMoth As Decimal,
                              ByVal sYear As Decimal,
                                     ByVal sOrgId As String,
                              ByVal IsDissolve As Decimal,
                              ByVal log As UserLog) As DataSet

        <OperationContract()>
        Function ExportReport_008(ByVal sPkgName As String,
                              ByVal sMoth As Decimal,
                              ByVal sYear As Decimal,
                                     ByVal sOrgId As String,
                              ByVal IsDissolve As Decimal,
                              ByVal log As UserLog) As DataSet

        <OperationContract()>
        Function ExportReport_010(ByVal sPkgName As String,
                              ByVal sMoth As Decimal,
                              ByVal sYear As Decimal,
                                     ByVal sOrgId As String,
                              ByVal IsDissolve As Decimal,
                              ByVal log As UserLog) As DataSet

        <OperationContract()>
        Function ExportReport_014(ByVal sPkgName As String,
                              ByVal sMoth As Decimal,
                              ByVal sYear As Decimal,
                                     ByVal sOrgId As String,
                              ByVal IsDissolve As Decimal,
                              ByVal log As UserLog) As DataSet

        <OperationContract()>
        Function ExportReport_005(ByVal sPkgName As String,
                              ByVal sMoth As Decimal,
                              ByVal sYear As Decimal,
                                     ByVal sOrgId As String,
                              ByVal IsDissolve As Decimal,
                              ByVal log As UserLog) As DataSet

        <OperationContract()>
        Function ExportPhieuLuong(ByVal lstEmployee As List(Of Decimal),
                                     ByVal orgID As Decimal?,
                                     ByVal isDissolve As Decimal?,
                                     ByVal periodID As Decimal,
                                  ByVal log As UserLog) As DataTable

        <OperationContract()>
        Function ActionSendPayslip(ByVal lstEmployee As List(Of Decimal),
                                     ByVal orgID As Decimal?,
                                     ByVal isDissolve As Decimal?,
                                     ByVal periodID As Decimal,
                                     ByVal log As UserLog) As Boolean

#End Region


#Region "LOG"

        <OperationContract()>
        Function GetActionLog(ByVal _filter As PA_ACTION_LOGDTO,
                                 Optional ByRef Total As Integer = 0,
                                 Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByVal Sorts As String = "ACTION_DATE desc") As List(Of PA_ACTION_LOGDTO)

        <OperationContract()>
        Function DeleteActionLogsPA(ByVal lstDeleteIds As List(Of Decimal)) As Integer

#End Region

#Region "Seniority"

        <OperationContract()>
        Function CalSeniorityProcess(ByVal _filter As PASeniorityProcessDTO, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function GetSeniorityProcess(ByVal _filter As PASeniorityProcessDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer, ByVal log As UserLog,
                                        Optional ByVal Sorts As String = "EMPLOYEE_CODE") As List(Of PASeniorityProcessDTO)

        <OperationContract()>
        Function GetSeniorityProcessImport(ByVal _filter As PASeniorityProcessDTO,
                                        ByVal log As UserLog) As DataTable

        <OperationContract()>
        Function SaveSeniorityProcessImport(ByVal dtData As DataTable,
                                               ByVal periodId As Decimal,
                                               ByVal log As UserLog)

        <OperationContract()>
        Function CalSeniorityMonthly(ByVal _filter As PASeniorityMonthlyDTO, ByVal log As UserLog) As Boolean


        <OperationContract()>
        Function GetSeniorityMonthly(ByVal _filter As PASeniorityMonthlyDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer, ByVal log As UserLog,
                                        Optional ByVal Sorts As String = "EMPLOYEE_CODE") As List(Of PASeniorityMonthlyDTO)


#End Region


#Region "MailRemark"

        <OperationContract()>
        Function GetMailRemark(ByVal _filter As PAMailRemarkDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer, ByVal log As UserLog,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PAMailRemarkDTO)

        <OperationContract()>
        Function InsertMailRemark(ByVal objMailRemark As PAMailRemarkDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyMailRemark(ByVal objMailRemark As PAMailRemarkDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DeleteMailRemark(ByVal lstID As List(Of Decimal)) As Boolean
#End Region


    End Interface
End Namespace


