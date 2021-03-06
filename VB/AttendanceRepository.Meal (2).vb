Imports System.IO
Imports Framework.Data
Imports System.Data.Objects
Imports Framework.Data.System.Linq.Dynamic
Imports System.Reflection
Imports Framework.Data.DataAccess
Imports Oracle.DataAccess.Client

Partial Public Class AttendanceRepository

#Region "List"

    Public Function Get_KITCHEN(ByVal is_blank As Decimal) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_MEAL_LIST.GET_KITCHEN",
                                           New With {.P_IS_BLANK = is_blank,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function Get_KITCHEN_BY_EMP(ByVal is_blank As Decimal, ByVal employee_id As Decimal, ByVal Meal_ID As Decimal) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_MEAL_LIST.GET_KITCHEN_BY_EMP",
                                           New With {.P_IS_BLANK = is_blank,
                                                     .P_EMPLOYEE_ID = employee_id,
                                                     .P_CUR = cls.OUT_CURSOR,
                                                     .P_MEAL_ID = Meal_ID})

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function Get_KITCHEN_BY_STUDENT(ByVal is_blank As Decimal, ByVal student_id As Decimal, ByVal Meal_ID As Decimal) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_MEAL_LIST.GET_KITCHEN_BY_STUDENT",
                                           New With {.P_IS_BLANK = is_blank,
                                                     .P_STUDENT_ID = student_id,
                                                     .P_CUR = cls.OUT_CURSOR,
                                                     .P_MEAL_ID = Meal_ID})

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function Get_KITCHEN_BY_ORG(ByVal is_blank As Decimal, ByVal Meal_ID As Decimal, ByVal _param As ParamDTO, ByVal log As UserLog) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_MEAL_LIST.GET_KITCHEN_BY_ORG",
                                           New With {.P_IS_BLANK = is_blank,
                                                     .PV_ORG_ID = _param.ORG_ID,
                                                     .P_MEAL_ID = Meal_ID,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function Get_MEAL_BY_EMP_EFFECT(ByVal is_blank As Decimal,
                                           ByVal employee_id As Decimal,
                                           ByVal effectDate As Date) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_MEAL_LIST.GET_MEAL_BY_EMP_EFFECT",
                                           New With {.P_IS_BLANK = is_blank,
                                                     .P_EMPLOYEE_ID = employee_id,
                                                     .P_EFFECT_DATE = effectDate,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function Get_MEAL_BY_EMP(ByVal is_blank As Decimal,
                                    ByVal employee_id As Decimal) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_MEAL_LIST.GET_MEAL_BY_EMP",
                                           New With {.P_IS_BLANK = is_blank,
                                                     .P_EMPLOYEE_ID = employee_id,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function


    Public Function Get_MEAL_BY_ORG(ByVal is_blank As Decimal,
                                    ByVal org_id As Decimal) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_MEAL_LIST.GET_MEAL_BY_ORG",
                                           New With {.P_IS_BLANK = is_blank,
                                                     .P_ORGID = org_id,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function


#End Region

#Region "AT_KITCHEN"

    Public Function Insert_AT_KITCHEN(ByVal objData As AT_KITCHEN_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim obj As New AT_KITCHEN
        Try
            obj.ID = Utilities.GetNextSequence(Context, Context.AT_KITCHEN.EntitySet.Name)
            obj.KITCHEN_CODE = objData.KITCHEN_CODE
            obj.KITCHEN_NAME = objData.KITCHEN_NAME
            obj.REMARK = objData.REMARK
            obj.IS_BREAKFAST = objData.IS_BREAKFAST
            obj.BRE_FROM = objData.BRE_FROM
            obj.BRE_TO = objData.BRE_TO
            obj.IS_LUNCH = objData.IS_LUNCH
            obj.LUN_FROM = objData.LUN_FROM
            obj.LUN_TO = objData.LUN_TO
            obj.IS_DINNER = objData.IS_DINNER
            obj.DIN_FROM = objData.DIN_FROM
            obj.DIN_TO = objData.DIN_TO
            obj.IS_EXTRA1 = objData.IS_EXTRA1
            obj.EXTRA1_FROM = objData.EXTRA1_FROM
            obj.EXTRA1_TO = objData.EXTRA1_TO
            obj.IS_EXTRA2 = objData.IS_EXTRA2
            obj.EXTRA2_FROM = objData.EXTRA2_FROM
            obj.EXTRA2_TO = objData.EXTRA2_TO
            obj.IS_EXTRA3 = objData.IS_EXTRA3
            obj.EXTRA3_FROM = objData.EXTRA3_FROM
            obj.EXTRA3_TO = objData.EXTRA3_TO
            obj.KITCHEN_PROVISION = objData.KITCHEN_PROVISION
            Context.AT_KITCHEN.AddObject(obj)
            Context.SaveChanges(log)
            gID = obj.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iMeal")
            Throw ex
        End Try
    End Function

    Public Function Modify_AT_KITCHEN(ByVal objData As AT_KITCHEN_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim obj As New AT_KITCHEN With {.ID = objData.ID}
        Try
            obj = (From p In Context.AT_KITCHEN Where p.ID = objData.ID).FirstOrDefault
            obj.KITCHEN_CODE = objData.KITCHEN_CODE
            obj.KITCHEN_NAME = objData.KITCHEN_NAME
            obj.REMARK = objData.REMARK
            obj.IS_BREAKFAST = objData.IS_BREAKFAST
            obj.BRE_FROM = objData.BRE_FROM
            obj.BRE_TO = objData.BRE_TO
            obj.IS_LUNCH = objData.IS_LUNCH
            obj.LUN_FROM = objData.LUN_FROM
            obj.LUN_TO = objData.LUN_TO
            obj.IS_DINNER = objData.IS_DINNER
            obj.DIN_FROM = objData.DIN_FROM
            obj.DIN_TO = objData.DIN_TO
            obj.IS_EXTRA1 = objData.IS_EXTRA1
            obj.EXTRA1_FROM = objData.EXTRA1_FROM
            obj.EXTRA1_TO = objData.EXTRA1_TO
            obj.IS_EXTRA2 = objData.IS_EXTRA2
            obj.EXTRA2_FROM = objData.EXTRA2_FROM
            obj.EXTRA2_TO = objData.EXTRA2_TO
            obj.IS_EXTRA3 = objData.IS_EXTRA3
            obj.EXTRA3_FROM = objData.EXTRA3_FROM
            obj.EXTRA3_TO = objData.EXTRA3_TO
            obj.KITCHEN_PROVISION = objData.KITCHEN_PROVISION
            Context.SaveChanges(log)
            gID = obj.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iMeal")
            Throw ex
        End Try
    End Function

    Public Function Delete_AT_KITCHEN(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lst As List(Of AT_KITCHEN)
        Try
            lst = (From p In Context.AT_KITCHEN Where lstID.Contains(p.ID)).ToList
            For index = 0 To lst.Count - 1
                Context.AT_KITCHEN.DeleteObject(lst(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iMeal")
            Throw ex
        End Try
    End Function

    Public Function Get_AT_KITCHENById(ByVal _id As Decimal?) As AT_KITCHEN_DTO
        Try

            Dim query = From p In Context.AT_KITCHEN
                        Where p.ID = _id

            Dim lst = query.Select(Function(p) New AT_KITCHEN_DTO With {
                                         .ID = p.ID,
                                         .KITCHEN_CODE = p.KITCHEN_CODE,
                                         .KITCHEN_NAME = p.KITCHEN_NAME,
                                         .REMARK = p.REMARK,
                                         .IS_BREAKFAST = p.IS_BREAKFAST,
                                         .BRE_FROM = p.BRE_FROM,
                                         .BRE_TO = p.BRE_TO,
                                         .IS_LUNCH = p.IS_LUNCH,
                                         .LUN_FROM = p.LUN_FROM,
                                         .LUN_TO = p.LUN_TO,
                                         .IS_DINNER = p.IS_DINNER,
                                         .DIN_FROM = p.DIN_FROM,
                                         .DIN_TO = p.DIN_TO,
                                         .IS_EXTRA1 = p.IS_EXTRA1,
                                         .EXTRA1_FROM = p.EXTRA1_FROM,
                                         .EXTRA1_TO = p.EXTRA1_TO,
                                         .IS_EXTRA2 = p.IS_EXTRA2,
                                         .EXTRA2_FROM = p.EXTRA2_FROM,
                                         .EXTRA2_TO = p.EXTRA2_TO,
                                         .IS_EXTRA3 = p.IS_EXTRA3,
                                         .EXTRA3_FROM = p.EXTRA3_FROM,
                                         .EXTRA3_TO = p.EXTRA3_TO,
                                         .CREATED_DATE = p.CREATED_DATE,
                                         .CREATED_BY = p.CREATED_BY,
                                         .CREATED_LOG = p.CREATED_LOG,
                                         .MODIFIED_DATE = p.MODIFIED_DATE,
                                         .MODIFIED_BY = p.MODIFIED_BY,
                                         .KITCHEN_PROVISION = p.KITCHEN_PROVISION,
                                         .MODIFIED_LOG = p.MODIFIED_LOG}).FirstOrDefault
            Return lst
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iMeal")
            Throw ex
        End Try
    End Function

    Public Function Validate_AT_KITCHEN(ByVal obj As AT_KITCHEN_DTO,
                                        ByVal _action As String,
                                        Optional ByRef _error As String = "") As Boolean
        Try
            Select Case _action
                Case "Using"
                    Dim query = (From p In Context.AT_KITCHEN_ORG
                                Where p.KITCHEN_ID = obj.ID).Count
                    If query > 0 Then
                        Return False
                    End If
                    query = (From p In Context.AT_MEAL_MANAGER
                                Where p.KITCHEN_ID = obj.ID).Count
                    If query > 0 Then
                        Return False
                    End If
                    query = (From p In Context.AT_MEAL_CHANGE
                                Where p.KITCHEN_ID = obj.ID Or
                                p.KITCHEN_ID_NEW = obj.ID).Count
                    If query > 0 Then
                        Return False
                    End If
                    query = (From p In Context.AT_MEAL_PARTNER
                                Where p.KITCHEN_ID = obj.ID).Count
                    If query > 0 Then
                        Return False
                    End If
                    query = (From p In Context.AT_TERMINALS_MEAL
                                Where p.KITCHEN_ID = obj.ID).Count
                    If query > 0 Then
                        Return False
                    End If
                Case "ExistCode"
                    Dim query
                    If obj.ID IsNot Nothing Then
                        query = (From p In Context.AT_KITCHEN
                                Where (p.KITCHEN_CODE.ToUpper = obj.KITCHEN_CODE.ToUpper And
                                       p.ID <> obj.ID)).Count
                    Else
                        query = (From p In Context.AT_KITCHEN
                                Where (p.KITCHEN_CODE.ToUpper = obj.KITCHEN_CODE.ToUpper)).Count

                    End If

                    If query > 0 Then
                        Return False
                    End If
                Case "CheckTime"
                    If obj.IS_BREAKFAST.Value Then
                        If obj.IS_LUNCH.Value Then
                            If obj.BRE_FROM >= obj.LUN_FROM AndAlso obj.BRE_FROM <= obj.LUN_TO Then
                                Return False
                            End If
                            If obj.BRE_TO >= obj.LUN_FROM AndAlso obj.BRE_TO <= obj.LUN_TO Then
                                Return False
                            End If
                        End If
                        If obj.IS_DINNER.Value Then
                            If obj.BRE_FROM >= obj.DIN_FROM AndAlso obj.BRE_FROM <= obj.DIN_TO Then
                                Return False
                            End If
                            If obj.BRE_TO >= obj.DIN_FROM AndAlso obj.BRE_TO <= obj.DIN_TO Then
                                Return False
                            End If
                        End If
                        If obj.IS_EXTRA1.Value Then
                            If obj.BRE_FROM >= obj.EXTRA1_FROM AndAlso obj.BRE_FROM <= obj.EXTRA1_TO Then
                                Return False
                            End If
                            If obj.BRE_TO >= obj.EXTRA1_FROM AndAlso obj.BRE_TO <= obj.EXTRA1_TO Then
                                Return False
                            End If
                        End If
                        If obj.IS_EXTRA2.Value Then
                            If obj.BRE_FROM >= obj.EXTRA2_FROM AndAlso obj.BRE_FROM <= obj.EXTRA2_TO Then
                                Return False
                            End If
                            If obj.BRE_TO >= obj.EXTRA2_FROM AndAlso obj.BRE_TO <= obj.EXTRA2_TO Then
                                Return False
                            End If
                        End If
                        If obj.IS_EXTRA3.Value Then
                            If obj.BRE_FROM >= obj.EXTRA3_FROM AndAlso obj.BRE_FROM <= obj.EXTRA3_TO Then
                                Return False
                            End If
                            If obj.BRE_TO >= obj.EXTRA3_FROM AndAlso obj.BRE_TO <= obj.EXTRA3_TO Then
                                Return False
                            End If
                        End If
                    End If
                    If obj.IS_LUNCH.Value Then
                        If obj.IS_BREAKFAST.Value Then
                            If obj.LUN_FROM >= obj.BRE_FROM AndAlso obj.LUN_FROM <= obj.BRE_TO Then
                                Return False
                            End If
                            If obj.LUN_TO >= obj.BRE_FROM AndAlso obj.LUN_TO <= obj.BRE_TO Then
                                Return False
                            End If
                        End If
                        If obj.IS_DINNER.Value Then
                            If obj.LUN_FROM >= obj.DIN_FROM AndAlso obj.LUN_FROM <= obj.DIN_TO Then
                                Return False
                            End If
                            If obj.LUN_TO >= obj.DIN_FROM AndAlso obj.LUN_TO <= obj.DIN_TO Then
                                Return False
                            End If
                        End If
                        If obj.IS_EXTRA1.Value Then
                            If obj.LUN_FROM >= obj.EXTRA1_FROM AndAlso obj.LUN_FROM <= obj.EXTRA1_TO Then
                                Return False
                            End If
                            If obj.LUN_TO >= obj.EXTRA1_FROM AndAlso obj.LUN_TO <= obj.EXTRA1_TO Then
                                Return False
                            End If
                        End If
                        If obj.IS_EXTRA2.Value Then
                            If obj.LUN_FROM >= obj.EXTRA2_FROM AndAlso obj.LUN_FROM <= obj.EXTRA2_TO Then
                                Return False
                            End If
                            If obj.LUN_TO >= obj.EXTRA2_FROM AndAlso obj.LUN_TO <= obj.EXTRA2_TO Then
                                Return False
                            End If
                        End If
                        If obj.IS_EXTRA3.Value Then
                            If obj.LUN_FROM >= obj.EXTRA3_FROM AndAlso obj.LUN_FROM <= obj.EXTRA3_TO Then
                                Return False
                            End If
                            If obj.LUN_TO >= obj.EXTRA3_FROM AndAlso obj.LUN_TO <= obj.EXTRA3_TO Then
                                Return False
                            End If
                        End If
                    End If
                    If obj.IS_DINNER.Value Then
                        If obj.IS_BREAKFAST.Value Then
                            If obj.DIN_FROM >= obj.BRE_FROM AndAlso obj.DIN_FROM <= obj.BRE_TO Then
                                Return False
                            End If
                            If obj.DIN_TO >= obj.BRE_FROM AndAlso obj.DIN_TO <= obj.BRE_TO Then
                                Return False
                            End If
                        End If
                        If obj.IS_LUNCH.Value Then
                            If obj.DIN_FROM >= obj.LUN_FROM AndAlso obj.DIN_FROM <= obj.LUN_TO Then
                                Return False
                            End If
                            If obj.DIN_TO >= obj.LUN_FROM AndAlso obj.DIN_TO <= obj.LUN_TO Then
                                Return False
                            End If
                        End If
                        If obj.IS_EXTRA1.Value Then
                            If obj.DIN_FROM >= obj.EXTRA1_FROM AndAlso obj.DIN_FROM <= obj.EXTRA1_TO Then
                                Return False
                            End If
                            If obj.DIN_TO >= obj.EXTRA1_FROM AndAlso obj.DIN_TO <= obj.EXTRA1_TO Then
                                Return False
                            End If
                        End If
                        If obj.IS_EXTRA2.Value Then
                            If obj.DIN_FROM >= obj.EXTRA2_FROM AndAlso obj.DIN_FROM <= obj.EXTRA2_TO Then
                                Return False
                            End If
                            If obj.DIN_TO >= obj.EXTRA2_FROM AndAlso obj.DIN_TO <= obj.EXTRA2_TO Then
                                Return False
                            End If
                        End If
                        If obj.IS_EXTRA3.Value Then
                            If obj.DIN_FROM >= obj.EXTRA3_FROM AndAlso obj.DIN_FROM <= obj.EXTRA3_TO Then
                                Return False
                            End If
                            If obj.DIN_TO >= obj.EXTRA3_FROM AndAlso obj.DIN_TO <= obj.EXTRA3_TO Then
                                Return False
                            End If
                        End If
                    End If
                    If obj.IS_EXTRA1.Value Then
                        If obj.IS_BREAKFAST.Value Then
                            If obj.EXTRA1_FROM >= obj.BRE_FROM AndAlso obj.EXTRA1_FROM <= obj.BRE_TO Then
                                Return False
                            End If
                            If obj.EXTRA1_TO >= obj.BRE_FROM AndAlso obj.EXTRA1_TO <= obj.BRE_TO Then
                                Return False
                            End If
                        End If
                        If obj.IS_LUNCH.Value Then
                            If obj.EXTRA1_FROM >= obj.LUN_FROM AndAlso obj.EXTRA1_FROM <= obj.LUN_TO Then
                                Return False
                            End If
                            If obj.EXTRA1_TO >= obj.LUN_FROM AndAlso obj.EXTRA1_TO <= obj.LUN_TO Then
                                Return False
                            End If
                        End If
                        If obj.IS_DINNER.Value Then
                            If obj.EXTRA1_FROM >= obj.DIN_FROM AndAlso obj.EXTRA1_FROM <= obj.DIN_TO Then
                                Return False
                            End If
                            If obj.EXTRA1_TO >= obj.DIN_FROM AndAlso obj.EXTRA1_TO <= obj.DIN_TO Then
                                Return False
                            End If
                        End If
                        If obj.IS_EXTRA2.Value Then
                            If obj.EXTRA1_FROM >= obj.EXTRA2_FROM AndAlso obj.EXTRA1_FROM <= obj.EXTRA2_TO Then
                                Return False
                            End If
                            If obj.EXTRA1_TO >= obj.EXTRA2_FROM AndAlso obj.EXTRA1_TO <= obj.EXTRA2_TO Then
                                Return False
                            End If
                        End If
                        If obj.IS_EXTRA3.Value Then
                            If obj.EXTRA1_FROM >= obj.EXTRA3_FROM AndAlso obj.EXTRA1_FROM <= obj.EXTRA3_TO Then
                                Return False
                            End If
                            If obj.EXTRA1_TO >= obj.EXTRA3_FROM AndAlso obj.EXTRA1_TO <= obj.EXTRA3_TO Then
                                Return False
                            End If
                        End If
                    End If
                    If obj.IS_EXTRA2.Value Then
                        If obj.IS_BREAKFAST.Value Then
                            If obj.EXTRA2_FROM >= obj.BRE_FROM AndAlso obj.EXTRA2_FROM <= obj.BRE_TO Then
                                Return False
                            End If
                            If obj.EXTRA2_TO >= obj.BRE_FROM AndAlso obj.EXTRA2_TO <= obj.BRE_TO Then
                                Return False
                            End If
                        End If
                        If obj.IS_LUNCH.Value Then
                            If obj.EXTRA2_FROM >= obj.LUN_FROM AndAlso obj.EXTRA2_FROM <= obj.LUN_TO Then
                                Return False
                            End If
                            If obj.EXTRA2_TO >= obj.LUN_FROM AndAlso obj.EXTRA2_TO <= obj.LUN_TO Then
                                Return False
                            End If
                        End If
                        If obj.IS_DINNER.Value Then
                            If obj.EXTRA2_FROM >= obj.DIN_FROM AndAlso obj.EXTRA2_FROM <= obj.DIN_TO Then
                                Return False
                            End If
                            If obj.EXTRA2_TO >= obj.DIN_FROM AndAlso obj.EXTRA2_TO <= obj.DIN_TO Then
                                Return False
                            End If
                        End If
                        If obj.IS_EXTRA1.Value Then
                            If obj.EXTRA2_FROM >= obj.EXTRA1_FROM AndAlso obj.EXTRA2_FROM <= obj.EXTRA1_TO Then
                                Return False
                            End If
                            If obj.EXTRA2_TO >= obj.EXTRA1_FROM AndAlso obj.EXTRA2_TO <= obj.EXTRA1_TO Then
                                Return False
                            End If
                        End If
                        If obj.IS_EXTRA3.Value Then
                            If obj.EXTRA2_FROM >= obj.EXTRA3_FROM AndAlso obj.EXTRA2_FROM <= obj.EXTRA3_TO Then
                                Return False
                            End If
                            If obj.EXTRA2_TO >= obj.EXTRA3_FROM AndAlso obj.EXTRA2_TO <= obj.EXTRA3_TO Then
                                Return False
                            End If
                        End If
                    End If
                    If obj.IS_EXTRA3.Value Then
                        If obj.IS_BREAKFAST.Value Then
                            If obj.EXTRA3_FROM >= obj.BRE_FROM AndAlso obj.EXTRA3_FROM <= obj.BRE_TO Then
                                Return False
                            End If
                            If obj.EXTRA3_TO >= obj.BRE_FROM AndAlso obj.EXTRA3_TO <= obj.BRE_TO Then
                                Return False
                            End If
                        End If
                        If obj.IS_LUNCH.Value Then
                            If obj.EXTRA3_FROM >= obj.LUN_FROM AndAlso obj.EXTRA3_FROM <= obj.LUN_TO Then
                                Return False
                            End If
                            If obj.EXTRA3_TO >= obj.LUN_FROM AndAlso obj.EXTRA3_TO <= obj.LUN_TO Then
                                Return False
                            End If
                        End If
                        If obj.IS_DINNER.Value Then
                            If obj.EXTRA3_FROM >= obj.DIN_FROM AndAlso obj.EXTRA3_FROM <= obj.DIN_TO Then
                                Return False
                            End If
                            If obj.EXTRA3_TO >= obj.DIN_FROM AndAlso obj.EXTRA3_TO <= obj.DIN_TO Then
                                Return False
                            End If
                        End If
                        If obj.IS_EXTRA3.Value Then
                            If obj.EXTRA1_FROM >= obj.EXTRA3_FROM AndAlso obj.EXTRA1_FROM <= obj.EXTRA3_TO Then
                                Return False
                            End If
                            If obj.EXTRA1_TO >= obj.EXTRA3_FROM AndAlso obj.EXTRA1_TO <= obj.EXTRA3_TO Then
                                Return False
                            End If
                        End If
                        If obj.IS_EXTRA1.Value Then
                            If obj.EXTRA3_FROM >= obj.EXTRA1_FROM AndAlso obj.EXTRA3_FROM <= obj.EXTRA1_TO Then
                                Return False
                            End If
                            If obj.EXTRA3_TO >= obj.EXTRA1_FROM AndAlso obj.EXTRA3_TO <= obj.EXTRA1_TO Then
                                Return False
                            End If
                        End If
                    End If
                    Return True
            End Select
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iMeal")
            Throw ex
        End Try
    End Function

    Public Function Get_AT_KITCHEN(ByVal _filter As AT_KITCHEN_DTO,
                                           Optional ByVal PageIndex As Integer = 0,
                                           Optional ByVal PageSize As Integer = Integer.MaxValue,
                                           Optional ByRef Total As Integer = 0,
                                           Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_KITCHEN_DTO)
        Try

            Dim query = From p In Context.AT_KITCHEN

            Dim lst = query.Select(Function(p) New AT_KITCHEN_DTO With {
                                        .ID = p.ID,
                                        .KITCHEN_CODE = p.KITCHEN_CODE,
                                        .KITCHEN_NAME = p.KITCHEN_NAME,
                                        .REMARK = p.REMARK,
                                        .IS_BREAKFAST = p.IS_BREAKFAST,
                                        .BRE_FROM = p.BRE_FROM,
                                        .BRE_TO = p.BRE_TO,
                                        .IS_LUNCH = p.IS_LUNCH,
                                        .LUN_FROM = p.LUN_FROM,
                                        .LUN_TO = p.LUN_TO,
                                        .IS_DINNER = p.IS_DINNER,
                                        .DIN_FROM = p.DIN_FROM,
                                        .DIN_TO = p.DIN_TO,
                                        .KITCHEN_PROVISION = p.KITCHEN_PROVISION,
                                        .IS_EXTRA1 = p.IS_EXTRA1,
                                         .EXTRA1_FROM = p.EXTRA1_FROM,
                                         .EXTRA1_TO = p.EXTRA1_TO,
                                         .IS_EXTRA2 = p.IS_EXTRA2,
                                         .EXTRA2_FROM = p.EXTRA2_FROM,
                                         .EXTRA2_TO = p.EXTRA2_TO,
                                         .IS_EXTRA3 = p.IS_EXTRA3,
                                         .EXTRA3_FROM = p.EXTRA3_FROM,
                                         .EXTRA3_TO = p.EXTRA3_TO,
                                        .CREATED_DATE = p.CREATED_DATE,
                                        .CREATED_BY = p.CREATED_BY,
                                        .CREATED_LOG = p.CREATED_LOG,
                                        .MODIFIED_DATE = p.MODIFIED_DATE,
                                        .MODIFIED_BY = p.MODIFIED_BY,
                                        .MODIFIED_LOG = p.MODIFIED_LOG})
            If Not String.IsNullOrEmpty(_filter.KITCHEN_CODE) Then
                lst = lst.Where(Function(f) f.KITCHEN_CODE.ToLower().Contains(_filter.KITCHEN_CODE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.KITCHEN_NAME) Then
                lst = lst.Where(Function(f) f.KITCHEN_NAME.ToLower().Contains(_filter.KITCHEN_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.REMARK) Then
                lst = lst.Where(Function(f) f.REMARK.ToLower().Contains(_filter.REMARK.ToLower()))
            End If
            If _filter.IS_BREAKFAST.HasValue Then
                lst = lst.Where(Function(f) f.IS_BREAKFAST = _filter.IS_BREAKFAST)
            End If
            If _filter.BRE_FROM.HasValue Then
                lst = lst.Where(Function(f) f.BRE_FROM = _filter.BRE_FROM)
            End If
            If _filter.BRE_TO.HasValue Then
                lst = lst.Where(Function(f) f.BRE_TO = _filter.BRE_TO)
            End If
            If _filter.IS_LUNCH.HasValue Then
                lst = lst.Where(Function(f) f.IS_LUNCH = _filter.IS_LUNCH)
            End If
            If _filter.LUN_FROM.HasValue Then
                lst = lst.Where(Function(f) f.LUN_FROM = _filter.LUN_FROM)
            End If
            If _filter.LUN_TO.HasValue Then
                lst = lst.Where(Function(f) f.LUN_TO = _filter.LUN_TO)
            End If
            If _filter.IS_DINNER.HasValue Then
                lst = lst.Where(Function(f) f.IS_DINNER = _filter.IS_DINNER)
            End If
            If _filter.DIN_FROM.HasValue Then
                lst = lst.Where(Function(f) f.DIN_FROM = _filter.DIN_FROM)
            End If
            If _filter.DIN_TO.HasValue Then
                lst = lst.Where(Function(f) f.DIN_TO = _filter.DIN_TO)
            End If
            If _filter.IS_EXTRA1.HasValue Then
                lst = lst.Where(Function(f) f.IS_EXTRA1 = _filter.IS_EXTRA1)
            End If
            If _filter.EXTRA1_FROM.HasValue Then
                lst = lst.Where(Function(f) f.EXTRA1_FROM = _filter.EXTRA1_FROM)
            End If
            If _filter.EXTRA1_TO.HasValue Then
                lst = lst.Where(Function(f) f.EXTRA1_TO = _filter.EXTRA1_TO)
            End If
            If _filter.IS_EXTRA2.HasValue Then
                lst = lst.Where(Function(f) f.IS_EXTRA2 = _filter.IS_EXTRA2)
            End If
            If _filter.EXTRA2_FROM.HasValue Then
                lst = lst.Where(Function(f) f.EXTRA2_FROM = _filter.EXTRA2_FROM)
            End If
            If _filter.EXTRA2_TO.HasValue Then
                lst = lst.Where(Function(f) f.EXTRA2_TO = _filter.EXTRA2_TO)
            End If

            If _filter.IS_EXTRA3.HasValue Then
                lst = lst.Where(Function(f) f.IS_EXTRA3 = _filter.IS_EXTRA3)
            End If
            If _filter.EXTRA3_FROM.HasValue Then
                lst = lst.Where(Function(f) f.EXTRA3_FROM = _filter.EXTRA3_FROM)
            End If
            If _filter.EXTRA3_TO.HasValue Then
                lst = lst.Where(Function(f) f.EXTRA3_TO = _filter.EXTRA3_TO)
            End If
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Return lst.ToList()
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iMeal")
            Throw ex
        End Try
    End Function

    Public Function CHECK_TIME_BY_EMP(ByVal Employee_ID As Decimal, ByVal Effect_date As Date) As Boolean

        Try
            Dim query = From o In Context.AT_MEAL_SETUP
                        From a In Context.ATV_KITCHEN_ORG.Where(Function(f) f.ORG_MEAL_SETUP_ID = o.ORG_ID)
                         From e In Context.HU_EMPLOYEE.Where(Function(f) f.ORG_ID = a.ID)
                         Where e.ID = Employee_ID
            Dim obj = query.Select(Function(p) New AT_MEAL_SETUP_DTO With {
                                                                         .ID = p.o.ID,
                                                                         .TYPE_ID = p.o.TYPE_ID,
                                                                         .TIME_NUMBER = p.o.TIME_NUMBER}).FirstOrDefault
            If obj IsNot Nothing Then
                Dim CompareDate As New Date
                If obj.TYPE_ID = 7575 Then
                    Dim day = obj.TIME_NUMBER \ 24
                    Dim hours = obj.TIME_NUMBER Mod 24
                    CompareDate = Date.Now.AddDays(day)
                    CompareDate = CompareDate.AddHours(hours)
                Else
                    CompareDate = Date.Now.AddDays(obj.TIME_NUMBER)
                End If
                If Date.Compare(CompareDate, Effect_date) >= 0 Then
                    Return False
                End If
                Dim datenow = Date.Now
                If Date.Compare(datenow.Date, Effect_date) >= 0 Then
                    Return False
                End If
            Else
                Dim datenow = Date.Now
                If Date.Compare(datenow.Date, Effect_date) >= 0 Then
                    Return False
                End If
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iMeal")
            Throw ex
        End Try
    End Function

    Public Function CHECK_TIME_BY_STUDENT(ByVal STUDENT_ID As Decimal, ByVal Effect_date As Date) As Boolean

        Try
            Dim query = From o In Context.AT_MEAL_SETUP
                        From a In Context.ATV_KITCHEN_ORG.Where(Function(f) f.ORG_MEAL_SETUP_ID = o.ORG_ID)
                         From e In Context.HU_STUDENT.Where(Function(f) f.ORG_ID = a.ID)
                         Where e.ID = STUDENT_ID

            Dim obj = query.Select(Function(p) New AT_MEAL_SETUP_DTO With {.ID = p.o.ID,
                                                                         .TYPE_ID = p.o.TYPE_ID,
                                                                         .TIME_NUMBER = p.o.TIME_NUMBER}).FirstOrDefault
            If obj IsNot Nothing Then
                Dim CompareDate As New Date
                If obj.TYPE_ID = 7575 Then
                    Dim day = obj.TIME_NUMBER \ 24
                    Dim hours = obj.TIME_NUMBER Mod 24
                    CompareDate = Date.Now.AddDays(day)
                    CompareDate = CompareDate.AddHours(hours)
                Else
                    CompareDate = Date.Now.AddDays(obj.TIME_NUMBER)
                End If
                If Date.Compare(CompareDate, Effect_date) >= 0 Then
                    Return False
                End If
                Dim datenow = Date.Now
                If Date.Compare(datenow.Date, Effect_date) >= 0 Then
                    Return False
                End If
            Else
                Dim datenow = Date.Now
                If Date.Compare(datenow.Date, Effect_date) >= 0 Then
                    Return False
                End If
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iMeal")
            Throw ex
        End Try
    End Function


    Public Function CHECK_TIME_BY_ORG(ByVal Org_id As Decimal, ByVal Effect_date As Date) As Boolean

        Try
            Dim query = From o In Context.AT_MEAL_SETUP
                        From a In Context.ATV_KITCHEN_ORG.Where(Function(f) f.ORG_MEAL_SETUP_ID = o.ORG_ID)
                        Where a.ID = Org_id
            Dim obj = query.Select(Function(p) New AT_MEAL_SETUP_DTO With {
                                                                         .ID = p.o.ID,
                                                                         .TYPE_ID = p.o.TYPE_ID,
                                                                         .TIME_NUMBER = p.o.TIME_NUMBER}).FirstOrDefault
            If obj IsNot Nothing Then
                Dim CompareDate As New Date
                If obj.TYPE_ID = 7575 Then
                    Dim day = obj.TIME_NUMBER \ 24
                    Dim hours = obj.TIME_NUMBER Mod 24
                    CompareDate = Date.Now.AddDays(day)
                    CompareDate = CompareDate.AddHours(hours)
                Else
                    CompareDate = Date.Now.AddDays(obj.TIME_NUMBER)
                End If
                If Date.Compare(CompareDate, Effect_date) >= 0 Then
                    Return False
                End If
                Dim datenow = Date.Now
                If Date.Compare(datenow.Date, Effect_date) >= 0 Then
                    Return False
                End If
            Else
                Dim datenow = Date.Now
                If Date.Compare(datenow.Date, Effect_date) >= 0 Then
                    Return False
                End If
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iMeal")
            Throw ex
        End Try
    End Function

#End Region

#Region "AT_TERMINALS_MEAL"
    Public Function GetAT_TERMINAL_MEAL(ByVal _filter As AT_TERMINALS_MEALDTO,
                                 Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                   Optional ByVal Sorts As String = "CREATED_DATE desc",
                                   Optional ByVal log As UserLog = Nothing) As List(Of AT_TERMINALS_MEALDTO)
        Try
            Dim query = From p In Context.AT_TERMINALS_MEAL
                        From k In Context.AT_KITCHEN.Where(Function(f) f.ID = p.KITCHEN_ID).DefaultIfEmpty
                        Select New AT_TERMINALS_MEALDTO With {
                                       .ID = p.ID,
                                       .TERMINAL_CODE = p.TERMINAL_CODE,
                                       .TERMINAL_NAME = p.TERMINAL_NAME,
                                       .ADDRESS_PLACE = p.ADDRESS_PLACE,
                                       .TERMINAL_IP = p.TERMINAL_IP,
                                       .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng Áp dụng"),
                                       .NOTE = p.NOTE,
                                       .PASS = p.PASS,
                                       .PORT = p.PORT,
                                       .KITCHEN_ID = p.KITCHEN_ID,
                                       .KITCHEN_CODE = k.KITCHEN_CODE,
                                       .KITCHEN_NAME = k.KITCHEN_NAME,
                                       .CREATED_DATE = p.CREATED_DATE,
                                       .CREATED_BY = p.CREATED_BY,
                                       .CREATED_LOG = p.CREATED_LOG,
                                       .MODIFIED_DATE = p.MODIFIED_DATE,
                                       .MODIFIED_BY = p.MODIFIED_BY,
                                       .MODIFIED_LOG = p.MODIFIED_LOG}

            Dim lst = query

            If Not String.IsNullOrEmpty(_filter.TERMINAL_CODE) Then
                lst = lst.Where(Function(f) f.TERMINAL_CODE.ToLower().Contains(_filter.TERMINAL_CODE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TERMINAL_NAME) Then
                lst = lst.Where(Function(f) f.TERMINAL_NAME.ToLower().Contains(_filter.TERMINAL_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TERMINAL_IP) Then
                lst = lst.Where(Function(f) f.TERMINAL_IP.ToLower().Contains(_filter.TERMINAL_IP.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ACTFLG) Then
                lst = lst.Where(Function(f) f.ACTFLG.ToLower().Contains(_filter.ACTFLG.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.NOTE) Then
                lst = lst.Where(Function(f) f.NOTE.ToLower().Contains(_filter.NOTE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ADDRESS_PLACE) Then
                lst = lst.Where(Function(f) f.ADDRESS_PLACE.ToLower().Contains(_filter.ADDRESS_PLACE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.PASS) Then
                lst = lst.Where(Function(f) f.PASS.ToLower().Contains(_filter.PASS.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.PORT) Then
                lst = lst.Where(Function(f) f.PORT.ToLower().Contains(_filter.PORT.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.KITCHEN_CODE) Then
                lst = lst.Where(Function(f) f.KITCHEN_CODE.ToLower().Contains(_filter.KITCHEN_CODE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.KITCHEN_NAME) Then
                lst = lst.Where(Function(f) f.KITCHEN_NAME.ToLower().Contains(_filter.KITCHEN_NAME.ToLower()))
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iAttendance")
            Throw ex
        End Try
    End Function

    Public Function GetAT_TERMINAL_MEAL_STATUS(ByVal _filter As AT_TERMINALS_MEALDTO,
                                 Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                   Optional ByVal Sorts As String = "CREATED_DATE desc",
                                   Optional ByVal log As UserLog = Nothing) As List(Of AT_TERMINALS_MEALDTO)
        Try

            Dim query = From p In Context.AT_TERMINALS_MEAL
                        From k In Context.AT_KITCHEN.Where(Function(f) f.ID = p.KITCHEN_ID).DefaultIfEmpty
                        Where p.ACTFLG = "A"
                        Select New AT_TERMINALS_MEALDTO With {
                                       .ID = p.ID,
                                       .TERMINAL_CODE = p.TERMINAL_CODE,
                                       .TERMINAL_NAME = p.TERMINAL_NAME,
                                       .ADDRESS_PLACE = p.ADDRESS_PLACE,
                                       .TERMINAL_IP = p.TERMINAL_IP,
                                       .TERMINAL_STATUS = p.TERMINAL_STATUS,
                                       .LAST_TIME_STATUS = p.LAST_TIME_STATUS,
                                       .LAST_TIME_UPDATE = p.LAST_TIME_UPDATE,
                                       .TERMINAL_ROW = p.TERMINAL_ROW,
                                       .NOTE = p.NOTE,
                                       .PASS = p.PASS,
                                       .PORT = p.PORT,
                                       .KITCHEN_ID = p.KITCHEN_ID,
                                       .KITCHEN_CODE = k.KITCHEN_CODE,
                                       .KITCHEN_NAME = k.KITCHEN_NAME,
                                       .CREATED_DATE = p.CREATED_DATE}

            Dim lst = query

            If Not String.IsNullOrEmpty(_filter.TERMINAL_CODE) Then
                lst = lst.Where(Function(f) f.TERMINAL_CODE.ToLower().Contains(_filter.TERMINAL_CODE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TERMINAL_STATUS) Then
                lst = lst.Where(Function(f) f.TERMINAL_STATUS.ToLower().Contains(_filter.TERMINAL_STATUS.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TERMINAL_NAME) Then
                lst = lst.Where(Function(f) f.TERMINAL_NAME.ToLower().Contains(_filter.TERMINAL_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TERMINAL_IP) Then
                lst = lst.Where(Function(f) f.TERMINAL_IP.ToLower().Contains(_filter.TERMINAL_IP.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ACTFLG) Then
                lst = lst.Where(Function(f) f.ACTFLG.ToLower().Contains(_filter.ACTFLG.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.NOTE) Then
                lst = lst.Where(Function(f) f.NOTE.ToLower().Contains(_filter.NOTE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ADDRESS_PLACE) Then
                lst = lst.Where(Function(f) f.ADDRESS_PLACE.ToLower().Contains(_filter.ADDRESS_PLACE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.PASS) Then
                lst = lst.Where(Function(f) f.PASS.ToLower().Contains(_filter.PASS.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.PORT) Then
                lst = lst.Where(Function(f) f.PORT.ToLower().Contains(_filter.PORT.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TERMINAL_STATUS) Then
                lst = lst.Where(Function(f) f.TERMINAL_STATUS.ToLower().Contains(_filter.TERMINAL_STATUS.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.KITCHEN_CODE) Then
                lst = lst.Where(Function(f) f.KITCHEN_CODE.ToLower().Contains(_filter.KITCHEN_CODE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.KITCHEN_NAME) Then
                lst = lst.Where(Function(f) f.KITCHEN_NAME.ToLower().Contains(_filter.KITCHEN_NAME.ToLower()))
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iAttendance")
            Throw ex
        End Try
    End Function

    Public Function InsertAT_TERMINAL_MEAL(ByVal objTitle As AT_TERMINALS_MEALDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New AT_TERMINALS_MEAL
        Dim iCount As Integer = 0
        Try
            objTitleData.ID = Utilities.GetNextSequence(Context, Context.AT_TERMINALS_MEAL.EntitySet.Name)
            objTitleData.TERMINAL_CODE = objTitle.TERMINAL_CODE
            objTitleData.TERMINAL_NAME = objTitle.TERMINAL_NAME
            objTitleData.ADDRESS_PLACE = objTitle.ADDRESS_PLACE
            objTitleData.TERMINAL_IP = objTitle.TERMINAL_IP
            objTitleData.ACTFLG = objTitle.ACTFLG
            objTitleData.NOTE = objTitle.NOTE
            objTitleData.PASS = objTitle.PASS
            objTitleData.PORT = objTitle.PORT
            objTitleData.KITCHEN_ID = objTitle.KITCHEN_ID
            Context.AT_TERMINALS_MEAL.AddObject(objTitleData)
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iAttendance")
            Throw ex
        End Try

    End Function

    Public Function ValidateAT_TERMINAL_MEAL(ByVal _validate As AT_TERMINALS_MEALDTO, ByVal sAction As String)
        Dim query
        Try
            Select Case sAction
                Case "ExistCode"
                    If _validate.TERMINAL_CODE <> Nothing Then
                        If _validate.ID <> 0 Then
                            query = (From p In Context.AT_TERMINALS_MEAL
                                     Where p.TERMINAL_CODE.ToUpper = _validate.TERMINAL_CODE.ToUpper _
                                     And p.ID <> _validate.ID).FirstOrDefault
                        Else
                            query = (From p In Context.AT_TERMINALS_MEAL
                                     Where p.TERMINAL_CODE.ToUpper = _validate.TERMINAL_CODE.ToUpper).FirstOrDefault
                        End If
                        Return (query Is Nothing)
                    End If
                Case "Using"
                    query = (From p In Context.AT_SWIPE_DATA_MEAL
                             Where _validate.lstID.Contains(p.TERMINAL_ID)).FirstOrDefault

                    Return (query Is Nothing)
            End Select
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iAttendance")
            Throw ex
        End Try
    End Function

    Public Function ModifyAT_TERMINAL_MEAL(ByVal objTitle As AT_TERMINALS_MEALDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New AT_TERMINALS_MEAL With {.ID = objTitle.ID}
        Try
            objTitleData = (From p In Context.AT_TERMINALS_MEAL Where p.ID = objTitleData.ID).SingleOrDefault
            objTitleData.TERMINAL_CODE = objTitle.TERMINAL_CODE
            objTitleData.TERMINAL_NAME = objTitle.TERMINAL_NAME
            objTitleData.ADDRESS_PLACE = objTitle.ADDRESS_PLACE
            objTitleData.TERMINAL_IP = objTitle.TERMINAL_IP
            objTitleData.NOTE = objTitle.NOTE
            objTitleData.PASS = objTitle.PASS
            objTitleData.PORT = objTitle.PORT
            objTitleData.KITCHEN_ID = objTitle.KITCHEN_ID
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iAttendance")
            Throw ex
        End Try
    End Function

    Public Function ActiveAT_TERMINAL_MEAL(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstData As List(Of AT_TERMINALS_MEAL)
        Try
            lstData = (From p In Context.AT_TERMINALS_MEAL Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                lstData(index).ACTFLG = bActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iAttendance")
            Throw ex
        End Try
    End Function

    Public Function DeleteAT_TERMINAL_MEAL(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstAT_TERMINAL_MEAL As List(Of AT_TERMINALS_MEAL)
        Try
            lstAT_TERMINAL_MEAL = (From p In Context.AT_TERMINALS_MEAL Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstAT_TERMINAL_MEAL.Count - 1
                Context.AT_TERMINALS_MEAL.DeleteObject(lstAT_TERMINAL_MEAL(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iAttendance")
            Throw ex
        End Try
    End Function
#End Region

#Region "AT_MEAL_SETUP"

    Public Function Modify_AT_MEAL_SETUP(ByVal objData As AT_MEAL_SETUP_DTO,
                                         ByVal log As UserLog) As Boolean

        Try

            Dim query = (From p In Context.AT_MEAL_SETUP
                         Where p.ORG_ID = objData.ORG_ID).FirstOrDefault

            If query Is Nothing Then
                query = New AT_MEAL_SETUP
                query.ID = Utilities.GetNextSequence(Context, Context.AT_MEAL_SETUP.EntitySet.Name)
                query.ORG_ID = objData.ORG_ID
                query.TYPE_ID = objData.TYPE_ID
                query.TIME_NUMBER = objData.TIME_NUMBER
                query.REMARK = objData.REMARK
                Context.AT_MEAL_SETUP.AddObject(query)
            Else
                query.TYPE_ID = objData.TYPE_ID
                query.TIME_NUMBER = objData.TIME_NUMBER
                query.REMARK = objData.REMARK
            End If
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iAttendance")
            Throw ex
        End Try
    End Function

    Public Function Delete_AT_MEAL_SETUP(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lst As List(Of AT_MEAL_SETUP)
        Try
            lst = (From p In Context.AT_MEAL_SETUP Where lstID.Contains(p.ID)).ToList
            For index = 0 To lst.Count - 1
                Context.AT_MEAL_SETUP.DeleteObject(lst(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iAttendance")
            Throw ex
        End Try
    End Function

    Public Function Get_AT_MEAL_SETUPById(ByVal _id As Decimal?) As AT_MEAL_SETUP_DTO
        Try

            Dim query = From p In Context.AT_MEAL_SETUP
                        Where p.ID = _id

            Dim lst = query.Select(Function(p) New AT_MEAL_SETUP_DTO With {
                                         .ID = p.ID,
                                         .ORG_ID = p.ORG_ID,
                                         .TYPE_ID = p.TYPE_ID,
                                         .TIME_NUMBER = p.TIME_NUMBER,
                                         .REMARK = p.REMARK,
                                         .CREATED_DATE = p.CREATED_DATE,
                                         .CREATED_BY = p.CREATED_BY,
                                         .CREATED_LOG = p.CREATED_LOG,
                                         .MODIFIED_DATE = p.MODIFIED_DATE,
                                         .MODIFIED_BY = p.MODIFIED_BY,
                                         .MODIFIED_LOG = p.MODIFIED_LOG}).FirstOrDefault
            Return lst
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iAttendance")
            Throw ex
        End Try
    End Function

    Public Function Get_AT_MEAL_SETUP(ByVal _filter As AT_MEAL_SETUP_DTO,
                                           Optional ByVal _param As ParamDTO = Nothing,
                                           Optional ByVal PageIndex As Integer = 0,
                                           Optional ByVal PageSize As Integer = Integer.MaxValue,
                                           Optional ByRef Total As Integer = 0,
                                           Optional ByVal Sorts As String = "ORG_PATH",
                                           Optional ByVal log As UserLog = Nothing) As List(Of AT_MEAL_SETUP_DTO)
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username.ToUpper,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using



            Dim query = From p In Context.AT_MEAL_SETUP
                        From o In Context.HUV_ORGANIZATION.Where(Function(F) F.ID = p.ORG_ID).DefaultIfEmpty
                        From o1 In Context.OT_OTHER_LIST.Where(Function(F) F.ID = p.TYPE_ID).DefaultIfEmpty
                        From g In Context.SE_CHOSEN_ORG.Where(Function(f) p.ORG_ID = f.ORG_ID And
                                                                  f.USERNAME.ToUpper = log.Username.ToUpper.ToUpper)

            Dim lst = query.Select(Function(p) New AT_MEAL_SETUP_DTO With {
                                        .ID = p.p.ID,
                                        .ORG_ID = p.p.ORG_ID,
                                        .ORG_NAME = p.o.NAME_VN,
                                        .ORG_PATH = p.o.ORG_PATH,
                                        .TYPE_ID = p.p.TYPE_ID,
                                        .TYPE_NAME = p.o1.NAME_VN,
                                        .TIME_NUMBER = p.p.TIME_NUMBER,
                                        .REMARK = p.p.REMARK,
                                        .CREATED_DATE = p.p.CREATED_DATE,
                                        .CREATED_BY = p.p.CREATED_BY})


            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                lst = lst.Where(Function(f) f.ORG_NAME.ToLower().Contains(_filter.ORG_NAME.ToLower()))
            End If

            If Not String.IsNullOrEmpty(_filter.TYPE_NAME) Then
                lst = lst.Where(Function(f) f.TYPE_NAME.ToLower().Contains(_filter.TYPE_NAME.ToLower()))
            End If

            If Not String.IsNullOrEmpty(_filter.REMARK) Then
                lst = lst.Where(Function(f) f.REMARK.ToLower().Contains(_filter.REMARK.ToLower()))
            End If

            If Not String.IsNullOrEmpty(_filter.ORG_PATH) Then
                lst = lst.Where(Function(f) f.ORG_PATH.ToLower().Contains(_filter.ORG_PATH.ToLower()))
            End If

            If _filter.ORG_ID.HasValue Then
                lst = lst.Where(Function(f) f.ORG_ID = _filter.ORG_ID)
            End If
            If _filter.TYPE_ID.HasValue Then
                lst = lst.Where(Function(f) f.TYPE_ID = _filter.TYPE_ID)
            End If
            If _filter.TIME_NUMBER.HasValue Then
                lst = lst.Where(Function(f) f.TIME_NUMBER = _filter.TIME_NUMBER)
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Return lst.ToList()
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iAttendance")
            Throw ex
        End Try
    End Function
#End Region

#Region "AT_KITCHEN_ORG"

    Public Function GetAT_KITCHEN_ORG(ByVal filter As AT_KITCHEN_ORG_DTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_KITCHEN_ORG_DTO)
        Try
            Dim query = From p In Context.AT_KITCHEN_ORG
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID And f.ID = filter.ORG_ID)
                        From t In Context.AT_KITCHEN.Where(Function(f) f.ID = p.KITCHEN_ID)
                        Order By t.KITCHEN_CODE
                        Select New AT_KITCHEN_ORG_DTO With {.ID = p.ID,
                                                            .ORG_ID = p.ORG_ID,
                                                            .KITCHEN_ID = p.KITCHEN_ID,
                                                            .KITCHEN_CODE = t.KITCHEN_CODE,
                                                            .KITCHEN_NAME = t.KITCHEN_NAME,
                                                            .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng")}


            Dim org = query

            If filter.KITCHEN_CODE <> "" Then
                org = org.Where(Function(p) p.KITCHEN_CODE.ToUpper.Contains(filter.KITCHEN_CODE.ToUpper))
            End If

            If filter.KITCHEN_NAME <> "" Then
                org = org.Where(Function(p) p.KITCHEN_NAME.ToUpper.Contains(filter.KITCHEN_NAME.ToUpper))
            End If

            If filter.ACTFLG <> "" Then
                org = org.Where(Function(p) p.ACTFLG.ToUpper.Contains(filter.ACTFLG.ToUpper))
            End If

            Total = org.Count
            org = org.Skip(PageIndex * PageSize).Take(PageSize)
            Return org.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iAttendance")
            Throw ex
        End Try


    End Function

    Public Function InsertAT_KITCHEN_ORG(ByVal lstKITCHEN_ORG As List(Of AT_KITCHEN_ORG_DTO),
                                   ByVal log As UserLog, ByRef gID As Decimal,
                                   Optional ByVal isSave As Boolean = True) As Boolean

        Dim litOrgID As List(Of Decimal) = (From p In lstKITCHEN_ORG Select p.ORG_ID).ToList

        Dim lstKITCHEN_ORGExist As New List(Of AT_KITCHEN_ORG)
        Try
            lstKITCHEN_ORGExist = (From p In Context.AT_KITCHEN_ORG Where litOrgID.Contains(p.ORG_ID)).ToList

            For idx = 0 To lstKITCHEN_ORG.Count - 1
                Dim obj As AT_KITCHEN_ORG_DTO = lstKITCHEN_ORG(idx)
                If (From p In lstKITCHEN_ORGExist Where p.KITCHEN_ID = obj.KITCHEN_ID).Count = 0 Then
                    Dim objData As New AT_KITCHEN_ORG
                    objData.ID = Utilities.GetNextSequence(Context, Context.AT_KITCHEN_ORG.EntitySet.Name)
                    objData.ORG_ID = obj.ORG_ID
                    objData.KITCHEN_ID = obj.KITCHEN_ID
                    objData.ACTFLG = obj.ACTFLG
                    Context.AT_KITCHEN_ORG.AddObject(objData)
                End If
            Next
            If isSave Then
                Context.SaveChanges(log)
            End If

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iAttendance")
            Throw ex
        End Try

    End Function

    Public Function CheckKitchenInUsing(ByVal lstID As List(Of Decimal),
                                        ByVal orgID As Decimal) As Boolean
        Try
            Dim i = (From p In Context.AT_MEAL_MANAGER
                     From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                     From o In Context.HUV_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID)
                     Where lstID.Contains(p.KITCHEN_ID) And o.ORG_ID2 = orgID).Count

            If i > 0 Then
                Return False
            End If

            i = (From p In Context.AT_MEAL_PARTNER
                 From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                 From o In Context.HUV_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID)
                 Where lstID.Contains(p.KITCHEN_ID) And o.ORG_ID2 = orgID).Count

            If i > 0 Then
                Return False
            End If

            i = (From p In Context.AT_MEAL_FORECAST_SUM
                 Where lstID.Contains(p.KITCHEN_ID) And p.ORG_ID = orgID).Count

            If i > 0 Then
                Return False
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iAttendance")
            Throw ex
        End Try
    End Function

    Public Function ActiveAT_KITCHEN_ORG(ByVal lstKITCHEN_ORG As List(Of Decimal), ByVal sActive As String,
                               ByVal log As UserLog) As Boolean
        Dim lstKITCHEN_ORGData As List(Of AT_KITCHEN_ORG)
        Try
            lstKITCHEN_ORGData = (From p In Context.AT_KITCHEN_ORG Where lstKITCHEN_ORG.Contains(p.ID)).ToList
            For index = 0 To lstKITCHEN_ORGData.Count - 1
                lstKITCHEN_ORGData(index).ACTFLG = sActive
                lstKITCHEN_ORGData(index).MODIFIED_DATE = DateTime.Now
                lstKITCHEN_ORGData(index).MODIFIED_BY = log.Username
                lstKITCHEN_ORGData(index).MODIFIED_LOG = log.ComputerName
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iAttendance")
            Throw ex
        End Try

    End Function

    Public Function DeleteAT_KITCHEN_ORG(ByVal lstKITCHEN_ORG As List(Of Decimal),
                                   ByVal log As UserLog) As Boolean
        Dim lstKITCHEN_ORGData As List(Of AT_KITCHEN_ORG)
        Try
            lstKITCHEN_ORGData = (From p In Context.AT_KITCHEN_ORG Where lstKITCHEN_ORG.Contains(p.ID)).ToList

            For idx = 0 To lstKITCHEN_ORGData.Count - 1
                Context.AT_KITCHEN_ORG.DeleteObject(lstKITCHEN_ORGData(idx))
            Next

            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iAttendance")
            Throw ex
        End Try

    End Function

#End Region

#Region "AT_MEAL_MANAGER"

    Public Function Insert_AT_MEAL_MANAGER(ByVal lstData As List(Of AT_MEAL_MANAGER_DTO), ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Try
            If lstData.Count > 0 Then
                Using cls As New DataAccess.QueryData
                    cls.ExecuteStore("PKG_MEAL_BUSINESS.DELETE_AT_MEAL_MANAGER_BY_EMP",
                                     New With {.P_EMPLOYEE_ID = lstData(0).EMPLOYEE_ID,
                                               .P_EFFECT_DATE = lstData(0).EFFECT_DATE})
                End Using

                For Each objData In lstData
                    Dim obj As New AT_MEAL_MANAGER
                    obj.ID = Utilities.GetNextSequence(Context, Context.AT_MEAL_MANAGER.EntitySet.Name)
                    obj.EMPLOYEE_ID = objData.EMPLOYEE_ID
                    obj.MEAL_ID = objData.MEAL_ID
                    obj.KITCHEN_ID = objData.KITCHEN_ID
                    obj.RATION_ID = objData.RATION_ID
                    obj.COST_EXTRA = objData.COST_EXTRA
                    obj.EFFECT_DATE = objData.EFFECT_DATE
                    obj.IS_CHECK = objData.IS_CHECK
                    obj.REMARK = objData.REMARK
                    Context.AT_MEAL_MANAGER.AddObject(obj)
                    Context.SaveChanges(log)
                    gID = obj.ID
                Next
            End If

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iAttendance")
            Throw ex
        End Try
    End Function

    Public Function Delete_AT_MEAL_MANAGER(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lst As List(Of AT_MEAL_MANAGER)
        Try
            lst = (From p In Context.AT_MEAL_MANAGER Where lstID.Contains(p.ID)).ToList
            For index = 0 To lst.Count - 1
                Context.AT_MEAL_MANAGER.DeleteObject(lst(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iAttendance")
            Throw ex
        End Try
    End Function

    Public Function Get_AT_MEAL_MANAGERById(ByVal obj As AT_MEAL_MANAGER_DTO) As List(Of AT_MEAL_MANAGER_DTO)
        Try

            'Dim query = From p In Context.AT_MEAL_MANAGER
            '            From m In Context.AT_MEAL.Where(Function(f) f.ID = p.MEAL_ID)
            '            From k In Context.AT_KITCHEN.Where(Function(f) f.ID = p.KITCHEN_ID)
            '            From r In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.RATION_ID)
            '            From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID).DefaultIfEmpty
            '            From w In Context.AT_WORKSIGN.Where(Function(f) f.EMPLOYEE_ID = p.EMPLOYEE_ID And
            '                                                    f.WORKINGDAY = p.EFFECT_DATE).DefaultIfEmpty
            '            From s In Context.AT_SHIFT.Where(Function(f) f.ID = w.SHIFT_ID).DefaultIfEmpty
            '            From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
            '  Order By p.EMPLOYEE_ID, p.EFFECT_DATE, p.MEAL_ID
            Dim query = From p In Context.AT_MEAL_MANAGER
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID).DefaultIfEmpty
                        From m In Context.AT_MEAL.Where(Function(f) f.ID = p.MEAL_ID).DefaultIfEmpty
                        From r In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.RATION_ID).DefaultIfEmpty
                        From k In Context.AT_KITCHEN.Where(Function(f) f.ID = p.KITCHEN_ID).DefaultIfEmpty
                        From w In Context.AT_WORKSIGN.Where(Function(f) f.EMPLOYEE_ID = p.EMPLOYEE_ID And f.WORKINGDAY = p.EFFECT_DATE).DefaultIfEmpty
                        From s In Context.AT_SHIFT.Where(Function(f) f.ID = w.SHIFT_ID).DefaultIfEmpty
                        From a In Context.AT_MEAL_COST_SETUP.Where(Function(f) f.KITCHEN_ID = p.KITCHEN_ID And f.MEAL_ID = p.MEAL_ID And f.RATION_ID = p.RATION_ID).DefaultIfEmpty
                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                       
            If obj.MEAL_ID IsNot Nothing Then
                query = query.Where(Function(f) f.p.MEAL_ID = obj.MEAL_ID)
            End If

            If obj.EMPLOYEE_ID IsNot Nothing Then
                query = query.Where(Function(f) f.p.EMPLOYEE_ID = obj.EMPLOYEE_ID)
            End If

            If obj.EFFECT_DATE IsNot Nothing Then
                query = query.Where(Function(f) f.p.EFFECT_DATE = obj.EFFECT_DATE)
            End If


            Dim lst = query.Select(Function(p) New AT_MEAL_MANAGER_DTO With {
                                         .ID = p.p.ID,
                                         .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                         .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                         .EMPLOYEE_NAME = p.e.FULLNAME_VN,
                                         .SHIFT_NAME = p.s.CODE,
                                         .ORG_ID = p.org.ID,
                                         .ORG_CODE = p.org.CODE,
                                         .ORG_NAME = p.org.NAME_VN,
                                         .ORG_DESC = p.org.DESCRIPTION_PATH,
                                         .ORG_HIER = p.org.HIERARCHICAL_PATH,
                                         .MEAL_ID = p.p.MEAL_ID,
                                         .MEAL_NAME = p.m.NAME,
                                         .KITCHEN_ID = p.p.KITCHEN_ID,
                                         .KITCHEN_NAME = p.k.KITCHEN_NAME,
                                         .RATION_ID = p.p.RATION_ID,
                                         .RATION_NAME = p.r.NAME_VN,
                                         .COST_EXTRA = p.p.COST_EXTRA,
                                         .EFFECT_DATE = p.p.EFFECT_DATE,
                                         .CREATED_DATE = p.p.CREATED_DATE,
                                         .CREATED_BY = p.p.CREATED_BY,
                                         .CREATED_LOG = p.p.CREATED_LOG,
                                         .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                         .MODIFIED_BY = p.p.MODIFIED_BY,
                                         .MODIFIED_LOG = p.p.MODIFIED_LOG,
                                         .IS_CHECK = p.p.IS_CHECK,
                                         .REMARK = p.p.REMARK}).ToList
            Return lst
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iAttendance")
            Throw ex
        End Try
    End Function

    Public Function Get_AT_MEAL_MANAGER(ByVal _filter As AT_MEAL_MANAGER_DTO,
                                           ByVal _param As ParamDTO,
                                           Optional ByVal PageIndex As Integer = 0,
                                           Optional ByVal PageSize As Integer = Integer.MaxValue,
                                           Optional ByRef Total As Integer = 0,
                                           Optional ByVal Sorts As String = "EMPLOYEE_CODE asc, EFFECT_DATE asc, MEAL_ID asc",
                                           Optional ByVal log As UserLog = Nothing) As List(Of AT_MEAL_MANAGER_DTO)
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username.ToUpper,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using

            Dim query = From p In Context.AT_MEAL_MANAGER
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID).DefaultIfEmpty
                        From m In Context.AT_MEAL.Where(Function(f) f.ID = p.MEAL_ID).DefaultIfEmpty
                        From o In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.RATION_ID).DefaultIfEmpty
                        From k In Context.AT_KITCHEN.Where(Function(f) f.ID = p.KITCHEN_ID).DefaultIfEmpty
                        From w In Context.AT_WORKSIGN.Where(Function(f) f.EMPLOYEE_ID = p.EMPLOYEE_ID And f.WORKINGDAY = p.EFFECT_DATE).DefaultIfEmpty
                        From s In Context.AT_SHIFT.Where(Function(f) f.ID = w.SHIFT_ID).DefaultIfEmpty
                        From a In Context.AT_MEAL_COST_SETUP.Where(Function(f) f.KITCHEN_ID = p.KITCHEN_ID And f.MEAL_ID = p.MEAL_ID And f.RATION_ID = p.RATION_ID).DefaultIfEmpty
                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From g In Context.SE_CHOSEN_ORG.Where(Function(f) e.ORG_ID = f.ORG_ID And
                                                                  f.USERNAME.ToUpper = log.Username.ToUpper.ToUpper)

            Dim lst = query.Select(Function(p) New AT_MEAL_MANAGER_DTO With {
                                        .ID = p.p.ID,
                                        .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                        .EMPLOYEE_NAME = p.e.FULLNAME_VN,
                                        .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                        .SHIFT_NAME = p.s.CODE,
                                        .ORG_NAME = p.org.NAME_VN,
                                        .ORG_DESC = p.org.DESCRIPTION_PATH,
                                        .MEAL_ID = p.p.MEAL_ID,
                                        .MEAL_NAME = p.m.NAME,
                                        .KITCHEN_ID = p.p.KITCHEN_ID,
                                        .KITCHEN_NAME = p.k.KITCHEN_NAME,
                                        .RATION_ID = p.p.RATION_ID,
                                        .RATION_NAME = p.o.NAME_VN,
                                        .COST = p.a.COST,
                                        .COST_EXTRA = p.p.COST_EXTRA,
                                        .EFFECT_DATE = p.p.EFFECT_DATE,
                                        .CREATED_DATE = p.p.CREATED_DATE,
                                        .CREATED_BY = p.p.CREATED_BY,
                                        .CREATED_LOG = p.p.CREATED_LOG,
                                        .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                        .MODIFIED_BY = p.p.MODIFIED_BY,
                                        .MODIFIED_LOG = p.p.MODIFIED_LOG,
                                        .IS_CHECK = p.p.IS_CHECK,
                                        .REMARK = p.p.REMARK,
                                        .IS_SWAP = p.p.IS_SWAP,
                                        .IS_CHANGE = p.p.IS_CHANGE})

            If _filter.KITCHEN_ID.HasValue Then
                lst = lst.Where(Function(f) f.KITCHEN_ID = _filter.KITCHEN_ID)
            End If

            If _filter.EMPLOYEE_ID.HasValue Then
                lst = lst.Where(Function(f) f.EMPLOYEE_ID = _filter.EMPLOYEE_ID)
            End If

            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                lst = lst.Where(Function(f) f.EMPLOYEE_CODE.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower()))
            End If

            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_NAME) Then
                lst = lst.Where(Function(f) f.EMPLOYEE_NAME.ToLower().Contains(_filter.EMPLOYEE_NAME.ToLower()))
            End If

            If Not String.IsNullOrEmpty(_filter.MEAL_NAME) Then
                lst = lst.Where(Function(f) f.MEAL_NAME.ToLower().Contains(_filter.MEAL_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.KITCHEN_NAME) Then
                lst = lst.Where(Function(f) f.KITCHEN_NAME.ToLower().Contains(_filter.KITCHEN_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.REMARK) Then
                lst = lst.Where(Function(f) f.REMARK.ToLower().Contains(_filter.REMARK.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                lst = lst.Where(Function(f) f.ORG_NAME.ToLower().Contains(_filter.ORG_NAME.ToLower()))
            End If
            If _filter.KITCHEN_ID.HasValue Then
                lst = lst.Where(Function(f) f.KITCHEN_ID = _filter.KITCHEN_ID)
            End If
            If _filter.RATION_ID.HasValue Then
                lst = lst.Where(Function(f) f.RATION_ID = _filter.RATION_ID)
            End If
            If _filter.COST_EXTRA.HasValue Then
                lst = lst.Where(Function(f) f.COST_EXTRA = _filter.COST_EXTRA)
            End If
            If _filter.EFFECT_DATE.HasValue Then
                lst = lst.Where(Function(f) f.EFFECT_DATE = _filter.EFFECT_DATE)
            End If

            If _filter.START_DATE.HasValue Then
                lst = lst.Where(Function(f) f.EFFECT_DATE >= _filter.START_DATE)
            End If

            If _filter.END_DATE.HasValue Then
                lst = lst.Where(Function(f) f.EFFECT_DATE <= _filter.END_DATE)
            End If
            ' lst = lst.OrderBy("EMPLOYEE_CODE asc, EFFECT_DATE asc, MEAL_ID asc")
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iAttendance")
            Throw ex
        End Try
    End Function

    Public Function Insert_AT_MEAL_MANAGER_BY_ORG(ByVal lstData As List(Of AT_MEAL_MANAGER_DTO),
                                                  ByVal _param As ParamDTO,
                                                  ByVal log As UserLog) As Boolean
        ' Dim obj As New AT_MEAL_MANAGER
        Try

            Using conMng As New ConnectionManager
                Using conn As New OracleConnection(conMng.GetConnectionString())
                    Using cmd As New OracleCommand()
                        Try
                            conn.Open()
                            cmd.Connection = conn
                            cmd.CommandType = CommandType.StoredProcedure
                            cmd.Transaction = cmd.Connection.BeginTransaction()
                            cmd.CommandText = "PKG_MEAL_BUSINESS.DELETE_AT_MEAL_MANAGER_BY_ORG"

                            Using resource As New DataAccess.OracleCommon()
                                Dim objParam = New With {.P_ORG_ID = _param.ORG_ID,
                                                         .P_USERNAME = log.Username,
                                                         .P_ISDISSOLVE = _param.IS_DISSOLVE,
                                                         .P_EFFECT_DATE = lstData(0).EFFECT_DATE}

                                If objParam IsNot Nothing Then
                                    For Each info As PropertyInfo In objParam.GetType().GetProperties()
                                        Dim bOut As Boolean = False
                                        Dim para = resource.GetParameter(info.Name, info.GetValue(objParam, Nothing), bOut)
                                        If para IsNot Nothing Then
                                            cmd.Parameters.Add(para)
                                        End If
                                    Next
                                End If
                                cmd.ExecuteNonQuery()
                            End Using

                            cmd.CommandText = "PKG_MEAL_BUSINESS.INSERT_AT_MEAL_MANAGER_BY_ORG"
                            For Each objData In lstData
                                cmd.Parameters.Clear()

                                Using resource As New DataAccess.OracleCommon()
                                    Dim objParam = New With {.P_ORG_ID = _param.ORG_ID,
                                                    .P_USERNAME = log.Username,
                                                    .P_ISDISSOLVE = _param.IS_DISSOLVE,
                                                    .P_MEAL_ID = objData.MEAL_ID,
                                                    .P_KITCHEN_ID = objData.KITCHEN_ID,
                                                    .P_RATION_ID = objData.RATION_ID,
                                                    .P_COST_EXTRA = objData.COST_EXTRA,
                                                    .P_IS_CHECK = objData.IS_CHECK,
                                                    .P_EFFECT_DATE = objData.EFFECT_DATE,
                                                    .P_REMARK = objData.REMARK}

                                    If objParam IsNot Nothing Then
                                        For Each info As PropertyInfo In objParam.GetType().GetProperties()
                                            Dim bOut As Boolean = False
                                            Dim para = resource.GetParameter(info.Name, info.GetValue(objParam, Nothing), bOut)
                                            If para IsNot Nothing Then
                                                cmd.Parameters.Add(para)
                                            End If
                                        Next
                                    End If
                                    cmd.ExecuteNonQuery()
                                End Using
                            Next
                            cmd.Transaction.Commit()
                        Catch ex As Exception
                            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iAttendance")
                            Throw ex
                            cmd.Transaction.Rollback()
                        Finally
                            'Dispose all resource
                            cmd.Dispose()
                            conn.Close()
                            conn.Dispose()
                        End Try
                    End Using
                End Using
            End Using

            ' gID = obj.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iAttendance")
            Throw ex
        End Try
    End Function

    Public Function Insert_AT_MEAL_MANAGER_BY_EMP(ByVal lstData As List(Of AT_MEAL_MANAGER_DTO),
                                                  ByVal lstEmp As List(Of EmployeeDTO),
                                                  ByVal _param As ParamDTO,
                                                  ByVal log As UserLog) As Boolean
        ' Dim obj As New AT_MEAL_MANAGER
        Try

            Using conMng As New ConnectionManager
                Using conn As New OracleConnection(conMng.GetConnectionString())
                    Using cmd As New OracleCommand()
                        Try
                            conn.Open()
                            cmd.Connection = conn
                            cmd.CommandType = CommandType.StoredProcedure
                            cmd.Transaction = cmd.Connection.BeginTransaction()

                            cmd.CommandText = "PKG_MEAL_BUSINESS.INSERT_AT_MEAL_MANAGER_BY_EMP"
                            For Each objData In lstData
                                cmd.Parameters.Clear()

                                Dim Emp As String = lstEmp.Select(Function(f) f.ID.ToString).Aggregate(Function(x, y) x & "," & y)

                                Using resource As New DataAccess.OracleCommon()
                                    Dim objParam = New With {.P_ORG_ID = _param.ORG_ID,
                                                    .P_USERNAME = log.Username,
                                                    .P_ISDISSOLVE = _param.IS_DISSOLVE,
                                                    .P_MEAL_ID = objData.MEAL_ID,
                                                    .P_KITCHEN_ID = objData.KITCHEN_ID,
                                                    .P_RATION_ID = objData.RATION_ID,
                                                    .P_COST_EXTRA = objData.COST_EXTRA,
                                                    .P_IS_CHECK = objData.IS_CHECK,
                                                    .P_EFFECT_DATE = objData.EFFECT_DATE,
                                                    .P_REMARK = objData.REMARK,
                                                    .LIST_EMP = Emp}

                                    If objParam IsNot Nothing Then
                                        For Each info As PropertyInfo In objParam.GetType().GetProperties()
                                            Dim bOut As Boolean = False
                                            Dim para = resource.GetParameter(info.Name, info.GetValue(objParam, Nothing), bOut)
                                            If para IsNot Nothing Then
                                                cmd.Parameters.Add(para)
                                            End If
                                        Next
                                    End If
                                    cmd.ExecuteNonQuery()
                                End Using
                            Next
                            cmd.Transaction.Commit()
                        Catch ex As Exception
                            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iAttendance")
                            Throw ex
                            cmd.Transaction.Rollback()
                        Finally
                            'Dispose all resource
                            cmd.Dispose()
                            conn.Close()
                            conn.Dispose()
                        End Try
                    End Using
                End Using
            End Using

            ' gID = obj.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iAttendance")
            Throw ex
        End Try
    End Function

    Public Function Validate_AT_MEAL_SWAP(ByVal objData As AT_MEAL_SWAP_DTO,
                                          ByVal _action As String,
                                          Optional ByRef _error As String = "") As Boolean
        Try
            Select Case _action
                Case "ExistMeal"
                    Dim query
                    query = (From p In Context.AT_MEAL_MANAGER
                             Where p.EMPLOYEE_ID = objData.EMPLOYEE_ID And
                             p.EFFECT_DATE = objData.EFFECT_DATE And
                             p.MEAL_ID = objData.MEAL_ID_NEW And
                             p.MEAL_ID <> objData.MEAL_ID).Count

                    If query > 0 Then
                        _error = "Nhân viên cần hoán đổi đã tồn tại bữa ăn"
                        Return False
                    End If

                    If objData.MEAL_ID_NEW Is Nothing Then
                        objData.MEAL_ID_NEW = 0
                    End If

                    query = (From p In Context.AT_MEAL_MANAGER
                             Where p.EMPLOYEE_ID = objData.EMPLOYEE_CHANGE_ID And
                             p.EFFECT_DATE = objData.EFFECT_DATE And
                             p.MEAL_ID = objData.MEAL_ID And
                             p.MEAL_ID <> objData.MEAL_ID_NEW).Count

                    If query > 0 Then
                        _error = "Nhân viên hoán đổi đã tồn tại bữa ăn"
                        Return False
                    End If
            End Select
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iAttendance")
            Throw ex
        End Try
    End Function

    Public Function Swap_AT_MEAL_MANAGER(ByVal objData As AT_MEAL_SWAP_DTO,
                                                ByVal log As UserLog) As Boolean
        Dim obj As New AT_MEAL_SWAP
        Dim lst As New List(Of Decimal)
        Try
            obj.ID = Utilities.GetNextSequence(Context, Context.AT_MEAL_SWAP.EntitySet.Name)
            obj.EFFECT_DATE = objData.EFFECT_DATE

            obj.EMPLOYEE_ID = objData.EMPLOYEE_ID
            obj.MEAL_ID = objData.MEAL_ID
            obj.KITCHEN_ID = objData.KITCHEN_ID
            obj.RATION_ID = objData.RATION_ID
            obj.COST_EXTRA = objData.COST_EXTRA
            obj.IS_CHECK = objData.IS_CHECK
            obj.REMARK = objData.REMARK

            obj.EMPLOYEE_CHANGE_ID = objData.EMPLOYEE_CHANGE_ID
            obj.MEAL_ID_NEW = objData.MEAL_ID_NEW
            obj.KITCHEN_ID_NEW = objData.KITCHEN_ID_NEW
            obj.RATION_ID_NEW = objData.RATION_ID_NEW
            obj.COST_EXTRA_NEW = objData.COST_EXTRA_NEW
            obj.IS_CHECK_NEW = objData.IS_CHECK_NEW
            obj.REMARK_NEW = objData.REMARK_NEW

            Context.AT_MEAL_SWAP.AddObject(obj)

            If objData.MEAL_ID_NEW IsNot Nothing Then

                Dim objEmp = (From p In Context.AT_MEAL_MANAGER
                              Where p.EMPLOYEE_ID = objData.EMPLOYEE_ID And
                              p.EFFECT_DATE = objData.EFFECT_DATE And
                              p.MEAL_ID = objData.MEAL_ID).FirstOrDefault

                Dim objEmpSwap = (From p In Context.AT_MEAL_MANAGER
                                  Where p.EMPLOYEE_ID = objData.EMPLOYEE_CHANGE_ID And
                                  p.EFFECT_DATE = objData.EFFECT_DATE And
                                  p.MEAL_ID = objData.MEAL_ID_NEW).FirstOrDefault

                If objEmp IsNot Nothing And objEmpSwap IsNot Nothing Then

                    objEmpSwap.MEAL_ID = objData.MEAL_ID
                    objEmpSwap.KITCHEN_ID = objData.KITCHEN_ID
                    objEmpSwap.RATION_ID = objData.RATION_ID
                    objEmpSwap.COST_EXTRA = objData.COST_EXTRA
                    objEmpSwap.IS_CHECK = objData.IS_CHECK
                    objEmpSwap.REMARK = objData.REMARK
                    objEmpSwap.IS_SWAP = True

                    objEmp.MEAL_ID = objData.MEAL_ID_NEW
                    objEmp.KITCHEN_ID = objData.KITCHEN_ID_NEW
                    objEmp.RATION_ID = objData.RATION_ID_NEW
                    objEmp.COST_EXTRA = objData.COST_EXTRA_NEW
                    objEmp.IS_CHECK = objData.IS_CHECK_NEW
                    objEmp.REMARK = objData.REMARK_NEW
                    objEmp.IS_SWAP = True

                    Context.SaveChanges(log)
                Else
                    Return False
                End If
            Else

                Dim objEmp = (From p In Context.AT_MEAL_MANAGER
                              Where p.EMPLOYEE_ID = objData.EMPLOYEE_ID And
                              p.EFFECT_DATE = objData.EFFECT_DATE And
                              p.MEAL_ID = objData.MEAL_ID).FirstOrDefault

                Context.AT_MEAL_MANAGER.DeleteObject(objEmp)

                Dim objEmpSwap As New AT_MEAL_MANAGER
                objEmpSwap.ID = Utilities.GetNextSequence(Context, Context.AT_MEAL_MANAGER.EntitySet.Name)
                objEmpSwap.EFFECT_DATE = objData.EFFECT_DATE
                objEmpSwap.EMPLOYEE_ID = objData.EMPLOYEE_CHANGE_ID
                objEmpSwap.MEAL_ID = objData.MEAL_ID
                objEmpSwap.KITCHEN_ID = objData.KITCHEN_ID
                objEmpSwap.RATION_ID = objData.RATION_ID
                objEmpSwap.COST_EXTRA = objData.COST_EXTRA
                objEmpSwap.IS_CHECK = objData.IS_CHECK
                objEmpSwap.REMARK = objData.REMARK
                objEmpSwap.IS_SWAP = True

                Context.AT_MEAL_MANAGER.AddObject(objEmpSwap)

                Context.SaveChanges(log)
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iAttendance")
            Throw ex
        End Try
    End Function

    Public Function GETDATA_MANAGER_IMPORT(ByVal obj As ParamDTO, ByVal log As UserLog) As DataSet
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataSet = cls.ExecuteStore("PKG_MEAL_BUSINESS.GETDATA_MANAGER_IMPORT",
                                               New With {.P_USERNAME = log.Username.ToUpper,
                                                         .P_ORG_ID = obj.ORG_ID,
                                                         .P_ISDISSOLVE = obj.IS_DISSOLVE,
                                                         .P_STARTDATE = obj.FROMDATE,
                                                         .P_ENDDATE = obj.ENDDATE,
                                                         .P_CUR = cls.OUT_CURSOR,
                                                         .P_CUR2 = cls.OUT_CURSOR,
                                                         .P_CUR3 = cls.OUT_CURSOR,
                                                         .P_CUR4 = cls.OUT_CURSOR,
                                                         .P_KITCHEN_ID = obj.KITCHEN_ID}, False)
                Return dtData
            End Using
            Return Nothing
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iAttendance")
            Throw ex
        End Try
    End Function

    Function ImportMealManagerValidate(ByVal obj As AT_MEAL_SETUP_DTO, ByVal _effectDate As Date) As Boolean
        Try
            If obj IsNot Nothing Then
                Dim CompareDate As New Date
                If obj.TYPE_ID = 7575 Then
                    Dim day = obj.TIME_NUMBER \ 24
                    Dim hours = obj.TIME_NUMBER Mod 24
                    CompareDate = Date.Now.AddDays(day)
                    CompareDate = CompareDate.AddHours(hours)
                Else
                    CompareDate = Date.Now.AddDays(obj.TIME_NUMBER)
                End If
                If Date.Compare(CompareDate, _effectDate) >= 0 Then
                    Return False
                End If
                Dim datenow = Date.Now
                If Date.Compare(datenow.Date, _effectDate) >= 0 Then
                    Return False
                End If
            Else
                Dim datenow = Date.Now
                If Date.Compare(datenow.Date, _effectDate) >= 0 Then
                    Return False
                End If
            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ImportMealManager(ByVal dtData As DataTable,
                                      ByVal StartDate As Date,
                                      ByVal EndDate As Date,
                                      ByRef dtError As DataTable,
                                      ByVal log As UserLog) As Boolean
        Dim lstEmp As New List(Of Decimal)
        Dim strEmp As String = ""
        Dim iRow As Integer = 10
        Try
            ' Validate server
            For Each row As DataRow In dtData.Rows
                If (From p In lstEmp Where p = row("EMPLOYEE_ID").ToString).Count = 0 Then
                    lstEmp.Add(row("EMPLOYEE_ID").ToString)
                End If
            Next

            strEmp = lstEmp.Select(Function(f) f.ToString).Aggregate(Function(x, y) x & "," & y)

            Using cls As New DataAccess.NonQueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_EMP2",
                                 New With {.P_USERNAME = log.Username.ToUpper,
                                           .P_EMP = strEmp})
            End Using

            Dim lstValidate = (From o In Context.AT_MEAL_SETUP
                               From a In Context.ATV_KITCHEN_ORG.Where(Function(f) f.ORG_MEAL_SETUP_ID = o.ORG_ID)
                               From e In Context.HU_EMPLOYEE.Where(Function(f) f.ORG_ID = a.ID)
                               From chosen In Context.SE_CHOSEN_EMP.Where(Function(f) f.EMPLOYEE_ID = e.ID)
                               Select New AT_MEAL_SETUP_DTO With {.EMPLOYEE_ID = e.ID,
                                                                  .TYPE_ID = o.TYPE_ID,
                                                                  .TIME_NUMBER = o.TIME_NUMBER}).ToList

            lstEmp = New List(Of Decimal)
            If Not dtError.Columns.Contains("STT") Then
                dtError.Columns.Add("STT")
            End If
            For Each row As DataRow In dtData.Rows
                Dim obj = (From p In lstValidate Where p.EMPLOYEE_ID = row("EMPLOYEE_ID").ToString).FirstOrDefault
                Dim isError As Boolean = False
                Dim rowError = dtError.NewRow

                Dim D1_DATE As Date = row("D1_DATE")
                Dim D2_DATE As Date = row("D2_DATE")
                Dim D3_DATE As Date = row("D3_DATE")
                Dim D4_DATE As Date = row("D4_DATE")
                Dim D5_DATE As Date = row("D5_DATE")
                Dim D6_DATE As Date = row("D6_DATE")
                Dim D7_DATE As Date = row("D7_DATE")

                If Not ImportMealManagerValidate(obj, D1_DATE) Then
                    rowError("D1_BA") = "Đã quá thời gian điều chỉnh bữa ăn"
                    isError = True
                End If

                If Not ImportMealManagerValidate(obj, D2_DATE) Then
                    rowError("D2_BA") = "Đã quá thời gian điều chỉnh bữa ăn"
                    isError = True
                End If

                If Not ImportMealManagerValidate(obj, D3_DATE) Then
                    rowError("D3_BA") = "Đã quá thời gian điều chỉnh bữa ăn"
                    isError = True
                End If

                If Not ImportMealManagerValidate(obj, D4_DATE) Then
                    rowError("D4_BA") = "Đã quá thời gian điều chỉnh bữa ăn"
                    isError = True
                End If

                If Not ImportMealManagerValidate(obj, D5_DATE) Then
                    rowError("D5_BA") = "Đã quá thời gian điều chỉnh bữa ăn"
                    isError = True
                End If

                If Not ImportMealManagerValidate(obj, D6_DATE) Then
                    rowError("D6_BA") = "Đã quá thời gian điều chỉnh bữa ăn"
                    isError = True
                End If

                If Not ImportMealManagerValidate(obj, D7_DATE) Then
                    rowError("D7_BA") = "Đã quá thời gian điều chỉnh bữa ăn"
                    isError = True
                End If

                If isError Then
                    rowError("STT") = iRow
                    rowError("EMPLOYEE_CODE") = row("EMPLOYEE_CODE").ToString
                    rowError("VN_FULLNAME") = row("VN_FULLNAME").ToString
                    rowError("ORG_DESC") = row("ORG_DESC").ToString
                    rowError("ORG_NAME") = row("ORG_NAME").ToString
                    rowError("TITLE_NAME") = row("TITLE_NAME").ToString
                    If (From p In lstEmp Where p = row("EMPLOYEE_ID").ToString).Count = 0 Then
                        lstEmp.Add(row("EMPLOYEE_ID").ToString)
                        dtError.Rows.Add(rowError)
                    End If
                End If

                iRow += 1
            Next

            If dtError.Rows.Count > 0 Then
                Return False
            End If

            Using conMng As New ConnectionManager
                Using conn As New OracleConnection(conMng.GetConnectionString())
                    Using cmd As New OracleCommand()
                        Using resource As New DataAccess.OracleCommon()
                            Try
                                conn.Open()
                                cmd.Connection = conn
                                cmd.Transaction = cmd.Connection.BeginTransaction()
                                cmd.CommandType = CommandType.StoredProcedure
                                cmd.CommandText = "PKG_MEAL_BUSINESS.INSERT_AT_MEAL_MANAGER_TEMP"

                                For Each row As DataRow In dtData.Rows
                                    cmd.Parameters.Clear()
                                    Dim objParam = New With {.P_EMPLOYEEID = row("EMPLOYEE_ID").ToString,
                                                             .P_MEAL_ID = row("MEAL_ID").ToString,
                                                             .P_D1_BA_ID = Utilities.Obj2Decima(row("D1_BA_ID"), Nothing),
                                                             .P_D2_BA_ID = Utilities.Obj2Decima(row("D2_BA_ID"), Nothing),
                                                             .P_D3_BA_ID = Utilities.Obj2Decima(row("D3_BA_ID"), Nothing),
                                                             .P_D4_BA_ID = Utilities.Obj2Decima(row("D4_BA_ID"), Nothing),
                                                             .P_D5_BA_ID = Utilities.Obj2Decima(row("D5_BA_ID"), Nothing),
                                                             .P_D6_BA_ID = Utilities.Obj2Decima(row("D6_BA_ID"), Nothing),
                                                             .P_D7_BA_ID = Utilities.Obj2Decima(row("D7_BA_ID"), Nothing),
                                                             .P_D1_SA_ID = Utilities.Obj2Decima(row("D1_SA_ID"), Nothing),
                                                             .P_D2_SA_ID = Utilities.Obj2Decima(row("D2_SA_ID"), Nothing),
                                                             .P_D3_SA_ID = Utilities.Obj2Decima(row("D3_SA_ID"), Nothing),
                                                             .P_D4_SA_ID = Utilities.Obj2Decima(row("D4_SA_ID"), Nothing),
                                                             .P_D5_SA_ID = Utilities.Obj2Decima(row("D5_SA_ID"), Nothing),
                                                             .P_D6_SA_ID = Utilities.Obj2Decima(row("D6_SA_ID"), Nothing),
                                                             .P_D7_SA_ID = Utilities.Obj2Decima(row("D7_SA_ID"), Nothing),
                                                             .P_D1_COST = Utilities.Obj2Decima(row("D1_COST"), Nothing),
                                                             .P_D2_COST = Utilities.Obj2Decima(row("D2_COST"), Nothing),
                                                             .P_D3_COST = Utilities.Obj2Decima(row("D3_COST"), Nothing),
                                                             .P_D4_COST = Utilities.Obj2Decima(row("D4_COST"), Nothing),
                                                             .P_D5_COST = Utilities.Obj2Decima(row("D5_COST"), Nothing),
                                                             .P_D6_COST = Utilities.Obj2Decima(row("D6_COST"), Nothing),
                                                             .P_D7_COST = Utilities.Obj2Decima(row("D7_COST"), Nothing),
                                                             .P_D1_C = Utilities.Obj2Decima(row("D1_C"), Nothing),
                                                             .P_D2_C = Utilities.Obj2Decima(row("D2_C"), Nothing),
                                                             .P_D3_C = Utilities.Obj2Decima(row("D3_C"), Nothing),
                                                             .P_D4_C = Utilities.Obj2Decima(row("D4_C"), Nothing),
                                                             .P_D5_C = Utilities.Obj2Decima(row("D5_C"), Nothing),
                                                             .P_D6_C = Utilities.Obj2Decima(row("D6_C"), Nothing),
                                                             .P_D7_C = Utilities.Obj2Decima(row("D7_C"), Nothing),
                                                             .P_D1_DATE = ToDate(row("D1_DATE")),
                                                             .P_D2_DATE = ToDate(row("D2_DATE")),
                                                             .P_D3_DATE = ToDate(row("D3_DATE")),
                                                             .P_D4_DATE = ToDate(row("D4_DATE")),
                                                             .P_D5_DATE = ToDate(row("D5_DATE")),
                                                             .P_D6_DATE = ToDate(row("D6_DATE")),
                                                             .P_D7_DATE = ToDate(row("D7_DATE"))}

                                    If objParam IsNot Nothing Then
                                        For Each info As PropertyInfo In objParam.GetType().GetProperties()
                                            Dim bOut As Boolean = False
                                            Dim para = resource.GetParameter(info.Name, info.GetValue(objParam, Nothing), bOut)
                                            If para IsNot Nothing Then
                                                cmd.Parameters.Add(para)
                                            End If
                                        Next
                                    End If
                                    cmd.ExecuteNonQuery()
                                Next

                                cmd.CommandText = "PKG_MEAL_BUSINESS.IMPORT_AT_MEAL_MANAGER"
                                cmd.Parameters.Clear()
                                Dim objParam1 = New With {.P_STARTDATE = StartDate,
                                                          .P_ENDDATE = EndDate,
                                                        .P_USERNAME = log.Username.ToUpper}

                                If objParam1 IsNot Nothing Then
                                    For Each info As PropertyInfo In objParam1.GetType().GetProperties()
                                        Dim bOut As Boolean = False
                                        Dim para = resource.GetParameter(info.Name, info.GetValue(objParam1, Nothing), bOut)
                                        If para IsNot Nothing Then
                                            cmd.Parameters.Add(para)
                                        End If
                                    Next
                                End If

                                cmd.ExecuteNonQuery()

                                cmd.Transaction.Commit()

                            Catch ex As Exception
                                cmd.Transaction.Rollback()
                                WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iAttendance")
                                Throw ex
                            Finally
                                'Dispose all resource
                                cmd.Dispose()
                                conn.Close()
                                conn.Dispose()
                            End Try
                        End Using
                    End Using
                End Using
            End Using

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iAttendance")
            Throw ex
        End Try
    End Function

    Public Function GetListEmployee_ByOrg(ByVal _param As ParamDTO, ByVal log As UserLog) As List(Of EmployeeDTO)
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username.ToUpper,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using

            Dim query As ObjectQuery(Of EmployeeDTO)
            query = (From p In Context.HU_EMPLOYEE
                     From title In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID).DefaultIfEmpty
                     From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                     From g In Context.SE_CHOSEN_ORG.Where(Function(f) p.ORG_ID = f.ORG_ID And
                                                                  f.USERNAME.ToUpper = log.Username.ToUpper.ToUpper)
                     Where p.JOIN_DATE.HasValue And p.JOIN_DATE <= _param.ENDDATE And p.WORK_STATUS.HasValue And _
                                     ((p.WORK_STATUS <> 257) Or (p.WORK_STATUS = 257 And p.TER_EFFECT_DATE >= _param.FROMDATE))
                     Order By p.EMPLOYEE_CODE
                        Select New EmployeeDTO With {
                         .EMPLOYEE_CODE = p.EMPLOYEE_CODE,
                         .ID = p.ID,
                         .FULLNAME_VN = p.FULLNAME_VN,
                         .FULLNAME_EN = p.FULLNAME_EN,
                         .ORG_ID = p.ORG_ID,
                         .ORG_NAME = org.NAME_VN,
                         .ORG_DESC = org.DESCRIPTION_PATH,
                         .TITLE_ID = p.TITLE_ID,
                         .JOIN_DATE = p.JOIN_DATE,
                         .TITLE_NAME_VN = title.NAME_VN
                        })

            Return query.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iMeal")

            Throw ex
        End Try
    End Function

#End Region

#Region "AT_MEAL_STUDENT"

    Public Function Insert_AT_MEAL_STUDENT(ByVal lstData As List(Of AT_MEAL_STUDENT_DTO), ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Try
            If lstData.Count > 0 Then
                Using cls As New DataAccess.QueryData
                    cls.ExecuteStore("PKG_MEAL_BUSINESS.DELETE_AT_MEAL_STUDENT_BY_EMP",
                                     New With {.P_STUDENT_ID = lstData(0).STUDENT_ID,
                                               .P_EFFECT_DATE = lstData(0).EFFECT_DATE})
                End Using

                For Each objData In lstData
                    Dim obj As New AT_MEAL_STUDENT
                    obj.ID = Utilities.GetNextSequence(Context, Context.AT_MEAL_STUDENT.EntitySet.Name)
                    obj.STUDENT_ID = objData.STUDENT_ID
                    obj.MEAL_ID = objData.MEAL_ID
                    obj.KITCHEN_ID = objData.KITCHEN_ID
                    obj.RATION_ID = objData.RATION_ID
                    obj.COST_EXTRA = objData.COST_EXTRA
                    obj.EFFECT_DATE = objData.EFFECT_DATE
                    obj.IS_CHECK = objData.IS_CHECK
                    obj.REMARK = objData.REMARK
                    Context.AT_MEAL_STUDENT.AddObject(obj)
                    Context.SaveChanges(log)
                    gID = obj.ID
                Next
            End If

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iAttendance")
            Throw ex
        End Try
    End Function

    Public Function Delete_AT_MEAL_STUDENT(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lst As List(Of AT_MEAL_STUDENT)
        Try
            lst = (From p In Context.AT_MEAL_STUDENT Where lstID.Contains(p.ID)).ToList
            For index = 0 To lst.Count - 1
                Context.AT_MEAL_STUDENT.DeleteObject(lst(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iAttendance")
            Throw ex
        End Try
    End Function

    Public Function Get_AT_MEAL_STUDENTById(ByVal obj As AT_MEAL_STUDENT_DTO) As List(Of AT_MEAL_STUDENT_DTO)
        Try

            Dim query = From p In Context.AT_MEAL_STUDENT
                        From m In Context.AT_MEAL.Where(Function(f) f.ID = p.MEAL_ID)
                        From k In Context.AT_KITCHEN.Where(Function(f) f.ID = p.KITCHEN_ID)
                        From r In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.RATION_ID)
                        From e In Context.HU_STUDENT.Where(Function(f) f.ID = p.STUDENT_ID).DefaultIfEmpty
                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
            '  Order By p.STUDENT_ID, p.EFFECT_DATE, p.MEAL_ID

            If obj.MEAL_ID IsNot Nothing Then
                query = query.Where(Function(f) f.p.MEAL_ID = obj.MEAL_ID)
            End If

            If obj.STUDENT_ID IsNot Nothing Then
                query = query.Where(Function(f) f.p.STUDENT_ID = obj.STUDENT_ID)
            End If

            If obj.EFFECT_DATE IsNot Nothing Then
                query = query.Where(Function(f) f.p.EFFECT_DATE = obj.EFFECT_DATE)
            End If


            Dim lst = query.Select(Function(p) New AT_MEAL_STUDENT_DTO With {
                                         .ID = p.p.ID,
                                         .STUDENT_ID = p.p.STUDENT_ID,
                                         .STUDENT_CODE = p.e.STUDENT_CODE,
                                         .STUDENT_NAME = p.e.FULLNAME_VN,
                                         .ORG_ID = p.org.ID,
                                         .ORG_CODE = p.org.CODE,
                                         .ORG_NAME = p.org.NAME_VN,
                                         .ORG_DESC = p.org.DESCRIPTION_PATH,
                                         .ORG_HIER = p.org.HIERARCHICAL_PATH,
                                         .MEAL_ID = p.p.MEAL_ID,
                                         .MEAL_NAME = p.m.NAME,
                                         .KITCHEN_ID = p.p.KITCHEN_ID,
                                         .KITCHEN_NAME = p.k.KITCHEN_NAME,
                                         .RATION_ID = p.p.RATION_ID,
                                         .RATION_NAME = p.r.NAME_VN,
                                         .COST_EXTRA = p.p.COST_EXTRA,
                                         .EFFECT_DATE = p.p.EFFECT_DATE,
                                         .CREATED_DATE = p.p.CREATED_DATE,
                                         .CREATED_BY = p.p.CREATED_BY,
                                         .CREATED_LOG = p.p.CREATED_LOG,
                                         .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                         .MODIFIED_BY = p.p.MODIFIED_BY,
                                         .MODIFIED_LOG = p.p.MODIFIED_LOG,
                                         .IS_CHECK = p.p.IS_CHECK,
                                         .REMARK = p.p.REMARK}).ToList
            Return lst
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iAttendance")
            Throw ex
        End Try
    End Function

    Public Function Get_AT_MEAL_STUDENT(ByVal _filter As AT_MEAL_STUDENT_DTO,
                                           ByVal _param As ParamDTO,
                                           Optional ByVal PageIndex As Integer = 0,
                                           Optional ByVal PageSize As Integer = Integer.MaxValue,
                                           Optional ByRef Total As Integer = 0,
                                           Optional ByVal Sorts As String = "STUDENT_CODE asc, EFFECT_DATE asc, MEAL_ID asc",
                                           Optional ByVal log As UserLog = Nothing) As List(Of AT_MEAL_STUDENT_DTO)
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username.ToUpper,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using

            Dim query = From p In Context.AT_MEAL_STUDENT
                        From e In Context.HU_STUDENT.Where(Function(f) f.ID = p.STUDENT_ID).DefaultIfEmpty
                        From m In Context.AT_MEAL.Where(Function(f) f.ID = p.MEAL_ID).DefaultIfEmpty
                        From o In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.RATION_ID).DefaultIfEmpty
                        From k In Context.AT_KITCHEN.Where(Function(f) f.ID = p.KITCHEN_ID).DefaultIfEmpty
                        From a In Context.AT_MEAL_COST_SETUP.Where(Function(f) f.KITCHEN_ID = p.KITCHEN_ID And f.MEAL_ID = p.MEAL_ID And f.RATION_ID = p.RATION_ID).DefaultIfEmpty
                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From g In Context.SE_CHOSEN_ORG.Where(Function(f) e.ORG_ID = f.ORG_ID And
                                                                  f.USERNAME.ToUpper = log.Username.ToUpper.ToUpper)

            Dim lst = query.Select(Function(p) New AT_MEAL_STUDENT_DTO With {
                                        .ID = p.p.ID,
                                        .STUDENT_ID = p.p.STUDENT_ID,
                                        .STUDENT_NAME = p.e.FULLNAME_VN,
                                        .STUDENT_CODE = p.e.STUDENT_CODE,
                                        .ORG_NAME = p.org.NAME_VN,
                                        .ORG_DESC = p.org.DESCRIPTION_PATH,
                                        .MEAL_ID = p.p.MEAL_ID,
                                        .MEAL_NAME = p.m.NAME,
                                        .KITCHEN_ID = p.p.KITCHEN_ID,
                                        .KITCHEN_NAME = p.k.KITCHEN_NAME,
                                        .RATION_ID = p.p.RATION_ID,
                                        .RATION_NAME = p.o.NAME_VN,
                                        .COST = p.a.COST,
                                        .COST_EXTRA = p.p.COST_EXTRA,
                                        .EFFECT_DATE = p.p.EFFECT_DATE,
                                        .CREATED_DATE = p.p.CREATED_DATE,
                                        .CREATED_BY = p.p.CREATED_BY,
                                        .CREATED_LOG = p.p.CREATED_LOG,
                                        .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                        .MODIFIED_BY = p.p.MODIFIED_BY,
                                        .MODIFIED_LOG = p.p.MODIFIED_LOG,
                                        .IS_CHECK = p.p.IS_CHECK,
                                        .REMARK = p.p.REMARK})

            If _filter.KITCHEN_ID.HasValue Then
                lst = lst.Where(Function(f) f.KITCHEN_ID = _filter.KITCHEN_ID)
            End If

            If _filter.STUDENT_ID.HasValue Then
                lst = lst.Where(Function(f) f.STUDENT_ID = _filter.STUDENT_ID)
            End If

            If Not String.IsNullOrEmpty(_filter.STUDENT_CODE) Then
                lst = lst.Where(Function(f) f.STUDENT_CODE.ToLower().Contains(_filter.STUDENT_CODE.ToLower()))
            End If

            If Not String.IsNullOrEmpty(_filter.STUDENT_NAME) Then
                lst = lst.Where(Function(f) f.STUDENT_NAME.ToLower().Contains(_filter.STUDENT_NAME.ToLower()))
            End If

            If Not String.IsNullOrEmpty(_filter.MEAL_NAME) Then
                lst = lst.Where(Function(f) f.MEAL_NAME.ToLower().Contains(_filter.MEAL_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.KITCHEN_NAME) Then
                lst = lst.Where(Function(f) f.KITCHEN_NAME.ToLower().Contains(_filter.KITCHEN_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.REMARK) Then
                lst = lst.Where(Function(f) f.REMARK.ToLower().Contains(_filter.REMARK.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                lst = lst.Where(Function(f) f.ORG_NAME.ToLower().Contains(_filter.ORG_NAME.ToLower()))
            End If
            If _filter.KITCHEN_ID.HasValue Then
                lst = lst.Where(Function(f) f.KITCHEN_ID = _filter.KITCHEN_ID)
            End If
            If _filter.RATION_ID.HasValue Then
                lst = lst.Where(Function(f) f.RATION_ID = _filter.RATION_ID)
            End If
            If _filter.COST_EXTRA.HasValue Then
                lst = lst.Where(Function(f) f.COST_EXTRA = _filter.COST_EXTRA)
            End If
            If _filter.EFFECT_DATE.HasValue Then
                lst = lst.Where(Function(f) f.EFFECT_DATE = _filter.EFFECT_DATE)
            End If

            If _filter.START_DATE.HasValue Then
                lst = lst.Where(Function(f) f.EFFECT_DATE >= _filter.START_DATE)
            End If

            If _filter.END_DATE.HasValue Then
                lst = lst.Where(Function(f) f.EFFECT_DATE <= _filter.END_DATE)
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iAttendance")
            Throw ex
        End Try
    End Function

    Public Function Insert_AT_MEAL_STUDENT_BY_ORG(ByVal lstData As List(Of AT_MEAL_STUDENT_DTO),
                                                  ByVal _param As ParamDTO,
                                                  ByVal log As UserLog) As Boolean
        ' Dim obj As New AT_MEAL_STUDENT
        Try

            Using conMng As New ConnectionManager
                Using conn As New OracleConnection(conMng.GetConnectionString())
                    Using cmd As New OracleCommand()
                        Try
                            conn.Open()
                            cmd.Connection = conn
                            cmd.CommandType = CommandType.StoredProcedure
                            cmd.Transaction = cmd.Connection.BeginTransaction()
                            cmd.CommandText = "PKG_MEAL_BUSINESS.DELETE_AT_MEAL_STUDENT_BY_ORG"

                            Using resource As New DataAccess.OracleCommon()
                                Dim objParam = New With {.P_ORG_ID = _param.ORG_ID,
                                                         .P_USERNAME = log.Username,
                                                         .P_ISDISSOLVE = _param.IS_DISSOLVE,
                                                         .P_EFFECT_DATE = lstData(0).EFFECT_DATE}

                                If objParam IsNot Nothing Then
                                    For Each info As PropertyInfo In objParam.GetType().GetProperties()
                                        Dim bOut As Boolean = False
                                        Dim para = resource.GetParameter(info.Name, info.GetValue(objParam, Nothing), bOut)
                                        If para IsNot Nothing Then
                                            cmd.Parameters.Add(para)
                                        End If
                                    Next
                                End If
                                cmd.ExecuteNonQuery()
                            End Using

                            cmd.CommandText = "PKG_MEAL_BUSINESS.INSERT_AT_MEAL_STUDENT_BY_ORG"
                            For Each objData In lstData
                                cmd.Parameters.Clear()

                                Using resource As New DataAccess.OracleCommon()
                                    Dim objParam = New With {.P_ORG_ID = _param.ORG_ID,
                                                    .P_USERNAME = log.Username,
                                                    .P_ISDISSOLVE = _param.IS_DISSOLVE,
                                                    .P_MEAL_ID = objData.MEAL_ID,
                                                    .P_KITCHEN_ID = objData.KITCHEN_ID,
                                                    .P_RATION_ID = objData.RATION_ID,
                                                    .P_COST_EXTRA = objData.COST_EXTRA,
                                                    .P_IS_CHECK = objData.IS_CHECK,
                                                    .P_EFFECT_DATE = objData.EFFECT_DATE,
                                                    .P_REMARK = objData.REMARK}

                                    If objParam IsNot Nothing Then
                                        For Each info As PropertyInfo In objParam.GetType().GetProperties()
                                            Dim bOut As Boolean = False
                                            Dim para = resource.GetParameter(info.Name, info.GetValue(objParam, Nothing), bOut)
                                            If para IsNot Nothing Then
                                                cmd.Parameters.Add(para)
                                            End If
                                        Next
                                    End If
                                    cmd.ExecuteNonQuery()
                                End Using
                            Next
                            cmd.Transaction.Commit()
                        Catch ex As Exception
                            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iAttendance")
                            Throw ex
                            cmd.Transaction.Rollback()
                        Finally
                            'Dispose all resource
                            cmd.Dispose()
                            conn.Close()
                            conn.Dispose()
                        End Try
                    End Using
                End Using
            End Using

            ' gID = obj.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iAttendance")
            Throw ex
        End Try
    End Function

    Public Function Insert_AT_MEAL_STUDENT_BY_EMP(ByVal lstData As List(Of AT_MEAL_STUDENT_DTO),
                                                  ByVal lstEmp As List(Of EmployeeDTO),
                                                  ByVal _param As ParamDTO,
                                                  ByVal log As UserLog) As Boolean
        ' Dim obj As New AT_MEAL_STUDENT
        Try

            Using conMng As New ConnectionManager
                Using conn As New OracleConnection(conMng.GetConnectionString())
                    Using cmd As New OracleCommand()
                        Try
                            conn.Open()
                            cmd.Connection = conn
                            cmd.CommandType = CommandType.StoredProcedure
                            cmd.Transaction = cmd.Connection.BeginTransaction()

                            cmd.CommandText = "PKG_MEAL_BUSINESS.INSERT_AT_MEAL_STUDENT_BY_EMP"
                            For Each objData In lstData
                                cmd.Parameters.Clear()

                                Dim Emp As String = lstEmp.Select(Function(f) f.ID.ToString).Aggregate(Function(x, y) x & "," & y)

                                Using resource As New DataAccess.OracleCommon()
                                    Dim objParam = New With {.P_ORG_ID = _param.ORG_ID,
                                                    .P_USERNAME = log.Username,
                                                    .P_ISDISSOLVE = _param.IS_DISSOLVE,
                                                    .P_MEAL_ID = objData.MEAL_ID,
                                                    .P_KITCHEN_ID = objData.KITCHEN_ID,
                                                    .P_RATION_ID = objData.RATION_ID,
                                                    .P_COST_EXTRA = objData.COST_EXTRA,
                                                    .P_IS_CHECK = objData.IS_CHECK,
                                                    .P_EFFECT_DATE = objData.EFFECT_DATE,
                                                    .P_REMARK = objData.REMARK,
                                                    .LIST_EMP = Emp}

                                    If objParam IsNot Nothing Then
                                        For Each info As PropertyInfo In objParam.GetType().GetProperties()
                                            Dim bOut As Boolean = False
                                            Dim para = resource.GetParameter(info.Name, info.GetValue(objParam, Nothing), bOut)
                                            If para IsNot Nothing Then
                                                cmd.Parameters.Add(para)
                                            End If
                                        Next
                                    End If
                                    cmd.ExecuteNonQuery()
                                End Using
                            Next
                            cmd.Transaction.Commit()
                        Catch ex As Exception
                            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iAttendance")
                            Throw ex
                            cmd.Transaction.Rollback()
                        Finally
                            'Dispose all resource
                            cmd.Dispose()
                            conn.Close()
                            conn.Dispose()
                        End Try
                    End Using
                End Using
            End Using

            ' gID = obj.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iAttendance")
            Throw ex
        End Try
    End Function

    Public Function GETDATA_STUDENT_IMPORT(ByVal obj As ParamDTO, ByVal log As UserLog) As DataSet
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataSet = cls.ExecuteStore("PKG_MEAL_BUSINESS.GETDATA_STUDENT_IMPORT",
                                               New With {.P_USERNAME = log.Username.ToUpper,
                                                         .P_ORG_ID = obj.ORG_ID,
                                                         .P_ISDISSOLVE = obj.IS_DISSOLVE,
                                                         .P_STARTDATE = obj.FROMDATE,
                                                         .P_ENDDATE = obj.ENDDATE,
                                                         .P_CUR = cls.OUT_CURSOR,
                                                         .P_CUR2 = cls.OUT_CURSOR,
                                                         .P_CUR3 = cls.OUT_CURSOR,
                                                         .P_CUR4 = cls.OUT_CURSOR,
                                                         .P_KITCHEN_ID = obj.KITCHEN_ID}, False)
                Return dtData
            End Using
            Return Nothing
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iAttendance")
            Throw ex
        End Try
    End Function

    Function ImportMealStudentValidate(ByVal obj As AT_MEAL_SETUP_DTO, ByVal _effectDate As Date) As Boolean
        Try
            If obj IsNot Nothing Then
                Dim CompareDate As New Date
                If obj.TYPE_ID = 7575 Then
                    Dim day = obj.TIME_NUMBER \ 24
                    Dim hours = obj.TIME_NUMBER Mod 24
                    CompareDate = Date.Now.AddDays(day)
                    CompareDate = CompareDate.AddHours(hours)
                Else
                    CompareDate = Date.Now.AddDays(obj.TIME_NUMBER)
                End If
                If Date.Compare(CompareDate, _effectDate) >= 0 Then
                    Return False
                End If
                Dim datenow = Date.Now
                If Date.Compare(datenow.Date, _effectDate) >= 0 Then
                    Return False
                End If
            Else
                Dim datenow = Date.Now
                If Date.Compare(datenow.Date, _effectDate) >= 0 Then
                    Return False
                End If
            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ImportMealStudent(ByVal dtData As DataTable,
                                      ByVal StartDate As Date,
                                      ByVal EndDate As Date,
                                      ByRef dtError As DataTable,
                                      ByVal log As UserLog) As Boolean
        Dim lstEmp As New List(Of Decimal)
        Dim strEmp As String = ""
        Dim iRow As Integer = 10
        Try
            ' Validate server
            For Each row As DataRow In dtData.Rows
                If (From p In lstEmp Where p = row("STUDENT_ID").ToString).Count = 0 Then
                    lstEmp.Add(row("STUDENT_ID").ToString)
                End If
            Next

            strEmp = lstEmp.Select(Function(f) f.ToString).Aggregate(Function(x, y) x & "," & y)

            Using cls As New DataAccess.NonQueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_EMP2",
                                 New With {.P_USERNAME = log.Username.ToUpper,
                                           .P_EMP = strEmp})
            End Using

            Dim lstValidate = (From o In Context.AT_MEAL_SETUP
                               From a In Context.ATV_KITCHEN_ORG.Where(Function(f) f.ORG_MEAL_SETUP_ID = o.ORG_ID)
                               From e In Context.HU_STUDENT.Where(Function(f) f.ORG_ID = a.ID)
                               From chosen In Context.SE_CHOSEN_EMP.Where(Function(f) f.EMPLOYEE_ID = e.ID)
                               Select New AT_MEAL_SETUP_DTO With {.EMPLOYEE_ID = e.ID,
                                                                  .TYPE_ID = o.TYPE_ID,
                                                                  .TIME_NUMBER = o.TIME_NUMBER}).ToList

            lstEmp = New List(Of Decimal)
            If Not dtError.Columns.Contains("STT") Then
                dtError.Columns.Add("STT")
            End If
            For Each row As DataRow In dtData.Rows
                Dim obj = (From p In lstValidate Where p.EMPLOYEE_ID = row("STUDENT_ID").ToString).FirstOrDefault
                Dim isError As Boolean = False
                Dim rowError = dtError.NewRow

                Dim D1_DATE As Date = row("D1_DATE")
                Dim D2_DATE As Date = row("D2_DATE")
                Dim D3_DATE As Date = row("D3_DATE")
                Dim D4_DATE As Date = row("D4_DATE")
                Dim D5_DATE As Date = row("D5_DATE")
                Dim D6_DATE As Date = row("D6_DATE")
                Dim D7_DATE As Date = row("D7_DATE")

                If Not ImportMealManagerValidate(obj, D1_DATE) Then
                    rowError("D1_BA") = "Đã quá thời gian điều chỉnh bữa ăn"
                    isError = True
                End If

                If Not ImportMealManagerValidate(obj, D2_DATE) Then
                    rowError("D2_BA") = "Đã quá thời gian điều chỉnh bữa ăn"
                    isError = True
                End If

                If Not ImportMealManagerValidate(obj, D3_DATE) Then
                    rowError("D3_BA") = "Đã quá thời gian điều chỉnh bữa ăn"
                    isError = True
                End If

                If Not ImportMealManagerValidate(obj, D4_DATE) Then
                    rowError("D4_BA") = "Đã quá thời gian điều chỉnh bữa ăn"
                    isError = True
                End If

                If Not ImportMealManagerValidate(obj, D5_DATE) Then
                    rowError("D5_BA") = "Đã quá thời gian điều chỉnh bữa ăn"
                    isError = True
                End If

                If Not ImportMealManagerValidate(obj, D6_DATE) Then
                    rowError("D6_BA") = "Đã quá thời gian điều chỉnh bữa ăn"
                    isError = True
                End If

                If Not ImportMealManagerValidate(obj, D7_DATE) Then
                    rowError("D7_BA") = "Đã quá thời gian điều chỉnh bữa ăn"
                    isError = True
                End If

                If isError Then
                    rowError("STT") = iRow
                    rowError("STUDENT_CODE") = row("STUDENT_CODE").ToString
                    rowError("VN_FULLNAME") = row("VN_FULLNAME").ToString
                    rowError("ORG_DESC") = row("ORG_DESC").ToString
                    rowError("ORG_NAME") = row("ORG_NAME").ToString
                    If (From p In lstEmp Where p = row("STUDENT_ID").ToString).Count = 0 Then
                        lstEmp.Add(row("STUDENT_ID").ToString)
                        dtError.Rows.Add(rowError)
                    End If
                End If

                iRow += 1
            Next

            If dtError.Rows.Count > 0 Then
                Return False
            End If

            Using conMng As New ConnectionManager
                Using conn As New OracleConnection(conMng.GetConnectionString())
                    Using cmd As New OracleCommand()
                        Using resource As New DataAccess.OracleCommon()
                            Try
                                conn.Open()
                                cmd.Connection = conn
                                cmd.Transaction = cmd.Connection.BeginTransaction()
                                cmd.CommandType = CommandType.StoredProcedure
                                cmd.CommandText = "PKG_MEAL_BUSINESS.INSERT_AT_MEAL_MANAGER_TEMP"

                                For Each row As DataRow In dtData.Rows
                                    cmd.Parameters.Clear()
                                    Dim objParam = New With {.P_EMPLOYEE_ID = row("STUDENT_ID").ToString,
                                                             .P_MEAL_ID = row("MEAL_ID").ToString,
                                                             .P_D1_BA_ID = Utilities.Obj2Decima(row("D1_BA_ID"), Nothing),
                                                             .P_D2_BA_ID = Utilities.Obj2Decima(row("D2_BA_ID"), Nothing),
                                                             .P_D3_BA_ID = Utilities.Obj2Decima(row("D3_BA_ID"), Nothing),
                                                             .P_D4_BA_ID = Utilities.Obj2Decima(row("D4_BA_ID"), Nothing),
                                                             .P_D5_BA_ID = Utilities.Obj2Decima(row("D5_BA_ID"), Nothing),
                                                             .P_D6_BA_ID = Utilities.Obj2Decima(row("D6_BA_ID"), Nothing),
                                                             .P_D7_BA_ID = Utilities.Obj2Decima(row("D7_BA_ID"), Nothing),
                                                             .P_D1_SA_ID = Utilities.Obj2Decima(row("D1_SA_ID"), Nothing),
                                                             .P_D2_SA_ID = Utilities.Obj2Decima(row("D2_SA_ID"), Nothing),
                                                             .P_D3_SA_ID = Utilities.Obj2Decima(row("D3_SA_ID"), Nothing),
                                                             .P_D4_SA_ID = Utilities.Obj2Decima(row("D4_SA_ID"), Nothing),
                                                             .P_D5_SA_ID = Utilities.Obj2Decima(row("D5_SA_ID"), Nothing),
                                                             .P_D6_SA_ID = Utilities.Obj2Decima(row("D6_SA_ID"), Nothing),
                                                             .P_D7_SA_ID = Utilities.Obj2Decima(row("D7_SA_ID"), Nothing),
                                                             .P_D1_COST = Utilities.Obj2Decima(row("D1_COST"), Nothing),
                                                             .P_D2_COST = Utilities.Obj2Decima(row("D2_COST"), Nothing),
                                                             .P_D3_COST = Utilities.Obj2Decima(row("D3_COST"), Nothing),
                                                             .P_D4_COST = Utilities.Obj2Decima(row("D4_COST"), Nothing),
                                                             .P_D5_COST = Utilities.Obj2Decima(row("D5_COST"), Nothing),
                                                             .P_D6_COST = Utilities.Obj2Decima(row("D6_COST"), Nothing),
                                                             .P_D7_COST = Utilities.Obj2Decima(row("D7_COST"), Nothing),
                                                             .P_D1_C = 0,
                                                             .P_D2_C = 0,
                                                             .P_D3_C = 0,
                                                             .P_D4_C = 0,
                                                             .P_D5_C = 0,
                                                             .P_D6_C = 0,
                                                             .P_D7_C = 0,
                                                             .P_D1_DATE = ToDate(row("D1_DATE")),
                                                             .P_D2_DATE = ToDate(row("D2_DATE")),
                                                             .P_D3_DATE = ToDate(row("D3_DATE")),
                                                             .P_D4_DATE = ToDate(row("D4_DATE")),
                                                             .P_D5_DATE = ToDate(row("D5_DATE")),
                                                             .P_D6_DATE = ToDate(row("D6_DATE")),
                                                             .P_D7_DATE = ToDate(row("D7_DATE"))}

                                    If objParam IsNot Nothing Then
                                        For Each info As PropertyInfo In objParam.GetType().GetProperties()
                                            Dim bOut As Boolean = False
                                            Dim para = resource.GetParameter(info.Name, info.GetValue(objParam, Nothing), bOut)
                                            If para IsNot Nothing Then
                                                cmd.Parameters.Add(para)
                                            End If
                                        Next
                                    End If
                                    cmd.ExecuteNonQuery()
                                Next

                                cmd.CommandText = "PKG_MEAL_BUSINESS.IMPORT_AT_MEAL_STUDENT"
                                cmd.Parameters.Clear()
                                Dim objParam1 = New With {.P_STARTDATE = StartDate,
                                                          .P_ENDDATE = EndDate,
                                                        .P_USERNAME = log.Username.ToUpper}

                                If objParam1 IsNot Nothing Then
                                    For Each info As PropertyInfo In objParam1.GetType().GetProperties()
                                        Dim bOut As Boolean = False
                                        Dim para = resource.GetParameter(info.Name, info.GetValue(objParam1, Nothing), bOut)
                                        If para IsNot Nothing Then
                                            cmd.Parameters.Add(para)
                                        End If
                                    Next
                                End If

                                cmd.ExecuteNonQuery()

                                cmd.Transaction.Commit()

                            Catch ex As Exception
                                cmd.Transaction.Rollback()
                                WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iAttendance")
                                Throw ex
                            Finally
                                'Dispose all resource
                                cmd.Dispose()
                                conn.Close()
                                conn.Dispose()
                            End Try
                        End Using
                    End Using
                End Using
            End Using

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iAttendance")
            Throw ex
        End Try
    End Function

    Public Function GetListStudent_ByOrg(ByVal _param As ParamDTO, ByVal log As UserLog) As List(Of EmployeeDTO)
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username.ToUpper,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using

            Dim query As ObjectQuery(Of EmployeeDTO)
            query = (From p In Context.HU_STUDENT
                     From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                     From g In Context.SE_CHOSEN_ORG.Where(Function(f) p.ORG_ID = f.ORG_ID And
                                                                  f.USERNAME.ToUpper = log.Username.ToUpper.ToUpper)
                     Where p.JOIN_DATE.HasValue And p.JOIN_DATE <= _param.ENDDATE And p.WORK_STATUS.HasValue And _
                                     ((p.WORK_STATUS <> 257) Or (p.WORK_STATUS = 257 And p.TER_EFFECT_DATE >= _param.FROMDATE))
                     Order By p.STUDENT_CODE
                        Select New EmployeeDTO With {
                         .EMPLOYEE_CODE = p.STUDENT_CODE,
                         .ID = p.ID,
                         .FULLNAME_VN = p.FULLNAME_VN,
                         .ORG_ID = p.ORG_ID,
                         .ORG_NAME = org.NAME_VN,
                         .ORG_DESC = org.DESCRIPTION_PATH,
                         .JOIN_DATE = p.JOIN_DATE
                        })

            Return query.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iMeal")

            Throw ex
        End Try
    End Function

#End Region

#Region "AT_MEAL_FORECAST_SUM"


    Public Function Get_AT_MEAL_FORECAST_SUM(ByVal _filter As AT_MEAL_FORECAST_SUM_DTO,
                                        ByVal _param As ParamDTO,
                                        ByRef Total As Integer,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc",
                                        Optional ByVal log As UserLog = Nothing) As List(Of AT_MEAL_FORECAST_SUM_DTO)
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username.ToUpper,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using

            Dim query = From p In Context.AT_MEAL_FORECAST_SUM
                        From o In Context.HUV_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID)
                        From kitchen In Context.AT_KITCHEN.Where(Function(f) f.ID = p.KITCHEN_ID)
                        From meal In Context.AT_MEAL.Where(Function(f) f.ID = p.MEAL_ID)
                        From ration In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.RATION_ID)
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) p.ORG_ID = f.ORG_ID And
                                                                  f.USERNAME.ToUpper = log.Username.ToUpper)
                        Select New AT_MEAL_FORECAST_SUM_DTO With {.ID = p.ID,
                                                                  .ORG_ID = p.ORG_ID,
                                                                  .ORG_CODE = o.CODE,
                                                                  .ORG_NAME = o.NAME_VN,
                                                                  .ORG_PATH = o.ORG_PATH,
                                                                  .EFFECT_DATE = p.EFFECT_DATE,
                                                                  .KITCHEN_ID = p.KITCHEN_ID,
                                                                  .KITCHEN_CODE = kitchen.KITCHEN_CODE,
                                                                  .KITCHEN_NAME = kitchen.KITCHEN_NAME,
                                                                  .MEAL_ID = p.MEAL_ID,
                                                                  .MEAL_NAME = meal.NAME,
                                                                  .RATION_ID = p.RATION_ID,
                                                                  .RATION_NAME = ration.NAME_VN,
                                                                  .RATION_VALUE = p.RATION_VALUE,
                                                                  .RATION_PARTNER_VALUE = p.RATION_PARTNER_VALUE,
                                                                  .KITCHEN_PROVISION = p.KITCHEN_PROVISION,
                                                                  .RATION_PROVISION = p.RATION_PROVISION,
                                                                  .RATION_ARISE = p.RATION_ARISE,
                                                                  .RATION_TOTAL = p.RATION_TOTAL,
                                                                  .KITCHEN_TOTAL = p.KITCHEN_TOTAL,
                                                                  .REMARK = p.REMARK,
                                                                  .CREATED_BY = p.CREATED_BY,
                                                                  .CREATED_DATE = p.CREATED_DATE,
                                                                  .CREATED_LOG = p.CREATED_LOG,
                                                                  .MODIFIED_BY = p.MODIFIED_BY,
                                                                  .MODIFIED_DATE = p.MODIFIED_DATE,
                                                                  .MODIFIED_LOG = p.MODIFIED_LOG}


            Dim lst = query

            If _filter.START_DATE.HasValue Then
                lst = lst.Where(Function(f) f.EFFECT_DATE >= _filter.START_DATE)
            End If

            If _filter.END_DATE.HasValue Then
                lst = lst.Where(Function(f) f.EFFECT_DATE <= _filter.END_DATE)
            End If

            If _filter.KITCHEN_ID.HasValue Then
                lst = lst.Where(Function(f) f.KITCHEN_ID = _filter.KITCHEN_ID)
            End If

            If Not String.IsNullOrEmpty(_filter.REMARK) Then
                lst = lst.Where(Function(f) f.REMARK.ToLower().Contains(_filter.REMARK.ToLower()))
            End If

            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                lst = lst.Where(Function(f) f.ORG_NAME.ToLower().Contains(_filter.ORG_NAME.ToLower()))
            End If

            If Not String.IsNullOrEmpty(_filter.ORG_CODE) Then
                lst = lst.Where(Function(f) f.ORG_CODE.ToLower().Contains(_filter.ORG_CODE.ToLower()))
            End If

            If Not String.IsNullOrEmpty(_filter.ORG_PATH) Then
                lst = lst.Where(Function(f) f.ORG_PATH.ToLower().Contains(_filter.ORG_PATH.ToLower()))
            End If

            If Not String.IsNullOrEmpty(_filter.RATION_NAME) Then
                lst = lst.Where(Function(f) f.RATION_NAME.ToLower().Contains(_filter.RATION_NAME.ToLower()))
            End If

            If Not String.IsNullOrEmpty(_filter.MEAL_NAME) Then
                lst = lst.Where(Function(f) f.MEAL_NAME.ToLower().Contains(_filter.MEAL_NAME.ToLower()))
            End If

            If Not String.IsNullOrEmpty(_filter.KITCHEN_NAME) Then
                lst = lst.Where(Function(f) f.KITCHEN_NAME.ToLower().Contains(_filter.KITCHEN_NAME.ToLower()))
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iAttendance")
            Throw ex
        End Try
    End Function

    Public Function CAL_AT_MEAL_FORECAST_SUM(ByVal _param As ParamDTO,
                                        ByVal log As UserLog) As Boolean
        Try
            Using cls As New DataAccess.NonQueryData
                cls.ExecuteStore("PKG_MEAL_BUSINESS.CAL_AT_MEAL_FORECAST",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORG_ID = _param.ORG_ID,
                                           .P_FROMDATE = _param.FROMDATE,
                                           .P_ENDDATE = _param.ENDDATE,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iAttendance")
            Throw ex
        End Try
    End Function


    Public Function Get_AT_MEAL_FORECAST_SUM_IMPORT(ByVal _param As AT_MEAL_FORECAST_SUM_DTO,
                                           ByVal log As UserLog) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_MEAL_BUSINESS.GET_AT_MEAL_FORECAST_IMPORT",
                                                           New With {.P_USERNAME = log.Username.ToUpper,
                                                                     .P_ORG_ID = _param.ORG_ID,
                                                                     .P_KITCHEN_ID = _param.KITCHEN_ID,
                                                                     .P_KITCHEN_NAME = _param.KITCHEN_NAME,
                                                                     .P_ORG_PATH = _param.ORG_PATH,
                                                                     .P_ORG_NAME = _param.ORG_NAME,
                                                                     .P_MEAL_NAME = _param.MEAL_NAME,
                                                                     .P_RATION_NAME = _param.RATION_NAME,
                                                                     .P_REMARK = _param.REMARK,
                                                                     .P_FROMDATE = _param.START_DATE,
                                                                     .P_ENDDATE = _param.END_DATE,
                                                                     .P_IS_DISSOLVE = _param.IS_DISSOLVE,
                                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function Import_AT_MEAL_FORECAST_SUM(ByVal lstData As List(Of AT_MEAL_FORECAST_SUM_DTO),
                                                ByVal log As UserLog) As Boolean
        Try
            For Each obj In lstData
                Dim objData = (From p In Context.AT_MEAL_FORECAST_SUM
                               Where p.KITCHEN_ID = obj.KITCHEN_ID And
                               p.ORG_ID = obj.ORG_ID And
                               p.EFFECT_DATE = obj.EFFECT_DATE And
                               p.RATION_ID = obj.RATION_ID And
                               p.MEAL_ID = obj.MEAL_ID).FirstOrDefault

                If objData IsNot Nothing Then
                    objData.RATION_ARISE = obj.RATION_ARISE
                    objData.REMARK = obj.REMARK
                    objData.KITCHEN_TOTAL = objData.RATION_TOTAL + obj.RATION_ARISE
                End If
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".InsertPlanReg")
            Throw ex
        End Try
    End Function


#End Region

#Region "AT_MEAL_PARTNER"

    Public Function Insert_AT_MEAL_PARTNER(ByVal objData As AT_MEAL_PARTNER_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim fromdate As Date?
        Try

            fromdate = objData.START_DATE
            While fromdate <= objData.END_DATE
                Dim obj As New AT_MEAL_PARTNER
                obj.ID = Utilities.GetNextSequence(Context, Context.AT_MEAL_PARTNER.EntitySet.Name)
                obj.EMPLOYEE_ID = objData.EMPLOYEE_ID
                obj.ORG_COST_ID = objData.ORG_COST_ID
                obj.ORG_MEAL_ID = objData.ORG_MEAL_ID
                obj.MEAL_ID = objData.MEAL_ID
                obj.KITCHEN_ID = objData.KITCHEN_ID
                obj.RATION_ID = objData.RATION_ID
                obj.EFFECT_DATE = fromdate
                obj.MEAL_VALUE = objData.MEAL_VALUE
                obj.PARTNER_TYPE = 8004
                obj.REMARK = objData.REMARK
                obj.PARTNER_NAME = objData.PARTNER_NAME
                Context.AT_MEAL_PARTNER.AddObject(obj)

                fromdate = fromdate.Value.AddDays(1)
            End While
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iAttendance")
            Throw ex
        End Try
    End Function

    Public Function Modify_AT_MEAL_PARTNER(ByVal objData As AT_MEAL_PARTNER_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim obj As New AT_MEAL_PARTNER With {.ID = objData.ID}
        Try
            obj = (From p In Context.AT_MEAL_PARTNER Where p.ID = objData.ID).FirstOrDefault
            obj.EMPLOYEE_ID = objData.EMPLOYEE_ID
            obj.ORG_COST_ID = objData.ORG_COST_ID
            obj.ORG_MEAL_ID = objData.ORG_MEAL_ID
            obj.MEAL_ID = objData.MEAL_ID
            obj.KITCHEN_ID = objData.KITCHEN_ID
            obj.RATION_ID = objData.RATION_ID
            obj.EFFECT_DATE = objData.EFFECT_DATE
            obj.MEAL_VALUE = objData.MEAL_VALUE
            obj.PARTNER_TYPE = 8004
            obj.REMARK = objData.REMARK
            obj.PARTNER_NAME = objData.PARTNER_NAME
            Context.SaveChanges(log)
            gID = obj.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iAttendance")
            Throw ex
        End Try
    End Function

    Public Function Delete_AT_MEAL_PARTNER(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lst As List(Of AT_MEAL_PARTNER)
        Try
            lst = (From p In Context.AT_MEAL_PARTNER Where lstID.Contains(p.ID)).ToList
            For index = 0 To lst.Count - 1
                Context.AT_MEAL_PARTNER.DeleteObject(lst(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iAttendance")
            Throw ex
        End Try
    End Function

    Public Function Get_AT_MEAL_PARTNERById(ByVal _id As Decimal?) As AT_MEAL_PARTNER_DTO
        Try

            Dim query = From p In Context.AT_MEAL_PARTNER
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID).DefaultIfEmpty
                        From orgcost In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_COST_ID).DefaultIfEmpty
                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From orgmeal In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_MEAL_ID).DefaultIfEmpty
                        From m In Context.AT_MEAL.Where(Function(f) f.ID = p.MEAL_ID).DefaultIfEmpty
                        From o In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.RATION_ID).DefaultIfEmpty
                        From o1 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.PARTNER_TYPE).DefaultIfEmpty
                        From k In Context.AT_KITCHEN.Where(Function(f) f.ID = p.KITCHEN_ID).DefaultIfEmpty
                        Where p.ID = _id

            Dim lst = query.Select(Function(p) New AT_MEAL_PARTNER_DTO With {
                                        .ID = p.p.ID,
                                        .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                        .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                        .EMPLOYEE_NAME = p.e.FULLNAME_VN,
                                        .ORG_NAME = p.org.NAME_VN,
                                        .ORG_DESC = p.org.DESCRIPTION_PATH,
                                        .ORG_COST_ID = p.p.ORG_COST_ID,
                                        .ORG_COST_NAME = p.orgcost.NAME_VN,
                                        .ORG_COST_DESC = p.orgcost.DESCRIPTION_PATH,
                                        .ORG_MEAL_ID = p.p.ORG_MEAL_ID,
                                        .ORG_MEAL_NAME = p.orgmeal.NAME_VN,
                                        .ORG_MEAL_DESC = p.orgmeal.DESCRIPTION_PATH,
                                        .MEAL_ID = p.p.MEAL_ID,
                                        .MEAL_NAME = p.m.NAME,
                                        .KITCHEN_ID = p.p.KITCHEN_ID,
                                        .KITCHEN_NAME = p.k.KITCHEN_NAME,
                                        .RATION_ID = p.p.RATION_ID,
                                        .RATION_NAME = p.o.NAME_VN,
                                        .EFFECT_DATE = p.p.EFFECT_DATE,
                                        .CREATED_DATE = p.p.CREATED_DATE,
                                        .CREATED_BY = p.p.CREATED_BY,
                                        .CREATED_LOG = p.p.CREATED_LOG,
                                        .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                        .MODIFIED_BY = p.p.MODIFIED_BY,
                                        .MODIFIED_LOG = p.p.MODIFIED_LOG,
                                        .PARTNER_TYPE = p.p.PARTNER_TYPE,
                                        .PARTNER_TYPE_NAME = p.o1.NAME_VN,
                                        .REMARK = p.p.REMARK,
                                        .PARTNER_NAME = p.p.PARTNER_NAME,
                                        .MEAL_VALUE = p.p.MEAL_VALUE}).FirstOrDefault
            Return lst
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iAttendance")
            Throw ex
        End Try
    End Function

    Public Function Get_AT_MEAL_PARTNER(ByVal _filter As AT_MEAL_PARTNER_DTO,
                                        ByVal _param As ParamDTO,
                                        Optional ByVal PageIndex As Integer = 0,
                                        Optional ByVal PageSize As Integer = Integer.MaxValue,
                                        Optional ByRef Total As Integer = 0,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc",
                                        Optional ByVal log As UserLog = Nothing) As List(Of AT_MEAL_PARTNER_DTO)
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username.ToUpper,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using

            Dim query = From p In Context.AT_MEAL_PARTNER
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID).DefaultIfEmpty
                        From orgcost In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_COST_ID).DefaultIfEmpty
                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From orgmeal In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_MEAL_ID).DefaultIfEmpty
                        From m In Context.AT_MEAL.Where(Function(f) f.ID = p.MEAL_ID).DefaultIfEmpty
                        From o In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.RATION_ID).DefaultIfEmpty
                        From o1 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.PARTNER_TYPE).DefaultIfEmpty
                        From k In Context.AT_KITCHEN.Where(Function(f) f.ID = p.KITCHEN_ID).DefaultIfEmpty
                        From g In Context.SE_CHOSEN_ORG.Where(Function(f) e.ORG_ID = f.ORG_ID And
                                                                  f.USERNAME.ToUpper = log.Username.ToUpper.ToUpper)

            Dim lst = query.Select(Function(p) New AT_MEAL_PARTNER_DTO With {
                                        .ID = p.p.ID,
                                        .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                        .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                        .EMPLOYEE_NAME = p.e.FULLNAME_VN,
                                        .ORG_NAME = p.org.NAME_VN,
                                        .ORG_COST_ID = p.p.ORG_COST_ID,
                                        .ORG_COST_NAME = p.orgcost.NAME_VN,
                                        .ORG_MEAL_ID = p.p.ORG_MEAL_ID,
                                        .ORG_MEAL_NAME = p.orgmeal.NAME_VN,
                                        .ORG_MEAL_DESC = p.orgmeal.DESCRIPTION_PATH,
                                        .MEAL_ID = p.p.MEAL_ID,
                                        .MEAL_NAME = p.m.NAME,
                                        .KITCHEN_ID = p.p.KITCHEN_ID,
                                        .KITCHEN_NAME = p.k.KITCHEN_NAME,
                                        .RATION_ID = p.p.RATION_ID,
                                        .RATION_NAME = p.o.NAME_VN,
                                        .EFFECT_DATE = p.p.EFFECT_DATE,
                                        .CREATED_DATE = p.p.CREATED_DATE,
                                        .CREATED_BY = p.p.CREATED_BY,
                                        .CREATED_LOG = p.p.CREATED_LOG,
                                        .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                        .MODIFIED_BY = p.p.MODIFIED_BY,
                                        .MODIFIED_LOG = p.p.MODIFIED_LOG,
                                        .PARTNER_TYPE = p.p.PARTNER_TYPE,
                                        .PARTNER_TYPE_NAME = p.o1.NAME_VN,
                                        .REMARK = p.p.REMARK,
                                         .PARTNER_NAME = p.p.PARTNER_NAME,
                                        .MEAL_VALUE = p.p.MEAL_VALUE})

            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                lst = lst.Where(Function(f) f.EMPLOYEE_CODE.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_NAME) Then
                lst = lst.Where(Function(f) f.EMPLOYEE_NAME.ToLower().Contains(_filter.EMPLOYEE_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.MEAL_NAME) Then
                lst = lst.Where(Function(f) f.MEAL_NAME.ToLower().Contains(_filter.MEAL_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.KITCHEN_NAME) Then
                lst = lst.Where(Function(f) f.KITCHEN_NAME.ToLower().Contains(_filter.KITCHEN_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ORG_COST_NAME) Then
                lst = lst.Where(Function(f) f.ORG_COST_NAME.ToLower().Contains(_filter.ORG_COST_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ORG_MEAL_NAME) Then
                lst = lst.Where(Function(f) f.ORG_MEAL_NAME.ToLower().Contains(_filter.ORG_MEAL_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.RATION_NAME) Then
                lst = lst.Where(Function(f) f.RATION_NAME.ToLower().Contains(_filter.RATION_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.PARTNER_TYPE_NAME) Then
                lst = lst.Where(Function(f) f.PARTNER_TYPE_NAME.ToLower().Contains(_filter.PARTNER_TYPE_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.REMARK) Then
                lst = lst.Where(Function(f) f.REMARK.ToLower().Contains(_filter.REMARK.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.PARTNER_NAME) Then
                lst = lst.Where(Function(f) f.PARTNER_NAME.ToLower().Contains(_filter.PARTNER_NAME.ToLower()))
            End If
            If _filter.ORG_COST_ID.HasValue Then
                lst = lst.Where(Function(f) f.ORG_COST_ID = _filter.ORG_COST_ID)
            End If
            If _filter.MEAL_ID.HasValue Then
                lst = lst.Where(Function(f) f.MEAL_ID = _filter.MEAL_ID)
            End If
            If _filter.KITCHEN_ID.HasValue Then
                lst = lst.Where(Function(f) f.KITCHEN_ID = _filter.KITCHEN_ID)
            End If
            If _filter.RATION_ID.HasValue Then
                lst = lst.Where(Function(f) f.RATION_ID = _filter.RATION_ID)
            End If
            If _filter.EFFECT_DATE.HasValue Then
                lst = lst.Where(Function(f) f.EFFECT_DATE = _filter.EFFECT_DATE)
            End If
            If _filter.START_DATE.HasValue Then
                lst = lst.Where(Function(f) f.EFFECT_DATE >= _filter.START_DATE)
            End If
            If _filter.END_DATE.HasValue Then
                lst = lst.Where(Function(f) f.EFFECT_DATE <= _filter.END_DATE)
            End If
            If _filter.MEAL_VALUE.HasValue Then
                lst = lst.Where(Function(f) f.MEAL_VALUE = _filter.MEAL_VALUE)
            End If
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Return lst.ToList()
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iAttendance")
            Throw ex
        End Try
    End Function


#End Region

#Region "AT_MEAL_CHANGE"

    Public Function Insert_AT_MEAL_CHANGE(ByVal objData As AT_MEAL_CHANGE_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim obj As New AT_MEAL_CHANGE
        Dim lst As New List(Of Decimal)
        Try
            obj.ID = Utilities.GetNextSequence(Context, Context.AT_MEAL_CHANGE.EntitySet.Name)
            obj.EMPLOYEE_ID = objData.EMPLOYEE_ID
            obj.EFFECT_DATE = objData.EFFECT_DATE

            obj.MEAL_ID = objData.MEAL_ID
            obj.KITCHEN_ID = objData.KITCHEN_ID
            obj.RATION_ID = objData.RATION_ID
            obj.COST_EXTRA = objData.COST_EXTRA
            obj.IS_CHECK = objData.IS_CHECK
            obj.REMARK = objData.REMARK

            obj.MEAL_ID_NEW = objData.MEAL_ID_NEW
            obj.KITCHEN_ID_NEW = objData.KITCHEN_ID_NEW
            obj.RATION_ID_NEW = objData.RATION_ID_NEW
            obj.COST_EXTRA_NEW = objData.COST_EXTRA_NEW
            obj.IS_CHECK_NEW = objData.IS_CHECK_NEW
            obj.REMARK_NEW = objData.REMARK_NEW

            obj.ORG_CHANGE_ID = objData.ORG_CHANGE_ID
            obj.IS_MISSION = objData.IS_MISSION
            obj.EMPLOYEE_REQUEST_ID = objData.EMPLOYEE_REQUEST_ID
            obj.ORG_COST_ID = objData.ORG_COST_ID
            obj.STATUS_ID = AttendanceCommon.OT_MEAL_CHANGE_STATUS.WAIT_APPROVE_ID

            Context.AT_MEAL_CHANGE.AddObject(obj)
            Context.SaveChanges(log)
            gID = obj.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iAttendance")
            Throw ex
        End Try
    End Function

    Public Function Modify_AT_MEAL_CHANGE(ByVal objData As AT_MEAL_CHANGE_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim obj As New AT_MEAL_CHANGE With {.ID = objData.ID}
        Try
            obj = (From p In Context.AT_MEAL_CHANGE Where p.ID = objData.ID).FirstOrDefault
            obj.EMPLOYEE_ID = objData.EMPLOYEE_ID
            obj.EFFECT_DATE = objData.EFFECT_DATE

            obj.MEAL_ID = objData.MEAL_ID
            obj.KITCHEN_ID = objData.KITCHEN_ID
            obj.RATION_ID = objData.RATION_ID
            obj.COST_EXTRA = objData.COST_EXTRA
            obj.IS_CHECK = objData.IS_CHECK
            obj.REMARK = objData.REMARK

            obj.MEAL_ID_NEW = objData.MEAL_ID_NEW
            obj.KITCHEN_ID_NEW = objData.KITCHEN_ID_NEW
            obj.RATION_ID_NEW = objData.RATION_ID_NEW
            obj.COST_EXTRA_NEW = objData.COST_EXTRA_NEW
            obj.IS_CHECK_NEW = objData.IS_CHECK_NEW
            obj.REMARK_NEW = objData.REMARK_NEW

            obj.ORG_CHANGE_ID = objData.ORG_CHANGE_ID
            obj.IS_MISSION = objData.IS_MISSION
            obj.EMPLOYEE_REQUEST_ID = objData.EMPLOYEE_REQUEST_ID
            obj.ORG_COST_ID = objData.ORG_COST_ID

            Context.SaveChanges(log)
            gID = obj.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iAttendance")
            Throw ex
        End Try
    End Function

    Public Function Delete_AT_MEAL_CHANGE(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean
        Dim lst As List(Of AT_MEAL_CHANGE)
        Try
            lst = (From p In Context.AT_MEAL_CHANGE Where lstID.Contains(p.ID)).ToList
            For index = 0 To lst.Count - 1
                Context.AT_MEAL_CHANGE.DeleteObject(lst(index))
            Next
            Context.SaveChanges(log)
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iAttendance")
            Throw ex
        End Try
    End Function

    Public Function Get_AT_MEAL_CHANGEById(ByVal _id As Decimal?) As AT_MEAL_CHANGE_DTO
        Try

            Dim query = From p In Context.AT_MEAL_CHANGE
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID).DefaultIfEmpty
                        From request In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_REQUEST_ID).DefaultIfEmpty
                        From orgchange In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_CHANGE_ID).DefaultIfEmpty
                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From orgcost In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_COST_ID).DefaultIfEmpty
                        From m In Context.AT_MEAL.Where(Function(f) f.ID = p.MEAL_ID).DefaultIfEmpty
                        From m1 In Context.AT_MEAL.Where(Function(f) f.ID = p.MEAL_ID_NEW).DefaultIfEmpty
                        From o In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.RATION_ID).DefaultIfEmpty
                        From o1 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.RATION_ID_NEW).DefaultIfEmpty
                        From k In Context.AT_KITCHEN.Where(Function(f) f.ID = p.KITCHEN_ID).DefaultIfEmpty
                        From k1 In Context.AT_KITCHEN.Where(Function(f) f.ID = p.KITCHEN_ID_NEW).DefaultIfEmpty
                        From ms In Context.AT_MEAL_COST_SETUP.Where(Function(f) f.KITCHEN_ID = p.KITCHEN_ID And
                                                                        f.RATION_ID = p.RATION_ID And
                                                                        f.MEAL_ID = p.MEAL_ID).DefaultIfEmpty
                        From ms1 In Context.AT_MEAL_COST_SETUP.Where(Function(f) f.KITCHEN_ID = p.KITCHEN_ID_NEW And
                                                                        f.RATION_ID = p.RATION_ID_NEW And
                                                                        f.MEAL_ID = p.MEAL_ID_NEW).DefaultIfEmpty
                        Where p.ID = _id
                        Select New AT_MEAL_CHANGE_DTO With {
                                       .ID = p.ID,
                                       .IS_MISSION = p.IS_MISSION,
                                       .ORG_CHANGE_ID = p.ORG_CHANGE_ID,
                                       .ORG_CHANGE_CODE = orgchange.CODE,
                                       .ORG_CHANGE_NAME = orgchange.NAME_VN,
                                       .EMPLOYEE_REQUEST_ID = p.EMPLOYEE_REQUEST_ID,
                                       .EMPLOYEE_REQUEST_NAME = request.FULLNAME_VN,
                                       .EMPLOYEE_REQUEST_CODE = request.EMPLOYEE_CODE,
                                       .ORG_COST_ID = p.ORG_CHANGE_ID,
                                       .ORG_COST_NAME = orgcost.NAME_VN,
                                       .ORG_COST_DESC = orgcost.DESCRIPTION_PATH,
                                       .ORG_ID = e.ORG_ID,
                                       .ORG_CODE = org.CODE,
                                       .ORG_NAME = org.NAME_VN,
                                       .EMPLOYEE_ID = p.EMPLOYEE_ID,
                                       .EMPLOYEE_NAME = e.FULLNAME_VN,
                                       .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                                       .EFFECT_DATE = p.EFFECT_DATE,
                                       .MEAL_ID = p.MEAL_ID,
                                       .MEAL_NAME = m.NAME,
                                       .KITCHEN_ID = p.KITCHEN_ID,
                                       .KITCHEN_NAME = k.KITCHEN_NAME,
                                       .RATION_ID = p.RATION_ID,
                                       .RATION_NAME = o.NAME_VN,
                                       .COST_EXTRA = p.COST_EXTRA,
                                       .IS_CHECK = p.IS_CHECK,
                                       .REMARK = p.REMARK,
                                       .COST = ms.COST,
                                       .MEAL_ID_NEW = p.MEAL_ID_NEW,
                                       .MEAL_NAME_NEW = m1.NAME,
                                       .KITCHEN_ID_NEW = p.KITCHEN_ID_NEW,
                                       .KITCHEN_NAME_NEW = k1.KITCHEN_NAME,
                                       .RATION_ID_NEW = p.RATION_ID_NEW,
                                       .RATION_NAME_NEW = o1.NAME_VN,
                                       .COST_EXTRA_NEW = p.COST_EXTRA_NEW,
                                       .CREATED_DATE = p.CREATED_DATE,
                                       .CREATED_BY = p.CREATED_BY,
                                       .CREATED_LOG = p.CREATED_LOG,
                                       .MODIFIED_DATE = p.MODIFIED_DATE,
                                       .MODIFIED_BY = p.MODIFIED_BY,
                                       .MODIFIED_LOG = p.MODIFIED_LOG,
                                       .IS_CHECK_NEW = p.IS_CHECK_NEW,
                                       .STATUS_ID = p.STATUS_ID,
                                       .REMARK_NEW = p.REMARK_NEW,
                                       .COST_NEW = ms1.COST}

            Return query.FirstOrDefault
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iAttendance")
            Throw ex
        End Try
    End Function

    Public Function Get_AT_MEAL_CHANGE(ByVal _filter As AT_MEAL_CHANGE_DTO,
                                           ByVal _param As ParamDTO,
                                           Optional ByVal PageIndex As Integer = 0,
                                           Optional ByVal PageSize As Integer = Integer.MaxValue,
                                           Optional ByRef Total As Integer = 0,
                                           Optional ByVal Sorts As String = "CREATED_DATE desc",
                                           Optional ByVal log As UserLog = Nothing) As List(Of AT_MEAL_CHANGE_DTO)
        Try

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username.ToUpper,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using

            Dim query = From p In Context.AT_MEAL_CHANGE
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID).DefaultIfEmpty
                        From request In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_REQUEST_ID).DefaultIfEmpty
                        From orgchange In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_CHANGE_ID).DefaultIfEmpty
                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From orgcost In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_COST_ID).DefaultIfEmpty
                        From m In Context.AT_MEAL.Where(Function(f) f.ID = p.MEAL_ID).DefaultIfEmpty
                        From m1 In Context.AT_MEAL.Where(Function(f) f.ID = p.MEAL_ID_NEW).DefaultIfEmpty
                        From o In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.RATION_ID).DefaultIfEmpty
                        From o1 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.RATION_ID_NEW).DefaultIfEmpty
                        From status In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.STATUS_ID).DefaultIfEmpty
                        From k In Context.AT_KITCHEN.Where(Function(f) f.ID = p.KITCHEN_ID).DefaultIfEmpty
                        From k1 In Context.AT_KITCHEN.Where(Function(f) f.ID = p.KITCHEN_ID_NEW).DefaultIfEmpty
                        From g In Context.SE_CHOSEN_ORG.Where(Function(f) (e.ORG_ID = f.ORG_ID) And
                                                                  f.USERNAME.ToUpper = log.Username.ToUpper.ToUpper)
                        From ms In Context.AT_MEAL_COST_SETUP.Where(Function(f) f.KITCHEN_ID = p.KITCHEN_ID And
                                                                        f.RATION_ID = p.RATION_ID And
                                                                        f.MEAL_ID = p.MEAL_ID).DefaultIfEmpty
                        From ms1 In Context.AT_MEAL_COST_SETUP.Where(Function(f) f.KITCHEN_ID = p.KITCHEN_ID_NEW And
                                                                        f.RATION_ID = p.RATION_ID_NEW And
                                                                        f.MEAL_ID = p.MEAL_ID_NEW).DefaultIfEmpty
                        Select New AT_MEAL_CHANGE_DTO With {
                                       .ID = p.ID,
                                       .EMPLOYEE_ID = p.EMPLOYEE_ID,
                                       .EMPLOYEE_NAME = e.FULLNAME_VN,
                                       .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                                       .EMPLOYEE_REQUEST_ID = p.EMPLOYEE_REQUEST_ID,
                                       .EMPLOYEE_REQUEST_NAME = request.FULLNAME_VN,
                                       .EMPLOYEE_REQUEST_CODE = request.EMPLOYEE_CODE,
                                       .IS_MISSION = p.IS_MISSION,
                                       .ORG_CHANGE_ID = p.ORG_CHANGE_ID,
                                       .ORG_CHANGE_NAME = orgchange.NAME_VN,
                                       .ORG_CHANGE_DESC = orgchange.DESCRIPTION_PATH,
                                       .ORG_COST_ID = p.ORG_CHANGE_ID,
                                       .ORG_COST_NAME = orgcost.NAME_VN,
                                       .ORG_COST_DESC = orgcost.DESCRIPTION_PATH,
                                       .ORG_ID = e.ORG_ID,
                                       .ORG_CODE = org.CODE,
                                       .ORG_NAME = org.NAME_VN,
                                       .ORG_DESC = org.DESCRIPTION_PATH,
                                       .EFFECT_DATE = p.EFFECT_DATE,
                                       .MEAL_ID = p.MEAL_ID,
                                       .MEAL_NAME = m.NAME,
                                       .KITCHEN_ID = p.KITCHEN_ID,
                                       .KITCHEN_NAME = k.KITCHEN_NAME,
                                       .RATION_ID = p.RATION_ID,
                                       .RATION_NAME = o.NAME_VN,
                                       .COST_EXTRA = p.COST_EXTRA,
                                       .IS_CHECK = p.IS_CHECK,
                                       .REMARK = p.REMARK,
                                       .COST = ms.COST,
                                       .MEAL_ID_NEW = p.MEAL_ID_NEW,
                                       .MEAL_NAME_NEW = m1.NAME,
                                       .KITCHEN_ID_NEW = p.KITCHEN_ID_NEW,
                                       .KITCHEN_NAME_NEW = k1.KITCHEN_NAME,
                                       .RATION_ID_NEW = p.RATION_ID_NEW,
                                       .RATION_NAME_NEW = o1.NAME_VN,
                                       .COST_EXTRA_NEW = p.COST_EXTRA_NEW,
                                       .CREATED_DATE = p.CREATED_DATE,
                                       .CREATED_BY = p.CREATED_BY,
                                       .CREATED_LOG = p.CREATED_LOG,
                                       .MODIFIED_DATE = p.MODIFIED_DATE,
                                       .MODIFIED_BY = p.MODIFIED_BY,
                                       .MODIFIED_LOG = p.MODIFIED_LOG,
                                       .IS_CHECK_NEW = p.IS_CHECK_NEW,
                                       .REMARK_NEW = p.REMARK_NEW,
                                       .COST_NEW = ms1.COST,
                                       .STATUS_ID = p.STATUS_ID,
                                       .STATUS_NAME = status.NAME_VN,
                                       .REASON = p.REASON}

            Dim lst = query

            If _filter.EMPLOYEE_ID.HasValue Then
                lst = lst.Where(Function(f) f.EMPLOYEE_ID = _filter.EMPLOYEE_ID)
            End If

            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                lst = lst.Where(Function(f) f.EMPLOYEE_CODE.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower()))
            End If

            If Not String.IsNullOrEmpty(_filter.REASON) Then
                lst = lst.Where(Function(f) f.REASON.ToLower().Contains(_filter.REASON.ToLower()))
            End If

            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_NAME) Then
                lst = lst.Where(Function(f) f.EMPLOYEE_NAME.ToLower().Contains(_filter.EMPLOYEE_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.MEAL_NAME) Then
                lst = lst.Where(Function(f) f.MEAL_NAME.ToLower().Contains(_filter.MEAL_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.MEAL_NAME_NEW) Then
                lst = lst.Where(Function(f) f.MEAL_NAME_NEW.ToLower().Contains(_filter.MEAL_NAME_NEW.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.KITCHEN_NAME) Then
                lst = lst.Where(Function(f) f.KITCHEN_NAME.ToLower().Contains(_filter.KITCHEN_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.KITCHEN_NAME_NEW) Then
                lst = lst.Where(Function(f) f.KITCHEN_NAME_NEW.ToLower().Contains(_filter.KITCHEN_NAME_NEW.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.RATION_NAME) Then
                lst = lst.Where(Function(f) f.RATION_NAME.ToLower().Contains(_filter.RATION_NAME.ToLower()))
            End If

            If Not String.IsNullOrEmpty(_filter.RATION_NAME_NEW) Then
                lst = lst.Where(Function(f) f.RATION_NAME_NEW.ToLower().Contains(_filter.RATION_NAME_NEW.ToLower()))
            End If

            If Not String.IsNullOrEmpty(_filter.STATUS_NAME) Then
                lst = lst.Where(Function(f) f.STATUS_NAME.ToLower().Contains(_filter.STATUS_NAME.ToLower()))
            End If

            If _filter.COST_EXTRA.HasValue Then
                lst = lst.Where(Function(f) f.COST_EXTRA = _filter.COST_EXTRA)
            End If
            If _filter.COST.HasValue Then
                lst = lst.Where(Function(f) f.COST = _filter.COST)
            End If
            If _filter.COST_EXTRA_NEW.HasValue Then
                lst = lst.Where(Function(f) f.COST_EXTRA_NEW = _filter.COST_EXTRA_NEW)
            End If

            If _filter.COST_NEW.HasValue Then
                lst = lst.Where(Function(f) f.COST_NEW = _filter.COST_NEW)
            End If

            If _filter.EFFECT_DATE.HasValue Then
                lst = lst.Where(Function(f) f.EFFECT_DATE = _filter.EFFECT_DATE)
            End If

            If Not String.IsNullOrEmpty(_filter.REMARK) Then
                lst = lst.Where(Function(f) f.REMARK.ToLower().Contains(_filter.REMARK.ToLower()))
            End If

            If Not String.IsNullOrEmpty(_filter.REMARK_NEW) Then
                lst = lst.Where(Function(f) f.REMARK_NEW.ToLower().Contains(_filter.REMARK_NEW.ToLower()))
            End If

            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_REQUEST_NAME) Then
                lst = lst.Where(Function(f) f.EMPLOYEE_REQUEST_NAME.ToLower().Contains(_filter.EMPLOYEE_REQUEST_NAME.ToLower()))
            End If

            If Not String.IsNullOrEmpty(_filter.ORG_COST_NAME) Then
                lst = lst.Where(Function(f) f.ORG_COST_NAME.ToLower().Contains(_filter.ORG_COST_NAME.ToLower()))
            End If

            If Not String.IsNullOrEmpty(_filter.ORG_CHANGE_NAME) Then
                lst = lst.Where(Function(f) f.ORG_CHANGE_NAME.ToLower().Contains(_filter.ORG_CHANGE_NAME.ToLower()))
            End If

            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                lst = lst.Where(Function(f) f.ORG_NAME.ToLower().Contains(_filter.ORG_NAME.ToLower()))
            End If

            If _filter.IS_MISSION.HasValue Then
                lst = lst.Where(Function(f) f.IS_MISSION = _filter.IS_MISSION)
            End If

            If _filter.START_DATE.HasValue Then
                lst = lst.Where(Function(f) f.EFFECT_DATE >= _filter.START_DATE)
            End If

            If _filter.END_DATE.HasValue Then
                lst = lst.Where(Function(f) f.EFFECT_DATE <= _filter.END_DATE)
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iAttendance")
            Throw ex
        End Try
    End Function

    Public Function Get_AT_MEAL_CHANGEApprove(ByVal _filter As AT_MEAL_CHANGE_DTO,
                                           ByVal _param As ParamDTO,
                                           Optional ByVal PageIndex As Integer = 0,
                                           Optional ByVal PageSize As Integer = Integer.MaxValue,
                                           Optional ByRef Total As Integer = 0,
                                           Optional ByVal Sorts As String = "CREATED_DATE desc",
                                           Optional ByVal log As UserLog = Nothing) As List(Of AT_MEAL_CHANGE_DTO)
        Try

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username.ToUpper,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using

            Dim query = From p In Context.AT_MEAL_CHANGE
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID).DefaultIfEmpty
                        From request In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_REQUEST_ID).DefaultIfEmpty
                        From orgchange In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_CHANGE_ID).DefaultIfEmpty
                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From orgcost In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_COST_ID).DefaultIfEmpty
                        From m In Context.AT_MEAL.Where(Function(f) f.ID = p.MEAL_ID).DefaultIfEmpty
                        From m1 In Context.AT_MEAL.Where(Function(f) f.ID = p.MEAL_ID_NEW).DefaultIfEmpty
                        From o In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.RATION_ID).DefaultIfEmpty
                        From o1 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.RATION_ID_NEW).DefaultIfEmpty
                        From status In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.STATUS_ID).DefaultIfEmpty
                        From k In Context.AT_KITCHEN.Where(Function(f) f.ID = p.KITCHEN_ID).DefaultIfEmpty
                        From k1 In Context.AT_KITCHEN.Where(Function(f) f.ID = p.KITCHEN_ID_NEW).DefaultIfEmpty
                        From g In Context.SE_CHOSEN_ORG.Where(Function(f) ((p.IS_MISSION = False And f.ORG_ID = e.ORG_ID) Or
                                                                           (p.IS_MISSION = True And f.ORG_ID = p.ORG_CHANGE_ID)) And
                                                                  f.USERNAME.ToUpper = log.Username.ToUpper.ToUpper)
                        From ms In Context.AT_MEAL_COST_SETUP.Where(Function(f) f.KITCHEN_ID = p.KITCHEN_ID And
                                                                        f.RATION_ID = p.RATION_ID And
                                                                        f.MEAL_ID = p.MEAL_ID).DefaultIfEmpty
                        From ms1 In Context.AT_MEAL_COST_SETUP.Where(Function(f) f.KITCHEN_ID = p.KITCHEN_ID_NEW And
                                                                        f.RATION_ID = p.RATION_ID_NEW And
                                                                        f.MEAL_ID = p.MEAL_ID_NEW).DefaultIfEmpty
                        Select New AT_MEAL_CHANGE_DTO With {
                                       .ID = p.ID,
                                       .EMPLOYEE_ID = p.EMPLOYEE_ID,
                                       .EMPLOYEE_NAME = e.FULLNAME_VN,
                                       .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                                       .EMPLOYEE_REQUEST_ID = p.EMPLOYEE_REQUEST_ID,
                                       .EMPLOYEE_REQUEST_NAME = request.FULLNAME_VN,
                                       .EMPLOYEE_REQUEST_CODE = request.EMPLOYEE_CODE,
                                       .IS_MISSION = p.IS_MISSION,
                                       .ORG_CHANGE_ID = p.ORG_CHANGE_ID,
                                       .ORG_CHANGE_NAME = orgchange.NAME_VN,
                                       .ORG_CHANGE_DESC = orgchange.DESCRIPTION_PATH,
                                       .ORG_COST_ID = p.ORG_CHANGE_ID,
                                       .ORG_COST_NAME = orgcost.NAME_VN,
                                       .ORG_COST_DESC = orgcost.DESCRIPTION_PATH,
                                       .ORG_ID = e.ORG_ID,
                                       .ORG_CODE = org.CODE,
                                       .ORG_NAME = org.NAME_VN,
                                       .ORG_DESC = org.DESCRIPTION_PATH,
                                       .EFFECT_DATE = p.EFFECT_DATE,
                                       .MEAL_ID = p.MEAL_ID,
                                       .MEAL_NAME = m.NAME,
                                       .KITCHEN_ID = p.KITCHEN_ID,
                                       .KITCHEN_NAME = k.KITCHEN_NAME,
                                       .RATION_ID = p.RATION_ID,
                                       .RATION_NAME = o.NAME_VN,
                                       .COST_EXTRA = p.COST_EXTRA,
                                       .IS_CHECK = p.IS_CHECK,
                                       .REMARK = p.REMARK,
                                       .COST = ms.COST,
                                       .MEAL_ID_NEW = p.MEAL_ID_NEW,
                                       .MEAL_NAME_NEW = m1.NAME,
                                       .KITCHEN_ID_NEW = p.KITCHEN_ID_NEW,
                                       .KITCHEN_NAME_NEW = k1.KITCHEN_NAME,
                                       .RATION_ID_NEW = p.RATION_ID_NEW,
                                       .RATION_NAME_NEW = o1.NAME_VN,
                                       .COST_EXTRA_NEW = p.COST_EXTRA_NEW,
                                       .CREATED_DATE = p.CREATED_DATE,
                                       .CREATED_BY = p.CREATED_BY,
                                       .CREATED_LOG = p.CREATED_LOG,
                                       .MODIFIED_DATE = p.MODIFIED_DATE,
                                       .MODIFIED_BY = p.MODIFIED_BY,
                                       .MODIFIED_LOG = p.MODIFIED_LOG,
                                       .IS_CHECK_NEW = p.IS_CHECK_NEW,
                                       .REMARK_NEW = p.REMARK_NEW,
                                       .COST_NEW = ms1.COST,
                                       .STATUS_ID = p.STATUS_ID,
                                       .STATUS_NAME = status.NAME_VN,
                                       .REASON = p.REASON}

            Dim lst = query

            If _filter.EMPLOYEE_ID.HasValue Then
                lst = lst.Where(Function(f) f.EMPLOYEE_ID = _filter.EMPLOYEE_ID)
            End If

            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                lst = lst.Where(Function(f) f.EMPLOYEE_CODE.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower()))
            End If

            If Not String.IsNullOrEmpty(_filter.REASON) Then
                lst = lst.Where(Function(f) f.REASON.ToLower().Contains(_filter.REASON.ToLower()))
            End If

            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_NAME) Then
                lst = lst.Where(Function(f) f.EMPLOYEE_NAME.ToLower().Contains(_filter.EMPLOYEE_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.MEAL_NAME) Then
                lst = lst.Where(Function(f) f.MEAL_NAME.ToLower().Contains(_filter.MEAL_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.MEAL_NAME_NEW) Then
                lst = lst.Where(Function(f) f.MEAL_NAME_NEW.ToLower().Contains(_filter.MEAL_NAME_NEW.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.KITCHEN_NAME) Then
                lst = lst.Where(Function(f) f.KITCHEN_NAME.ToLower().Contains(_filter.KITCHEN_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.KITCHEN_NAME_NEW) Then
                lst = lst.Where(Function(f) f.KITCHEN_NAME_NEW.ToLower().Contains(_filter.KITCHEN_NAME_NEW.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.RATION_NAME) Then
                lst = lst.Where(Function(f) f.RATION_NAME.ToLower().Contains(_filter.RATION_NAME.ToLower()))
            End If

            If Not String.IsNullOrEmpty(_filter.RATION_NAME_NEW) Then
                lst = lst.Where(Function(f) f.RATION_NAME_NEW.ToLower().Contains(_filter.RATION_NAME_NEW.ToLower()))
            End If

            If Not String.IsNullOrEmpty(_filter.STATUS_NAME) Then
                lst = lst.Where(Function(f) f.STATUS_NAME.ToLower().Contains(_filter.STATUS_NAME.ToLower()))
            End If

            If _filter.COST_EXTRA.HasValue Then
                lst = lst.Where(Function(f) f.COST_EXTRA = _filter.COST_EXTRA)
            End If
            If _filter.COST.HasValue Then
                lst = lst.Where(Function(f) f.COST = _filter.COST)
            End If
            If _filter.COST_EXTRA_NEW.HasValue Then
                lst = lst.Where(Function(f) f.COST_EXTRA_NEW = _filter.COST_EXTRA_NEW)
            End If

            If _filter.COST_NEW.HasValue Then
                lst = lst.Where(Function(f) f.COST_NEW = _filter.COST_NEW)
            End If

            If _filter.EFFECT_DATE.HasValue Then
                lst = lst.Where(Function(f) f.EFFECT_DATE = _filter.EFFECT_DATE)
            End If

            If Not String.IsNullOrEmpty(_filter.REMARK) Then
                lst = lst.Where(Function(f) f.REMARK.ToLower().Contains(_filter.REMARK.ToLower()))
            End If

            If Not String.IsNullOrEmpty(_filter.REMARK_NEW) Then
                lst = lst.Where(Function(f) f.REMARK_NEW.ToLower().Contains(_filter.REMARK_NEW.ToLower()))
            End If

            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_REQUEST_NAME) Then
                lst = lst.Where(Function(f) f.EMPLOYEE_REQUEST_NAME.ToLower().Contains(_filter.EMPLOYEE_REQUEST_NAME.ToLower()))
            End If

            If Not String.IsNullOrEmpty(_filter.ORG_COST_NAME) Then
                lst = lst.Where(Function(f) f.ORG_COST_NAME.ToLower().Contains(_filter.ORG_COST_NAME.ToLower()))
            End If

            If Not String.IsNullOrEmpty(_filter.ORG_CHANGE_NAME) Then
                lst = lst.Where(Function(f) f.ORG_CHANGE_NAME.ToLower().Contains(_filter.ORG_CHANGE_NAME.ToLower()))
            End If

            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                lst = lst.Where(Function(f) f.ORG_NAME.ToLower().Contains(_filter.ORG_NAME.ToLower()))
            End If

            If _filter.IS_MISSION.HasValue Then
                lst = lst.Where(Function(f) f.IS_MISSION = _filter.IS_MISSION)
            End If

            If _filter.START_DATE.HasValue Then
                lst = lst.Where(Function(f) f.EFFECT_DATE >= _filter.START_DATE)
            End If

            If _filter.END_DATE.HasValue Then
                lst = lst.Where(Function(f) f.EFFECT_DATE <= _filter.END_DATE)
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iAttendance")
            Throw ex
        End Try
    End Function

    Public Function Approve_AT_MEAL_CHANGE(ByVal lstID As List(Of Decimal),
                                          ByVal _status As Decimal,
                                          ByVal _reason As String,
                                          ByVal log As UserLog) As Boolean
        Dim lst As List(Of AT_MEAL_CHANGE)
        Try
            lst = (From p In Context.AT_MEAL_CHANGE Where lstID.Contains(p.ID)).ToList
            For Each obj In lst
                obj.STATUS_ID = _status
                obj.REASON = _reason

                If _status = AttendanceCommon.OT_MEAL_CHANGE_STATUS.APPROVE_ID Then

                    If obj.MEAL_ID IsNot Nothing Then
                        ' Xóa thông tin cũ
                        Dim lstManager = (From p In Context.AT_MEAL_MANAGER
                                          Where p.EMPLOYEE_ID = obj.EMPLOYEE_ID And
                                          p.EFFECT_DATE = obj.EFFECT_DATE And
                                          p.MEAL_ID = obj.MEAL_ID).ToList

                        For Each objManager In lstManager
                            Context.AT_MEAL_MANAGER.DeleteObject(objManager)
                        Next


                    End If

                    If obj.MEAL_ID_NEW IsNot Nothing Then
                        ' Xóa thông tin mới
                        Dim lstManagerNew = (From p In Context.AT_MEAL_MANAGER
                                          Where p.EMPLOYEE_ID = obj.EMPLOYEE_ID And
                                          p.EFFECT_DATE = obj.EFFECT_DATE And
                                          p.MEAL_ID = obj.MEAL_ID_NEW).ToList

                        For Each objManagerNew In lstManagerNew
                            Context.AT_MEAL_MANAGER.DeleteObject(objManagerNew)
                        Next


                        If Not obj.IS_MISSION Then
                            ' Thêm thông tin
                            Dim objManagerNew As New AT_MEAL_MANAGER
                            objManagerNew.ID = Utilities.GetNextSequence(Context, Context.AT_MEAL_MANAGER.EntitySet.Name)
                            objManagerNew.EMPLOYEE_ID = obj.EMPLOYEE_ID
                            objManagerNew.MEAL_ID = obj.MEAL_ID_NEW
                            objManagerNew.KITCHEN_ID = obj.KITCHEN_ID_NEW
                            objManagerNew.RATION_ID = obj.RATION_ID_NEW
                            objManagerNew.COST_EXTRA = obj.COST_EXTRA_NEW
                            objManagerNew.EFFECT_DATE = obj.EFFECT_DATE
                            objManagerNew.IS_CHECK = obj.IS_CHECK_NEW
                            objManagerNew.REMARK = obj.REMARK_NEW
                            objManagerNew.IS_CHANGE = True
                            Context.AT_MEAL_MANAGER.AddObject(objManagerNew)
                        End If
                    End If
                End If

                Context.SaveChanges(log)
            Next
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iAttendance")
            Throw ex
        End Try
    End Function

    Public Function GETDATA_CHANGE_IMPORT(ByVal obj As ParamDTO, ByVal log As UserLog) As DataSet
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataSet = cls.ExecuteStore("PKG_MEAL_BUSINESS.GETDATA_CHANGE_IMPORT",
                                               New With {.P_USERNAME = log.Username.ToUpper,
                                                         .P_ORG_ID = obj.ORG_ID,
                                                         .P_ISDISSOLVE = obj.IS_DISSOLVE,
                                                         .P_STARTDATE = obj.FROMDATE,
                                                         .P_ENDDATE = obj.ENDDATE,
                                                         .P_CUR = cls.OUT_CURSOR,
                                                         .P_CUR2 = cls.OUT_CURSOR,
                                                         .P_CUR3 = cls.OUT_CURSOR,
                                                         .P_CUR4 = cls.OUT_CURSOR,
                                                         .P_CUR5 = cls.OUT_CURSOR,
                                                         .P_CUR6 = cls.OUT_CURSOR}, False)
                Return dtData
            End Using
            Return Nothing
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iAttendance")
            Throw ex
        End Try
    End Function


    Public Function ImportMealChange(ByVal lstID As List(Of String),
                                     ByVal dtData As DataTable,
                                     ByRef dtError As DataTable,
                                     ByVal log As UserLog) As Boolean
        Dim rowError As DataRow
        Dim isError As Boolean = False
        Dim sError As String = String.Empty
        Dim irow = 9
        Try
            If Not dtData.Columns.Contains("EMPLOYEE_REQUEST_ID") Then
                dtData.Columns.Add("EMPLOYEE_REQUEST_ID", GetType(String))
            End If

            For Each row As DataRow In dtData.Rows
                isError = False
                rowError = dtError.NewRow
                If row("IS_MISSION").ToString = -1 Then
                    Dim empCode = row("EMPLOYEE_REQUEST_NAME").ToString
                    Dim empID = (From p In Context.HU_EMPLOYEE
                                 Where p.EMPLOYEE_CODE = empCode And p.TER_EFFECT_DATE Is Nothing
                                 Order By p.ID Descending
                                 Select p.ID).FirstOrDefault

                    If empID = 0 Then
                        isError = True
                        rowError("EMPLOYEE_REQUEST_NAME") = "Mã người yêu cầu không tồn tại"
                    Else
                        row("EMPLOYEE_REQUEST_ID") = empID
                    End If

                End If

                If isError Then
                    rowError("STT") = irow
                    rowError("EMPLOYEE_CODE") = row("EMPLOYEE_CODE").ToString
                    rowError("VN_FULLNAME") = row("VN_FULLNAME").ToString
                    rowError("TITLE_NAME") = row("TITLE_NAME").ToString
                    rowError("ORG_DESC") = row("ORG_DESC").ToString
                    rowError("ORG_NAME") = row("ORG_NAME").ToString
                    dtError.Rows.Add(rowError)
                End If
                irow += 1
            Next

            If dtError.Rows.Count > 0 Then
                Return False
            End If

            For Each row As DataRow In dtData.Rows
                Dim obj As New AT_MEAL_CHANGE
                obj.ID = Utilities.GetNextSequence(Context, Context.AT_MEAL_CHANGE.EntitySet.Name)
                obj.EMPLOYEE_ID = row("EMPLOYEE_ID").ToString
                obj.EFFECT_DATE = row("EFFECT_DATE").ToString
                If row("MEAL_OLD_ID").ToString <> "" Then
                    obj.MEAL_ID = row("MEAL_OLD_ID").ToString
                    obj.KITCHEN_ID = row("KITCHEN_OLD_ID").ToString
                    obj.RATION_ID = row("RATION_OLD_ID").ToString
                    If row("COST_OLD").ToString <> "" Then
                        obj.COST_EXTRA = row("COST_OLD").ToString
                    End If
                    obj.IS_CHECK = row("IS_CHECK_OLD").ToString
                    obj.REMARK = row("REMARK_OLD").ToString
                End If

                If row("MEAL_NEW_ID").ToString <> "" Then
                    obj.MEAL_ID_NEW = row("MEAL_NEW_ID").ToString
                    obj.KITCHEN_ID_NEW = row("KITCHEN_NEW_ID").ToString
                    obj.RATION_ID_NEW = row("RATION_NEW_ID").ToString
                    If row("COST_NEW").ToString <> "" Then
                        obj.COST_EXTRA_NEW = row("COST_NEW").ToString
                    End If
                    obj.IS_CHECK_NEW = row("IS_CHECK_NEW").ToString
                    obj.REMARK_NEW = row("REMARK_NEW").ToString
                End If
                obj.IS_MISSION = False
                If row("IS_MISSION").ToString = -1 Then
                    obj.ORG_CHANGE_ID = row("ORG_CHANGE_ID").ToString
                    obj.IS_MISSION = True
                    obj.IS_CHECK_NEW = False
                    obj.EMPLOYEE_REQUEST_ID = row("EMPLOYEE_REQUEST_ID").ToString
                    obj.ORG_COST_ID = row("ORG_COST_ID").ToString
                End If
                obj.STATUS_ID = AttendanceCommon.OT_MEAL_CHANGE_STATUS.WAIT_APPROVE_ID

                Context.AT_MEAL_CHANGE.AddObject(obj)
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iAttendance")
            Throw ex
        End Try
    End Function


#End Region

#Region "AT_MEAL_COST_SETUP"

    Public Function Insert_AT_MEAL_COST_SETUP(ByVal lst As List(Of AT_MEAL_COST_SETUP_DTO), ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        Try
            For Each o In lst
                Dim obj As New AT_MEAL_COST_SETUP

                If o.ID Is Nothing Then
                    obj.ID = Utilities.GetNextSequence(Context, Context.AT_MEAL_COST_SETUP.EntitySet.Name)
                    obj.KITCHEN_ID = o.KITCHEN_ID
                    obj.MEAL_ID = o.MEAL_ID
                    obj.RATION_ID = o.RATION_ID
                    obj.COST = o.COST
                    Context.AT_MEAL_COST_SETUP.AddObject(obj)

                Else
                    obj = (From p In Context.AT_MEAL_COST_SETUP Where p.ID = o.ID).FirstOrDefault
                    obj.KITCHEN_ID = o.KITCHEN_ID
                    obj.MEAL_ID = o.MEAL_ID
                    obj.RATION_ID = o.RATION_ID
                    obj.COST = o.COST
                    Context.SaveChanges(log)

                End If
                gID = obj.ID
            Next

            Context.SaveChanges(log)

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iAttendance")
            Throw ex
        End Try
    End Function

    Public Function Modify_AT_MEAL_COST_SETUP(ByVal objData As AT_MEAL_COST_SETUP_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim obj As New AT_MEAL_COST_SETUP With {.ID = objData.ID}
        Try
            obj = (From p In Context.AT_MEAL_COST_SETUP Where p.ID = objData.ID).FirstOrDefault
            obj.KITCHEN_ID = objData.KITCHEN_ID
            obj.MEAL_ID = objData.MEAL_ID
            obj.RATION_ID = objData.RATION_ID
            obj.COST = objData.COST
            Context.SaveChanges(log)


            gID = obj.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iAttendance")
            Throw ex
        End Try
    End Function

    Public Function Delete_AT_MEAL_COST_SETUP(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lst As List(Of AT_MEAL_COST_SETUP)
        Try
            lst = (From p In Context.AT_MEAL_COST_SETUP Where lstID.Contains(p.ID)).ToList
            For index = 0 To lst.Count - 1
                Context.AT_MEAL_COST_SETUP.DeleteObject(lst(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iAttendance")
            Throw ex
        End Try
    End Function

    Public Function Get_AT_MEAL_COST_SETUPById(ByVal _id As Decimal?) As AT_MEAL_COST_SETUP_DTO
        Try

            Dim query = From p In Context.AT_MEAL_COST_SETUP
                        From m In Context.AT_MEAL.Where(Function(f) f.ID = p.MEAL_ID)
                        From k In Context.AT_KITCHEN.Where(Function(f) f.ID = p.KITCHEN_ID)
                        From r In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.RATION_ID)
                        Where p.ID = _id

            Dim lst = query.Select(Function(p) New AT_MEAL_COST_SETUP_DTO With {
                                        .ID = p.p.ID,
                                        .KITCHEN_ID = p.p.KITCHEN_ID,
                                        .KITCHEN_NAME = p.k.KITCHEN_NAME,
                                        .MEAL_ID = p.p.MEAL_ID,
                                        .MEAL_NAME = p.m.NAME,
                                        .RATION_ID = p.p.RATION_ID,
                                        .RATION_NAME = p.r.NAME_VN,
                                        .COST = p.p.COST,
                                        .CREATED_DATE = p.p.CREATED_DATE}).FirstOrDefault
            Return lst
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iAttendance")
            Throw ex
        End Try
    End Function

    Public Function Get_AT_MEAL_COST_SETUP(ByVal _filter As AT_MEAL_COST_SETUP_DTO,
                                           Optional ByVal PageIndex As Integer = 0,
                                           Optional ByVal PageSize As Integer = Integer.MaxValue,
                                           Optional ByRef Total As Integer = 0,
                                           Optional ByVal Sorts As String = "KITCHEN_ID,MEAL_ID desc") As List(Of AT_MEAL_COST_SETUP_DTO)
        Try

            Dim query = From r In Context.OT_OTHER_LIST
                        From k In Context.AT_KITCHEN
                        From m In Context.AT_MEAL.Where(Function(f) (f.ID = 1 And k.IS_BREAKFAST = -1) Or (f.ID = 2 And k.IS_LUNCH = -1) Or
                                                            (f.ID = 3 And k.IS_DINNER = -1) Or (f.ID = 4 And k.IS_EXTRA1 = -1) Or
                                                            (f.ID = 5 And k.IS_EXTRA2 = -1) Or (f.ID = 6 And k.IS_EXTRA3 = -1))
                        From p In Context.AT_MEAL_COST_SETUP.Where(Function(f) f.KITCHEN_ID = k.ID And f.MEAL_ID = m.ID And f.RATION_ID = r.ID).DefaultIfEmpty
                         Where (r.TYPE_ID = 2052 And r.ACTFLG = "A")
                         Order By k.ID, m.ID

            Dim lst = query.Select(Function(p) New AT_MEAL_COST_SETUP_DTO With {
                                        .ID = p.p.ID,
                                        .KITCHEN_ID = p.k.ID,
                                        .KITCHEN_CODE = p.k.KITCHEN_CODE,
                                        .KITCHEN_NAME = p.k.KITCHEN_NAME,
                                        .MEAL_ID = p.m.ID,
                                        .MEAL_NAME = p.m.NAME,
                                        .RATION_ID = p.r.ID,
                                        .RATION_NAME = p.r.NAME_VN,
                                        .COST = p.p.COST,
                                        .CREATED_DATE = p.p.CREATED_DATE})
            If Not String.IsNullOrEmpty(_filter.MEAL_NAME) Then
                lst = lst.Where(Function(f) f.MEAL_NAME.ToLower().Contains(_filter.MEAL_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.KITCHEN_NAME) Then
                lst = lst.Where(Function(f) f.KITCHEN_NAME.ToLower().Contains(_filter.KITCHEN_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.KITCHEN_CODE) Then
                lst = lst.Where(Function(f) f.KITCHEN_CODE.ToLower().Contains(_filter.KITCHEN_CODE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.RATION_NAME) Then
                lst = lst.Where(Function(f) f.RATION_NAME.ToLower().Contains(_filter.RATION_NAME.ToLower()))
            End If
            If _filter.KITCHEN_ID.HasValue Then
                lst = lst.Where(Function(f) f.KITCHEN_ID = _filter.KITCHEN_ID)
            End If
            If _filter.MEAL_ID.HasValue Then
                lst = lst.Where(Function(f) f.MEAL_ID = _filter.MEAL_ID)
            End If
            If _filter.RATION_ID.HasValue Then
                lst = lst.Where(Function(f) f.RATION_ID = _filter.RATION_ID)
            End If
            If _filter.COST.HasValue Then
                lst = lst.Where(Function(f) f.COST = _filter.COST)
            End If

            ' lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Return lst.ToList()
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iAttendance")
            Throw ex
        End Try
    End Function
#End Region

#Region "AT_MEAL_REAL"


    Public Function Get_AT_MEAL_REAL(ByVal _filter As AT_MEAL_REAL_DTO,
                                     ByVal _param As ParamDTO,
                                     ByRef Total As Integer,
                                     ByVal PageIndex As Integer,
                                     ByVal PageSize As Integer,
                                     Optional ByVal Sorts As String = "CREATED_DATE desc",
                                     Optional ByVal log As UserLog = Nothing) As List(Of AT_MEAL_REAL_DTO)
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username.ToUpper,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using

            Dim query = From p In Context.AT_MEAL_REAL
                        From o In Context.HUV_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID)
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From kitchen In Context.AT_KITCHEN.Where(Function(f) f.ID = p.KITCHEN_ID)
                        From meal In Context.AT_MEAL.Where(Function(f) f.ID = p.MEAL_ID)
                        From ration In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.RATION_ID)
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) p.ORG_ID = f.ORG_ID And
                                                                  f.USERNAME.ToUpper = log.Username.ToUpper)
                        Select New AT_MEAL_REAL_DTO With {.ID = p.ID,
                                                          .ORG_ID = p.ORG_ID,
                                                          .ORG_CODE = o.CODE,
                                                          .ORG_NAME = o.NAME_VN,
                                                          .ORG_PATH = o.ORG_PATH,
                                                          .EMPLOYEE_ID = p.EMPLOYEE_ID,
                                                          .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                                                          .EMPLOYEE_NAME = e.FULLNAME_VN,
                                                          .EFFECT_DATE = p.EFFECT_DATE,
                                                          .KITCHEN_ID = p.KITCHEN_ID,
                                                          .KITCHEN_CODE = kitchen.KITCHEN_CODE,
                                                          .KITCHEN_NAME = kitchen.KITCHEN_NAME,
                                                          .MEAL_ID = p.MEAL_ID,
                                                          .MEAL_NAME = meal.NAME,
                                                          .RATION_ID = p.RATION_ID,
                                                          .RATION_NAME = ration.NAME_VN,
                                                          .IS_CHECK = p.IS_CHECK,
                                                          .COST_EXTRA = p.COST_EXTRA,
                                                          .COST_MEAL = p.COST_MEAL,
                                                          .COST_RATION = p.COST_RATION,
                                                          .SWIPE_VALUE = p.SWIPE_VALUE,
                                                          .IS_VALID = If(p.IS_VALID = 1, False, True),
                                                          .IS_EAT = p.IS_EAT,
                                                          .CREATED_BY = p.CREATED_BY,
                                                          .CREATED_DATE = p.CREATED_DATE,
                                                          .CREATED_LOG = p.CREATED_LOG}


            Dim lst = query

            If _filter.START_DATE.HasValue Then
                lst = lst.Where(Function(f) f.EFFECT_DATE >= _filter.START_DATE)
            End If

            If _filter.END_DATE.HasValue Then
                lst = lst.Where(Function(f) f.EFFECT_DATE <= _filter.END_DATE)
            End If

            If _filter.EFFECT_DATE.HasValue Then
                lst = lst.Where(Function(f) f.EFFECT_DATE = _filter.EFFECT_DATE)
            End If

            If _filter.KITCHEN_ID.HasValue Then
                lst = lst.Where(Function(f) f.KITCHEN_ID = _filter.KITCHEN_ID)
            End If

            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                lst = lst.Where(Function(f) f.EMPLOYEE_CODE.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower()))
            End If

            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_NAME) Then
                lst = lst.Where(Function(f) f.EMPLOYEE_NAME.ToLower().Contains(_filter.EMPLOYEE_NAME.ToLower()))
            End If

            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                lst = lst.Where(Function(f) f.ORG_NAME.ToLower().Contains(_filter.ORG_NAME.ToLower()))
            End If

            If Not String.IsNullOrEmpty(_filter.ORG_CODE) Then
                lst = lst.Where(Function(f) f.ORG_CODE.ToLower().Contains(_filter.ORG_CODE.ToLower()))
            End If

            If Not String.IsNullOrEmpty(_filter.ORG_PATH) Then
                lst = lst.Where(Function(f) f.ORG_PATH.ToLower().Contains(_filter.ORG_PATH.ToLower()))
            End If

            If Not String.IsNullOrEmpty(_filter.RATION_NAME) Then
                lst = lst.Where(Function(f) f.RATION_NAME.ToLower().Contains(_filter.RATION_NAME.ToLower()))
            End If

            If Not String.IsNullOrEmpty(_filter.MEAL_NAME) Then
                lst = lst.Where(Function(f) f.MEAL_NAME.ToLower().Contains(_filter.MEAL_NAME.ToLower()))
            End If

            If Not String.IsNullOrEmpty(_filter.KITCHEN_NAME) Then
                lst = lst.Where(Function(f) f.KITCHEN_NAME.ToLower().Contains(_filter.KITCHEN_NAME.ToLower()))
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iAttendance")
            Throw ex
        End Try
    End Function

    Public Function CAL_AT_MEAL_REAL(ByVal _param As ParamDTO,
                                     ByVal log As UserLog) As Boolean
        Try
            Using cls As New DataAccess.NonQueryData
                cls.ExecuteStore("PKG_MEAL_BUSINESS.CAL_AT_MEAL_REAL",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORG_ID = _param.ORG_ID,
                                           .P_FROMDATE = _param.FROMDATE,
                                           .P_ENDDATE = _param.ENDDATE,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iAttendance")
            Throw ex
        End Try
    End Function

#End Region

#Region "AT_MEAL_EXPLAN"

    Public Function Get_AT_MEAL_EXPLAN(ByVal _filter As AT_MEAL_EXPLAN_DTO,
                                        ByVal _param As ParamDTO,
                                        ByRef Total As Integer,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc",
                                        Optional ByVal log As UserLog = Nothing) As List(Of AT_MEAL_EXPLAN_DTO)
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username.ToUpper,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using

            Dim query = From p In Context.AT_MEAL_EXPLAN
                        From o In Context.HUV_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID)
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From kitchen In Context.AT_KITCHEN.Where(Function(f) f.ID = p.KITCHEN_ID).DefaultIfEmpty
                        From meal In Context.AT_MEAL.Where(Function(f) f.ID = p.MEAL_ID).DefaultIfEmpty
                        From ration In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.RATION_ID And
                                                                   f.TYPE_ID = 2052).DefaultIfEmpty
                        From ex In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.EXPLAN_TYPE And
                                                                   f.TYPE_ID = 2061).DefaultIfEmpty
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) p.ORG_ID = f.ORG_ID And
                                                                  f.USERNAME.ToUpper = log.Username.ToUpper)
                        Select New AT_MEAL_EXPLAN_DTO With {.ID = p.ID,
                                                            .ORG_ID = p.ORG_ID,
                                                            .ORG_CODE = o.CODE,
                                                            .ORG_NAME = o.NAME_VN,
                                                            .ORG_PATH = o.ORG_PATH,
                                                            .EMPLOYEE_ID = p.EMPLOYEE_ID,
                                                            .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                                                            .EMPLOYEE_NAME = e.FULLNAME_VN,
                                                            .WORKINGDAY = p.WORKINGDAY,
                                                            .KITCHEN_ID = p.KITCHEN_ID,
                                                            .KITCHEN_CODE = kitchen.KITCHEN_CODE,
                                                            .KITCHEN_NAME = kitchen.KITCHEN_NAME,
                                                            .MEAL_ID = p.MEAL_ID,
                                                            .MEAL_NAME = meal.NAME,
                                                            .RATION_ID = p.RATION_ID,
                                                            .RATION_NAME = ration.NAME_VN,
                                                            .MEAL_REASON = p.MEAL_REASON,
                                                            .MEAL_REASON_DEDUCT = p.MEAL_REASON_DEDUCT,
                                                            .VALTIME = p.VALTIME,
                                                            .EXPLAN_TYPE = p.EXPLAN_TYPE,
                                                            .EXPLAN_TYPE_NAME = ex.NAME_VN,
                                                            .IS_EAT = p.IS_EAT,
                                                            .IS_DEDUCT = p.IS_DEDUCT,
                                                            .CREATED_BY = p.CREATED_BY,
                                                            .CREATED_DATE = p.CREATED_DATE,
                                                            .CREATED_LOG = p.CREATED_LOG,
                                                            .MODIFIED_BY = p.MODIFIED_BY,
                                                            .MODIFIED_DATE = p.MODIFIED_DATE,
                                                            .MODIFIED_LOG = p.MODIFIED_LOG}


            Dim lst = query

            If _filter.START_DATE.HasValue Then
                lst = lst.Where(Function(f) f.WORKINGDAY >= _filter.START_DATE)
            End If

            If _filter.END_DATE.HasValue Then
                lst = lst.Where(Function(f) f.WORKINGDAY <= _filter.END_DATE)
            End If

            If _filter.KITCHEN_ID.HasValue Then
                lst = lst.Where(Function(f) f.KITCHEN_ID = _filter.KITCHEN_ID)
            End If

            If _filter.WORKINGDAY.HasValue Then
                lst = lst.Where(Function(f) f.WORKINGDAY = _filter.WORKINGDAY)
            End If


            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                lst = lst.Where(Function(f) f.EMPLOYEE_CODE.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower()))
            End If

            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_NAME) Then
                lst = lst.Where(Function(f) f.EMPLOYEE_NAME.ToLower().Contains(_filter.EMPLOYEE_NAME.ToLower()))
            End If

            If Not String.IsNullOrEmpty(_filter.MEAL_REASON) Then
                lst = lst.Where(Function(f) f.MEAL_REASON.ToLower().Contains(_filter.MEAL_REASON.ToLower()))
            End If

            If Not String.IsNullOrEmpty(_filter.MEAL_REASON_DEDUCT) Then
                lst = lst.Where(Function(f) f.MEAL_REASON_DEDUCT.ToLower().Contains(_filter.MEAL_REASON_DEDUCT.ToLower()))
            End If

            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                lst = lst.Where(Function(f) f.ORG_NAME.ToLower().Contains(_filter.ORG_NAME.ToLower()))
            End If

            If Not String.IsNullOrEmpty(_filter.ORG_CODE) Then
                lst = lst.Where(Function(f) f.ORG_CODE.ToLower().Contains(_filter.ORG_CODE.ToLower()))
            End If

            If Not String.IsNullOrEmpty(_filter.ORG_PATH) Then
                lst = lst.Where(Function(f) f.ORG_PATH.ToLower().Contains(_filter.ORG_PATH.ToLower()))
            End If

            If Not String.IsNullOrEmpty(_filter.RATION_NAME) Then
                lst = lst.Where(Function(f) f.RATION_NAME.ToLower().Contains(_filter.RATION_NAME.ToLower()))
            End If

            If Not String.IsNullOrEmpty(_filter.MEAL_NAME) Then
                lst = lst.Where(Function(f) f.MEAL_NAME.ToLower().Contains(_filter.MEAL_NAME.ToLower()))
            End If

            If Not String.IsNullOrEmpty(_filter.EXPLAN_TYPE_NAME) Then
                lst = lst.Where(Function(f) f.EXPLAN_TYPE_NAME.ToLower().Contains(_filter.EXPLAN_TYPE_NAME.ToLower()))
            End If

            If Not String.IsNullOrEmpty(_filter.KITCHEN_NAME) Then
                lst = lst.Where(Function(f) f.KITCHEN_NAME.ToLower().Contains(_filter.KITCHEN_NAME.ToLower()))
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iAttendance")
            Throw ex
        End Try
    End Function

    Public Function Get_AT_MEAL_EXPLAN_IMPORT(ByVal _param As AT_MEAL_EXPLAN_DTO,
                                           ByVal log As UserLog) As DataSet
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataSet = cls.ExecuteStore("PKG_MEAL_BUSINESS.GET_AT_MEAL_EXPLAN_IMPORT",
                                                           New With {.P_USERNAME = log.Username.ToUpper,
                                                                     .P_ORG_ID = _param.ORG_ID,
                                                                     .P_EMPLOYEE_CODE = _param.EMPLOYEE_CODE,
                                                                     .P_EMPLOYEE_NAME = _param.EMPLOYEE_NAME,
                                                                     .P_KITCHEN_ID = _param.KITCHEN_ID,
                                                                     .P_KITCHEN_NAME = _param.KITCHEN_NAME,
                                                                     .P_ORG_PATH = _param.ORG_PATH,
                                                                     .P_ORG_NAME = _param.ORG_NAME,
                                                                     .P_MEAL_NAME = _param.MEAL_NAME,
                                                                     .P_RATION_NAME = _param.RATION_NAME,
                                                                     .P_MEAL_REASON = _param.MEAL_REASON,
                                                                     .P_FROMDATE = _param.START_DATE,
                                                                     .P_ENDDATE = _param.END_DATE,
                                                                     .P_IS_DISSOLVE = _param.IS_DISSOLVE,
                                                                     .P_CUR = cls.OUT_CURSOR,
                                                                     .P_CUR1 = cls.OUT_CURSOR,
                                                                     .P_CUR2 = cls.OUT_CURSOR,
                                                                     .P_CUR3 = cls.OUT_CURSOR}, False)

                Return dtData
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function Import_AT_MEAL_EXPLAN(ByVal lstData As List(Of AT_MEAL_EXPLAN_DTO),
                                          ByVal log As UserLog,
                                          ByRef dtError As DataTable) As Boolean

        Dim startdate As Date?
        Dim enddate As Date?
        Dim strPosOut As String = ""
        Dim irow = 9
        Try

            Using conMng As New ConnectionManager
                Using conn As New OracleConnection(conMng.GetConnectionString())
                    Using cmd As New OracleCommand()
                        Using resource As New DataAccess.OracleCommon()
                            Try
                                conn.Open()
                                cmd.Connection = conn
                                cmd.Transaction = cmd.Connection.BeginTransaction()
                                cmd.CommandType = CommandType.StoredProcedure
                                cmd.CommandText = "PKG_MEAL_BUSINESS.INSERT_AT_MEAL_EXPLAN_TEMP"

                                For Each obj In lstData
                                    If startdate Is Nothing Then
                                        startdate = obj.WORKINGDAY
                                    End If

                                    If enddate Is Nothing Then
                                        enddate = obj.WORKINGDAY
                                    End If

                                    If startdate > obj.WORKINGDAY Then
                                        startdate = obj.WORKINGDAY
                                    End If

                                    If enddate < obj.WORKINGDAY Then
                                        enddate = obj.WORKINGDAY
                                    End If

                                    cmd.Parameters.Clear()
                                    Dim objParam = New With {.P_USERNAME = log.Username.ToUpper,
                                                             .P_ORG_ID = obj.ORG_ID,
                                                             .P_EMPLOYEE_ID = obj.EMPLOYEE_ID,
                                                             .P_WORKINGDAY = obj.WORKINGDAY,
                                                             .P_VALTIME = obj.VALTIME,
                                                             .P_EXPLAN_TYPE = obj.EXPLAN_TYPE,
                                                             .P_KITCHEN_ID = obj.KITCHEN_ID,
                                                             .P_MEAL_ID = obj.MEAL_ID,
                                                             .P_RATION_ID = obj.RATION_ID,
                                                             .P_MEAL_REASON = obj.MEAL_REASON,
                                                             .P_MEAL_REASON_DEDUCT = obj.MEAL_REASON_DEDUCT,
                                                             .P_IS_EAT = obj.IS_EAT,
                                                             .P_IS_DEDUCT = obj.IS_DEDUCT,
                                                             .PO_ERROR = resource.OUT_STRING}
                                    strPosOut = ""
                                    If objParam IsNot Nothing Then
                                        Dim idx As Integer = 0
                                        For Each info As PropertyInfo In objParam.GetType().GetProperties()
                                            Dim bOut As Boolean = False
                                            Dim para = resource.GetParameter(info.Name, info.GetValue(objParam, Nothing), bOut)
                                            If para IsNot Nothing Then
                                                If bOut Then
                                                    strPosOut += idx.ToString + ";"
                                                End If
                                                cmd.Parameters.Add(para)
                                                idx += 1
                                            End If
                                        Next
                                    End If
                                    cmd.ExecuteNonQuery()

                                    ' Lấy dữ liệu kiểu out để trả về
                                    If strPosOut <> "" Then
                                        strPosOut = strPosOut.Substring(0, strPosOut.Length - 1)
                                        If objParam IsNot Nothing Then
                                            For Each str As String In strPosOut.Split(";")
                                                Dim key = cmd.Parameters(Integer.Parse(str)).ParameterName
                                                For Each info As PropertyInfo In objParam.GetType().GetProperties()
                                                    If info.Name = key Then
                                                        Select Case cmd.Parameters(Integer.Parse(str)).OracleDbType
                                                            Case OracleDbType.NVarchar2
                                                                info.SetValue(objParam, cmd.Parameters(Integer.Parse(str)).Value.ToString, Nothing)
                                                            Case OracleDbType.Date
                                                                info.SetValue(objParam, cmd.Parameters(Integer.Parse(str)).Value.ToString("dd/MM/yyyy"), Nothing)
                                                            Case OracleDbType.Decimal
                                                                info.SetValue(objParam, cmd.Parameters(Integer.Parse(str)).Value.ToString, Nothing)
                                                        End Select
                                                        Exit For
                                                    End If
                                                Next
                                            Next
                                        End If
                                    End If
                                    If objParam.PO_ERROR.ToUpper <> "NULL" Then
                                        Dim rowError = dtError.NewRow
                                        rowError("STT") = irow
                                        rowError("EMPLOYEE_CODE") = obj.EMPLOYEE_CODE
                                        rowError("EMPLOYEE_NAME") = obj.EMPLOYEE_NAME
                                        rowError("KITCHEN_NAME") = obj.KITCHEN_NAME
                                        rowError("WORKINGDAY") = Format(obj.WORKINGDAY, "dd/MM/yyyy")
                                        rowError("MEAL_NAME") = obj.MEAL_NAME & " " & objParam.PO_ERROR
                                        dtError.Rows.Add(rowError)
                                    End If

                                    irow += 1
                                Next
                                If dtError.Rows.Count > 0 Then
                                    cmd.Transaction.Rollback()
                                    Return False
                                End If
                                cmd.CommandText = "PKG_MEAL_BUSINESS.IMPORT_AT_MEAL_EXPLAN"
                                cmd.Parameters.Clear()

                                Dim objParam1 = New With {.P_STARTDATE = startdate,
                                                          .P_ENDDATE = enddate,
                                                          .P_USERNAME = log.Username.ToUpper}

                                If objParam1 IsNot Nothing Then
                                    For Each info As PropertyInfo In objParam1.GetType().GetProperties()
                                        Dim bOut As Boolean = False
                                        Dim para = resource.GetParameter(info.Name, info.GetValue(objParam1, Nothing), bOut)
                                        If para IsNot Nothing Then
                                            cmd.Parameters.Add(para)
                                        End If
                                    Next
                                End If

                                cmd.ExecuteNonQuery()

                                cmd.Transaction.Commit()

                            Catch ex As Exception
                                cmd.Transaction.Rollback()
                                WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iAttendance")
                                Throw ex
                            Finally
                                'Dispose all resource
                                cmd.Dispose()
                                conn.Close()
                                conn.Dispose()
                            End Try
                        End Using
                    End Using
                End Using
            End Using

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iAttendance")
            Throw ex
        End Try
    End Function

#End Region

#Region "HU_STUDENT"


    Public Function Insert_HU_STUDENT(ByVal objData As HU_STUDENT_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim obj As New HU_STUDENT
        Try
            obj.ID = Utilities.GetNextSequence(Context, Context.HU_STUDENT.EntitySet.Name)
            obj.STUDENT_CODE = objData.STUDENT_CODE
            obj.FIRST_NAME_VN = objData.FIRST_NAME_VN
            obj.LAST_NAME_VN = objData.LAST_NAME_VN
            obj.FULLNAME_VN = objData.FULLNAME_VN
            obj.ORG_ID = objData.ORG_ID
            obj.JOIN_DATE = objData.JOIN_DATE
            obj.TER_EFFECT_DATE = objData.TER_EFFECT_DATE
            obj.ID_NO = objData.ID_NO
            obj.ID_DATE = objData.ID_DATE
            obj.ID_PLACE = objData.ID_PLACE
            obj.BIRTH_PLACE = objData.BIRTH_PLACE
            obj.GENDER = objData.GENDER
            obj.PER_ADDRESS = objData.PER_ADDRESS
            obj.MOBILE_PHONE = objData.MOBILE_PHONE
            Context.HU_STUDENT.AddObject(obj)
            Context.SaveChanges(log)
            gID = obj.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iAttendance")
            Throw ex
        End Try
    End Function

    Public Function Modify_HU_STUDENT(ByVal objData As HU_STUDENT_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim obj As New HU_STUDENT With {.ID = objData.ID}
        Try
            obj = (From p In Context.HU_STUDENT Where p.ID = objData.ID).FirstOrDefault
            obj.STUDENT_CODE = objData.STUDENT_CODE
            obj.FIRST_NAME_VN = objData.FIRST_NAME_VN
            obj.LAST_NAME_VN = objData.LAST_NAME_VN
            obj.FULLNAME_VN = objData.FULLNAME_VN
            obj.ORG_ID = objData.ORG_ID
            obj.JOIN_DATE = objData.JOIN_DATE
            obj.TER_EFFECT_DATE = objData.TER_EFFECT_DATE
            obj.ID_NO = objData.ID_NO
            obj.ID_DATE = objData.ID_DATE
            obj.ID_PLACE = objData.ID_PLACE
            obj.BIRTH_PLACE = objData.BIRTH_PLACE
            obj.GENDER = objData.GENDER
            obj.PER_ADDRESS = objData.PER_ADDRESS
            obj.MOBILE_PHONE = objData.MOBILE_PHONE
            Context.SaveChanges(log)
            gID = obj.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iAttendance")
            Throw ex
        End Try
    End Function

    Public Function Delete_HU_STUDENT(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lst As List(Of HU_STUDENT)
        Try
            lst = (From p In Context.HU_STUDENT Where lstID.Contains(p.ID)).ToList
            For index = 0 To lst.Count - 1
                Context.HU_STUDENT.DeleteObject(lst(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iAttendance")
            Throw ex
        End Try
    End Function

    
    Public Function Get_HU_STUDENTById(ByVal _id As Decimal?) As HU_STUDENT_DTO
        Try

            Dim query = From p In Context.HU_STUDENT
                        Where p.ID = _id

            Dim lst = query.Select(Function(p) New HU_STUDENT_DTO With {
                                         .ID = p.ID,
                                         .STUDENT_CODE = p.STUDENT_CODE,
                                         .FIRST_NAME_VN = p.FIRST_NAME_VN,
                                         .LAST_NAME_VN = p.LAST_NAME_VN,
                                         .FULLNAME_VN = p.FULLNAME_VN,
                                         .ORG_ID = p.ORG_ID,
                                         .JOIN_DATE = p.JOIN_DATE,
                                         .TER_EFFECT_DATE = p.TER_EFFECT_DATE,
                                         .ID_NO = p.ID_NO,
                                         .ID_DATE = p.ID_DATE,
                                         .ID_PLACE = p.ID_PLACE,
                                         .BIRTH_PLACE = p.BIRTH_PLACE,
                                         .GENDER = p.GENDER,
                                         .PER_ADDRESS = p.PER_ADDRESS,
                                         .MOBILE_PHONE = p.MOBILE_PHONE,
                                         .CREATED_DATE = p.CREATED_DATE,
                                         .CREATED_BY = p.CREATED_BY,
                                         .CREATED_LOG = p.CREATED_LOG,
                                         .MODIFIED_DATE = p.MODIFIED_DATE,
                                         .MODIFIED_BY = p.MODIFIED_BY,
                                         .MODIFIED_LOG = p.MODIFIED_LOG}).FirstOrDefault
            Return lst
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iAttendance")
            Throw ex
        End Try
    End Function

    Public Function Get_HU_STUDENT(ByVal _filter As HU_STUDENT_DTO,
                                           ByVal _param As ParamDTO,
                                           Optional ByVal PageIndex As Integer = 0,
                                           Optional ByVal PageSize As Integer = Integer.MaxValue,
                                           Optional ByRef Total As Integer = 0,
                                           Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of HU_STUDENT_DTO)
        Try

            Dim query = From p In Context.HU_STUDENT

            Dim lst = query.Select(Function(p) New HU_STUDENT_DTO With {
                                        .ID = p.ID,
                                        .STUDENT_CODE = p.STUDENT_CODE,
                                        .FIRST_NAME_VN = p.FIRST_NAME_VN,
                                        .LAST_NAME_VN = p.LAST_NAME_VN,
                                        .FULLNAME_VN = p.FULLNAME_VN,
                                        .ORG_ID = p.ORG_ID,
                                        .JOIN_DATE = p.JOIN_DATE,
                                        .TER_EFFECT_DATE = p.TER_EFFECT_DATE,
                                        .ID_NO = p.ID_NO,
                                        .ID_DATE = p.ID_DATE,
                                        .ID_PLACE = p.ID_PLACE,
                                        .BIRTH_PLACE = p.BIRTH_PLACE,
                                        .GENDER = p.GENDER,
                                        .PER_ADDRESS = p.PER_ADDRESS,
                                        .MOBILE_PHONE = p.MOBILE_PHONE,
                                        .CREATED_DATE = p.CREATED_DATE,
                                        .CREATED_BY = p.CREATED_BY,
                                        .CREATED_LOG = p.CREATED_LOG,
                                        .MODIFIED_DATE = p.MODIFIED_DATE,
                                        .MODIFIED_BY = p.MODIFIED_BY,
                                        .MODIFIED_LOG = p.MODIFIED_LOG})
            If Not String.IsNullOrEmpty(_filter.STUDENT_CODE) Then
                lst = lst.Where(Function(f) f.STUDENT_CODE.ToLower().Contains(_filter.STUDENT_CODE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.FIRST_NAME_VN) Then
                lst = lst.Where(Function(f) f.FIRST_NAME_VN.ToLower().Contains(_filter.FIRST_NAME_VN.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.LAST_NAME_VN) Then
                lst = lst.Where(Function(f) f.LAST_NAME_VN.ToLower().Contains(_filter.LAST_NAME_VN.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.FULLNAME_VN) Then
                lst = lst.Where(Function(f) f.FULLNAME_VN.ToLower().Contains(_filter.FULLNAME_VN.ToLower()))
            End If
            If _filter.ORG_ID.HasValue Then
                lst = lst.Where(Function(f) f.ORG_ID = _filter.ORG_ID)
            End If
            If _filter.JOIN_DATE.HasValue Then
                lst = lst.Where(Function(f) f.JOIN_DATE = _filter.JOIN_DATE)
            End If
            If _filter.TER_EFFECT_DATE.HasValue Then
                lst = lst.Where(Function(f) f.TER_EFFECT_DATE = _filter.TER_EFFECT_DATE)
            End If
            If Not String.IsNullOrEmpty(_filter.ID_NO) Then
                lst = lst.Where(Function(f) f.ID_NO.ToLower().Contains(_filter.ID_NO.ToLower()))
            End If
            If _filter.ID_DATE.HasValue Then
                lst = lst.Where(Function(f) f.ID_DATE = _filter.ID_DATE)
            End If
            If Not String.IsNullOrEmpty(_filter.ID_PLACE) Then
                lst = lst.Where(Function(f) f.ID_PLACE.ToLower().Contains(_filter.ID_PLACE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.BIRTH_PLACE) Then
                lst = lst.Where(Function(f) f.BIRTH_PLACE.ToLower().Contains(_filter.BIRTH_PLACE.ToLower()))
            End If
            If _filter.GENDER.HasValue Then
                lst = lst.Where(Function(f) f.GENDER = _filter.GENDER)
            End If
            If Not String.IsNullOrEmpty(_filter.PER_ADDRESS) Then
                lst = lst.Where(Function(f) f.PER_ADDRESS.ToLower().Contains(_filter.PER_ADDRESS.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.MOBILE_PHONE) Then
                lst = lst.Where(Function(f) f.MOBILE_PHONE.ToLower().Contains(_filter.MOBILE_PHONE.ToLower()))
            End If
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Return lst.ToList()
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iAttendance")
            Throw ex
        End Try
    End Function

#End Region

End Class
