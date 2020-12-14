using Nasa.Data.Contracts.Asteroid;
using Nasa.Data.Models.CloseApproach;
using System.Collections.Generic;

namespace Nasa.Data.Models.Excel.Tables
{
    public class CloseApproachInfoSubTable : ICloseApproachDataContainer
    {
        public string Name { get; set; }
        public IEnumerable<CloseApproachData> CloseApproachData { get; set; }
    }
}
