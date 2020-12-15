using Microsoft.AspNetCore.Mvc;
using Nasa.Data.Models.PictureOfTheDay;
using Nasa.Services.Contracts;
using Nasa.Web.Models;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Nasa.Web.Controllers
{
    public class HomeController : Controller
    {
        private const string ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        private const string FileName = "AsteroidData.xlsx";

        private readonly INasaService nasaService;
        private readonly IExcelConverter excelConverter;

        public HomeController(INasaService nasaService, IExcelConverter excelConverter)
        {
            this.nasaService = nasaService;
            this.excelConverter = excelConverter;
        }

        public async Task<IActionResult> DownloadFile()
        {
            var asteroids = await nasaService.GetAsteroidDataAsync(511, 20);

            var sheets = excelConverter.CreateSpreadsheets(asteroids);

            var excelPackage = excelConverter.CreateExcelPackage(sheets);

            var file = await excelPackage.GetAsByteArrayAsync();

            return File(file, ContentType, FileName);
        }

        [HttpGet]
        public async Task<IActionResult> GetApod(DateTime date)
        {
            date = date == DateTime.MinValue ? DateTime.Now : date;

            var apod = await nasaService.GetAstronomyPictureOfTheDay(date);

            if (apod == null)
            {
                return View("Error", new ErrorViewModel
                {
                    ErrorMessage = "No data found for this date."
                });
            }

            return View(apod);
        }

        [HttpPost]
        public IActionResult GetApod(AstronomyPictureOfTheDay model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            return RedirectToAction("GetApod", new { date = model.Date });
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
