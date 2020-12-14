using Nasa.Data.Models.Orbital;

namespace Nasa.Data.Models.Contracts
{
    public interface IOrbitalDataContainer
    {
        public OrbitalData OrbitalData { get; set; }
    }
}
