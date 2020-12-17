using Nasa.Data.Extensions;
using Nasa.Data.JsonSerializers;
using Nasa.Data.Models.Excel.Sheets.Abstract;
using Nasa.Data.Models.Excel.Tables;
using Newtonsoft.Json;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Nasa.Data.Models.Excel.Sheets
{
    /// <summary>
    /// The spreadsheet containing the close approach data for a given asteroid collection.
    /// </summary>
    public class CloseApproachSpreadsheet : BaseExcelSpreadsheet
    {
        public override IEnumerable<object> SerializationData { get; set; }
        public override string SpreadsheetName => "Close Approach Data";

        public override ExcelWorksheet AddAsExcelSheet(ExcelWorksheets excelWorksheets, TableStyles tableStyle, string headerStyle)
        {
            var spreadSheet = excelWorksheets.Add(SpreadsheetName);

            var currentRow = 1;

            var allDataTables = ConvertToDataTables();

            var subTableCollection = SerializationData.Select(a => (CloseApproachInfoSubTable)a).ToList();

            var counter = 0;

            foreach (var dataTable in allDataTables)
            {
                if (dataTable.Rows.Count == 0)
                {
                    continue;
                }

                foreach (var column in dataTable.Columns)
                {
                    var columnName = column.ToString();
                    var friendlyName = column.ToString()
                                    .ToFriendlyString(" ", a => a == '_', b => b.CapitalizeFirstLetter(), true);

                    dataTable.Columns[column.ToString()].ColumnName = friendlyName;
                }

                spreadSheet.Cells[currentRow, 1].Value = subTableCollection[counter].Name;
                spreadSheet.Cells[currentRow, 1].StyleName = headerStyle;

                spreadSheet.Cells[currentRow + 1, 1].LoadFromDataTable(dataTable, true, tableStyle);

                currentRow += dataTable.Rows.Count + 2;

                spreadSheet.Cells.AutoFitColumns();

                counter++;
            }

            //No rows of close approach data were added and the spreadsheet is empty, no need to include it.
            if (!spreadSheet.Cells.Any())
            {
                excelWorksheets.Delete(spreadSheet);

                return null;
            }

            return spreadSheet;
        }

        public override IEnumerable<DataTable> ConvertToDataTables()
        {
            var objectToConvert = SerializationData.Select(a => (CloseApproachInfoSubTable)a);

            var jsonSerializer = new UnwrappedObjectSerializer(true);

            var dataTableList = new List<DataTable>();

            foreach (var subTable in objectToConvert)
            {
                var dataAsJson = JsonConvert.SerializeObject(subTable.CloseApproachData, jsonSerializer);

                var dataTable = JsonConvert.DeserializeObject<DataTable>(dataAsJson);

                dataTableList.Add(dataTable);
            }

            return dataTableList;
        }
    }
}
