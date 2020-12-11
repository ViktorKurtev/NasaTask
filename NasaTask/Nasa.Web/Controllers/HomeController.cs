using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nasa.Services.Contracts;
using Nasa.Web.Models;

namespace Nasa.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly INasaService nasaService;

        public HomeController(ILogger<HomeController> logger, INasaService nasaService)
        {
            _logger = logger;
            this.nasaService = nasaService;
        }

        public async Task<IActionResult> Index()
        {
            var asteroids = await nasaService.GetAsteroidDataAsync(1, 20);

            return new JsonResult(asteroids);

            //return View(asteroids);
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
