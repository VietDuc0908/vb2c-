using System;
using PayrollDAL;
using Framework.Data;
// NOTE: You can use the "Rename" command on the context menu to change the interface name "IService1" in both code and config file together.
namespace PayrollBusiness.ServiceContracts
{
    [ServiceContract()]
    public interface IPayrollBusiness
    {
        #region Common
        [OperationContract()]
        bool CheckExistInDatabase(List<decimal> lstID, TABLE_NAME table);


        [OperationContract()]
        bool IS_PERIODSTATUS(PA_ParamDTO _param, UserLog log);


        [OperationContract()]
        bool IS_PERIOD_COLEXSTATUS(PA_ParamDTO _param, UserLog log);
        #endregion

        #region Calculate Salary
        [OperationContract()]
        bool Load_Calculate_Load(List<PAEmployeeCalculateDTO> lstEmp, int OrgId, int PeriodId, int IsDissolve, int IsLoad, UserLog log);
        [OperationContract()]
        bool Load_data_sum(int OrgId, int PeriodId, int IsDissolve, int IsLoad, UserLog log);
        [OperationContract()]
        bool Calculate_data_sum(int OrgId, int PeriodId, int IsDissolve, int IsLoad, UserLog log);

        [OperationContract()]
        bool Calculate_data_temp(int OrgId, int PeriodId, int IsDissolve, int IsLoad, UserLog log);
        [OperationContract()]
        List<PAListSalariesDTO> GetListSalaryVisibleCol();
        [OperationContract()]
        DataTable GetLitsCalculate(PA_ParamDTO param, int IsLoad, int PageIndex, int PageSize, int PageType, ref int TotalRow, UserLog log, string Sorts = "CREATED_DATE DESC");
        [OperationContract()]
        bool ActiveOrDeactive(PA_ParamDTO _param, UserLog log);
        #endregion


        #region Import Salary
        [OperationContract()]
        DataTable GetImportSalary(int PeriodId, int OrgId, int IsDissolve, string EmployeeId, int PageIndex, int PageSize, ref int TotalRow, UserLog log, string Sorts = "CREATED_DATE DESC");
        [OperationContract()]
        List<PAListSalariesDTO> GetSalaryList();
        [OperationContract()]
        bool SaveImport(decimal Period, DataTable dtData, List<string> lstColVal, UserLog log, ref int RecordSussces);
        #endregion

        #region "Hold Salary"
        [OperationContract()]
        List<PAHoldSalaryDTO> GetHoldSalaryList(int PeriodId, int OrgId, int IsDissolve, UserLog log, int PageIndex, int PageSize, ref int Total, string Sorts = "CREATED_DATE desc");

        [OperationContract()]
        bool InsertHoldSalary(List<PAHoldSalaryDTO> objPeriod, UserLog log);

        [OperationContract()]
        bool DeleteHoldSalary(List<decimal> lstDelete);
        #endregion


        #region "Taxation List"
        [OperationContract()]
        List<PATaxationDTO> GetTaxation(PATaxationDTO _filter, int PageIndex, int PageSize, ref int Total, string Sorts = "CREATED_DATE desc");

        [OperationContract()]
        bool InsertTaxation(PATaxationDTO objTaxation, UserLog log, ref decimal gID);

        [OperationContract()]
        bool ModifyTaxation(PATaxationDTO objTaxation, UserLog log, ref decimal gID);

        [OperationContract()]
        bool ActiveTaxation(List<decimal> lstID, UserLog log, string bActive);

        [OperationContract()]
        bool DeleteTaxation(List<decimal> lstID);
        #endregion

        #region "Payment List"
        [OperationContract()]
        List<PAPaymentListDTO> GetPaymentList(int PageIndex, int PageSize, ref int Total, string Sorts = "CREATED_DATE desc");
        [OperationContract()]
        List<PAPaymentListDTO> GetPaymentListAll(string Sorts = "CREATED_DATE desc");
        [OperationContract()]
        bool InsertPaymentList(PAPaymentListDTO objPaymentList, UserLog log, ref decimal gID);

        [OperationContract()]
        bool ModifyPaymentList(PAPaymentListDTO objPaymentList, UserLog log, ref decimal gID);

        [OperationContract()]
        bool ActivePaymentList(List<decimal> lstID, UserLog log, string bActive);

        [OperationContract()]
        bool DeletePaymentList(List<decimal> lstID);
        #endregion

