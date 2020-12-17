using AutoMapper;
using Nasa.Data.Contracts.Spreadsheets;
using Nasa.Data.Extensions;
using Nasa.Data.Models.Asteroid;
using Nasa.Data.Models.Excel.Sheets;
using Nasa.Data.Models.Excel.Tables;
using Nasa.Services.Contracts;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using System.Collections.Generic;
using System.Linq;

namespace Nasa.Services.Services
{
    /// <summary>
    /// Excel converter service, responsible for creating the spreadsheets as well as the final excel package.
    /// </summary>
    public class ExcelConverterService : IExcelConverterService
    {
        private const string headerStyle = "HeaderStyle";
        private readonly IMapper mapper;

        public ExcelConverterService(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public ExcelPackage CreateExcelPackage(IEnumerable<IExcelConvertible> spreadsheets)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            ExcelPackage excelPackage = new ExcelPackage();

            excelPackage.Workbook.AddNameHeaderStyle(headerStyle);

            foreach (var sheet in spreadsheets)
            {
                sheet.AddAsExcelSheet(excelPackage.Workbook.Worksheets, TableStyles.Medium2, headerStyle);
            }

            return excelPackage;
        }

        public IEnumerable<IExcelConvertible> CreateSpreadsheets(IEnumerable<AsteroidData> asteroidData)
        {
            var basicDataSpreadsheet = mapper.Map<BasicDataSpreadsheet>(asteroidData.Select(a => mapper.Map<BasicInfoRow>(a)));
            var orbitalDataSpreadsheet = mapper.Map<OrbitalDataSpreadsheet>(asteroidData.Select(a => mapper.Map<OrbitalDataInfoRow>(a)));
            var closeApproachSpreadsheet = mapper.Map<CloseApproachSpreadsheet>(asteroidData.Select(a => mapper.Map<CloseApproachInfoSubTable>(a)));

            return new IExcelConvertible[] { basicDataSpreadsheet, orbitalDataSpreadsheet, closeApproachSpreadsheet };
        }
    }
}
