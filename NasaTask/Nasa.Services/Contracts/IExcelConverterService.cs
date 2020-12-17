using Nasa.Data.Contracts.Spreadsheets;
using Nasa.Data.Models.Asteroid;
using OfficeOpenXml;
using System.Collections.Generic;

namespace Nasa.Services.Contracts
{
    /// <summary>
    /// Excel converter service, responsible for creating the spreadsheets as well as the final excel package.
    /// </summary>
    public interface IExcelConverterService
    {
        /// <summary>
        /// Converts a collection of Asteroid Data to a collection of spreadsheets.
        /// </summary>
        /// <param name="asteroidData">Data to Convert to spreadsheets.</param>
        /// <returns>The collection of IExcelConvertible spreadsheets, ready to be added to an excel package.</returns>
        public IEnumerable<IExcelConvertible> CreateSpreadsheets(IEnumerable<AsteroidData> asteroidData);

        /// <summary>
        /// Creates an ExcelPackage from an existing collection of spreadsheets.
        /// </summary>
        /// <param name="spreadsheets">Collection of IExcelConvertible spreadsheets.</param>
        /// <returns>The created ExcelPackage.</returns>
        public ExcelPackage CreateExcelPackage(IEnumerable<IExcelConvertible> spreadsheets);
    }
}
