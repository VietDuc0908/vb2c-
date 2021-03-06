Imports AttendanceBusiness.ServiceContracts
Imports AttendanceDAL
Imports Framework.Data
Imports System.Collections.Generic
Imports LinqKit

' NOTE: You can use the "Rename" command on the context menu to change the class name "Service1" in both code and config file together.
Namespace AttendanceBusiness.ServiceImplementations
    Partial Public Class AttendanceBusiness
#Region "HOLIDAY"

        Public Function GetHoliday(ByVal _filter As AT_HOLIDAYDTO,
                                    Optional ByVal PageIndex As Integer = 0,
                                        Optional ByVal PageSize As Integer = Integer.MaxValue,
                                        Optional ByRef Total As Integer = 0,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_HOLIDAYDTO) Implements ServiceContracts.IAttendanceBusiness.GetHoliday
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetHoliday(_filter, PageIndex, PageSize, Total, Sorts)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertHOLIDAY(ByVal objHOLIDAY As AT_HOLIDAYDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.InsertHOLIDAY
            Using rep As New AttendanceRepository
                Try

                    Return rep.InsertHoliday(objHOLIDAY, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ValidateHOLIDAY(ByVal objHOLIDAY As AT_HOLIDAYDTO) As Boolean Implements ServiceContracts.IAttendanceBusiness.ValidateHOLIDAY
            Using rep As New AttendanceRepository
                Try

                    Return rep.ValidateHoliday(objHOLIDAY)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyHOLIDAY(ByVal objHOLIDAY As AT_HOLIDAYDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.ModifyHOLIDAY
            Using rep As New AttendanceRepository
                Try

                    Return rep.ModifyHoliday(objHOLIDAY, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ActiveHoliday(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean Implements ServiceContracts.IAttendanceBusiness.ActiveHoliday
            Using rep As New AttendanceRepository
                Try

                    Return rep.ActiveHoliday(lstID, log, bActive)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteHOLIDAY(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IAttendanceBusiness.DeleteHOLIDAY
            Using rep As New AttendanceRepository
                Try

                    Return rep.DeleteHoliday(lstID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "Holiday gerenal"

        Public Function GetHolidayGerenal(ByVal _filter As AT_HOLIDAY_GENERALDTO,
                                    Optional ByVal PageIndex As Integer = 0,
                                        Optional ByVal PageSize As Integer = Integer.MaxValue,
                                        Optional ByRef Total As Integer = 0,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_HOLIDAY_GENERALDTO) Implements ServiceContracts.IAttendanceBusiness.GetHolidayGerenal
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetHolidayGerenal(_filter, PageIndex, PageSize, Total, Sorts)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertHolidayGerenal(ByVal objHOLIDAYGR As AT_HOLIDAY_GENERALDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.InsertHolidayGerenal
            Using rep As New AttendanceRepository
                Try

                    Return rep.InsertHolidayGerenal(objHOLIDAYGR, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ValidateHolidayGerenal(ByVal objHOLIDAYGR As AT_HOLIDAY_GENERALDTO) As Boolean Implements ServiceContracts.IAttendanceBusiness.ValidateHolidayGerenal
            Using rep As New AttendanceRepository
                Try

                    Return rep.ValidateHolidayGerenal(objHOLIDAYGR)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyHoliday(ByVal objHOLIDAYGR As AT_HOLIDAY_GENERALDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.ModifyHolidayGerenal
            Using rep As New AttendanceRepository
                Try

                    Return rep.ModifyHolidayGerenal(objHOLIDAYGR, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ActiveHolidayGerenal(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean Implements ServiceContracts.IAttendanceBusiness.ActiveHolidayGerenal
            Using rep As New AttendanceRepository
                Try

                    Return rep.ActiveHolidayGerenal(lstID, log, bActive)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteHolidayGerenal(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IAttendanceBusiness.DeleteHolidayGerenal
            Using rep As New AttendanceRepository
                Try

                    Return rep.DeleteHolidayGerenal(lstID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region " AT_FML Danh mục kiểu công"
        Public Function GetSignByPage(ByVal pagecode As String) As List(Of AT_TIME_MANUALDTO) Implements ServiceContracts.IAttendanceBusiness.GetSignByPage
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetSignByPage(pagecode)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetAT_FML(ByVal _filter As AT_FMLDTO,
                                      Optional ByVal PageIndex As Integer = 0,
                                        Optional ByVal PageSize As Integer = Integer.MaxValue,
                                        Optional ByRef Total As Integer = 0,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_FMLDTO) Implements ServiceContracts.IAttendanceBusiness.GetAT_FML
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetAT_FML(_filter, PageIndex, PageSize, Total, Sorts)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertAT_FML(ByVal objAT_FML As AT_FMLDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.InsertAT_FML
            Using rep As New AttendanceRepository
                Try

                    Return rep.InsertAT_FML(objAT_FML, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ValidateAT_FML(ByVal objAT_FML As AT_FMLDTO) As Boolean Implements ServiceContracts.IAttendanceBusiness.ValidateAT_FML
            Using rep As New AttendanceRepository
                Try

                    Return rep.ValidateAT_FML(objAT_FML)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyAT_FML(ByVal objAT_FML As AT_FMLDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.ModifyAT_FML
            Using rep As New AttendanceRepository
                Try

                    Return rep.ModifyAT_FML(objAT_FML, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ActiveAT_FML(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean Implements ServiceContracts.IAttendanceBusiness.ActiveAT_FML
            Using rep As New AttendanceRepository
                Try

                    Return rep.ActiveAT_FML(lstID, log, bActive)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteAT_FML(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IAttendanceBusiness.DeleteAT_FML
            Using rep As New AttendanceRepository
                Try

                    Return rep.DeleteAT_FML(lstID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "Danh mục ca"
        Public Function GetAT_GSIGN(ByVal _filter As AT_GSIGNDTO,
                                     Optional ByVal PageIndex As Integer = 0,
                                        Optional ByVal PageSize As Integer = Integer.MaxValue,
                                        Optional ByRef Total As Integer = 0,
                                       Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_GSIGNDTO) Implements ServiceContracts.IAttendanceBusiness.GetAT_GSIGN
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetAT_GSIGN(_filter, PageIndex, PageSize, Total, Sorts)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertAT_GSIGN(ByVal objGSFND As AT_GSIGNDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.InsertAT_GSIGN
            Using rep As New AttendanceRepository
                Try

                    Return rep.InsertAT_GSIGN(objGSFND, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ValidateAT_GSIGN(ByVal objGSFND As AT_GSIGNDTO) As Boolean Implements ServiceContracts.IAttendanceBusiness.ValidateAT_GSIGN
            Using rep As New AttendanceRepository
                Try

                    Return rep.ValidateAT_GSIGN(objGSFND)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyAT_GSIGN(ByVal objGSFND As AT_GSIGNDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.ModifyAT_GSIGN
            Using rep As New AttendanceRepository
                Try

                    Return rep.ModifyAT_GSIGN(objGSFND, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ActiveAT_GSIGN(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean Implements ServiceContracts.IAttendanceBusiness.ActiveAT_GSIGN
            Using rep As New AttendanceRepository
                Try

                    Return rep.ActiveAT_GSIGN(lstID, log, bActive)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteAT_GSIGN(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IAttendanceBusiness.DeleteAT_GSIGN
            Using rep As New AttendanceRepository
                Try

                    Return rep.DeleteAT_GSIGN(lstID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "Quy định đi muộn về sớm"
        Public Function GetAT_DMVS(ByVal _filter As AT_DMVSDTO,
                                    Optional ByVal PageIndex As Integer = 0,
                                        Optional ByVal PageSize As Integer = Integer.MaxValue,
                                        Optional ByRef Total As Integer = 0,
                                       Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_DMVSDTO) Implements ServiceContracts.IAttendanceBusiness.GetAT_DMVS
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetAT_DMVS(_filter, PageIndex, PageSize, Total, Sorts)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertAT_DMVS(ByVal objData As AT_DMVSDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.InsertAT_DMVS
            Using rep As New AttendanceRepository
                Try

                    Return rep.InsertAT_DMVS(objData, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ValidateAT_DMVS(ByVal objData As AT_DMVSDTO) As Boolean Implements ServiceContracts.IAttendanceBusiness.ValidateAT_DMVS
            Using rep As New AttendanceRepository
                Try

                    Return rep.ValidateAT_DMVS(objData)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyAT_DMVS(ByVal objData As AT_DMVSDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.ModifyAT_DMVS
            Using rep As New AttendanceRepository
                Try

                    Return rep.ModifyAT_DMVS(objData, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ActiveAT_DMVS(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean Implements ServiceContracts.IAttendanceBusiness.ActiveAT_DMVS
            Using rep As New AttendanceRepository
                Try

                    Return rep.ActiveAT_DMVS(lstID, log, bActive)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteAT_DMVS(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IAttendanceBusiness.DeleteAT_DMVS
            Using rep As New AttendanceRepository
                Try

                    Return rep.DeleteAT_DMVS(lstID)
                Catch ex As Exception
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
                                     Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_SHIFTDTO) Implements ServiceContracts.IAttendanceBusiness.GetAT_SHIFT
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetAT_SHIFT(_filter, PageIndex, PageSize, Total, Sorts)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertAT_SHIFT(ByVal objData As AT_SHIFTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.InsertAT_SHIFT
            Using rep As New AttendanceRepository
                Try

                    Return rep.InsertAT_SHIFT(objData, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ValidateAT_SHIFT(ByVal objData As AT_SHIFTDTO) As Boolean Implements ServiceContracts.IAttendanceBusiness.ValidateAT_SHIFT
            Using rep As New AttendanceRepository
                Try

                    Return rep.ValidateAT_SHIFT(objData)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyAT_SHIFT(ByVal objData As AT_SHIFTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.ModifyAT_SHIFT
            Using rep As New AttendanceRepository
                Try

                    Return rep.ModifyAT_SHIFT(objData, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ActiveAT_SHIFT(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean Implements ServiceContracts.IAttendanceBusiness.ActiveAT_SHIFT
            Using rep As New AttendanceRepository
                Try

                    Return rep.ActiveAT_SHIFT(lstID, log, bActive)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteAT_SHIFT(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IAttendanceBusiness.DeleteAT_SHIFT
            Using rep As New AttendanceRepository
                Try

                    Return rep.DeleteAT_SHIFT(lstID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetAT_TIME_MANUALBINCOMBO() As DataTable Implements ServiceContracts.IAttendanceBusiness.GetAT_TIME_MANUALBINCOMBO
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetAT_TIME_MANUALBINCOMBO()
                Catch ex As Exception
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
                                    Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_HOLIDAY_OBJECTDTO) Implements ServiceContracts.IAttendanceBusiness.GetAT_Holiday_Object
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetAT_Holiday_Object(_filter, PageIndex, PageSize, Total, Sorts)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertAT_Holiday_Object(ByVal objData As AT_HOLIDAY_OBJECTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.InsertAT_Holiday_Object
            Using rep As New AttendanceRepository
                Try

                    Return rep.InsertAT_Holiday_Object(objData, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ValidateAT_Holiday_Object(ByVal objData As AT_HOLIDAY_OBJECTDTO) As Boolean Implements ServiceContracts.IAttendanceBusiness.ValidateAT_Holiday_Object
            Using rep As New AttendanceRepository
                Try

                    Return rep.ValidateAT_Holiday_Object(objData)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyAT_Holiday_Object(ByVal objData As AT_HOLIDAY_OBJECTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.ModifyAT_Holiday_Object
            Using rep As New AttendanceRepository
                Try

                    Return rep.ModifyAT_Holiday_Object(objData, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ActiveAT_Holiday_Object(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean Implements ServiceContracts.IAttendanceBusiness.ActiveAT_Holiday_Object
            Using rep As New AttendanceRepository
                Try

                    Return rep.ActiveAT_Holiday_Object(lstID, log, bActive)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteAT_Holiday_Object(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IAttendanceBusiness.DeleteAT_Holiday_Object
            Using rep As New AttendanceRepository
                Try

                    Return rep.DeleteAT_Holiday_Object(lstID)
                Catch ex As Exception
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
                                    Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_SETUP_SPECIALDTO) Implements ServiceContracts.IAttendanceBusiness.GetAT_SETUP_SPECIAL
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetAT_SETUP_SPECIAL(_filter, PageIndex, PageSize, Total, Sorts)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertAT_SETUP_SPECIAL(ByVal objData As AT_SETUP_SPECIALDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.InsertAT_SETUP_SPECIAL
            Using rep As New AttendanceRepository
                Try

                    Return rep.InsertAT_SETUP_SPECIAL(objData, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ValidateAT_SETUP_SPECIAL(ByVal objData As AT_SETUP_SPECIALDTO) As Boolean Implements ServiceContracts.IAttendanceBusiness.ValidateAT_SETUP_SPECIAL
            Using rep As New AttendanceRepository
                Try

                    Return rep.ValidateAT_SETUP_SPECIAL(objData)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyAT_SETUP_SPECIAL(ByVal objData As AT_SETUP_SPECIALDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.ModifyAT_SETUP_SPECIAL
            Using rep As New AttendanceRepository
                Try

                    Return rep.ModifyAT_SETUP_SPECIAL(objData, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ActiveAT_SETUP_SPECIAL(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean Implements ServiceContracts.IAttendanceBusiness.ActiveAT_SETUP_SPECIAL
            Using rep As New AttendanceRepository
                Try

                    Return rep.ActiveAT_SETUP_SPECIAL(lstID, log, bActive)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteAT_SETUP_SPECIAL(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IAttendanceBusiness.DeleteAT_SETUP_SPECIAL
            Using rep As New AttendanceRepository
                Try

                    Return rep.DeleteAT_SETUP_SPECIAL(lstID)
                Catch ex As Exception
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
                                    Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_SETUP_TIME_EMPDTO) Implements ServiceContracts.IAttendanceBusiness.GetAT_SETUP_TIME_EMP
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetAT_SETUP_TIME_EMP(_filter, PageIndex, PageSize, Total, Sorts)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertAT_SETUP_TIME_EMP(ByVal objData As AT_SETUP_TIME_EMPDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.InsertAT_SETUP_TIME_EMP
            Using rep As New AttendanceRepository
                Try
                    Return rep.InsertAT_SETUP_TIME_EMP(objData, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ValidateAT_SETUP_SPECIAL(ByVal objData As AT_SETUP_TIME_EMPDTO) As Boolean Implements ServiceContracts.IAttendanceBusiness.ValidateAT_SETUP_TIME_EMP
            Using rep As New AttendanceRepository
                Try
                    Return rep.ValidateAT_SETUP_TIME_EMP(objData)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyAT_SETUP_TIME_EMP(ByVal objData As AT_SETUP_TIME_EMPDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.ModifyAT_SETUP_TIME_EMP
            Using rep As New AttendanceRepository
                Try
                    Return rep.ModifyAT_SETUP_TIME_EMP(objData, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ActiveAT_SETUP_TIME_EMP(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean Implements ServiceContracts.IAttendanceBusiness.ActiveAT_SETUP_TIME_EMP
            Using rep As New AttendanceRepository
                Try

                    Return rep.ActiveAT_SETUP_TIME_EMP(lstID, log, bActive)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteAT_SETUP_TIME_EMP(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IAttendanceBusiness.DeleteAT_SETUP_TIME_EMP
            Using rep As New AttendanceRepository
                Try
                    Return rep.DeleteAT_SETUP_TIME_EMP(lstID)
                Catch ex As Exception
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
                                    Optional ByVal Sorts As String = "CREATED_DATE desc",
                                   Optional ByVal log As UserLog = Nothing) As List(Of AT_TERMINALSDTO) Implements ServiceContracts.IAttendanceBusiness.GetAT_TERMINAL
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetAT_TERMINAL(_filter, PageIndex, PageSize, Total, Sorts, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetAT_TERMINAL_STATUS(ByVal _filter As AT_TERMINALSDTO,
                                  Optional ByVal PageIndex As Integer = 0,
                                        Optional ByVal PageSize As Integer = Integer.MaxValue,
                                        Optional ByRef Total As Integer = 0,
                                    Optional ByVal Sorts As String = "CREATED_DATE desc",
                                   Optional ByVal log As UserLog = Nothing) As List(Of AT_TERMINALSDTO) Implements ServiceContracts.IAttendanceBusiness.GetAT_TERMINAL_STATUS
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetAT_TERMINAL_STATUS(_filter, PageIndex, PageSize, Total, Sorts, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function InsertAT_TERMINAL(ByVal objData As AT_TERMINALSDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.InsertAT_TERMINAL
            Using rep As New AttendanceRepository
                Try

                    Return rep.InsertAT_TERMINAL(objData, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ValidateAT_TERMINAL(ByVal objData As AT_TERMINALSDTO) As Boolean Implements ServiceContracts.IAttendanceBusiness.ValidateAT_TERMINAL
            Using rep As New AttendanceRepository
                Try

                    Return rep.ValidateAT_TERMINAL(objData)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyAT_TERMINAL(ByVal objData As AT_TERMINALSDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.ModifyAT_TERMINAL
            Using rep As New AttendanceRepository
                Try

                    Return rep.ModifyAT_TERMINAL(objData, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ActiveAT_TERMINAL(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean Implements ServiceContracts.IAttendanceBusiness.ActiveAT_TERMINAL
            Using rep As New AttendanceRepository
                Try

                    Return rep.ActiveAT_TERMINAL(lstID, log, bActive)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteAT_TERMINAL(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IAttendanceBusiness.DeleteAT_TERMINAL
            Using rep As New AttendanceRepository
                Try

                    Return rep.DeleteAT_TERMINAL(lstID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "Đăng ký chấm công mặc định"
        Public Function GetAT_SIGNDEFAULT(ByVal _filter As AT_SIGNDEFAULTDTO,
                                   Optional ByVal PageIndex As Integer = 0,
                                        Optional ByVal PageSize As Integer = Integer.MaxValue,
                                        Optional ByRef Total As Integer = 0,
                                   Optional ByVal Sorts As String = "CREATED_DATE desc",
                                   Optional ByVal log As UserLog = Nothing) As List(Of AT_SIGNDEFAULTDTO) Implements ServiceContracts.IAttendanceBusiness.GetAT_SIGNDEFAULT
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetAT_SIGNDEFAULT(_filter, PageIndex, PageSize, Total, Sorts, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetAT_ListShift() As DataTable Implements ServiceContracts.IAttendanceBusiness.GetAT_ListShift
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetAT_ListShift()
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetAT_PERIOD() As DataTable Implements ServiceContracts.IAttendanceBusiness.GetAT_PERIOD
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetAT_PERIOD()
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetEmployeeID(ByVal employee_code As String, ByVal period_id As Decimal) As DataTable Implements ServiceContracts.IAttendanceBusiness.GetEmployeeID
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetEmployeeID(employee_code, period_id)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetEmployeeIDInSign(ByVal employee_code As String) As DataTable Implements ServiceContracts.IAttendanceBusiness.GetEmployeeIDInSign
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetEmployeeIDInSign(employee_code)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetEmployeeByTimeID(ByVal time_id As Decimal) As DataTable Implements ServiceContracts.IAttendanceBusiness.GetEmployeeByTimeID
            Using rep As New AttendanceRepository
                Try
                    Return rep.GetEmployeeByTimeID(time_id)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertAT_SIGNDEFAULT(ByVal objSIGN As AT_SIGNDEFAULTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.InsertAT_SIGNDEFAULT
            Using rep As New AttendanceRepository
                Try

                    Return rep.InsertAT_SIGNDEFAULT(objSIGN, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyAT_SINGDEFAULT(ByVal objSIGN As AT_SIGNDEFAULTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.ModifyAT_SIGNDEFAULT
            Using rep As New AttendanceRepository
                Try

                    Return rep.ModifyAT_SIGNDEFAULT(objSIGN, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ValidateAT_SINGDEFAULT(ByVal objSIGN As AT_SIGNDEFAULTDTO) As Boolean Implements ServiceContracts.IAttendanceBusiness.ValidateAT_SIGNDEFAULT
            Using rep As New AttendanceRepository
                Try

                    Return rep.ValidateAT_SIGNDEFAULT(objSIGN)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ActiveAT_SIGNDEFAULT(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean Implements ServiceContracts.IAttendanceBusiness.ActiveAT_SIGNDEFAULT
            Using rep As New AttendanceRepository
                Try

                    Return rep.ActiveAT_SIGNDEFAULT(lstID, log, bActive)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteAT_SIGNDEFAULT(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IAttendanceBusiness.DeleteAT_SIGNDEFAULT
            Using rep As New AttendanceRepository
                Try

                    Return rep.DeleteAT_SIGNDEFAULT(lstID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "đăng ký nghỉ trên iportal"
        Function GetPlanningAppointmentByEmployee(ByVal empid As Decimal,
                                                  ByVal startdate As DateTime,
                                                  ByVal enddate As DateTime, _
                                                  ByVal listSign As List(Of AT_TIME_MANUALDTO)) As List(Of AT_TIMESHEET_REGISTERDTO) _
                                              Implements IAttendanceBusiness.GetPlanningAppointmentByEmployee
            Using rep As New AttendanceRepository
                Try
                    Return rep.GetPlanningAppointmentByEmployee(empid, startdate, enddate, listSign)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function InsertPortalRegister(ByVal itemRegister As AT_PORTAL_REG_DTO,
                                             ByVal log As UserLog) As Boolean _
                                         Implements IAttendanceBusiness.InsertPortalRegister
            Using rep As New AttendanceRepository
                Try
                    Return rep.InsertPortalRegister(itemRegister, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetHolidayByCalender(ByVal startdate As Date,
                                             ByVal enddate As Date) As List(Of Date) _
                                         Implements IAttendanceBusiness.GetHolidayByCalender
            Using rep As New AttendanceRepository
                Try
                    Return rep.GetHolidayByCalender(startdate, enddate)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetRegisterAppointmentInPortalByEmployee(ByVal empid As Decimal,
                                                                 ByVal startdate As Date,
                                                                 ByVal enddate As Date,
                                                                ByVal listSign As List(Of AT_TIME_MANUALDTO),
                                                                ByVal status As List(Of Short)) As List(Of AT_TIMESHEET_REGISTERDTO) _
                                                            Implements IAttendanceBusiness.GetRegisterAppointmentInPortalByEmployee
            Using rep As New AttendanceRepository
                Try
                    Return rep.GetRegisterAppointmentInPortalByEmployee(empid, startdate, enddate, listSign, status)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetTotalLeaveInYear(ByVal empid As Decimal, ByVal p_year As Decimal) As Decimal _
                                                          Implements IAttendanceBusiness.GetTotalLeaveInYear
            Using rep As New AttendanceRepository
                Try
                    Return rep.GetTotalLeaveInYear(empid, p_year)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function DeletePortalRegisterByDate(ByVal listappointment As List(Of AT_TIMESHEET_REGISTERDTO),
                                                   ByVal listSign As List(Of AT_TIME_MANUALDTO)) As Boolean _
                                               Implements IAttendanceBusiness.DeletePortalRegisterByDate
            Using rep As New AttendanceRepository
                Try
                    Return rep.DeletePortalRegisterByDate(listappointment, listSign)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function DeletePortalRegister(ByVal id As Decimal) As Boolean _
            Implements IAttendanceBusiness.DeletePortalRegister
            Using rep As New AttendanceRepository
                Try
                    Return rep.DeletePortalRegister(id)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function SendRegisterToApprove(ByVal objLstRegisterId As List(Of Decimal),
                                              ByVal process As String, ByVal currentUrl As String) As String _
                                          Implements IAttendanceBusiness.SendRegisterToApprove
            Using rep As New AttendanceRepository
                Try
                    Return rep.SendRegisterToApprove(objLstRegisterId, process, currentUrl)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "Phê duyệt đăng ký nghỉ trên iportal"
        Public Function GetListSignCode(ByVal gSignCode As String) As List(Of AT_TIME_MANUALDTO) _
            Implements IAttendanceBusiness.GetListSignCode
            Using rep As New AttendanceRepository
                Try
                    Return rep.GetListSignCode(gSignCode)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Function GetListWaitingForApprove(ByVal approveId As Decimal,
                                          ByVal process As String,
                                          ByVal filter As ATRegSearchDTO) As List(Of AT_PORTAL_REG_DTO) _
                                      Implements IAttendanceBusiness.GetListWaitingForApprove
            Using rep As New AttendanceRepository
                Try
                    Return rep.GetListWaitingForApprove(approveId, process, filter)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Function ApprovePortalRegister(ByVal regID As Guid,
                                       ByVal approveId As Decimal,
                                       ByVal status As Integer,
                                       ByVal note As String,
                                       ByVal currentUrl As String,
                                       ByVal process As String,
                                       ByVal log As UserLog) As Boolean _
                                   Implements IAttendanceBusiness.ApprovePortalRegister
            Using rep As New AttendanceRepository
                Try
                    Return rep.ApprovePortalRegister(regID, approveId, status, note, currentUrl, process, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Function GetEmployeeList() As DataTable _
            Implements IAttendanceBusiness.GetEmployeeList
            Using rep As New AttendanceRepository
                Try
                    Return rep.GetEmployeeList()
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Function GetLeaveDay(ByVal dDate As Date) As DataTable _
            Implements IAttendanceBusiness.GetLeaveDay
            Using rep As New AttendanceRepository
                Try
                    Return rep.GetLeaveDay(dDate)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "Thiết lập kiểu công"

        Public Function GetAT_TIME_MANUAL(ByVal _filter As AT_TIME_MANUALDTO,
                                     Optional ByVal PageIndex As Integer = 0,
                                        Optional ByVal PageSize As Integer = Integer.MaxValue,
                                        Optional ByRef Total As Integer = 0,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_TIME_MANUALDTO) Implements ServiceContracts.IAttendanceBusiness.GetAT_TIME_MANUAL
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetAT_TIME_MANUAL(_filter, PageIndex, PageSize, Total, Sorts)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetAT_TIME_MANUALById(ByVal _id As Decimal?) As AT_TIME_MANUALDTO Implements ServiceContracts.IAttendanceBusiness.GetAT_TIME_MANUALById
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetAT_TIME_MANUALById(_id)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertAT_TIME_MANUAL(ByVal objHOLIDAY As AT_TIME_MANUALDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.InsertAT_TIME_MANUAL
            Using rep As New AttendanceRepository
                Try

                    Return rep.InsertAT_TIME_MANUAL(objHOLIDAY, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ValidateAT_TIME_MANUAL(ByVal objHOLIDAY As AT_TIME_MANUALDTO) As Boolean Implements ServiceContracts.IAttendanceBusiness.ValidateAT_TIME_MANUAL
            Using rep As New AttendanceRepository
                Try

                    Return rep.ValidateAT_TIME_MANUAL(objHOLIDAY)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyAT_TIME_MANUAL(ByVal objHOLIDAY As AT_TIME_MANUALDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.ModifyAT_TIME_MANUAL
            Using rep As New AttendanceRepository
                Try

                    Return rep.ModifyAT_TIME_MANUAL(objHOLIDAY, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ActiveAT_TIME_MANUAL(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean Implements ServiceContracts.IAttendanceBusiness.ActiveAT_TIME_MANUAL
            Using rep As New AttendanceRepository
                Try

                    Return rep.ActiveAT_TIME_MANUAL(lstID, log, bActive)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteAT_TIME_MANUAL(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IAttendanceBusiness.DeleteAT_TIME_MANUAL
            Using rep As New AttendanceRepository
                Try

                    Return rep.DeleteAT_TIME_MANUAL(lstID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetDataImportCO() As DataTable Implements ServiceContracts.IAttendanceBusiness.GetDataImportCO
            Using rep As New AttendanceRepository
                Try
                    Return rep.GetDataImportCO()
                Catch ex As Exception
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
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_LISTPARAM_SYSTEAMDTO) Implements ServiceContracts.IAttendanceBusiness.GetListParamItime
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetListParamItime(_filter, PageIndex, PageSize, Total, Sorts)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertListParamItime(ByVal objData As AT_LISTPARAM_SYSTEAMDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.InsertListParamItime
            Using rep As New AttendanceRepository
                Try

                    Return rep.InsertListParamItime(objData, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ValidateListParamItime(ByVal objData As AT_LISTPARAM_SYSTEAMDTO) As Boolean Implements ServiceContracts.IAttendanceBusiness.ValidateListParamItime
            Using rep As New AttendanceRepository
                Try

                    Return rep.ValidateListParamItime(objData)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyListParamItime(ByVal objData As AT_LISTPARAM_SYSTEAMDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.ModifyListParamItime
            Using rep As New AttendanceRepository
                Try

                    Return rep.ModifyListParamItime(objData, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ActiveListParamItime(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean Implements ServiceContracts.IAttendanceBusiness.ActiveListParamItime
            Using rep As New AttendanceRepository
                Try

                    Return rep.ActiveListParamItime(lstID, log, bActive)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteListParamItime(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IAttendanceBusiness.DeleteListParamItime
            Using rep As New AttendanceRepository
                Try

                    Return rep.DeleteListParamItime(lstID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region ""
        Function AutoGenCode(ByVal firstChar As String, ByVal tableName As String, ByVal colName As String) As String _
           Implements ServiceContracts.IAttendanceBusiness.AutoGenCode
            Using rep As New AttendanceRepository
                Try

                    Dim lst = rep.AutoGenCode(firstChar, tableName, colName)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Function CheckExistInDatabase(ByVal lstID As List(Of Decimal), ByVal table As AttendanceCommon.TABLE_NAME) As Boolean _
            Implements ServiceContracts.IAttendanceBusiness.CheckExistInDatabase
            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.CheckExistInDatabase(lstID, table)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Function CheckExistInDatabaseAT_SIGNDEFAULT(ByVal lstID As List(Of Decimal), ByVal lstWorking As List(Of Date), ByVal lstShift As List(Of Decimal), ByVal table As AttendanceCommon.TABLE_NAME) As Boolean _
           Implements ServiceContracts.IAttendanceBusiness.CheckExistInDatabaseAT_SIGNDEFAULT
            Using rep As New AttendanceRepository
                Try

                    Dim lst = rep.CheckExistInDatabaseAT_SIGNDEFAULT(lstID, lstWorking, lstShift, table)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region
    End Class
End Namespace
