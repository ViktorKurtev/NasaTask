using OfficeOpenXml;
using OfficeOpenXml.Table;
using System.Collections.Generic;
using System.Data;

namespace Nasa.Data.Contracts.Spreadsheets
{
    public interface IExcelConvertible
    {
        public ExcelWorksheet AddAsExcelSheet(ExcelWorksheets excelWorksheets, TableStyles tableStyle, string headerStyle);
    }
}
