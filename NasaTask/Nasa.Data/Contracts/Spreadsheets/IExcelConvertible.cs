using OfficeOpenXml;
using OfficeOpenXml.Table;
using System.Collections.Generic;
using System.Data;

namespace Nasa.Data.Contracts.Spreadsheets
{
    public interface IExcelConvertible
    {
        public string SpreadsheetName { get; }
        public ExcelWorksheet AddAsExcelSheet(ExcelWorksheets excelWorksheets, TableStyles tableStyle, string headerStyle);
        public IEnumerable<DataTable> ConvertToDataTables();
    }
}
