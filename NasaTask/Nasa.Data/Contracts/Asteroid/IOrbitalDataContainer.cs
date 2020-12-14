using Nasa.Data.Models.Orbital;

namespace Nasa.Data.Contracts.Asteroid
{
    public interface IOrbitalDataContainer
    {
        public OrbitalData OrbitalData { get; set; }
    }
}
