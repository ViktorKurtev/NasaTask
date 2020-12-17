using Nasa.Data.Models.Orbital;

namespace Nasa.Data.Contracts.Asteroid
{
    /// <summary>
    /// Interface containing the Orbital Data for a given asteroid.
    /// </summary>
    public interface IOrbitalDataContainer
    {
        /// <summary>
        /// Orbital Data for this asteroid.
        /// </summary
        public OrbitalData OrbitalData { get; set; }
    }
}
