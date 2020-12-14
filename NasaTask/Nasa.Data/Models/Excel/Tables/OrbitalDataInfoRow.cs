using Nasa.Data.Contracts.Asteroid;
using Nasa.Data.Models.Orbital;

namespace Nasa.Data.Models.Excel.Tables
{
    public class OrbitalDataInfoRow : IOrbitalDataContainer
    {
        public string Name { get; set; }
        public OrbitalData OrbitalData { get; set; }
    }
}
