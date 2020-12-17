using OfficeOpenXml;
using OfficeOpenXml.Table;
using System.Collections.Generic;
using System.Data;

namespace Nasa.Data.Contracts.Spreadsheets
{
    /// <summary>
    /// Interface exposing methods and properties used to convert data to an excel spreadsheet.
    /// </summary>
    public interface IExcelConvertible
    {
        /// <summary>
        /// Name of the spreadsheet.
        /// </summary>
        public string SpreadsheetName { get; }

        /// <summary>
        /// Adds the data of this object as an excel spreadsheet with a table style and a predefined header style to use for headers.
        /// </summary>
        /// <param name="excelWorksheets">Excel Worksheet collection to add the spreadsheet to.</param>
        /// <param name="tableStyle">TableStyle enum to choose the spreadsheet style.</param>
        /// <param name="headerStyle">Name of the header to use for the headers (if any) on the spreadsheet. Keep in mind that it needs
        /// to either exist already in Excel or you have to create it yourself beforehand.</param>
        /// <returns>The spreadsheet object after it's added or null if no data was added to it.</returns>
        public ExcelWorksheet AddAsExcelSheet(ExcelWorksheets excelWorksheets, TableStyles tableStyle, string headerStyle);

        /// <summary>
        /// Converts the data of this object to a collection of data tables. 
        /// </summary>
        /// <returns>Collection of data tables.</returns>
        public IEnumerable<DataTable> ConvertToDataTables();
    }
}
