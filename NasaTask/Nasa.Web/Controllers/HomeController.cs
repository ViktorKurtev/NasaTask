using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Nasa.Data.JsonSerializers;
using Nasa.Data.Models.PictureOfTheDay;
using Nasa.Data.Models.ViewModels;
using Nasa.Services.Contracts;
using Nasa.Web.Models;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Nasa.Web.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// Xlsx content type shorthand.
        /// </summary>
        private const string ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

        private const string FileName = "AsteroidDataPage{0}.xlsx";

        private const int PageSize = 20;

        private readonly INasaService nasaService;
        private readonly IExcelConverterService excelConverter;
        private readonly IMapper mapper;

        public HomeController(INasaService nasaService, IExcelConverterService excelConverter, IMapper mapper)
        {
            this.nasaService = nasaService;
            this.excelConverter = excelConverter;
            this.mapper = mapper;
        }

        /// <summary>
        /// Gets a specific asteroid by id and returns a detailed view.
        /// </summary>
        /// <param name="asteroidId"></param>
        /// <returns></returns>
        public async Task<IActionResult> GetAsteroidDetails(string asteroidId)
        {
            var asteroid = await nasaService.GetAsteroidDataAsync(asteroidId);

            var serializedJson = JsonConvert.SerializeObject(asteroid.CloseApproachData, new UnwrappedObjectSerializer(true));

            var spreadsheets = excelConverter.CreateSpreadsheets(new[] { asteroid });

            var dataTables = spreadsheets.Select(a => mapper.Map<TableViewModel>(a));

            return View("AsteroidDetails", dataTables);
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

            var sheets = excelConverter.CreateSpreadsheets(asteroids.Asteroids);

            var excelPackage = excelConverter.CreateExcelPackage(sheets);

            var file = await excelPackage.GetAsByteArrayAsync();

            return File(file, ContentType, string.Format(FileName, pageNum));
        }

        /// <summary>
        /// Returns the view of the Astronomy Picture of the day for a specific date.
        /// </summary>
        /// <param name="date">Date for the astronomy picture of the day.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetApod(DateTime date)
        {
            date = date == DateTime.MinValue ? DateTime.Now : date;

            var apod = await nasaService.GetAstronomyPictureOfTheDayAsync(date);

            if (apod == null)
            {
                return View("Error", new ErrorViewModel
                {
                    ErrorMessage = "No data found for this date."
                });
            }

            return View(apod);
        }

        /// <summary>
        /// Form post method that takes the date info from the current astronomy picture of the day view and calls the same view again
        /// with a new date.
        /// </summary>
        /// <param name="model">Astronomy Picture of the day model which contains the new requested date.</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult GetApod(AstronomyPictureOfTheDay model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            return RedirectToAction("GetApod", new { date = model.Date });
        }

        /// <summary>
        /// Index view which has a table with a specific asteroid page from the Nasa Api.
        /// </summary>
        /// <param name="pageNum">Page to show table for.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Index(int pageNum = 1)
        {
            var asteroids = await nasaService.GetAsteroidDataCollectionAsync(pageNum - 1, PageSize);

            var viewModel = mapper.Map<AsteroidPageViewModel>(asteroids);

            viewModel.PageNumber++;

            return View("AsteroidList", viewModel);
        }
        
        /// <summary>
        /// Form post method that takes the page info from the current Index view and calls it again with a new page.
        /// </summary>
        /// <param name="model">AsteroidPageViewModel with the new page information.</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Index(AsteroidPageViewModel model)
        {
            var page = Math.Clamp(model.PageNumber, 1, model.PageCount);

            return RedirectToAction("Index", new { pageNum = page });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
