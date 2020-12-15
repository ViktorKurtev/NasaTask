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
    public abstract class BaseExcelSpreadsheet
    {
        protected virtual string SpreadsheetName { get; } = "DefaultSpreadsheetName";
        public abstract IEnumerable<object> SerializationData { get; set; }
        protected virtual bool AppendParentNamesOnSerialize { get; }

        public virtual ExcelWorksheet AddAsExcelSheet(ExcelWorksheets excelWorksheets, TableStyles tableStyle, string headerStyle)
        {
            var spreadSheet = excelWorksheets.Add(SpreadsheetName);

            var dataTable = ConvertToDataTable().First();

            foreach (var column in dataTable.Columns)
            {
                var columnName = column.ToString();
                var friendlyName = column.ToString()
                                .ToFriendlyString(" ", a => a == '_', b => b.CapitalizeFirstLetter());

                dataTable.Columns[column.ToString()].ColumnName = friendlyName;
            }

            spreadSheet.Cells[1, 1].LoadFromDataTable(dataTable, true, tableStyle);

            spreadSheet.Cells.AutoFitColumns();

            return spreadSheet;
        }

        protected virtual IEnumerable<DataTable> ConvertToDataTable()
        {
            var jsonSerializer = new UnwrappedObjectSerializer(AppendParentNamesOnSerialize);

            var dataAsJson = JsonConvert.SerializeObject(SerializationData, jsonSerializer);

            var dataTable = JsonConvert.DeserializeObject<DataTable>(dataAsJson);

            return new[] { dataTable };
        }
    }
}
