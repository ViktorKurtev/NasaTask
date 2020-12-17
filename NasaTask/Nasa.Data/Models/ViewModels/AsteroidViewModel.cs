namespace Nasa.Data.Models.ViewModels
{
    /// <summary>
    /// Asteroid View Model containing some of the more basic properties to display in a table before details for specific asteroid
    /// are requested.
    /// </summary>
    public class AsteroidViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string NasaJplUrl { get; set; }
        public bool? IsPotentiallyHazardousAsteroid { get; set; }
        public int CloseApproachCount { get; set; }
    }
}
