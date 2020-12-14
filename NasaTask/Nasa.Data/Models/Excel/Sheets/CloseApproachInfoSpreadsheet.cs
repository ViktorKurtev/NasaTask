using Nasa.Data.Contracts.Spreadsheets;
using Nasa.Data.Extensions;
using Nasa.Data.JsonSerializers;
using Nasa.Data.Models.CloseApproach;
using Nasa.Data.Models.Excel.Tables;
using Newtonsoft.Json;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Nasa.Data.Models.Excel.Sheets
{
    public class CloseApproachInfoSpreadsheet : IExcelConvertible
    {
        public IEnumerable<CloseApproachInfoSubTable> CloseApproachInfoSubTables { get; set; }

        public ExcelWorksheet AddAsExcelSheet(ExcelWorksheets excelWorksheets, TableStyles tableStyle, string headerStyle)
        {
            var spreadSheet = excelWorksheets.Add("Close Approach Data");

            var currentRow = 1;

            foreach (var subTable in CloseApproachInfoSubTables)
            {
                var dataTable = ConvertToDataTable(subTable.CloseApproachData);

                foreach (var column in dataTable.Columns)
                {
                    var columnName = column.ToString();
                    var friendlyName = column.ToString()
                                    .ToFriendlyString(" ", a => a == '_', b => b.CapitalizeFirstLetter());

                    dataTable.Columns[column.ToString()].ColumnName = friendlyName;
                }

                spreadSheet.Cells[currentRow, 1].Value = subTable.Name;
                spreadSheet.Cells[currentRow, 1].StyleName = headerStyle;

                spreadSheet.Cells[currentRow + 1, 1].LoadFromDataTable(dataTable, true, tableStyle);

                currentRow += dataTable.Rows.Count + 2;

                spreadSheet.Cells.AutoFitColumns();
            }

            return spreadSheet;
        }

        private DataTable ConvertToDataTable(IEnumerable<CloseApproachData> objectToConvert)
        {
            var jsonSerializer = new UnwrappedObjectSerializer(true, typeof(CloseApproachData));

            var dataAsJson = JsonConvert.SerializeObject(objectToConvert, jsonSerializer);

            var dataTable = JsonConvert.DeserializeObject<DataTable>(dataAsJson);

            return dataTable;
        }
    }
}
