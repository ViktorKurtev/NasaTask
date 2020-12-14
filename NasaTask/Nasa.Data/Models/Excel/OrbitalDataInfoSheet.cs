using Nasa.Data.Models.Contracts;
using Nasa.Data.Models.Orbital;

namespace Nasa.Data.Models.Excel
{
    public class OrbitalDataInfoSheet : IOrbitalDataContainer
    {
        public string Name { get; set; }
        public OrbitalData OrbitalData { get; set; }
    }
}
