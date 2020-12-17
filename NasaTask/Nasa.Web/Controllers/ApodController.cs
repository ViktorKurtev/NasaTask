using Microsoft.AspNetCore.Mvc;
using Nasa.Data.Models.PictureOfTheDay;
using Nasa.Services.Contracts;
using Nasa.Web.Models;
using System;
using System.Threading.Tasks;

namespace Nasa.Web.Controllers
{
    /// <summary>
    /// Astronomy Picture of the Day controller.
    /// </summary>
    public class ApodController : Controller
    {
        private readonly INasaService nasaService;

        public ApodController(INasaService nasaService)
        {
            this.nasaService = nasaService;
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
    }
}
