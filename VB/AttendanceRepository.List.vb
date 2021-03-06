Imports Attendance.AttendanceBusiness
Imports Framework.UI

Partial Class AttendanceRepository
    Inherits AttendanceRepositoryBase
#Region "Holiday"

    Public Function GetHoliday(ByVal _filter As AT_HOLIDAYDTO,
                                      Optional ByVal PageIndex As Integer = 0,
                                        Optional ByVal PageSize As Integer = Integer.MaxValue,
                                        Optional ByRef Total As Integer = 0,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_HOLIDAYDTO)
        Dim lstHoliday As List(Of AT_HOLIDAYDTO)

        Using rep As New AttendanceBusinessClient
            Try
                lstHoliday = rep.GetHoliday(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstHoliday
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertHoliday(ByVal objHoliday As AT_HOLIDAYDTO, ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.InsertHOLIDAY(objHoliday, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateHoliday(ByVal objHoliday As AT_HOLIDAYDTO) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ValidateHOLIDAY(objHoliday)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyHoliday(ByVal objHoliday As AT_HOLIDAYDTO, ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ModifyHOLIDAY(objHoliday, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveHoliday(ByVal lstHoliday As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ActiveHoliday(lstHoliday, Me.Log, sActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteHoliday(ByVal lstHoliday As List(Of Decimal)) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.DeleteHOLIDAY(lstHoliday)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "Holiday Gerenal"

    Public Function GetHolidayGerenal(ByVal _filter As AT_HOLIDAY_GENERALDTO,
                                     Optional ByVal PageIndex As Integer = 0,
                                        Optional ByVal PageSize As Integer = Integer.MaxValue,
                                        Optional ByRef Total As Integer = 0,
                                       Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_HOLIDAY_GENERALDTO)
        Dim lstHoliday As List(Of AT_HOLIDAY_GENERALDTO)

        Using rep As New AttendanceBusinessClient
            Try
                lstHoliday = rep.GetHolidayGerenal(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstHoliday
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertHolidayGerenal(ByVal objHoliday As AT_HOLIDAY_GENERALDTO, ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.InsertHolidayGerenal(objHoliday, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateHolidayGerenal(ByVal objHoliday As AT_HOLIDAY_GENERALDTO) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ValidateHolidayGerenal(objHoliday)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyHolidayGerenal(ByVal objHoliday As AT_HOLIDAY_GENERALDTO, ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ModifyHolidayGerenal(objHoliday, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveHolidayGerenal(ByVal lstHoliday As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ActiveHolidayGerenal(lstHoliday, Me.Log, sActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteHolidayGerenal(ByVal lstHoliday As List(Of Decimal)) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.DeleteHolidayGerenal(lstHoliday)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "danh mục kiểu công"
    Public Function GetSignByPage(ByVal pagecode As String) As List(Of AT_TIME_MANUALDTO)
        Dim lstManual As List(Of AT_TIME_MANUALDTO)

        Using rep As New AttendanceBusinessClient
            Try
                lstManual = rep.GetSignByPage(pagecode)
                Return lstManual
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function

    Public Function GetAT_FML(ByVal _filter As AT_FMLDTO,
                                    Optional ByVal PageIndex As Integer = 0,
                                        Optional ByVal PageSize As Integer = Integer.MaxValue,
                                        Optional ByRef Total As Integer = 0,
                                      Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_FMLDTO)
        Dim lstHoliday As List(Of AT_FMLDTO)

        Using rep As New AttendanceBusinessClient
            Try
                lstHoliday = rep.GetAT_FML(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstHoliday
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertAT_FML(ByVal objHoliday As AT_FMLDTO, ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.InsertAT_FML(objHoliday, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateAT_FML(ByVal objHoliday As AT_FMLDTO) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ValidateAT_FML(objHoliday)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyAT_FML(ByVal objHoliday As AT_FMLDTO, ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ModifyAT_FML(objHoliday, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveAT_FML(ByVal lstHoliday As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ActiveAT_FML(lstHoliday, Me.Log, sActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteAT_FML(ByVal lstHoliday As List(Of Decimal)) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.DeleteAT_FML(lstHoliday)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "danh mục ca làm việc"
    Public Function GetAT_GSIGN(ByVal _filter As AT_GSIGNDTO,
                                    Optional ByVal PageIndex As Integer = 0,
                                        Optional ByVal PageSize As Integer = Integer.MaxValue,
                                        Optional ByRef Total As Integer = 0,
                                     Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_GSIGNDTO)
        Dim lstAt_GSIGN As List(Of AT_GSIGNDTO)

        Using rep As New AttendanceBusinessClient
            Try
                lstAt_GSIGN = rep.GetAT_GSIGN(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstAt_GSIGN
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertAT_GSIGN(ByVal objHoliday As AT_GSIGNDTO, ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.InsertAT_GSIGN(objHoliday, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateAT_GSIGN(ByVal objHoliday As AT_GSIGNDTO) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ValidateAT_GSIGN(objHoliday)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyAT_GSIGN(ByVal objHoliday As AT_GSIGNDTO, ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ModifyAT_GSIGN(objHoliday, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveAT_GSIGN(ByVal lstAT_GSIGN As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ActiveAT_GSIGN(lstAT_GSIGN, Me.Log, sActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteAT_GSIGN(ByVal lstAT_GSIGN As List(Of Decimal)) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.DeleteAT_GSIGN(lstAT_GSIGN)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "quyết định phạt đi muộn về sớm"
    Public Function GetAT_DMVS(ByVal _filter As AT_DMVSDTO,
                                    Optional ByVal PageIndex As Integer = 0,
                                        Optional ByVal PageSize As Integer = Integer.MaxValue,
                                        Optional ByRef Total As Integer = 0,
                                     Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_DMVSDTO)
        Dim lstDMVS As List(Of AT_DMVSDTO)

        Using rep As New AttendanceBusinessClient
            Try
                lstDMVS = rep.GetAT_DMVS(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstDMVS
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertAT_DMVS(ByVal objDMVS As AT_DMVSDTO, ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.InsertAT_DMVS(objDMVS, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateAT_DMVS(ByVal objDMVS As AT_DMVSDTO) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ValidateAT_DMVS(objDMVS)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyAT_DMVS(ByVal objDMVS As AT_DMVSDTO, ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ModifyAT_DMVS(objDMVS, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveAT_DMVS(ByVal lstDMVS As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ActiveAT_DMVS(lstDMVS, Me.Log, sActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteAT_DMVS(ByVal lstDMVS As List(Of Decimal)) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.DeleteAT_DMVS(lstDMVS)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "Danh mục ca làm việc"
    Public Function GetAT_SHIFT(ByVal _filter As AT_SHIFTDTO,
                                Optional ByVal PageIndex As Integer = 0,
                                Optional ByVal PageSize As Integer = Integer.MaxValue,
                                Optional ByRef Total As Integer = 0,
                                Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_SHIFTDTO)
        Dim lstDMVS As List(Of AT_SHIFTDTO)

        Using rep As New AttendanceBusinessClient
            Try
                lstDMVS = rep.GetAT_SHIFT(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstDMVS
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function

    Public Function InsertAT_SHIFT(ByVal objDMVS As AT_SHIFTDTO, ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.InsertAT_SHIFT(objDMVS, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateAT_SHIFT(ByVal objDMVS As AT_SHIFTDTO) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ValidateAT_SHIFT(objDMVS)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyAT_SHIFT(ByVal objDMVS As AT_SHIFTDTO, ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ModifyAT_SHIFT(objDMVS, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveAT_SHIFT(ByVal lstDMVS As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ActiveAT_SHIFT(lstDMVS, Me.Log, sActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteAT_SHIFT(ByVal lstDMVS As List(Of Decimal)) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.DeleteAT_SHIFT(lstDMVS)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetAT_TIME_MANUALBINCOMBO() As DataTable
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.GetAT_TIME_MANUALBINCOMBO()
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
#End Region

#Region "Thiết lập số ngày nghỉ theo đối tượng"
    Public Function GetAT_Holiday_Object(ByVal _filter As AT_HOLIDAY_OBJECTDTO,
                                     Optional ByVal PageIndex As Integer = 0,
                                        Optional ByVal PageSize As Integer = Integer.MaxValue,
                                        Optional ByRef Total As Integer = 0,
                                   Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_HOLIDAY_OBJECTDTO)
        Dim lstHolidayObj As List(Of AT_HOLIDAY_OBJECTDTO)

        Using rep As New AttendanceBusinessClient
            Try
                lstHolidayObj = rep.GetAT_Holiday_Object(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstHolidayObj
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function

    Public Function InsertAT_Holiday_Object(ByVal objDMVS As AT_HOLIDAY_OBJECTDTO, ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.InsertAT_Holiday_Object(objDMVS, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateAT_Holiday_Object(ByVal objDMVS As AT_HOLIDAY_OBJECTDTO) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ValidateAT_Holiday_Object(objDMVS)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyAT_Holiday_Object(ByVal objDMVS As AT_HOLIDAY_OBJECTDTO, ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ModifyAT_Holiday_Object(objDMVS, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveAT_Holiday_Object(ByVal lstHolidayObj As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ActiveAT_Holiday_Object(lstHolidayObj, Me.Log, sActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteAT_Holiday_Object(ByVal lstHolidayObj As List(Of Decimal)) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.DeleteAT_Holiday_Object(lstHolidayObj)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
#End Region

#Region "Thiết lập đối tượng chấm công theo cấp nhân sự"
    Public Function GetAT_SETUP_SPECIAL(ByVal _filter As AT_SETUP_SPECIALDTO,
                                    Optional ByVal PageIndex As Integer = 0,
                                        Optional ByVal PageSize As Integer = Integer.MaxValue,
                                        Optional ByRef Total As Integer = 0,
                                   Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_SETUP_SPECIALDTO)
        Dim lstSetUp_SP As List(Of AT_SETUP_SPECIALDTO)

        Using rep As New AttendanceBusinessClient
            Try
                lstSetUp_SP = rep.GetAT_SETUP_SPECIAL(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstSetUp_SP
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function

    Public Function InsertAT_SETUP_SPECIAL(ByVal objDMVS As AT_SETUP_SPECIALDTO, ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.InsertAT_SETUP_SPECIAL(objDMVS, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateAT_SETUP_SPECIAL(ByVal objDMVS As AT_SETUP_SPECIALDTO) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ValidateAT_SETUP_SPECIAL(objDMVS)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyAT_SETUP_SPECIAL(ByVal objDMVS As AT_SETUP_SPECIALDTO, ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ModifyAT_SETUP_SPECIAL(objDMVS, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveAT_SETUP_SPECIAL(ByVal lstSetUp As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ActiveAT_SETUP_SPECIAL(lstSetUp, Me.Log, sActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function DeleteAT_SETUP_SPECIAL(ByVal lstSetUp As List(Of Decimal)) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.DeleteAT_SETUP_SPECIAL(lstSetUp)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
#End Region

#Region "Thiết lập đối tượng chấm công theo nhân viên"
    Public Function GetAT_SETUP_TIME_EMP(ByVal _filter As AT_SETUP_TIME_EMPDTO,
                                    Optional ByVal PageIndex As Integer = 0,
                                    Optional ByVal PageSize As Integer = Integer.MaxValue,
                                    Optional ByRef Total As Integer = 0,
                                    Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_SETUP_TIME_EMPDTO)
        Dim lstSetUp_SP As List(Of AT_SETUP_TIME_EMPDTO)

        Using rep As New AttendanceBusinessClient
            Try
                lstSetUp_SP = rep.GetAT_SETUP_TIME_EMP(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstSetUp_SP
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function

    Public Function InsertAT_SETUP_TIME_EMP(ByVal objDMVS As AT_SETUP_TIME_EMPDTO, ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.InsertAT_SETUP_TIME_EMP(objDMVS, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateAT_SETUP_TIME_EMP(ByVal objDMVS As AT_SETUP_TIME_EMPDTO) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ValidateAT_SETUP_TIME_EMP(objDMVS)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyAT_SETUP_TIME_EMP(ByVal objDMVS As AT_SETUP_TIME_EMPDTO, ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ModifyAT_SETUP_TIME_EMP(objDMVS, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveAT_SETUP_TIME_EMP(ByVal lstSetUp As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ActiveAT_SETUP_TIME_EMP(lstSetUp, Me.Log, sActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function DeleteAT_SETUP_TIME_EMP(ByVal lstSetUp As List(Of Decimal)) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.DeleteAT_SETUP_TIME_EMP(lstSetUp)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
#End Region

#Region "Thiết lập máy chấm công"
    Public Function GetAT_TERMINAL(ByVal _filter As AT_TERMINALSDTO,
                                    Optional ByVal PageIndex As Integer = 0,
                                        Optional ByVal PageSize As Integer = Integer.MaxValue,
                                        Optional ByRef Total As Integer = 0,
                                   Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_TERMINALSDTO)
        Dim lstTerminal As List(Of AT_TERMINALSDTO)

        Using rep As New AttendanceBusinessClient
            Try
                lstTerminal = rep.GetAT_TERMINAL(_filter, PageIndex, PageSize, Total, Sorts, Log)
                Return lstTerminal
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function

    Public Function GetAT_TERMINAL_STATUS(ByVal _filter As AT_TERMINALSDTO,
                                        Optional ByVal PageIndex As Integer = 0,
                                            Optional ByVal PageSize As Integer = Integer.MaxValue,
                                            Optional ByRef Total As Integer = 0,
                                       Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_TERMINALSDTO)
        Dim lstTerminal As List(Of AT_TERMINALSDTO)

        Using rep As New AttendanceBusinessClient
            Try
                lstTerminal = rep.GetAT_TERMINAL_STATUS(_filter, PageIndex, PageSize, Total, Sorts, Log)
                Return lstTerminal
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function

    Public Function InsertAT_TERMINAL(ByVal objTerminal As AT_TERMINALSDTO, ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.InsertAT_TERMINAL(objTerminal, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateAT_TERMINAL(ByVal objTerminal As AT_TERMINALSDTO) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ValidateAT_TERMINAL(objTerminal)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyAT_TERMINAL(ByVal objTerminal As AT_TERMINALSDTO, ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ModifyAT_TERMINAL(objTerminal, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveAT_TERMINAL(ByVal lstTerminal As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ActiveAT_TERMINAL(lstTerminal, Me.Log, sActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteAT_TERMINAL(ByVal lstTerminal As List(Of Decimal)) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.DeleteAT_TERMINAL(lstTerminal)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
#End Region

#Region "Thiết lập chấm công đặc biệt"
    Public Function GetAT_SIGNDEFAULT(ByVal _filter As AT_SIGNDEFAULTDTO,
                                     Optional ByVal PageIndex As Integer = 0,
                                        Optional ByVal PageSize As Integer = Integer.MaxValue,
                                        Optional ByRef Total As Integer = 0,
                                   Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_SIGNDEFAULTDTO)
        Dim lstSign As List(Of AT_SIGNDEFAULTDTO)

        Using rep As New AttendanceBusinessClient
            Try
                lstSign = rep.GetAT_SIGNDEFAULT(_filter, PageIndex, PageSize, Total, Sorts, Me.Log)
                Return lstSign
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function

    Public Function GetAT_ListShift() As DataTable
        Dim lstSign As DataTable

        Using rep As New AttendanceBusinessClient
            Try
                lstSign = rep.GetAT_ListShift()
                Return lstSign
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function

    Public Function GetAT_PERIOD() As DataTable
        Dim lstSign As DataTable

        Using rep As New AttendanceBusinessClient
            Try
                lstSign = rep.GetAT_PERIOD()
                Return lstSign
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function

    Public Function GetEmployeeID(ByVal employee_code As String, ByVal period_id As Decimal) As DataTable
        Dim lstSign As DataTable

        Using rep As New AttendanceBusinessClient
            Try
                lstSign = rep.GetEmployeeID(employee_code, period_id)
                Return lstSign
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function

    Public Function GetEmployeeIDInSign(ByVal employee_code As String) As DataTable
        Dim lstSign As DataTable

        Using rep As New AttendanceBusinessClient
            Try
                lstSign = rep.GetEmployeeIDInSign(employee_code)
                Return lstSign
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function

    Public Function GetEmployeeByTimeID(ByVal time_id As Decimal) As DataTable
        Dim lstSign As DataTable

        Using rep As New AttendanceBusinessClient
            Try
                lstSign = rep.GetEmployeeByTimeID(time_id)
                Return lstSign
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function

    Public Function InsertAT_SIGNDEFAULT(ByVal objSign As AT_SIGNDEFAULTDTO, ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.InsertAT_SIGNDEFAULT(objSign, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyAT_SIGNDEFAULT(ByVal objSign As AT_SIGNDEFAULTDTO, ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ModifyAT_SIGNDEFAULT(objSign, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateAT_SIGNDEFAULT(ByVal objSign As AT_SIGNDEFAULTDTO) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ValidateAT_SIGNDEFAULT(objSign)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveAT_SIGNDEFAULT(ByVal lstSign As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ActiveAT_SIGNDEFAULT(lstSign, Me.Log, sActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteAT_SIGNDEFAULT(ByVal lstSign As List(Of Decimal)) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.DeleteAT_SIGNDEFAULT(lstSign)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
#End Region

#Region "Đăng ký nghỉ trên iportal"
    Public Function GetRegisterAppointmentInPortalByEmployee(ByVal empid As Decimal, ByVal startdate As Date, ByVal enddate As Date,
                                                                ByVal listSign As List(Of AT_TIME_MANUALDTO), ByVal status As List(Of Short)) As List(Of AT_TIMESHEET_REGISTERDTO)

        Try
            _isAvailable = False
            Using rep As New AttendanceBusinessClient
                Try
                    Return rep.GetRegisterAppointmentInPortalByEmployee(empid, startdate, enddate, listSign, status)
                Catch ex As Exception
                    rep.Abort()
                    Throw ex
                End Try
            End Using

        Catch ex As Exception
            Throw ex
        Finally
            _isAvailable = True
        End Try

    End Function
    Public Function GetTotalLeaveInYear(ByVal empid As Decimal, ByVal p_year As Decimal) As Decimal

        Try
            _isAvailable = False
            Using rep As New AttendanceBusinessClient
                Try
                    Return rep.GetTotalLeaveInYear(empid, p_year)
                Catch ex As Exception
                    rep.Abort()
                    Throw ex
                End Try
            End Using

        Catch ex As Exception
            Throw ex
        Finally
            _isAvailable = True
        End Try

    End Function

    Public Function InsertPortalRegister(ByVal itemRegister As AttendanceBusiness.AT_PORTAL_REG_DTO) As Boolean
        Try
            _isAvailable = False

            Using rep As New AttendanceBusinessClient
                Try
                    Return rep.InsertPortalRegister(itemRegister, Me.Log)
                Catch ex As Exception
                    rep.Abort()
                    Throw ex
                End Try
            End Using

        Catch ex As Exception
            Throw ex
        Finally
            _isAvailable = True
        End Try

    End Function

    Public Function GetHolidayByCalender(ByVal startdate As Date, ByVal enddate As Date) As List(Of Date)
        Try
            _isAvailable = False
            Using rep As New AttendanceBusinessClient
                Try
                    Return rep.GetHolidayByCalender(startdate, enddate)
                Catch ex As Exception
                    rep.Abort()
                    Throw ex
                End Try
            End Using

        Catch ex As Exception
            Throw ex
        Finally
            _isAvailable = True
        End Try
    End Function

    Public Function DeletePortalRegisterByDate(ByVal listappointment As List(Of AT_TIMESHEET_REGISTERDTO), ByVal listSign As List(Of AT_TIME_MANUALDTO)) As Boolean
        Try
            _isAvailable = False
            Using rep As New AttendanceBusinessClient
                Try
                    Return rep.DeletePortalRegisterByDate(listappointment, listSign)
                Catch ex As Exception
                    rep.Abort()
                    Throw ex
                End Try
            End Using

        Catch ex As Exception
            Throw ex
        Finally
            _isAvailable = True
        End Try

    End Function

    Public Function DeletePortalRegister(ByVal id As Decimal) As Boolean
        Try
            _isAvailable = False
            Using rep As New AttendanceBusinessClient
                Try
                    Return rep.DeletePortalRegister(id)
                Catch ex As Exception
                    rep.Abort()
                    Throw ex
                End Try
            End Using

        Catch ex As Exception
            Throw ex
        Finally
            _isAvailable = True
        End Try

    End Function

    Public Function SendRegisterToApprove(ByVal objLstRegisterId As List(Of Decimal), ByVal process As String, ByVal currentUrl As String) As String
        Try
            _isAvailable = False
            Using rep As New AttendanceBusinessClient
                Try
                    Return rep.SendRegisterToApprove(objLstRegisterId, process, currentUrl)
                Catch ex As Exception
                    rep.Abort()
                    Throw ex
                End Try
            End Using

        Catch ex As Exception
            Throw ex
        Finally
            _isAvailable = True
        End Try

    End Function

#End Region

#Region "Phê duyệt đăng ký nghỉ trên iportal"
    Public Function GetListSignCode(ByVal gSignCode As String) As List(Of AT_TIME_MANUALDTO)
        Try
            _isAvailable = False
            Using rep As New AttendanceBusinessClient
                Try
                    Return rep.GetListSignCode(gSignCode)
                Catch ex As Exception
                    rep.Abort()
                    Throw ex
                End Try
            End Using

        Catch ex As Exception
            Throw ex
        Finally
            _isAvailable = True
        End Try
    End Function
    Public Function GetListWaitingForApprove(ByVal approveId As Decimal, ByVal process As String, ByVal filter As AttendanceBusiness.ATRegSearchDTO) As List(Of AttendanceBusiness.AT_PORTAL_REG_DTO)
        Try
            _isAvailable = False

            Using rep As New AttendanceBusinessClient
                Try
                    Return rep.GetListWaitingForApprove(approveId, process, filter)
                Catch ex As Exception
                    rep.Abort()
                    Throw ex
                End Try
            End Using

        Catch ex As Exception
            Throw ex
        Finally
            _isAvailable = True
        End Try
    End Function
    Public Function ApprovePortalRegister(ByVal regID As Guid, ByVal approveId As Decimal,
                                          ByVal status As Integer, ByVal note As String,
                                          ByVal currentUrl As String, ByVal process As String) As Boolean
        Try
            _isAvailable = False
            Using rep As New AttendanceBusinessClient
                Try
                    Return rep.ApprovePortalRegister(regID, approveId, status, note, currentUrl, process, Log)
                Catch ex As Exception
                    rep.Abort()
                    Throw ex
                End Try
            End Using

        Catch ex As Exception
            Throw ex
        Finally
            _isAvailable = True
        End Try
    End Function
    Public Function GetEmployeeList() As DataTable
        Try
            _isAvailable = False

            Using rep As New AttendanceBusinessClient
                Try
                    Return rep.GetEmployeeList()
                Catch ex As Exception
                    rep.Abort()
                    Throw ex
                End Try
            End Using

        Catch ex As Exception
            Throw ex
        Finally
            _isAvailable = True
        End Try
    End Function
    Public Function GetLeaveDay(ByVal dDate As Date) As DataTable
        Try
            _isAvailable = False

            Using rep As New AttendanceBusinessClient
                Try
                    Return rep.GetLeaveDay(dDate)
                Catch ex As Exception
                    rep.Abort()
                    Throw ex
                End Try
            End Using

        Catch ex As Exception
            Throw ex
        Finally
            _isAvailable = True
        End Try
    End Function
#End Region

#Region "Thiết lập kiểu công"

    Public Function GetAT_TIME_MANUAL(ByVal _filter As AT_TIME_MANUALDTO,
                                    Optional ByVal PageIndex As Integer = 0,
                                        Optional ByVal PageSize As Integer = Integer.MaxValue,
                                        Optional ByRef Total As Integer = 0,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_TIME_MANUALDTO)
        Dim lstHoliday As List(Of AT_TIME_MANUALDTO)

        Using rep As New AttendanceBusinessClient
            Try
                lstHoliday = rep.GetAT_TIME_MANUAL(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstHoliday
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetAT_TIME_MANUALById(ByVal _id As Decimal?) As AT_TIME_MANUALDTO
        Dim lstHoliday As AT_TIME_MANUALDTO
        Using rep As New AttendanceBusinessClient
            Try
                lstHoliday = rep.GetAT_TIME_MANUALById(_id)
                Return lstHoliday
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertAT_TIME_MANUAL(ByVal objHoliday As AT_TIME_MANUALDTO, ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.InsertAT_TIME_MANUAL(objHoliday, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateAT_TIME_MANUAL(ByVal objHoliday As AT_TIME_MANUALDTO) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ValidateAT_TIME_MANUAL(objHoliday)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyAT_TIME_MANUAL(ByVal objHoliday As AT_TIME_MANUALDTO, ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ModifyAT_TIME_MANUAL(objHoliday, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveAT_TIME_MANUAL(ByVal lstHoliday As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ActiveAT_TIME_MANUAL(lstHoliday, Me.Log, sActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteAT_TIME_MANUAL(ByVal lstHoliday As List(Of Decimal)) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.DeleteAT_TIME_MANUAL(lstHoliday)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function GetDataImportCO() As DataTable
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.GetDataImportCO()
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
#End Region

#Region "Danh mục tham số hệ thống"
    Public Function GetListParamItime(ByVal _filter As AT_LISTPARAM_SYSTEAMDTO,
                                     Optional ByVal PageIndex As Integer = 0,
                                        Optional ByVal PageSize As Integer = Integer.MaxValue,
                                        Optional ByRef Total As Integer = 0,
                                       Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_LISTPARAM_SYSTEAMDTO)
        Dim lstHoliday As List(Of AT_LISTPARAM_SYSTEAMDTO)

        Using rep As New AttendanceBusinessClient
            Try
                lstHoliday = rep.GetListParamItime(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstHoliday
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertListParamItime(ByVal lstData As AT_LISTPARAM_SYSTEAMDTO, ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.InsertListParamItime(lstData, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateListParamItime(ByVal lstData As AT_LISTPARAM_SYSTEAMDTO) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ValidateListParamItime(lstData)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyListParamItime(ByVal lstData As AT_LISTPARAM_SYSTEAMDTO, ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ModifyListParamItime(lstData, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveListParamItime(ByVal lstData As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ActiveListParamItime(lstData, Me.Log, sActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteListParamItime(ByVal lstData As List(Of Decimal)) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.DeleteListParamItime(lstData)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
#End Region

#Region ""
    Public Function AutoGenCode(ByVal firstChar As String, ByVal tableName As String, ByVal colName As String) As String
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.AutoGenCode(firstChar, tableName, colName)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function CheckExistInDatabase(ByVal lstID As List(Of Decimal), ByVal table As AttendanceCommonTABLE_NAME) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.CheckExistInDatabase(lstID, table)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function CheckExistInDatabaseAT_SIGNDEFAULT(ByVal lstID As List(Of Decimal), ByVal lstWorking As List(Of Date), ByVal lstShift As List(Of Decimal), ByVal table As AttendanceCommonTABLE_NAME) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.CheckExistInDatabaseAT_SIGNDEFAULT(lstID, lstWorking, lstShift, table)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
#End Region

End Class
