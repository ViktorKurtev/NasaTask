using System;
using System.Collections.Generic;
using System.Text;

namespace Nasa.Data.Models.ViewModels
{
    public class AsteroidViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string NasaJplUrl { get; set; }
        public bool? IsPotentiallyHazardousAsteroid { get; set; }
        public int CloseApproachCount { get; set; }
    }
}
