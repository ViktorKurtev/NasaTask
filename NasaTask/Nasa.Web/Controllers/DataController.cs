using Microsoft.AspNetCore.Mvc;
using Nasa.Services.Contracts;
using Nasa.Web.Models;
using System.Threading.Tasks;

namespace Nasa.Web.Controllers
{
    public class DataController : Controller
    {
        /// <summary>
        /// Xlsx content type shorthand.
        /// </summary>
        private const string ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

        private const string FileName = "AsteroidDataPage{0}.xlsx";
        private const int PageSize = 20;

        private readonly INasaService nasaService;
        private readonly IExcelConverterService excelConverter;

        public DataController(INasaService nasaService, IExcelConverterService excelConverter)
        {
            this.nasaService = nasaService;
            this.excelConverter = excelConverter;
        }

        /// <summary>
        /// Downloads a specific asteroid page from the Nasa Api as an xlsx file.
        /// </summary>
        /// <param name="pageNum">Page to download.</param>
        /// <returns></returns>
        public async Task<IActionResult> DownloadFile(int pageNum)
        {
            //We subtract one since this page variable comes from the view which has the page count incremented
            //by one so it displays properly.
            var asteroids = await nasaService.GetAsteroidDataCollectionAsync(pageNum - 1, PageSize);

            if (asteroids == null)
            {
                return View("Error", new ErrorViewModel
                {
                    ErrorMessage = $"Page number '{pageNum}'s doesn't exist."
                });
            }

            var sheets = excelConverter.CreateSpreadsheets(asteroids.Asteroids);

            var excelPackage = excelConverter.CreateExcelPackage(sheets);

            var file = await excelPackage.GetAsByteArrayAsync();

            return File(file, ContentType, string.Format(FileName, pageNum));
        }
    }
}