        #region "Object Salary"
        [OperationContract()]
        List<PAObjectSalaryDTO> GetObjectSalary(int PageIndex, int PageSize, ref int Total, string Sorts = "CREATED_DATE desc");
        [OperationContract()]
        List<PAObjectSalaryDTO> GetObjectSalaryAll(string Sorts = "CREATED_DATE desc");
        [OperationContract()]
        bool InsertObjectSalary(PAObjectSalaryDTO objObjectSalary, UserLog log, ref decimal gID);

        [OperationContract()]
        bool ValidateObjectSalary(PAObjectSalaryDTO objObjectSalary);

        [OperationContract()]
        bool ModifyObjectSalary(PAObjectSalaryDTO objObjectSalary, UserLog log, ref decimal gID);

        [OperationContract()]
        bool ActiveObjectSalary(List<decimal> lstID, UserLog log, string bActive);

        [OperationContract()]
        bool DeleteObjectSalary(List<decimal> lstID);
        #endregion

        #region "Period List"
        [OperationContract()]
        List<ATPeriodDTO> GetPeriodList(int PageIndex, int PageSize, ref int Total, string Sorts = "START_DATE desc");

        [OperationContract()]
        List<ATPeriodDTO> GetPeriodbyYear(decimal year);

        [OperationContract()]
        List<decimal> GetOrgByPeriodID(decimal periodID);

        [OperationContract()]
        bool InsertPeriod(ATPeriodDTO objPeriod, List<AT_ORG_PERIOD> objOrgPeriod, UserLog log, ref decimal gID);

        [OperationContract()]
        bool ModifyPeriod(ATPeriodDTO objPeriod, List<AT_ORG_PERIOD> objOrgPeriod, UserLog log, ref decimal gID);

        [OperationContract()]
        bool DeletePeriod(ATPeriodDTO lstPeriod);

        [OperationContract()]
        bool ValidateATPeriod(ATPeriodDTO objPeriod);

        [OperationContract()]
        bool ValidateATPeriodDay(ATPeriodDTO objPeriod);
        #endregion

        #region "List Salary"
        [OperationContract()]
        bool DeleteFomulerGroup(PAFomulerGroup lstDelete);
        [OperationContract()]
        bool ModifyFomulerGroup(PAFomulerGroup objPeriod, UserLog log, ref decimal gID);
        [OperationContract()]
        bool InsertFomulerGroup(PAFomulerGroup objPeriod, UserLog log, ref decimal gID);
        [OperationContract()]
        List<PAFomulerGroup> GetAllFomulerGroup();
        [OperationContract()]
        List<PAFomuler> GetListAllSalary(decimal gID);
        [OperationContract()]
        DataTable GetListInputColumn(decimal gID);
        [OperationContract()]
        List<OT_OTHERLIST_DTO> GetListCalculation();
        [OperationContract()]
        bool SaveFomuler(PAFomuler objData, UserLog log, ref decimal gID);
        [OperationContract()]
        bool CheckFomuler(string sCol, string sFormuler, decimal objID);
        [OperationContract()]
        bool ActiveFolmulerGroup(List<decimal> lstID, UserLog log, decimal bActive);
        #endregion

        #region "Salary Group"
        [OperationContract()]
        List<SalaryGroupDTO> GetSalaryGroup(SalaryGroupDTO _filter, int PageIndex, int PageSize, ref int Total, string Sorts = "CREATED_DATE desc");
        [OperationContract()]
        bool InsertSalaryGroup(SalaryGroupDTO objSalaryGroup, UserLog log, ref decimal gID);
        [OperationContract()]
        bool ValidateSalaryGroup(SalaryGroupDTO objSalaryGroup);
        [OperationContract()]
        bool ModifySalaryGroup(SalaryGroupDTO objSalaryGroup, UserLog log, ref decimal gID);
        [OperationContract()]
        bool DeleteSalaryGroup(List<decimal> lstID);
        [OperationContract()]
        SalaryGroupDTO GetEffectSalaryGroup();
        #endregion

        #region "Salary Level"
        [OperationContract()]
        List<SalaryLevelDTO> GetSalaryLevel(SalaryLevelDTO _filter, int PageIndex, int PageSize, ref int Total, string Sorts = "CREATED_DATE desc");
        [OperationContract()]
        bool InsertSalaryLevel(SalaryLevelDTO objSalaryLevel, UserLog log, ref decimal gID);
        [OperationContract()]
        bool ValidateSalaryLevel(SalaryLevelDTO objSalaryLevel);
        [OperationContract()]
        bool ModifySalaryLevel(SalaryLevelDTO objSalaryLevel, UserLog log, ref decimal gID);

