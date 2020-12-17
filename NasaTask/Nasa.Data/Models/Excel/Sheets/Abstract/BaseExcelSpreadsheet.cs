using Nasa.Data.Contracts.Spreadsheets;
using Nasa.Data.Extensions;
using Nasa.Data.JsonSerializers;
using Newtonsoft.Json;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Nasa.Data.Models.Excel.Sheets.Abstract
{
    /// <summary>
    /// The base Excel Spreadsheet class that implements the IExcelConvertible interface. It is used as a starting
    /// point for the different types of spreadsheets that can be exported.
    /// </summary>
    public abstract class BaseExcelSpreadsheet : IExcelConvertible
    {
        public virtual string SpreadsheetName { get; } = "DefaultSpreadsheetName";

        /// <summary>
        /// Collection of Objects to be used as data for the excel spreadsheet. They need to be in a format
        /// that can be converted to a DataTable class. Essentially they need to be a collection of some type we want to convert to an
        /// excel spreadsheet.
        /// </summary>
        public abstract IEnumerable<object> SerializationData { get; set; }

        /// <summary>
        /// Because the object is first unwrapped before it is written to an excel sheet, there may be naming collisions. If this property
        /// is set to true, the serializer appends parent names recursively, avoiding naming collisions but making column names longer.
        /// </summary>
        protected virtual bool AppendParentNamesOnSerialize { get; }

        public virtual ExcelWorksheet AddAsExcelSheet(ExcelWorksheets excelWorksheets, TableStyles tableStyle, string headerStyle)
        {
            var spreadSheet = excelWorksheets.Add(SpreadsheetName);

            var dataTable = ConvertToDataTables().First();

            foreach (var column in dataTable.Columns)
            {
                var columnName = column.ToString();
                var friendlyName = column.ToString()
                                .ToFriendlyString(" ", a => a == '_', b => b.CapitalizeFirstLetter(), true);

                dataTable.Columns[column.ToString()].ColumnName = friendlyName;
            }

            spreadSheet.Cells[1, 1].LoadFromDataTable(dataTable, true, tableStyle);

            spreadSheet.Cells.AutoFitColumns();

            //No rows of close approach data were added and the spreadsheet is empty, no need to include it.
            if (!spreadSheet.Cells.Any())
            {
                excelWorksheets.Delete(spreadSheet);

                return null;
            }

            return spreadSheet;
        }

        public virtual IEnumerable<DataTable> ConvertToDataTables()
        {
            var jsonSerializer = new UnwrappedObjectSerializer(AppendParentNamesOnSerialize);

            var dataAsJson = JsonConvert.SerializeObject(SerializationData, jsonSerializer);

            var dataTable = JsonConvert.DeserializeObject<DataTable>(dataAsJson);

            return new[] { dataTable };
        }
    }
}
