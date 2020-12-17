using System;
using System.Collections.Generic;
using System.Text;

namespace Nasa.Data.Models.ViewModels
{
    public class AsteroidPageViewModel
    {
        public int PageNumber { get; set; }
        public int PageCount { get; set; }
        public IEnumerable<AsteroidViewModel> Asteroids { get; set; }
    }
}
