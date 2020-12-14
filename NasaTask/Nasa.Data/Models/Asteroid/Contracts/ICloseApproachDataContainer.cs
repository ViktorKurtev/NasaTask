using Nasa.Data.Models.CloseApproach;
using System.Collections.Generic;

namespace Nasa.Data.Models.Contracts
{
    public interface ICloseApproachDataContainer
    {
        public IEnumerable<CloseApproachData> CloseApproachData { get; set; }
    }
}
