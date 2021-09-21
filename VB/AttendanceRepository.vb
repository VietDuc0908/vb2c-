Imports Attendance.AttendanceBusiness
Imports Framework.UI

Public Class AttendanceRepository
    Inherits AttendanceRepositoryBase
    Private _isAvailable As Boolean
    Public Function GetComboboxData(ByRef cbxData As ComboBoxDataDTO) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.GetComboboxData(cbxData)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
End Class