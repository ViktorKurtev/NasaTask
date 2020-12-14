using Nasa.Data.Contracts.Spreadsheets;
using Nasa.Data.Models.Asteroid;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nasa.Services.Contracts
{
    public interface IExcelConverter
    {
        public IEnumerable<IExcelConvertible> CreateSpreadsheets(IEnumerable<AsteroidData> asteroidData);
        public ExcelPackage CreateExcelPackage(IEnumerable<IExcelConvertible> spreadsheets);
    }
}
