using Nasa.Data.Models.Excel.Sheets.Abstract;
using System.Collections.Generic;

namespace Nasa.Data.Models.Excel.Sheets
{
    /// <summary>
    /// The spreadsheet containing the orbital data for a given asteroid collection.
    /// </summary>
    public class OrbitalDataSpreadsheet : BaseExcelSpreadsheet
    {
        public override IEnumerable<object> SerializationData { get; set; }
        public override string SpreadsheetName => "Orbital Data";
        protected override bool AppendParentNamesOnSerialize => false;
    }
}
