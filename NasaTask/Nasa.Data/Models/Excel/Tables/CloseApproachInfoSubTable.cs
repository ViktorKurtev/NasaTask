using Nasa.Data.Contracts.Asteroid;
using Nasa.Data.Models.CloseApproach;
using System.Collections.Generic;

namespace Nasa.Data.Models.Excel.Tables
{
    /// <summary>
    /// Class containing the Close Approach Data for one asteroid.
    /// </summary>
    public class CloseApproachInfoSubTable : ICloseApproachDataContainer
    {
        /// <summary>
        /// Asteroid Name.
        /// </summary>
        public string Name { get; set; }

        public IEnumerable<CloseApproachData> CloseApproachData { get; set; }
    }
}
