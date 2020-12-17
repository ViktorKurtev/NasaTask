using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Nasa.Data.Extensions;
using Nasa.Data.JsonSerializers;
using Nasa.Data.Models.Asteroid;
using Nasa.Data.Models.CloseApproach;
using Nasa.Data.Models.PictureOfTheDay;
using Nasa.Data.Models.ViewModels;
using Nasa.Services.Contracts;
using Nasa.Web.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Nasa.Web.Controllers
{
    public class HomeController : Controller
    {
        private const string ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        private const string FileName = "AsteroidDataPage{0}.xlsx";

        private const int PageSize = 20;

        private readonly INasaService nasaService;
        private readonly IExcelConverter excelConverter;
        private readonly IMapper mapper;

        public HomeController(INasaService nasaService, IExcelConverter excelConverter, IMapper mapper)
        {
            this.nasaService = nasaService;
            this.excelConverter = excelConverter;
            this.mapper = mapper;
        }

        public async Task<IActionResult> GetAsteroidDetails(string asteroidId)
        {
            var asteroid = await nasaService.GetAsteroidDataAsync(asteroidId);

            var serializedJson = JsonConvert.SerializeObject(asteroid.CloseApproachData, new UnwrappedObjectSerializer(true));

            var spreadsheets = excelConverter.CreateSpreadsheets(new[] { asteroid });

            var dataTables = spreadsheets.Select(a => mapper.Map<TableViewModel>(a));

            return View("AsteroidDetails", dataTables);
        }

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

        [HttpGet]
        public async Task<IActionResult> Index(int pageNum = 1)
        {
            var asteroids = await nasaService.GetAsteroidDataCollectionAsync(pageNum - 1, PageSize);

            var viewModel = mapper.Map<AsteroidPageViewModel>(asteroids);

            viewModel.PageNumber++;

            return View("AsteroidList", viewModel);
        }

        [HttpPost]
        public IActionResult Index(AsteroidPageViewModel model)
        {
            var page = Math.Clamp(model.PageNumber, 1, model.PageCount);

            return RedirectToAction("Index", new { pageNum = page });
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
