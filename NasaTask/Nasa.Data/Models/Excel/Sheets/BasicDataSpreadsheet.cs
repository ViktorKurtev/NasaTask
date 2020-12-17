using Nasa.Data.Models.Excel.Sheets.Abstract;
using System.Collections.Generic;

namespace Nasa.Data.Models.Excel.Sheets
{
    /// <summary>
    /// The spreadsheet containing the basic asteroid data for a given asteroid collection.
    /// </summary>
    public class BasicDataSpreadsheet : BaseExcelSpreadsheet
    {
        public override IEnumerable<object> SerializationData { get; set; }
        public override string SpreadsheetName => "Basic Data";
        protected override bool AppendParentNamesOnSerialize => true;
    }
}
