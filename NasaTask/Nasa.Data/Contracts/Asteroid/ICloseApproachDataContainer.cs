using Nasa.Data.Models.CloseApproach;
using System.Collections.Generic;

namespace Nasa.Data.Contracts.Asteroid
{
    public interface ICloseApproachDataContainer
    {
        public IEnumerable<CloseApproachData> CloseApproachData { get; set; }
    }
}