        [OperationContract()]
        bool ActiveSalaryLevel(List<decimal> lstID, UserLog log, string bActive);

        [OperationContract()]
        bool DeleteSalaryLevel(SalaryLevelDTO[] lstSalaryLevel);
        #endregion

        #region "Salary Rank"
        [OperationContract()]
        List<SalaryRankDTO> GetSalaryRank(SalaryRankDTO _filter, int PageIndex, int PageSize, ref int Total, string Sorts = "CREATED_DATE desc");
        [OperationContract()]
        bool InsertSalaryRank(SalaryRankDTO objSalaryRank, UserLog log, ref decimal gID);
        [OperationContract()]
        bool ValidateSalaryRank(SalaryRankDTO objSalaryRank);
        [OperationContract()]
        bool ModifySalaryRank(SalaryRankDTO objSalaryRank, UserLog log, ref decimal gID);

        [OperationContract()]
        bool ActiveSalaryRank(List<decimal> lstID, UserLog log, string bActive);

        [OperationContract()]
        bool DeleteSalaryRank(SalaryRankDTO[] lstSalaryRank);
        #endregion


        #region "Salary List"
        [OperationContract()]
        List<PAListSalariesDTO> GetListSalaries(PAListSalariesDTO _filter, int TypePaymentId, int PageIndex = 0, int PageSize = int.MaxValue, ref int Total = 0, string Sorts = "CREATED_DATE desc");
        [OperationContract()]
        bool CheckColName(string COl_NAME, decimal TypeID);
        [OperationContract()]
        bool InsertListSalaries(PAListSalariesDTO objListSalaries, UserLog log, ref decimal gID);

        [OperationContract()]
        bool ModifyListSalaries(PAListSalariesDTO objListSalaries, UserLog log, ref decimal gID);

        [OperationContract()]
        bool ActiveListSalaries(List<decimal> lstID, UserLog log, string bActive);

        [OperationContract()]
        bool DeleteListSalaries(List<decimal> lstID);
        #endregion

        #region "lunch list : Đơn giá tiền ăn trưa"
        [OperationContract()]
        List<ATPriceLunchDTO> GetPriceLunchList(int PageIndex, int PageSize, ref int Total, string Sorts = "EFFECT_DATE desc", UserLog log = null/* TODO Change to default(_) if this is not a reference type */);
        [OperationContract()]
        List<ATPriceLunchDTO> GetPriceLunch(decimal year);

        [OperationContract()]
        bool InsertPriceLunch(ATPriceLunchDTO objPeriod, List<PA_ORG_LUNCH> objOrgPeriod, UserLog log, ref decimal gID);
        [OperationContract()]
        bool ModifyPriceLunch(ATPriceLunchDTO objPeriod, List<PA_ORG_LUNCH> objOrgPeriod, UserLog log, ref decimal gID);
        [OperationContract()]
        bool DeletePriceLunch(ATPriceLunchDTO lstPeriod);
        [OperationContract()]
        bool ValidateATPriceLunch(ATPriceLunchDTO _validate);
        [OperationContract()]
        bool ValidateATPriceLunchOrg(ATPriceLunchDTO _validate);
        [OperationContract()]
        List<decimal> GetOrgByLunchID(decimal lunchID);
        #endregion

        #region "tien an trua theo nhan vien"
        [OperationContract()]
        List<PA_EMP_LUNCHDTO> GetPA_EMP_LUNCH(PA_EMP_LUNCHDTO _filter, PA_ParamDTO _param, ref int Total = 0, int PageIndex = 0, int PageSize = int.MaxValue, string Sorts = "EFFECT_DATE desc", UserLog log = null/* TODO Change to default(_) if this is not a reference type */);

        [OperationContract()]
        PA_EMP_LUNCHDTO GetPA_EMP_LUNCHbyID(PA_EMP_LUNCHDTO _filter);

        [OperationContract()]
        bool InsertPA_EMP_LUNCH(List<PA_EMP_LUNCHDTO> lst, PA_EMP_LUNCHDTO objEmp, UserLog log, ref decimal gID);

