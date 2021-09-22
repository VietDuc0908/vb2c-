using System;
using Microsoft.VisualBasic;
using Aspose.Cells;

public class Util
{
    public static bool ExportTemplate(string sReportFileName, DataTable dtData, DataTable dtVariable, string filename)
    {
        string filePath;
        string templatefolder;

        WorkbookDesigner designer;
        try
        {
            templatefolder = ConfigurationManager.AppSettings("ReportTemplatesFolder");
            filePath = AppDomain.CurrentDomain.BaseDirectory + @"\" + templatefolder + @"\" + sReportFileName;

            if (File.Exists(filePath))
            {
                designer = new WorkbookDesigner();
                designer.Open(filePath);
                designer.SetDataSource(dtData);

                if (dtVariable != null)
                {
                    int intCols = dtVariable.Columns.Count;
                    for (int i = 0; i <= intCols - 1; i++)
                        designer.SetDataSource(dtVariable.Columns(i).ColumnName.ToString(), dtVariable.Rows(0).ItemArray(i).ToString());
                }
                designer.Process();
                designer.Workbook.CalculateFormula();
                designer.Workbook.Save(HttpContext.Current.Response, filename + ".xls", ContentDisposition.Attachment, new XlsSaveOptions());
            }
        }
        catch (Exception ex)
        {
            return false;
        }
        return true;
    }
    public static bool Export2template(string sReportFileName, string filename = "")
    {
        string filePath;
        string templatefolder;
        WorkbookDesigner designer;
        string fileAttachInfo;
        try
        {
            templatefolder = ConfigurationManager.AppSettings("ReportTemplates");
            filePath = AppDomain.CurrentDomain.BaseDirectory + @"\" + templatefolder + @"\" + sReportFileName;
            FileFormatType Format = new Aspose.Cells.FileFormatType();
            FileInfo fi = new FileInfo(filePath);
            if (fi.Extension == ".xlsx")
            {
                Format = FileFormatType.Xlsx;
                if (filename != "")
                    fileAttachInfo = filename + ".xlsx";
                else
                    fileAttachInfo = Guid.NewGuid().ToString() + ".xlsx";
            }
            else
            {
                Format = FileFormatType.Excel97To2003;
                if (filename != "")
                    fileAttachInfo = filename + ".xls";
                else
                    fileAttachInfo = Guid.NewGuid().ToString() + ".xls";
            }
            designer.Save(fileAttachInfo, SaveType.OpenInBrowser, Format, HttpContext.Current.Response);
        }
        catch (Exception ex)
        {
        }
    }
    public static bool Export2Excel(string sReportFileName, DataSet dsSource, DataTable dtVariable, string filename = "")
    {
        // Dim strResult As String = ""
        string filePath;
        string templatefolder;
        // Dim exportfolder As String
        WorkbookDesigner designer;
        string fileAttachInfo;
        try
        {
            // Dim strRoot As String = System.Windows.Forms.Application.StartupPath & "\lib\"

            // Dim l As New Aspose.Cells.License()
            // Dim strLicense As String = strRoot & "Aspose.Cells.lic"
            // l.SetLicense(strLicense)

            templatefolder = ConfigurationManager.AppSettings("ReportTemplatesFolder");
            filePath = AppDomain.CurrentDomain.BaseDirectory + @"\" + templatefolder + @"\" + sReportFileName;

            if (File.Exists(filePath))
            {
                designer = new WorkbookDesigner();
                designer.Open(filePath);

                designer.SetDataSource(dsSource);

                if (dtVariable != null)
                {
                    int intCols = dtVariable.Columns.Count;
                    for (int i = 0; i <= intCols - 1; i++)
                        designer.SetDataSource(dtVariable.Columns(i).ColumnName.ToString(), dtVariable.Rows(0).ItemArray(i).ToString());
                }

                designer.Process();
                // exportfolder = ConfigurationManager.AppSettings("ExcelFileFolder")
                // Dim fileAttachInfo As String = AppDomain.CurrentDomain.BaseDirectory + "\" + exportfolder + "\"
                // If Not System.IO.Directory.Exists(fileAttachInfo) Then
                // System.IO.Directory.CreateDirectory(fileAttachInfo)
                // End If

                // Save the excel file
                FileFormatType Format = new Aspose.Cells.FileFormatType();
                FileInfo fi = new FileInfo(filePath);
                if (fi.Extension == ".xlsx")
                {
                    Format = FileFormatType.Xlsx;
                    if (filename != "")
                        fileAttachInfo = filename + ".xlsx";
                    else
                        fileAttachInfo = Guid.NewGuid().ToString() + ".xlsx";
                }
                else
                {
                    Format = FileFormatType.Excel97To2003;
                    if (filename != "")
                        fileAttachInfo = filename + ".xls";
                    else
                        fileAttachInfo = Guid.NewGuid().ToString() + ".xls";
                }
                designer.Workbook.CalculateFormula();
                designer.Save(fileAttachInfo, SaveType.OpenInBrowser, Format, HttpContext.Current.Response);
            }
        }
        catch (Exception ex)
        {
            return false;
        }
        return true;
    }

