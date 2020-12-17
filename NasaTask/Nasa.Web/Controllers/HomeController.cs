using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Nasa.Data.JsonSerializers;
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
        /// Index view which has a table with a specific asteroid page from the Nasa Api.
        /// </summary>
        /// <param name="pageNum">Page to show table for.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Index(int pageNum = 1)
        {
            var asteroids = await nasaService.GetAsteroidDataCollectionAsync(pageNum - 1, PageSize);

            if (asteroids == null || !asteroids.Asteroids.Any())
            {
                return View("Error", new ErrorViewModel
                {
                    ErrorMessage = $"Page number '{pageNum}' doesn't exist."
                });
            }

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

        /// <summary>
        /// Gets a specific asteroid by id and returns a detailed view.
        /// </summary>
        /// <param name="asteroidId"></param>
        /// <returns></returns>
        public async Task<IActionResult> GetAsteroidDetails(string asteroidId)
        {
            var asteroid = await nasaService.GetAsteroidDataAsync(asteroidId);

            if (asteroid == null)
            {
                return View("Error", new ErrorViewModel
                {
                    ErrorMessage = $"Asteroid with id '{asteroidId}' doesn't exist."
                });
            }

            var serializedJson = JsonConvert.SerializeObject(asteroid.CloseApproachData, new UnwrappedObjectSerializer(true));

            var spreadsheets = excelConverter.CreateSpreadsheets(new[] { asteroid });

            var dataTables = spreadsheets.Select(a => mapper.Map<TableViewModel>(a));

            return View("AsteroidDetails", dataTables);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