        [OperationContract()]
        bool ModifyPA_EMP_LUNCH(PA_EMP_LUNCHDTO obj, UserLog log, ref decimal gID);

        [OperationContract()]
        bool DeletePA_EMP_LUNCH(List<decimal> lstID);
        #endregion

        #region "Test services"
        [OperationContract()]
        string TestService(string str);
        #endregion

        #region "get Combobox"
        [OperationContract()]
        bool GetComboboxData(ref ComboBoxDataDTO cbxData);
        #endregion

        #region "IPORTAL - View phiếu lương"
        [OperationContract()]
        DataTable GetPayrollSheetSum(int PeriodId, string EmployeeId, UserLog log, string Sorts = "CREATED_DATE DESC");

        [OperationContract()]
        bool CheckPeriod(int PeriodId, decimal EmployeeId);
        #endregion

        #region "Báo cáo lương"
        [OperationContract()]
        List<Se_ReportDTO> GetReportById(Se_ReportDTO _filter, int PageIndex, int PageSize, ref int Total, UserLog log, string Sorts = "CODE ASC");
        [OperationContract()]
        DataSet ExportReport(string sPkgName, decimal sMoth, decimal sYear, string sOrgId, decimal IsDissolve, UserLog log);

        [OperationContract()]
        DataSet ExportReport_008(string sPkgName, decimal sMoth, decimal sYear, string sOrgId, decimal IsDissolve, UserLog log);

        [OperationContract()]
        DataSet ExportReport_010(string sPkgName, decimal sMoth, decimal sYear, string sOrgId, decimal IsDissolve, UserLog log);

        [OperationContract()]
        DataSet ExportReport_014(string sPkgName, decimal sMoth, decimal sYear, string sOrgId, decimal IsDissolve, UserLog log);

        [OperationContract()]
        DataSet ExportReport_005(string sPkgName, decimal sMoth, decimal sYear, string sOrgId, decimal IsDissolve, UserLog log);

        [OperationContract()]
        DataTable ExportPhieuLuong(List<decimal> lstEmployee, decimal? orgID, decimal? isDissolve, decimal periodID, UserLog log);

        [OperationContract()]
        bool ActionSendPayslip(List<decimal> lstEmployee, decimal? orgID, decimal? isDissolve, decimal periodID, UserLog log);
        #endregion


        #region "LOG"
        [OperationContract()]
        List<PA_ACTION_LOGDTO> GetActionLog(PA_ACTION_LOGDTO _filter, ref int Total = 0, int PageIndex = 0, int PageSize = int.MaxValue, string Sorts = "ACTION_DATE desc");

        [OperationContract()]
        int DeleteActionLogsPA(List<decimal> lstDeleteIds);
        #endregion

        #region "Seniority"
        [OperationContract()]
        bool CalSeniorityProcess(PASeniorityProcessDTO _filter, UserLog log);

        [OperationContract()]
        List<PASeniorityProcessDTO> GetSeniorityProcess(PASeniorityProcessDTO _filter, int PageIndex, int PageSize, ref int Total, UserLog log, string Sorts = "EMPLOYEE_CODE");

        [OperationContract()]
        DataTable GetSeniorityProcessImport(PASeniorityProcessDTO _filter, UserLog log);

        [OperationContract()]
        void SaveSeniorityProcessImport(DataTable dtData, decimal periodId, UserLog log);

        [OperationContract()]
        bool CalSeniorityMonthly(PASeniorityMonthlyDTO _filter, UserLog log);


        [OperationContract()]
        List<PASeniorityMonthlyDTO> GetSeniorityMonthly(PASeniorityMonthlyDTO _filter, int PageIndex, int PageSize, ref int Total, UserLog log, string Sorts = "EMPLOYEE_CODE");
        #endregion



        #region "MailRemark"
        [OperationContract()]
        List<PAMailRemarkDTO> GetMailRemark(PAMailRemarkDTO _filter, int PageIndex, int PageSize, ref int Total, UserLog log, string Sorts = "CREATED_DATE desc");

        [OperationContract()]
        bool InsertMailRemark(PAMailRemarkDTO objMailRemark, UserLog log, ref decimal gID);

        [OperationContract()]
        bool ModifyMailRemark(PAMailRemarkDTO objMailRemark, UserLog log, ref decimal gID);

        [OperationContract()]
        bool DeleteMailRemark(List<decimal> lstID);
        #endregion
    }
}
