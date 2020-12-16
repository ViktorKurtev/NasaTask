using Nasa.Data.Contracts.Spreadsheets;
using Nasa.Data.Extensions;
using Nasa.Data.JsonSerializers;
using Nasa.Data.Models.Excel.Sheets.Abstract;
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
    public class BasicInfoSpreadsheet : BaseExcelSpreadsheet
    {
        public override IEnumerable<object> SerializationData { get; set; }
        public override string SpreadsheetName => "Basic Data";
        protected override bool AppendParentNamesOnSerialize => true;
    }
}
