Imports AttendanceBusiness.ServiceContracts
Imports AttendanceDAL
Imports Framework.Data
Imports System.Collections.Generic
Imports LinqKit

' NOTE: You can use the "Rename" command on the context menu to change the class name "Service1" in both code and config file together.
Namespace AttendanceBusiness.ServiceImplementations
    Partial Public Class AttendanceBusiness
        Implements IAttendanceBusiness
#Region "Get data combobox"
        Public Function GetComboboxData(ByRef cbxData As ComboBoxDataDTO) As Boolean _
            Implements IAttendanceBusiness.GetComboboxData
            Using rep As New AttendanceRepository
                Try
                    Return rep.GetComboboxData(cbxData)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region
    End Class
End Namespace
