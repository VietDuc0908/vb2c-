using System;
using Framework.Data;
using LinqKit;
using System.Data.Objects.DataClasses;
using System.Data.Common;
using System.Data.Entity;
using System.Threading;
using Framework.Data.System.Linq.Dynamic;
using Framework.Data.SystemConfig;
using System.Configuration;

public partial class AttendanceRepository : IDisposable
{
    private AttendanceContext _ctx;
    private bool _isAvailable;

    public AttendanceContext Context
    {
        get
        {
            if (_ctx == null)
            {
                _ctx = new AttendanceContext();
                _ctx.ContextOptions.LazyLoadingEnabled = true;
            }
            return _ctx;
        }
    }

    public bool GetComboboxData(ref ComboBoxDataDTO cbxData)
    {
        try
        {
            // Danh sách các đối tượng cư trú
            if (cbxData.GET_LIST_TYPEPUNISH)
            {
                cbxData.LIST_LIST_TYPEPUNISH = (from p in Context.OT_OTHER_LIST
                                                join t in Context.OT_OTHER_LIST_TYPE on p.TYPE_ID equals t.ID
                                                where p.ACTFLG == "A" & t.CODE == "TYPE_PUNISH"
                                                orderby p.CREATED_DATE descending
                                                select new OT_OTHERLIST_DTO() { ID = p.ID, CODE = p.CODE, NAME_EN = p.NAME_EN, NAME_VN = p.NAME_VN, TYPE_ID = p.TYPE_ID }).ToList()();
            }
            if (cbxData.GET_LIST_TYPESHIFT)
            {
                cbxData.LIST_LIST_TYPESHIFT = (from p in Context.OT_OTHER_LIST
                                               join t in Context.OT_OTHER_LIST_TYPE on p.TYPE_ID equals t.ID
                                               where p.ACTFLG == "A" & t.CODE == "TYPE_SHIFT"
                                               orderby p.CREATED_DATE descending
                                               select new OT_OTHERLIST_DTO() { ID = p.ID, CODE = p.CODE, NAME_EN = p.NAME_EN, NAME_VN = p.NAME_VN, TYPE_ID = p.TYPE_ID }).ToList();
            }
            if (cbxData.GET_LIST_APPLY_LAW)
            {
                cbxData.LIST_LIST_APPLY_LAW = (from p in Context.AT_GSIGN
                                               where p.ACTFLG == "A"
                                               orderby p.NAME_VN descending
                                               select new AT_GSIGNDTO() { ID = p.ID, CODE = p.CODE, NAME_VN = "[" + p.CODE + "] " + p.NAME_VN }).ToList();
            }
            if (cbxData.GET_LIST_PENALIZEA)
            {
                cbxData.LIST_LIST_PENALIZEA = (from p in Context.AT_DMVS
                                               where p.ACTFLG == "A"
                                               orderby p.NAME_VN descending
                                               select new AT_DMVSDTO() { ID = p.ID, CODE = p.CODE, NAME_VN = p.NAME_VN }).ToList(); 
            }

            if (cbxData.GET_LIST_SHIFT)
            {
                cbxData.LIST_LIST_SHIFT = (from p in Context.AT_SHIFT
                                           where p.ACTFLG == "A"
                                           orderby p.NAME_VN descending
                                           select new AT_SHIFTDTO() { ID = p.ID, CODE = p.CODE, NAME_VN = "[" + p.CODE + "] " + p.NAME_VN }).ToList();
            }
            if (cbxData.GET_LIST_SIGN)
            {
                cbxData.LIST_LIST_SIGN = (from p in Context.AT_FML
                                          where p.ACTFLG == "A"
                                          orderby p.NAME_VN descending
                                          select new AT_FMLDTO() { ID = p.ID, CODE = p.CODE, NAME_VN = "[" + p.CODE + "] " + p.NAME_VN }).ToList();
            }
            if (cbxData.GET_LIST_TYPEEMPLOYEE)
            {
                cbxData.LIST_LIST_TYPEEMPLOYEE = (from p in Context.OT_OTHER_LIST
                                                  join t in Context.OT_OTHER_LIST_TYPE on p.TYPE_ID equals t.ID
                                                  where p.ACTFLG == "A" & t.CODE == "TYPE_EMPLOYEE"
                                                  orderby p.CREATED_DATE descending
                                                  select new OT_OTHERLIST_DTO() { ID = p.ID, CODE = p.CODE, NAME_EN = p.NAME_EN, NAME_VN = p.NAME_VN, TYPE_ID = p.TYPE_ID }).ToList();
            }
            if (cbxData.GET_LIST_TYPEE_FML)
            {
                cbxData.LIST_LIST_TYPE_FML = (from p in Context.AT_FML
                                              where p.ACTFLG == "A"
                                              orderby p.NAME_VN descending
                                              select new AT_FMLDTO() { ID = p.ID, CODE = p.CODE, NAME_VN = "[" + p.CODE + "] " + p.NAME_VN }).ToList();
            }
            if (cbxData.GET_LIST_REST_DAY)
            {
                cbxData.LIST_LIST_REST_DAY = (from p in Context.OT_OTHER_LIST
                                              join t in Context.OT_OTHER_LIST_TYPE on p.TYPE_ID equals t.ID
                                              where p.ACTFLG == "A" & t.CODE == "AT_TIMELEAVE"
                                              orderby p.CREATED_DATE descending
                                              select new OT_OTHERLIST_DTO() { ID = p.ID, CODE = p.CODE, NAME_EN = p.NAME_EN, NAME_VN = p.NAME_VN, TYPE_ID = p.TYPE_ID }).ToList();
            }
            if (cbxData.GET_LIST_TYPE_DMVS)
            {
                cbxData.LIST_LIST_TYPE_DMVS = (from p in Context.AT_TIME_MANUAL
                                               where p.ACTFLG == "A" & p.CODE == "RDT" | p.CODE == "RVS"
                                               orderby p.NAME descending
                                               select new OT_OTHERLIST_DTO() { ID = p.ID, CODE = p.CODE, NAME_VN = p.NAME }).ToList();
            }

            if (cbxData.GET_LIST_TYPE_MANUAL_LEAVE)
            {
                cbxData.LIST_LIST_TYPE_MANUAL_LEAVE = (From p In Context.AT_TIME_MANUAL
                                                       From F In Context.AT_FML.Where(Function(f) f.ID = p.MORNING_ID).DefaultIfEmpty
                                                       From F2 In Context.AT_FML.Where(Function(f2) f2.ID = p.AFTERNOON_ID).DefaultIfEmpty
                                                       Where p.ACTFLG = "A" And(F.IS_LEAVE = -1 Or F2.IS_LEAVE = -1) Order By p.NAME Descending
                                                       Select New AT_TIME_MANUALDTO With {
                                                           .ID = p.ID,
                                                           .CODE = p.CODE,
                                                           .MORNING_ID = p.MORNING_ID,
                                                           .AFTERNOON_ID = p.AFTERNOON_ID,
                                                           .NAME_VN = "[" & p.CODE & "] " & p.NAME}).ToList()
            }



            if (cbxData.GET_LIST_TYPE_MANUAL)
                cbxData.LIST_LIST_TYPE_MANUAL = (from p in Context.AT_TIME_MANUAL
                                                 where p.ACTFLG == "A" & p.CODE != "RVS" & p.CODE != "RDT"
                                                 orderby p.NAME descending
                                                 select new AT_TIME_MANUALDTO() { ID = p.ID, CODE = p.CODE, NAME_VN = "[" + p.CODE + "] " + p.NAME }).ToList();

            if (cbxData.GET_LIST_SHIFT_SUNDAY)
                cbxData.LIST_LIST_SHIFT_SUNDAY = (from p in Context.AT_TIME_MANUAL
                                                  where p.ACTFLG == "A" & p.CODE == "OFF"
                                                  orderby p.NAME descending
                                                  select new AT_TIME_MANUALDTO() { ID = p.ID, CODE = p.CODE, NAME_VN = "[" + p.CODE + "] " + p.NAME }).ToList();
            // danh mục cấp nhân sự
            if (cbxData.GET_LIST_STAFF_RANK)
            {
                cbxData.LIST_LIST_STAFF_RANK = (from p in Context.HU_STAFF_RANK
                                                where p.ACTFLG == "A"
                                                orderby p.NAME descending
                                                select new HU_STAFF_RANKDTO() { ID = p.ID, CODE = p.CODE, NAME = p.NAME }).ToList();
            }
            if (cbxData.GET_LIST_HS_OT)
            {
                cbxData.LIST_LIST_HS_OT = (from p in Context.OT_OTHER_LIST
                                           join t in Context.OT_OTHER_LIST_TYPE on p.TYPE_ID equals t.ID
                                           where p.ACTFLG == "A" & t.CODE == "HS_OT"
                                           orderby p.ID descending
                                           select new OT_OTHERLIST_DTO() { ID = p.ID, CODE = p.CODE, NAME_EN = p.NAME_EN, NAME_VN = p.NAME_VN, TYPE_ID = p.TYPE_ID }).ToList();
            }
            if (cbxData.GET_LIST_TYPE_OT)
                cbxData.LIST_LIST_TYPE_OT = (from p in Context.OT_OTHER_LIST
                                             join t in Context.OT_OTHER_LIST_TYPE on p.TYPE_ID equals t.ID
                                             where p.ACTFLG == "A" & t.CODE == "TYPE_OT"
                                             orderby p.ID descending
                                             select new OT_OTHERLIST_DTO() { ID = p.ID, CODE = p.CODE, NAME_EN = p.NAME_EN, NAME_VN = p.NAME_VN, TYPE_ID = p.TYPE_ID }).ToList();

            if (cbxData.GET_LIST_TERMINALS_MEAL)
                cbxData.LIST_LIST_TERMINALS_MEAL = (from p in Context.AT_TERMINALS_MEAL
                                                    where p.ACTFLG == "A"
                                                    orderby p.TERMINAL_NAME
                                                    select new AT_TERMINALS_MEALDTO() { ID = p.ID, TERMINAL_NAME = p.TERMINAL_NAME }).ToList();

            if (cbxData.GET_LIST_TIME_TYPE)
                cbxData.LIST_LIST_TIME_TYPE = (from p in Context.OT_OTHER_LIST
                                               join t in Context.OT_OTHER_LIST_TYPE on p.TYPE_ID equals t.ID
                                               where p.ACTFLG == "A" & t.CODE == "TIME_TYPE"
                                               orderby p.CREATED_DATE descending
                                               select new OT_OTHERLIST_DTO() { ID = p.ID, CODE = p.CODE, NAME_EN = p.NAME_EN, NAME_VN = p.NAME_VN, TYPE_ID = p.TYPE_ID }).ToList();

            // loại suất ăn
            if (cbxData.GET_LIST_RATION)
                cbxData.LIST_LIST_RATION = (from p in Context.OT_OTHER_LIST
                                            join t in Context.OT_OTHER_LIST_TYPE on p.TYPE_ID equals t.ID
                                            where p.ACTFLG == "A" & t.CODE == "RATION"
                                            orderby p.CREATED_DATE descending
                                            select new OT_OTHERLIST_DTO() { ID = p.ID, CODE = p.CODE, NAME_EN = p.NAME_EN, NAME_VN = p.NAME_VN, TYPE_ID = p.TYPE_ID }).ToList();

            // Danh sách bếp
            if (cbxData.GET_LIST_KITCHEN)
                cbxData.LIST_LIST_KITCHEN = (from p in Context.AT_KITCHEN
                                             orderby p.CREATED_DATE descending
                                             select new OT_OTHERLIST_DTO()
                                             {
                                                 ID = p.ID,
                                                 CODE = p.KITCHEN_CODE,
                                                 NAME_VN = p.KITCHEN_NAME
                                             }).ToList();

            // Danh sách bếp bữa ăn
            if (cbxData.GET_LIST_MEAL)
                cbxData.LIST_LIST_MEAL = (from p in Context.AT_MEAL
                                          orderby p.ID
                                          select new OT_OTHERLIST_DTO()
                                          {
                                              ID = p.ID,
                                              CODE = p.CODE,
                                              NAME_VN = p.NAME
                                          }).ToList();

            // danh sách loại đối tác
            if (cbxData.GET_LIST_PARTNER)
                cbxData.LIST_LIST_PARTNER = (from p in Context.OT_OTHER_LIST
                                             join t in Context.OT_OTHER_LIST_TYPE on p.TYPE_ID equals t.ID
                                             where p.ACTFLG == "A" & t.CODE == "PARTNER_TYPE"
                                             orderby p.CREATED_DATE descending
                                             select new OT_OTHERLIST_DTO()
                                             {
                                                 ID = p.ID,
                                                 CODE = p.CODE,
                                                 NAME_VN = p.NAME_VN
                                             }).ToList();

            return true;
        }
        catch (Exception ex)
        {
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod().Name, "iTime");
            return false;
        }
        finally
        {
        }
    }

    public List<ApproveUserDTO> GetApproveUsers(decimal employeeId, string processCode, List<decimal> lstOrg = null, bool isTimesheet = false)
    {
        try
        {
            List<ApproveUserDTO> listResult = new List<ApproveUserDTO>();

            var process = Context.SE_APP_PROCESS.SingleOrDefault(p => p.PROCESS_CODE == processCode);

            if (process == null)
                throw new Exception("Chưa thiết lập quy trình phê duyệt HOẶC Mã quy trình phê duyệt sai.");

            // Lấy template phê duyệt đang áp dụng cho nhân viên
            List<SE_APP_SETUP> usingSetups = GetCurrentEmployeeSetup(employeeId, process, lstOrg, isTimesheet);

            if (usingSetups.Count > 0)
            {
                ;/* Cannot convert LocalDeclarationStatementSyntax, System.InvalidOperationException: Sequence contains more than one element
   at System.Linq.Enumerable.Single[TSource](IEnumerable`1 source)
   at ICSharpCode.CodeConverter.CSharp.VisualBasicConverter.NodesVisitor.VisitQueryExpression(QueryExpressionSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.QueryExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.Visit(SyntaxNode node)
   at ICSharpCode.CodeConverter.CSharp.CommentConvertingNodesVisitor.DefaultVisit(SyntaxNode node)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.VisitQueryExpression(QueryExpressionSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.QueryExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at ICSharpCode.CodeConverter.CSharp.CommonConversions.ConvertInitializer(VariableDeclaratorSyntax declarator)
   at ICSharpCode.CodeConverter.CSharp.CommonConversions.SplitVariableDeclarations(VariableDeclaratorSyntax declarator)
   at ICSharpCode.CodeConverter.CSharp.VisualBasicConverter.MethodBodyVisitor.VisitLocalDeclarationStatement(LocalDeclarationStatementSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.LocalDeclarationStatementSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.Visit(SyntaxNode node)
   at ICSharpCode.CodeConverter.CSharp.CommentConvertingMethodBodyVisitor.ConvertWithTrivia(SyntaxNode node)
   at ICSharpCode.CodeConverter.CSharp.CommentConvertingMethodBodyVisitor.DefaultVisit(SyntaxNode node)

Input: 
                Dim usingS = From t In usingSetups
                             From d In Context.SE_APP_TEMPLATE.Where(Function(F) F.ID = t.TEMPLATE_ID)
                             From dt In Context.SE_APP_TEMPLATE_DTL.Where(Function(F) F.TEMPLATE_ID = d.ID)

 */
                var firstTemplate = usingS.OrderByDescending(p => p.d.TEMPLATE_ORDER).FirstOrDefault();
                List(Of SE_APP_TEMPLATE_DTL) usingTemplateDetail  = firstTemplate.AT_APP_TEMPLATE.AT_APP_TEMPLATE_DTL.ToList()

                foreach(SE_APP_TEMPLATE_DTL detailSetting in usingTemplateDetail)
                '    Dim itemAdd As ApproveUserDTO = Nothing
                '    If detailSetting.APP_TYPE = 0 Then
                '        itemAdd = GetDirectManagerApprove(employeeId, detailSetting.APP_LEVEL)
                '    Else
                '        itemAdd = GetEmployeeApprove(detailSetting.APP_ID, detailSetting.APP_LEVEL)
                '    End If

                '    If itemAdd IsNot Nothing Then
                '        itemAdd.INFORM_DATE = detailSetting.INFORM_DATE
                '        itemAdd.INFORM_EMAIL = detailSetting.INFORM_EMAIL

                '        listResult.Add(itemAdd)
                '    End If
                'Next

                Dim usingTemplateDetail = (From t In usingSetups
                                           From d In Context.SE_APP_TEMPLATE.Where(Function(F) F.ID = t.TEMPLATE_ID)
                                           From dt In Context.SE_APP_TEMPLATE_DTL.Where(Function(F) F.TEMPLATE_ID = d.ID) Select dt)

 */


                foreach (SE_APP_TEMPLATE_DTL detailSetting in usingTemplateDetail)
                {
                    ApproveUserDTO itemAdd = null/* TODO Change to default(_) if this is not a reference type */;
                    if (detailSetting.APP_TYPE == 0)
                        itemAdd = GetDirectManagerApprove(employeeId, detailSetting.APP_LEVEL);
                    else
                        itemAdd = GetEmployeeApprove(detailSetting.APP_ID, detailSetting.APP_LEVEL);

                    if (itemAdd != null)
                    {
                        itemAdd.INFORM_DATE = detailSetting.INFORM_DATE;
                        itemAdd.INFORM_EMAIL = detailSetting.INFORM_EMAIL;

                        listResult.Add(itemAdd);
                    }
                }

                if (usingTemplateDetail.Count == listResult.Count)
                    return listResult;
                else
                    return null;
            }

            return listResult;
        }
        catch (Exception ex)
        {
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod().Name, "iTime");
            throw ex;
        }
    }

    private List<SE_APP_SETUP> GetCurrentEmployeeSetup(decimal employeeId, SE_APP_PROCESS process, List<decimal> lstOrg = null, bool isTimesheet = false)
    {
        List<SE_APP_SETUP> _setup = new List<SE_APP_SETUP>();
        var setupEmployee = Context.SE_APP_SETUP.SingleOrDefault(p => p.EMPLOYEE_ID == employeeId
                                                                        && p.PROCESS_ID == process.ID
                                                                        && (p.FROM_DATE <= DateTime.Now
                                                                                 && (!p.TO_DATE.HasValue
                                                                                          || (p.TO_DATE.HasValue
                                                                                                  && DateTime.Now <= p.TO_DATE.Value))));

        if (setupEmployee != null)
            _setup.Add(setupEmployee);

        var setupOrg = GetCurrentEmployeeOrgSetup(employeeId, process, lstOrg, isTimesheet);
        if (setupOrg.Count > 0)
            _setup.AddRange(setupOrg);
        // _setup.Add(GetCurrentEmployeeOrgSetup(employeeId, process))
        return _setup;
    }

    private List<SE_APP_SETUP> GetCurrentEmployeeOrgSetup(decimal employeeId, SE_APP_PROCESS process, List<decimal> lstOrg = null, bool isTimesheet = false)
    {
        try
        {
            List<SE_APP_SETUP> _setup = new List<SE_APP_SETUP>();
            if (isTimesheet)
            {
                // lấy thiết lập theo org truyền vào
                if (lstOrg.Count == 0)
                    return null;
                var lstTemp = (from p in Context.HU_ORGANIZATION
                               where lstOrg.Contains(p.ID)
                               select p).ToList()();

                foreach (var Org in lstTemp)
                {
                    var currentOrgSetup = GetCurrentOrgSetup(Org.ID, process);
                    if (currentOrgSetup != null)
                        _setup.Add(currentOrgSetup);
                }

                if (lstTemp.Count != _setup.Count)
                {
                    bool isParent = false;
                    foreach (var Org in lstTemp)
                    {
                        while (Org.PARENT_ID.HasValue)
                        {
                            Org = Context.HU_ORGANIZATION.SingleOrDefault(p => p.ID == Org.PARENT_ID.Value);
                            var OrgSetup = GetCurrentOrgSetup(Org.ID, process);
                            if (OrgSetup != null)
                            {
                                isParent = true;
                                _setup.Add(OrgSetup);
                            }
                        }
                    }
                    if (isParent)
                        return _setup;
                    else
                        return new List<SE_APP_SETUP>();
                }
                else
                    foreach (var Org in lstTemp)
                    {
                        while (Org.PARENT_ID.HasValue)
                        {
                            Org = Context.HU_ORGANIZATION.SingleOrDefault(p => p.ID == Org.PARENT_ID.Value);
                            var OrgSetup = GetCurrentOrgSetup(Org.ID, process);
                            if (OrgSetup != null)
                                _setup.Add(OrgSetup);
                        }
                    }
            }
            else
            {
                ;/* Cannot convert LocalDeclarationStatementSyntax, System.InvalidOperationException: Sequence contains more than one element
   at System.Linq.Enumerable.Single[TSource](IEnumerable`1 source)
   at ICSharpCode.CodeConverter.CSharp.VisualBasicConverter.NodesVisitor.VisitQueryExpression(QueryExpressionSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.QueryExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.Visit(SyntaxNode node)
   at ICSharpCode.CodeConverter.CSharp.CommentConvertingNodesVisitor.DefaultVisit(SyntaxNode node)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.VisitQueryExpression(QueryExpressionSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.QueryExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at ICSharpCode.CodeConverter.CSharp.VisualBasicConverter.NodesVisitor.VisitParenthesizedExpression(ParenthesizedExpressionSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.ParenthesizedExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.Visit(SyntaxNode node)
   at ICSharpCode.CodeConverter.CSharp.CommentConvertingNodesVisitor.DefaultVisit(SyntaxNode node)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.VisitParenthesizedExpression(ParenthesizedExpressionSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.ParenthesizedExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at ICSharpCode.CodeConverter.CSharp.VisualBasicConverter.NodesVisitor.VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.MemberAccessExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.Visit(SyntaxNode node)
   at ICSharpCode.CodeConverter.CSharp.CommentConvertingNodesVisitor.DefaultVisit(SyntaxNode node)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.MemberAccessExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at ICSharpCode.CodeConverter.CSharp.VisualBasicConverter.NodesVisitor.VisitInvocationExpression(InvocationExpressionSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.InvocationExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.Visit(SyntaxNode node)
   at ICSharpCode.CodeConverter.CSharp.CommentConvertingNodesVisitor.DefaultVisit(SyntaxNode node)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.VisitInvocationExpression(InvocationExpressionSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.InvocationExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at ICSharpCode.CodeConverter.CSharp.CommonConversions.ConvertInitializer(VariableDeclaratorSyntax declarator)
   at ICSharpCode.CodeConverter.CSharp.CommonConversions.SplitVariableDeclarations(VariableDeclaratorSyntax declarator)
   at ICSharpCode.CodeConverter.CSharp.VisualBasicConverter.MethodBodyVisitor.VisitLocalDeclarationStatement(LocalDeclarationStatementSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.LocalDeclarationStatementSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.Visit(SyntaxNode node)
   at ICSharpCode.CodeConverter.CSharp.CommentConvertingMethodBodyVisitor.ConvertWithTrivia(SyntaxNode node)
   at ICSharpCode.CodeConverter.CSharp.CommentConvertingMethodBodyVisitor.DefaultVisit(SyntaxNode node)

Input: 
                ' lấy ORG hiện tại của nhân viên (lấy trong HU_WORKING)
                Dim currentWorking = (From T In Context.HU_ORGANIZATION
                                     From W In Context.HU_WORKING.Where(Function(p) p.EMPLOYEE_ID = employeeId AndAlso p.EFFECT_DATE <= Date.Now AndAlso (Not p.EXPIRE_DATE.HasValue OrElse (p.EXPIRE_DATE.HasValue AndAlso Date.Now <= p.EXPIRE_DATE.Value))) Order By W.EFFECT_DATE Select T, W).ToList()()()

 */
                if ((currentWorking != null))
                {
                    var OrgID = (from p in currentWorking
                                 select p).FirstOrDefault.T.ID;
                    var OrgPA = (from p in currentWorking
                                 select p).FirstOrDefault.T.PARENT_ID;

                    var currentOrgSetup = GetCurrentOrgSetup(OrgID, process);

                    if (currentOrgSetup != null)
                        _setup.Add(currentOrgSetup);

                    while (OrgPA.HasValue)
                    {
                        OrgID = Context.HU_ORGANIZATION.SingleOrDefault(p => p.ID == OrgPA).ID;
                        OrgPA = Context.HU_ORGANIZATION.SingleOrDefault(p => p.ID == OrgPA).PARENT_ID;
                        var OrgSetup = GetCurrentOrgSetup(OrgID, process);
                        if (OrgSetup != null)
                            _setup.Add(OrgSetup);
                    }
                }
            }
            return _setup;
        }
        catch (Exception ex)
        {
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod().Name, "iTime");
            throw ex;
        }
    }

    private SE_APP_SETUP GetCurrentOrgSetup(decimal orgId, SE_APP_PROCESS process)
    {
        try
        {
            var setupOrg = Context.SE_APP_SETUP.SingleOrDefault(p => p.ORG_ID.HasValue
                                                                  && p.ORG_ID == orgId
                                                                  && p.PROCESS_ID == process.ID
                                                                  && (p.FROM_DATE <= DateTime.Now
                                                                           && (!p.TO_DATE.HasValue
                                                                                    || (p.TO_DATE.HasValue
                                                                                            && DateTime.Now <= p.TO_DATE.Value))));

            // Nếu có thiết lập riêng cho nhân viên
            return setupOrg;
        }
        catch (Exception ex)
        {
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod().Name, "iTime");
            throw ex;
        }
    }

    private ApproveUserDTO GetEmployeeApprove(decimal employeeId, int level)
    {
        ;/* Cannot convert LocalDeclarationStatementSyntax, System.InvalidOperationException: Sequence contains more than one element
   at System.Linq.Enumerable.Single[TSource](IEnumerable`1 source)
   at ICSharpCode.CodeConverter.CSharp.VisualBasicConverter.NodesVisitor.VisitQueryExpression(QueryExpressionSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.QueryExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.Visit(SyntaxNode node)
   at ICSharpCode.CodeConverter.CSharp.CommentConvertingNodesVisitor.DefaultVisit(SyntaxNode node)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.VisitQueryExpression(QueryExpressionSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.QueryExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at ICSharpCode.CodeConverter.CSharp.VisualBasicConverter.NodesVisitor.VisitParenthesizedExpression(ParenthesizedExpressionSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.ParenthesizedExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.Visit(SyntaxNode node)
   at ICSharpCode.CodeConverter.CSharp.CommentConvertingNodesVisitor.DefaultVisit(SyntaxNode node)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.VisitParenthesizedExpression(ParenthesizedExpressionSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.ParenthesizedExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at ICSharpCode.CodeConverter.CSharp.VisualBasicConverter.NodesVisitor.VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.MemberAccessExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.Visit(SyntaxNode node)
   at ICSharpCode.CodeConverter.CSharp.CommentConvertingNodesVisitor.DefaultVisit(SyntaxNode node)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.MemberAccessExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at ICSharpCode.CodeConverter.CSharp.VisualBasicConverter.NodesVisitor.VisitInvocationExpression(InvocationExpressionSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.InvocationExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.Visit(SyntaxNode node)
   at ICSharpCode.CodeConverter.CSharp.CommentConvertingNodesVisitor.DefaultVisit(SyntaxNode node)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.VisitInvocationExpression(InvocationExpressionSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.InvocationExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at ICSharpCode.CodeConverter.CSharp.CommonConversions.ConvertInitializer(VariableDeclaratorSyntax declarator)
   at ICSharpCode.CodeConverter.CSharp.CommonConversions.SplitVariableDeclarations(VariableDeclaratorSyntax declarator)
   at ICSharpCode.CodeConverter.CSharp.VisualBasicConverter.MethodBodyVisitor.VisitLocalDeclarationStatement(LocalDeclarationStatementSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.LocalDeclarationStatementSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.Visit(SyntaxNode node)
   at ICSharpCode.CodeConverter.CSharp.CommentConvertingMethodBodyVisitor.ConvertWithTrivia(SyntaxNode node)
   at ICSharpCode.CodeConverter.CSharp.CommentConvertingMethodBodyVisitor.DefaultVisit(SyntaxNode node)

Input: 

        Dim approveUser = (From p In Context.HU_EMPLOYEE_CV
                           From e In Context.HU_EMPLOYEE.Where(Function(F) F.ID = p.EMPLOYEE_ID).DefaultIfEmpty
                           Where p.EMPLOYEE_ID = employeeId
                           Select New ApproveUserDTO With {.EMPLOYEE_ID = employeeId,
                                                           .EMPLOYEE_NAME = e.FULLNAME_VN,
                                                           .EMAIL = p.WORK_EMAIL,
                                                           .LEVEL = level
                                                          }).SingleOrDefault()

 */
        return approveUser;
    }

    private ApproveUserDTO GetDirectManagerApprove(decimal employeeId, int level)
    {
        try
        {
            var employee = Context.HU_EMPLOYEE.SingleOrDefault(p => p.ID == employeeId);
            if (employee.DIRECT_MANAGER.HasValue)
            {
                var approveUser = (from cv in Context.HU_EMPLOYEE_CV
                                   where cv.EMPLOYEE_ID == employee.DIRECT_MANAGER.Value
                                   select cv).FirstOrDefault;

                if (approveUser != null)
                    return new ApproveUserDTO()
                    {
                        EMPLOYEE_ID = approveUser.EMPLOYEE_ID,
                        EMPLOYEE_NAME = employee.FULLNAME_VN,
                        EMAIL = approveUser.WORK_EMAIL,
                        LEVEL = level
                    };
                else
                    return null/* TODO Change to default(_) if this is not a reference type */;
            }
            else
                return null/* TODO Change to default(_) if this is not a reference type */;
            return null/* TODO Change to default(_) if this is not a reference type */;
        }
        catch (Exception ex)
        {
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod().Name, "iTime");
            return null/* TODO Change to default(_) if this is not a reference type */;
        }
    }

    public Dictionary<string, string> GetConfig(ModuleID eModule)
    {
        using (SystemConfig config = new SystemConfig())
        {
            try
            {
                return config.GetConfig(eModule);
            }
            catch (Exception ex)
            {
                WriteExceptionLog(ex, MethodBase.GetCurrentMethod().Name, "iTime");
                throw ex;
            }
        }
    }

    private T GetObjectById<T>(decimal id) where T : EntityObject
    {
        string containerName = Context.DefaultContainerName;
        string setName = Context.CreateObjectSet<T>.EntitySet.Name;
        // Build entity key
        var entityKey = new EntityKey(containerName + "." + setName, "ID", id);
        return (EntityObject)Context.GetObjectByKey(entityKey);
    }

    private bool disposedValue; // To detect redundant calls
    // IDisposable
    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposedValue)
        {
            if (disposing)
            {
            }
        }
        this.disposedValue = true;
    }

    // TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    // Protected Overrides Sub Finalize()
    // ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    // Dispose(False)
    // MyBase.Finalize()
    // End Sub

    // This code added by Visual Basic to correctly implement the disposable pattern.
    public void Dispose()
    {
        // Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
