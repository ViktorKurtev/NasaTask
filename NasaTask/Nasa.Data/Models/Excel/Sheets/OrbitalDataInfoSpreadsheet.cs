using Nasa.Data.Contracts.Spreadsheets;
using Nasa.Data.Extensions;
using Nasa.Data.JsonSerializers;
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
    public class OrbitalDataInfoSpreadsheet : IExcelConvertible
    {
        public IEnumerable<OrbitalDataInfoRow> OrbitalDataInfoRows { get; set; }

        public ExcelWorksheet AddAsExcelSheet(ExcelWorksheets excelWorksheets, TableStyles tableStyle, string headerStyle)
        {
            var spreadSheet = excelWorksheets.Add("Orbital Data");

            var dataTable = ConvertToDataTable();

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

        private DataTable ConvertToDataTable()
        {
            var jsonSerializer = new UnwrappedObjectSerializer(false, typeof(OrbitalDataInfoRow));

            var dataAsJson = JsonConvert.SerializeObject(OrbitalDataInfoRows, jsonSerializer);

            var dataTable = JsonConvert.DeserializeObject<DataTable>(dataAsJson);

            return dataTable;
        }
    }
}