    public static bool ExportExcelByDataTable(string sReportFileName, DataTable dtTable, DataTable dtVariable, string filename = "", int startRow = 0, int startColumn = 0, bool isInsert = false, int SheetIndex = 0)
    {
        // Dim strResult As String = """
        string filePath;
        string templatefolder;
        // Dim exportfolder As String
        WorkbookDesigner designer;
        string fileAttachInfo;
        try
        {
            // Dim strRoot As String = System.Windows.Forms.Application.StartupPath & "\lib\"

            // Dim l As New Aspose.Cells.License()
            // Dim strLicense As String = strRoot & "Aspose.Cells.lic"
            // l.SetLicense(strLicense)

            templatefolder = ConfigurationManager.AppSettings("ReportTemplatesFolder");
            filePath = AppDomain.CurrentDomain.BaseDirectory + @"\" + templatefolder + @"\" + sReportFileName;

            if (File.Exists(filePath))
            {
                designer = new WorkbookDesigner();
                designer.Open(filePath);
                designer.Workbook.Worksheets(SheetIndex).Cells.ImportDataTable(dtTable, false, startRow, startColumn, isInsert);

                designer.Process();
                // exportfolder = ConfigurationManager.AppSettings("ExcelFileFolder")
                // Dim fileAttachInfo As String = AppDomain.CurrentDomain.BaseDirectory + "\" + exportfolder + "\"
                // If Not System.IO.Directory.Exists(fileAttachInfo) Then
                // System.IO.Directory.CreateDirectory(fileAttachInfo)
                // End If

                // Save the excel file
                FileFormatType Format = new Aspose.Cells.FileFormatType();
                FileInfo fi = new FileInfo(filePath);
                if (fi.Extension == ".xlsx")
                {
                    Format = FileFormatType.Xlsx;
                    if (filename != "")
                        fileAttachInfo = filename + ".xlsx";
                    else
                        fileAttachInfo = Guid.NewGuid().ToString() + ".xlsx";
                }
                else
                {
                    Format = FileFormatType.Excel97To2003;
                    if (filename != "")
                        fileAttachInfo = filename + ".xls";
                    else
                        fileAttachInfo = Guid.NewGuid().ToString() + ".xls";
                }
                designer.Workbook.CalculateFormula();
                designer.Save(fileAttachInfo, SaveType.OpenInBrowser, Format, HttpContext.Current.Response);
            }
        }
        catch (Exception ex)
        {
            return false;
        }
        return true;
    }

    public static Workbook CreateExcelWorkbook(string sReportFile, DataSet dsSource, DataTable dtVariable)
    {
        // Dim strResult As String = ""
        string filePath;
        // Dim exportfolder As String
        WorkbookDesigner designer;
        try
        {
            filePath = sReportFile;

            if (File.Exists(filePath))
            {
                designer = new WorkbookDesigner();

                designer.Open(filePath);

                designer.SetDataSource(dsSource);

                if (dtVariable != null)
                {
                    int intCols = dtVariable.Columns.Count;
                    for (int i = 0; i <= intCols - 1; i++)
                        designer.SetDataSource(dtVariable.Columns(i).ColumnName.ToString(), dtVariable.Rows(0).ItemArray(i).ToString());
                }

                designer.Process();

                return designer.Workbook;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static bool Export2Pdf(string sReportFileName, DataSet dsSource, DataTable dtVariable)
    {
        // Dim strResult As String = ""
        string filePath;
        string templatefolder;
        // Dim exportfolder As String
        WorkbookDesigner designer;
        string fileAttachInfo;
        try
        {
            templatefolder = ConfigurationManager.AppSettings("ReportTemplatesFolder");
            filePath = AppDomain.CurrentDomain.BaseDirectory + @"\" + templatefolder + @"\" + sReportFileName;

            if (File.Exists(filePath))
            {
                designer = new WorkbookDesigner();
                designer.Open(filePath);

                designer.SetDataSource(dsSource);

                if (dtVariable != null)
                {
                    int intCols = dtVariable.Columns.Count;
                    for (int i = 0; i <= intCols - 1; i++)
                        designer.SetDataSource(dtVariable.Columns(i).ColumnName.ToString(), dtVariable.Rows(0).ItemArray(i).ToString());
                }

                designer.Process();

                // Save the excel file
                FileFormatType Format = new Aspose.Cells.FileFormatType();
                FileInfo fi = new FileInfo(filePath);
                Format = FileFormatType.Pdf;
                fileAttachInfo = Guid.NewGuid().ToString() + ".pdf";

                designer.Save(fileAttachInfo, SaveType.OpenInBrowser, Format, HttpContext.Current.Response);
            }
        }
        catch (Exception ex)
        {
            return false;
        }
        return true;
    }
}
