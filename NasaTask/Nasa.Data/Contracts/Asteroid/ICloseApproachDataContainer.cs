using Nasa.Data.Models.CloseApproach;
using System.Collections.Generic;

namespace Nasa.Data.Contracts.Asteroid
{
    /// <summary>
    /// Interface containing a collection of the Close Approach Data for a given asteroid.
    /// </summary>
    public interface ICloseApproachDataContainer
    {
        /// <summary>
        /// Close Approach Data collection for this asteroid.
        /// </summary>
        public IEnumerable<CloseApproachData> CloseApproachData { get; set; }
    }
}
