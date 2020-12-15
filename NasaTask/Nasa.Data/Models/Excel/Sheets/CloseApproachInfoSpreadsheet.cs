using Nasa.Data.Contracts.Spreadsheets;
using Nasa.Data.Extensions;
using Nasa.Data.JsonSerializers;
using Nasa.Data.Models.CloseApproach;
using Nasa.Data.Models.Excel.Sheets.Abstract;
using Nasa.Data.Models.Excel.Tables;
using Newtonsoft.Json;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Nasa.Data.Models.Excel.Sheets
{
    public class CloseApproachInfoSpreadsheet : BaseExcelSpreadsheet, IExcelConvertible
    {
        public override IEnumerable<object> SerializationData { get; set; }
        protected override string SpreadsheetName => "Close Approach Data";

        public override ExcelWorksheet AddAsExcelSheet(ExcelWorksheets excelWorksheets, TableStyles tableStyle, string headerStyle)
        {
            var spreadSheet = excelWorksheets.Add(SpreadsheetName);

            var currentRow = 1;

            var allDataTables = ConvertToDataTable();

            var subTableCollection = SerializationData.Select(a => (CloseApproachInfoSubTable)a).ToList();

            var counter = 0;

            foreach (var dataTable in allDataTables)
            {
                foreach (var column in dataTable.Columns)
                {
                    var columnName = column.ToString();
                    var friendlyName = column.ToString()
                                    .ToFriendlyString(" ", a => a == '_', b => b.CapitalizeFirstLetter());

                    dataTable.Columns[column.ToString()].ColumnName = friendlyName;
                }

                spreadSheet.Cells[currentRow, 1].Value = subTableCollection[counter].Name;
                spreadSheet.Cells[currentRow, 1].StyleName = headerStyle;

                spreadSheet.Cells[currentRow + 1, 1].LoadFromDataTable(dataTable, true, tableStyle);

                currentRow += dataTable.Rows.Count + 2;

                spreadSheet.Cells.AutoFitColumns();

                counter++;
            }

            return spreadSheet;
        }

        protected override IEnumerable<DataTable> ConvertToDataTable()
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
