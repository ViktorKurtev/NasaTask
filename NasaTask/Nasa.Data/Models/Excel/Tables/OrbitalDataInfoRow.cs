using Nasa.Data.Contracts.Asteroid;
using Nasa.Data.Models.Orbital;

namespace Nasa.Data.Models.Excel.Tables
{
    /// <summary>
    /// Class containing the orbital data of one asteroid.
    /// </summary>
    public class OrbitalDataInfoRow : IOrbitalDataContainer
    {
        /// <summary>
        /// Asteroid Name.
        /// </summary>
        public string Name { get; set; }

        public OrbitalData OrbitalData { get; set; }
    }
}
